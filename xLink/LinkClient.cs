using NewLife.Log;
using NewLife.Net;
using NewLife.Reflection;
using NewLife.Remoting;
using NewLife.Serialization;
using NewLife.Threading;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using xLink.Common;

namespace xLink
{
    /// <summary>物联客户端</summary>
    [Api(null)]
    public class LinkClient : ApiClient
    {
        #region 属性
        /// <summary>用户名</summary>
        public String UserName { get; set; }

        /// <summary>密码</summary>
        public String Password { get; set; }

        /// <summary>动作前缀</summary>
        public String ActionPrefix { get; set; }

        /// <summary>已登录</summary>
        public Boolean Logined { get; set; }

        /// <summary>最后一次登录成功后的消息</summary>
        public IDictionary<String, Object> Info { get; private set; }

        /// <summary>请求到服务端并返回的延迟时间</summary>
        public TimeSpan Delay { get; set; }
        #endregion

        #region 构造
        /// <summary>实例化</summary>
        /// <param name="uri"></param>
        public LinkClient(String uri) : base(uri)
        {
            Log = XTrace.Log;

            StatPeriod = 60;
            ShowError = true;

#if DEBUG
            EncoderLog = XTrace.Log;
            StatPeriod = 10;
#endif
        }
        #endregion

        #region 执行
        /// <summary>异步调用</summary>
        /// <param name="resultType"></param>
        /// <param name="action"></param>
        /// <param name="args"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public override Task<Object> InvokeAsync(Type resultType, String action, Object args = null, Byte flag = 0)
        {
            if (!ActionPrefix.IsNullOrEmpty() && !action.Contains("/")) action = ActionPrefix + "/" + action;

            return base.InvokeAsync(resultType, action, args, flag);
        }
        #endregion

        #region 登录
        /// <summary>连接后自动登录</summary>
        /// <param name="client">客户端</param>
        /// <param name="force">强制登录</param>
        protected override async Task<Object> OnLoginAsync(ISocketClient client, Boolean force)
        {
            if (Logined && !force) return null;

            Logined = false;

            var user = UserName;
            var pass = Password;
            //if (user.IsNullOrEmpty()) return null;
            //if (user.IsNullOrEmpty()) throw new ArgumentNullException(nameof(user), "用户名不能为空！");
            //if (pass.IsNullOrEmpty()) throw new ArgumentNullException(nameof(pass), "密码不能为空！");
            if (!pass.IsNullOrEmpty()) pass = pass.MD5();

            var arg = new
            {
                user,
                pass,
            };

            // 克隆一份，避免修改原始数据
            var dic = arg.ToDictionary();
            dic.Merge(GetLoginInfo(), false);

            var act = "Login";
            if (!ActionPrefix.IsNullOrEmpty()) act = ActionPrefix + "/" + act;

            var rs = await base.InvokeWithClientAsync<Object>(client, act, dic);
            var inf = rs.ToJson();
            XTrace.WriteLine("登录{0}成功！{1}", Servers.FirstOrDefault(), inf);

            Logined = true;

            Info = rs as IDictionary<String, Object>;

            if (_timer == null) _timer = new TimerX(DoPing, null, 10_000, 30_000);

            return rs;
        }

        /// <summary>获取登录附加信息</summary>
        /// <returns></returns>
        protected virtual Object GetLoginInfo()
        {
            var asm = AssemblyX.Entry;

            var mcs = MachineHelper.GetMacs().Join(",", e => e.ToHex("-"));
            var ip = NetHelper.MyIP();
            var ext = new
            {
                asm.Version,
                asm.Compile,

                OSName = Environment.OSVersion.Platform + "",
                OSVersion = Environment.OSVersion.VersionString,

                Environment.MachineName,
                Environment.UserName,

                CPU = Environment.ProcessorCount,

                Macs = mcs,

                InstallPath = ".".GetFullPath(),
                Runtime = Environment.Version + "",
                LocalIP = ip + "",

                Time = DateTime.Now,
            };

            return ext;
        }
        #endregion

        #region 心跳
        private TimerX _timer;
        private void DoPing(Object state) => PingAsync().Wait();

        /// <summary>心跳</summary>
        /// <returns></returns>
        public async Task<Object> PingAsync()
        {
            // 获取需要携带的心跳信息
            var ext = GetPingInfo();

            // 执行心跳
            var rs = await InvokeAsync<Object>("Ping", ext).ConfigureAwait(false);

            // 获取响应中的Time，乃请求时送过去的本地时间，用于计算延迟
            if (rs is IDictionary<String, Object> dic && dic.TryGetValue("Time", out var obj))
            {
                var dt = obj.ToDateTime();
                if (dt.Year > 2000)
                {
                    // 计算延迟
                    Delay = DateTime.Now - dt;
                }
            }

            return rs;
        }

        /// <summary>心跳前获取附加信息</summary>
        /// <returns></returns>
        protected virtual Object GetPingInfo()
        {
            var asm = AssemblyX.Entry;

            var pcs = new List<Process>();
            foreach (var item in Process.GetProcesses().OrderBy(e => e.SessionId).ThenBy(e => e.ProcessName))
            {
                var name = item.ProcessName;
                if (name.EqualIgnoreCase("svchost", "dllhost", "conhost")) continue;

                pcs.Add(item);
            }

            var mcs = MachineHelper.GetMacs().Join(",", e => e.ToHex("-"));
            var ext = new
            {
                Macs = mcs,

                Processes = pcs.Join(",", e => e.ProcessName),

                Time = DateTime.Now,
                Delay = (Int32)Delay.TotalMilliseconds,
            };

            return ext;
        }
        #endregion

        #region 业务
        #endregion
    }
}