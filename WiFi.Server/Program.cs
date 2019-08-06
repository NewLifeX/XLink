using NewLife;
using NewLife.Agent;
using NewLife.Log;
using NewLife.Net;
using NewLife.Threading;
using System;

namespace WiFi.Server
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
            ServiceName = "WiFiServer";

            // 异步初始化数据
            ThreadPoolX.QueueUserWorkItem(() =>
            {
                var set = XCode.Setting.Current;
                if (set.IsNew)
                {
                    set.Debug = false;
                    set.ShowSQL = false;
                    set.SQLiteDbPath = "../Data";
#if DEBUG
                    set.SQLiteDbPath = "../../Data";
#endif
                    set.SaveAsync();
                }
            });
        }

        /// <summary>服务器</summary>
        NetServer Svr;

        /// <summary>启动工作</summary>
        protected override void StartWork(String reason)
        {
            base.StartWork(reason);

            var set = Setting.Current;

            // 实例化服务器
            var svr = new WiFiServer
            {
                Name = "平台",
                Port = set.Port,
            };
            svr.Log = XTrace.Log;
            svr.SessionLog = svr.Log;

            if (set.Debug)
            {
                svr.SocketLog = svr.Log;
                //svr.LogSend = true;
                //svr.LogReceive = true;

                svr.CommandLog = true;
            }

            svr.Start();

            Svr = svr;
        }

        /// <summary>停止工作</summary>
        protected override void StopWork(String reason)
        {
            base.StopWork(reason);

            Svr.TryDispose();
            Svr = null;
        }
    }
}