using NewLife;
using NewLife.Net;
using NewLife.Reflection;
using NewLife.Remoting;
using NewLife.Threading;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace xLink
{
    /// <summary>物联服务器</summary>
    public class LinkServer : ApiServer
    {
        #region 属性
        /// <summary>显示总会话数的时间间隔。默认60秒</summary>
        public Int32 ShowSessionCount { get; set; }

        /// <summary>附加参数</summary>
        public IDictionary<String, Object> Parameters { get; set; } = new Dictionary<String, Object>();
        #endregion

        #region 构造
        /// <summary>实例化令牌服务器</summary>
        public LinkServer()
        {
            ShowSessionCount = 60;

            // 初始数据
            var dic = Parameters;
            dic["OS"] = Environment.OSVersion + "";
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
        /// <summary>启动</summary>
        public override void Start()
        {
            if (_timer == null && ShowSessionCount > 0) _timer = new TimerX(ShowCount, null, 0, ShowSessionCount * 1000);

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

            var svr = Server as NetServer;
            if (svr == null) return 0;

            foreach (var item in ids)
            {
                var ss = svr.GetSession(item);
                if (ss != null) ss.TryDispose();
            }

            return ids.Length;
        }
        #endregion

        #region 辅助
        private TimerX _timer { get; set; }
        void ShowCount(Object stat)
        {
            var svr = Server as NetServer;
            if (svr == null) return;

            if (svr.SessionCount > 0) svr.WriteLog("会话总数:{0:n0}/{1:n0}", svr.SessionCount, svr.MaxSessionCount);
        }
        #endregion
    }
}