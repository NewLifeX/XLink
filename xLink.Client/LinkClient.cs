using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using NewLife.Log;
using NewLife.Net;
using NewLife.Reflection;
using NewLife.Remoting;
using NewLife.Serialization;
using xLink.Models;

namespace xLink
{
    /// <summary>物联客户端</summary>
    [Api(null)]
    public class LinkClient : ApiClient
    {
        #region 属性
        ///// <summary>远程地址</summary>
        //public NetUri Remote { get; set; }

        /// <summary>用户名</summary>
        public String UserName { get; set; }

        /// <summary>密码</summary>
        public String Password { get; set; }

        /// <summary>动作前缀</summary>
        public String ActionPrefix { get; set; }

        /// <summary>附加参数</summary>
        public IDictionary<String, Object> Parameters { get; set; } = new Dictionary<String, Object>();

        public Boolean Logined { get; private set; }

        /// <summary>最后一次登录成功后的消息</summary>
        public IDictionary<String, Object> Info { get; private set; }
        #endregion

        #region 构造
        public LinkClient(String uri) : base(uri)
        {
            Log = XTrace.Log;

            StatPeriod = 60;
            ShowError = true;

#if DEBUG
            EncoderLog = XTrace.Log;
            StatPeriod = 10;
#endif

            //Remote = uri;

            // 初始数据
            var dic = Parameters;
            dic["OS"] = Environment.OSVersion + "";
            dic["Machine"] = Environment.MachineName;
            dic["Agent"] = Environment.UserName;
            dic["ProcessID"] = Process.GetCurrentProcess().Id;

            var asmx = AssemblyX.Entry;
            dic["Version"] = asmx?.Version;
            dic["Compile"] = asmx?.Compile;

            // 注册当前类所有接口
            Manager.Register(this, null);
            //Register(this, nameof(OnWrite));
        }
        #endregion

        #region 执行
        ///// <summary>打开连接</summary>
        ///// <returns></returns>
        //public override Boolean Open()
        //{
        //    if (!base.Open()) return false;

        //    GetClient(true);

        //    return true;
        //}

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
        protected override async Task<Object> OnLoginAsync(ISocketClient client)
        {
            var user = UserName;
            var pass = Password;
            //if (user.IsNullOrEmpty()) return null;
            if (user.IsNullOrEmpty()) throw new ArgumentNullException(nameof(user), "用户名不能为空！");
            //if (pass.IsNullOrEmpty()) throw new ArgumentNullException(nameof(pass), "密码不能为空！");

            var asmx = AssemblyX.Entry;

            var arg = new
            {
                user,
                pass = pass.MD5(),
            };

            // 克隆一份，避免修改原始数据
            var dic = arg.ToDictionary();
            dic.Merge(Parameters, false);

            var act = "Login";
            if (!ActionPrefix.IsNullOrEmpty()) act = ActionPrefix + "/" + act;

            var rs = await base.InvokeWithClientAsync<Object>(client, act, dic);
            var inf = rs.ToJson();
            if (Setting.Current.Debug) XTrace.WriteLine("登录{0}成功！{1}", Servers.FirstOrDefault(), inf);

            Logined = true;

            return Info = rs as IDictionary<String, Object>;
        }

        ///// <summary>登录</summary>
        ///// <returns></returns>
        //public async Task<Object> LoginAsync()
        //{
        //    await Task.Yield();

        //    GetClient(true);

        //    return Info;
        //}
        #endregion

        #region 心跳
        /// <summary>心跳</summary>
        /// <returns></returns>
        public async Task<Object> PingAsync()
        {
            await Task.Yield();

            return await InvokeAsync<Object>("Ping", new { time = DateTime.Now });
        }
        #endregion

        #region 读写
        /// <summary>写入数据，返回整个数据区</summary>
        /// <param name="id">设备</param>
        /// <param name="start"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<Byte[]> Write(String id, Int32 start, params Byte[] data)
        {
            var rs = await InvokeAsync<DataModel>("Write", new { id, start, data = data.ToHex() });
            return rs.Data.ToHex();
        }

        /// <summary>读取对方数据</summary>
        /// <param name="id">设备</param>
        /// <param name="start"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public async Task<Byte[]> Read(String id, Int32 start, Int32 count)
        {
            var rs = await InvokeAsync<DataModel>("Read", new { id, start, count });
            return rs.Data.ToHex();
        }

        public Func<String, Byte[]> GetData;
        public Action<String, Byte[]> SetData;

        /// <summary>收到写入请求</summary>
        /// <param name="id">设备</param>
        /// <param name="start"></param>
        /// <param name="data"></param>
        [Api("Write")]
        private DataModel OnWrite(String id, Int32 start, String data)
        {
            var buf = GetData?.Invoke(id);
            if (buf == null) throw new ApiException(405, "找不到设备！");

            var ds = data.ToHex();

            // 检查扩容
            if (start + ds.Length > buf.Length)
            {
                var buf2 = new Byte[start + ds.Length];
                buf2.Write(0, buf);
                buf = buf2;
            }
            buf.Write(start, ds);
            buf[0] = (Byte)buf.Length;

            // 保存回去
            SetData?.Invoke(id, buf);

            return new DataModel { ID = id, Start = 0, Data = buf.ToHex() };
        }

        /// <summary>收到读取请求</summary>
        /// <param name="id">设备</param>
        /// <param name="start"></param>
        /// <param name="count"></param>
        [Api("Read")]
        private DataModel OnRead(String id, Int32 start, Int32 count)
        {
            var buf = GetData?.Invoke(id);
            if (buf == null) throw new ApiException(405, "找不到设备！");

            return new DataModel { ID = id, Start = start, Data = buf.ReadBytes(start, count).ToHex() };
        }
        #endregion

        #region 业务
        #endregion
    }
}