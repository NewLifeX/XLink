using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using NewLife;
using NewLife.Log;
using NewLife.Net;
using NewLife.Reflection;
using NewLife.Remoting;
using NewLife.Threading;

namespace xLink
{
    /// <summary>物联服务器</summary>
    public class LinkServer : ApiServer
    {
        #region 属性
        /// <summary>协议类型</summary>
        public NetType ProtocolType { get; set; }

        /// <summary>端口</summary>
        public Int32 Port { get; set; }

        /// <summary>显示总会话数的时间间隔。默认60秒</summary>
        public Int32 ShowSessionCount { get; set; }

        /// <summary>附加参数</summary>
        public IDictionary<String, Object> Parameters { get; set; } = new Dictionary<String, Object>();
        #endregion

        #region 构造
        /// <summary>实例化令牌服务器</summary>
        public LinkServer()
        {
            IsReusable = true;

            ShowSessionCount = 60;

            // 初始数据
            var dic = Parameters;
            dic["OS"] = Runtime.OSName;
            dic["Agent"] = $"{Environment.UserName}_{Environment.MachineName}";

            var asmx = AssemblyX.Create(Assembly.GetCallingAssembly());
            dic["Version"] = asmx.Version;
        }

        /// <summary>销毁</summary>
        /// <param name="disposing"></param>
        protected override void OnDispose(Boolean disposing)
        {
            base.OnDispose(disposing);

            _timer.TryDispose();
        }
        #endregion

        #region 方法
        /// <summary>确认创建服务器</summary>
        public NetServer EnsureServer()
        {
            var svr = Servers.FirstOrDefault() as NetServer;
            if (svr == null)
            {
                var uri = new NetUri(ProtocolType, IPAddress.Any, Port);
                svr = Add(uri) as NetServer;
            }
            return svr;
        }

        /// <summary>启动</summary>
        public override void Start()
        {
            var svr = EnsureServer();

            // 默认6分钟超时
            // svr.SessionTimeout = 6 * 60;

            // 默认打开数据库日志
            //var dbLog = new TokenLogProvider();
            //var mixLog = new CompositeLog(dbLog, Log);
            svr.Log = Log;

            if (_timer == null && ShowSessionCount > 0) _timer = new TimerX(ShowCount, null, 0, ShowSessionCount * 000);

            var dic = Parameters;
            dic["Type"] = Name;

            base.Start();
        }
        #endregion

        #region 过期清理
        /// <summary>删除过期会话</summary>
        /// <param name="ids">会话ID</param>
        /// <returns></returns>
        public Int32 ClearExpire(Int32[] ids)
        {
            if (ids.Length == 0) return 0;

            var svr = Servers.FirstOrDefault() as NetServer;
            if (svr == null) return 0;

            foreach (var item in ids)
            {
                var ss = svr.GetSession(item);
                if (ss != null) ss.TryDispose();
            }

            return ids.Length;
        }
        #endregion

        #region 服务提供者
        /// <summary>获取服务提供者</summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        public override Object GetService(Type serviceType)
        {
            if (serviceType == GetType()) return this;
            if (serviceType == typeof(LinkServer)) return this;

            return base.GetService(serviceType);
        }
        #endregion

        #region 辅助
        /// <summary>设置内部日志是否开启</summary>
        /// <param name="session">是否开启会话级日志</param>
        /// <param name="socket">是否开启Socket级日志</param>
        /// <param name="encoder">是否显示编码日志</param>
        public void SetLog(Boolean session, Boolean socket, Boolean encoder = false)
        {
            var svr = Servers.FirstOrDefault() as NetServer;
            if (svr == null) return;

            // 置空可以让其使用当前Log日志
            svr.SessionLog = session ? null : Logger.Null;
            svr.SocketLog = socket ? null : Logger.Null;

            if (Encoder != null) Encoder.Log = Log;
        }

        private TimerX _timer { get; set; }
        void ShowCount(Object stat)
        {
            var svr = Servers.FirstOrDefault() as NetServer;
            if (svr == null) return;

            if (svr.SessionCount > 0) svr.WriteLog("会话总数:{0:n0}/{1:n0}", svr.SessionCount, svr.MaxSessionCount);
        }
        #endregion
    }
}