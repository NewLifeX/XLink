using NewLife;
using NewLife.Agent;
using NewLife.Log;
using NewLife.Net;
using NewLife.Threading;
using System;
using xLink.Entity;

namespace xLink
{
    class Program
    {
        static void Main(String[] args) => new MyService().Main();
    }

    class MyService : AgentServiceBase<MyService>
    {
        /// <summary>构造函数</summary>
        public MyService()
        {
            ServiceName = "LinkServer";

            // 异步初始化数据
            ThreadPoolX.QueueUserWorkItem(() =>
            {
                var set = XCode.Setting.Current;
                if (set.IsNew)
                {
                    set.Debug = false;
                    set.ShowSQL = false;
                    set.SQLiteDbPath = "../Data";
                    set.SaveAsync();
                }
            });
        }

        /// <summary>服务器</summary>
        LinkServer Svr;

        /// <summary>启动工作</summary>
        protected override void StartWork(String reason)
        {
            base.StartWork(reason);

            // 每次上线清空一次在线表
            _expireTimer = new TimerX(CheckExpire, null, 0, 60_000) { Async = true };

            var set = Setting.Current;

            // 实例化服务器
            var svr = new LinkServer
            {
                Name = "平台",
                Port = set.Port,
            };
            svr.Log = XTrace.Log;
            svr.SetLog(set.Debug, set.SocketDebug, set.EncoderDebug);

            // 遍历注册各服务控制器
            svr.Register<DeviceController>();
            svr.Register<UserController>();

            if (set.EncoderDebug) svr.Encoder.Log = svr.Log;

            svr.Start();

            Svr = svr;

            // 如果是控制台调试，则在标题显示统计
            if (!Environment.CommandLine.EndsWith(" -s")) _showTimer = new TimerX(ShowStat, null, 1000, 1000) { Async = true };
        }

        /// <summary>停止工作</summary>
        protected override void StopWork(String reason)
        {
            base.StopWork(reason);

            _expireTimer.TryDispose();
            _showTimer.TryDispose();
            Svr.TryDispose();
            Svr = null;
        }

        #region 定时删除在线
        TimerX _expireTimer;
        void CheckExpire(Object state)
        {
            var timeout = Setting.Current.SessionTimeout;

            //if (Svr != null) Svr.ClearExpire(timeout);
            DeviceOnline.ClearExpire(timeout);
            UserOnline.ClearExpire(timeout);
        }
        #endregion

        private String _Title;
        private TimerX _showTimer;
        private void ShowStat(Object stat)
        {
            if (_Title == null) _Title = Console.Title;

            var ns = Svr.Server as NetServer;
            Console.Title = "{0} {1}".F(_Title, ns.GetStat());
        }
    }
}