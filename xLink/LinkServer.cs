using NewLife;
using NewLife.Log;
using NewLife.Net;
using NewLife.Remoting;
using NewLife.Threading;
using System;
using System.Collections.Generic;
using xLink.Services;

namespace xLink
{
    /// <summary>物联服务器</summary>
    /// <remarks>
    /// 物联服务器，用于承载继承自LinkService的服务接口，并衍生出LinkSession以管理长连接状态。
    /// 服务层是无状态架构，每次请求都会实例化LinkService新实例，LinkService每次接受新连接后，都创建新的LinkSession会话，在多次请求中共享。
    /// </remarks>
    public class LinkServer : ApiServer
    {
        #region 属性
        /// <summary>会话超时时间。超过该时间将删除会话，默认20*60秒</summary>
        public Int32 SessionTimeout { get; set; }

        private IList<ILinkService> _services = new List<ILinkService>();
        #endregion

        #region 构造
        /// <summary>实例化令牌服务器</summary>
        public LinkServer()
        {
            Port = 2233;
            Log = XTrace.Log;

            StatPeriod = 60;
            ShowError = true;

            // 使用Socket层会话超时时间
            SessionTimeout = NewLife.Net.Setting.Current.SessionTimeout;

#if DEBUG
            EncoderLog = XTrace.Log;
            StatPeriod = 10;
#endif
        }

        /// <summary>销毁</summary>
        /// <param name="disposing"></param>
        protected override void OnDispose(Boolean disposing)
        {
            base.OnDispose(disposing);

            _expireTimer.TryDispose();
            _expireTimer = null;
        }
        #endregion

        #region 方法
        /// <summary>启动</summary>
        public override void Start()
        {
            var svr = EnsureCreate();
            svr.Log = Log;

            base.Start();

            // 每次上线清空一次在线表
            _expireTimer = new TimerX(CheckExpire, null, 0, 60_000) { Async = true };
        }

        /// <summary>添加物联服务</summary>
        /// <typeparam name="TService"></typeparam>
        public void Add<TService>() where TService : class, ILinkService, new()
        {
            var svc = new TService();

            // 注册服务接口
            Register<TService>();

            // 记录服务，用于清理过期等操作
            _services.Add(svc);
        }
        #endregion

        #region 过期清理
        TimerX _expireTimer;
        void CheckExpire(Object state)
        {
            var timeout = SessionTimeout;
            if (timeout <= 0) return;

            foreach (var svc in _services)
            {
                svc.ClearExpire(timeout);
            }
        }

        /// <summary>删除过期会话</summary>
        /// <param name="ids">会话ID</param>
        /// <returns></returns>
        public Int32 ClearExpire(Int32[] ids)
        {
            if (ids.Length == 0) return 0;

            if (!(Server is NetServer svr)) return 0;

            foreach (var item in ids)
            {
                var ss = svr.GetSession(item);
                if (ss != null) ss.TryDispose();
            }

            return ids.Length;
        }
        #endregion

        #region 辅助
        /// <summary>设置内部日志是否开启</summary>
        /// <param name="session">是否开启会话级日志</param>
        /// <param name="socket">是否开启Socket级日志</param>
        /// <param name="encoder">是否显示编码日志</param>
        public void SetLog(Boolean session, Boolean socket, Boolean encoder = false)
        {
            if (!(Server is NetServer svr)) return;

            // 置空可以让其使用当前Log日志
            svr.SessionLog = session ? null : Logger.Null;
            svr.SocketLog = socket ? null : Logger.Null;

            //if (Encoder != null) Encoder.Log = Log;
            if (encoder) EncoderLog = Log;
        }
        #endregion
    }
}