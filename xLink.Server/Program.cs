using NewLife;
using NewLife.Agent;
using NewLife.Log;
using NewLife.Net;
using NewLife.Threading;
using System;
using xLink.Services;

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
            svr.Add<DeviceService>();
            svr.Add<UserService>();

            svr.Start();

            Svr = svr;

            // 如果是控制台调试，则在标题显示统计
            if (!Environment.CommandLine.EndsWith(" -s")) _showTimer = new TimerX(ShowStat, null, 1000, 1000) { Async = true };
        }

        /// <summary>停止工作</summary>
        protected override void StopWork(String reason)
        {
            base.StopWork(reason);

            _showTimer.TryDispose();
            Svr.TryDispose();
            Svr = null;
        }

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