using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using NewLife;
using NewLife.Agent;
using NewLife.Log;
using NewLife.Net;
using NewLife.Reflection;
using NewLife.Remoting;
using NewLife.Threading;
using xLink.Entity;

namespace xLink.Server
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
        private String _DisplayName = "物联网平台";
        /// <summary>显示名称</summary>
        public override String DisplayName { get { return _DisplayName; } }

        /// <summary>描述</summary>
        public override String Description
        {
            get
            {
                var set = Setting.Current;
                return "物联网平台 端口:{0}".F(set.Port);
            }
        }

        /// <summary>构造函数</summary>
        public MyService()
        {
            var set = Setting.Current;
            if (!set.ServiceName.IsNullOrEmpty()) ServiceName = set.ServiceName.Trim();
            set.ServiceName = ServiceName;
            _DisplayName += "_{0}".F(set.Port);
        }

        /// <summary>服务器</summary>
        LinkServer Svr;

        /// <summary>启动工作</summary>
        protected override void StartWork(String reason)
        {
            // 异步初始化数据
            Task.Run(() =>
            {
                var set2 = XCode.Setting.Current;
                if (set2.IsNew)
                {
                    set2.Debug = false;
                    set2.SQLiteDbPath = "../Data";
                    set2.Save();
                }
            });

            base.StartWork(reason);

            // 每次上线清空一次在线表
            _timer = new TimerX(CheckExpire, null, 0, 60000);

            var set = Setting.Current;

            // 实例化服务器
            Svr = new LinkServer
            {
                Name = "平台",
                Port = set.Port,
                Encrypted = false,
                Compressed = false,
            };
            Svr.EnsureServer();
            Svr.Log = XTrace.Log;
            Svr.SetLog(set.Debug, set.SocketDebug, set.EncoderDebug);

            // 遍历注册各服务控制器
            Svr.Register<DeviceSession>();
            Svr.Register<UserSession>();

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

            var ns = Svr.Servers.FirstOrDefault() as NetServer;
            Console.Title = "{0} {1}".F(_Title, ns.GetStat());
        }
    }
}