using System;
using NewLife.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewLife;
using NewLife.Agent;
using NewLife.Log;
using NewLife.Net;
using NewLife.Threading;

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
        public override void StartWork()
        {
            base.StartWork();

            // 每次上线清空一次在线表
            _timer = new TimerX(CheckExpire, null, 0, 10000);

            var set = Setting.Current;

            // 实例化服务器
            Svr = new LinkServer
            {
                Name = "平台",
                Port = set.Port
            };
            Svr.EnsureServer();
            Svr.Log = XTrace.Log;
            Svr.SetLog(set.Debug, set.SocketDebug);
#if DEBUG
            Svr.SetLog(set.Debug, set.SocketDebug, true, true);
#endif

            // 遍历注册各服务控制器
            foreach (var item in typeof(LinkSession).GetAllSubclasses(true))
            {
                // 触发异步初始化
                var obj = item.CreateInstance();

                Svr.Manager.Register(item, null, true);
            }

            Svr.Start();

            // 如果是控制台调试，则在标题显示统计
            if (!Environment.CommandLine.EndsWith(" -s")) _Timer = new TimerX(ShowStat, null, 1000, 1000);
        }

        /// <summary>
        /// 停止工作
        /// </summary>
        public override void StopWork()
        {
            base.StopWork();

            _timer.TryDispose();
            Svr.TryDispose();
        }

        #region 定时删除在线
        TimerX _timer;
        void CheckExpire(Object state)
        {
            var timeout = Setting.Current.SessionTimeout;

            //if (Svr != null) Svr.ClearExpire(timeout);
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