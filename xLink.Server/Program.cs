using System;
using System.Threading.Tasks;
using NewLife;
using NewLife.Agent;
using NewLife.Log;
using NewLife.Net;
using NewLife.Threading;
using xLink.Entity;

namespace xLink
{
    class Program
    {
        static void Main(String[] args)
        {
            MyService.ServiceMain();
        }
    }

    class MyService : AgentServiceBase<MyService>
    {
        /// <summary>构造函数</summary>
        public MyService()
        {
            ServiceName = "LinkServer";

            // 异步初始化数据
            Task.Run(() =>
            {
                var set2 = XCode.Setting.Current;
                if (set2.IsNew)
                {
                    set2.Debug = false;
                    set2.ShowSQL = false;
                    set2.SQLiteDbPath = "../Data";
                    set2.SaveAsync();
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
            _timer = new TimerX(CheckExpire, null, 0, 60000);

            var set = Setting.Current;

            // 实例化服务器
            Svr = new LinkServer
            {
                Name = "平台",
                Port = set.Port,
                //Encrypted = false,
                //Compressed = false,
            };
            //Svr.EnsureServer();
            Svr.Log = XTrace.Log;
            Svr.SetLog(set.Debug, set.SocketDebug, set.EncoderDebug);

            // 遍历注册各服务控制器
            Svr.Register<DeviceController>();
            Svr.Register<UserController>();

            Svr.Start();

            if (set.EncoderDebug) Svr.Encoder.Log = Svr.Log;

            // 如果是控制台调试，则在标题显示统计
            if (!Environment.CommandLine.EndsWith(" -s")) _Timer = new TimerX(ShowStat, null, 1000, 1000);
        }

        /// <summary>
        /// 停止工作
        /// </summary>
        protected override void StopWork(String reason)
        {
            base.StopWork(reason);

            _timer.TryDispose();
            Svr.TryDispose();
        }

        public override Boolean Work(Int32 index) => false;

        #region 定时删除在线
        TimerX _timer;
        void CheckExpire(Object state)
        {
            var timeout = Setting.Current.SessionTimeout;

            //if (Svr != null) Svr.ClearExpire(timeout);
            DeviceOnline.ClearExpire(timeout);
            UserOnline.ClearExpire(timeout);
        }
        #endregion

        private String _Title;
        private TimerX _Timer;
        private void ShowStat(Object stat)
        {
            if (_Title == null) _Title = Console.Title;

            var ns = Svr.Server as NetServer;
            Console.Title = "{0} {1}".F(_Title, ns.GetStat());
        }
    }
}