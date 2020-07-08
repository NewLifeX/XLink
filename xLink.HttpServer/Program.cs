using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using NewLife.Log;
using XCode.DataAccessLayer;
using xLink.Entity;
using xLinkServer.Common;
using xLinkServer.Services;

namespace xLinkServer
{
    public class Program
    {
        public static void Main(String[] args)
        {
            XTrace.UseConsole();

            // 异步初始化
            Task.Run(InitAsync);

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(String[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureLogging(logging => { logging.AddXLog(); })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
        }

        private static DeviceOnlineService _online;
        private static void InitAsync()
        {
            // 配置
            var set = NewLife.Common.SysConfig.Current;
            if (set.Name == "NewLife.Cube" || set.DisplayName == "魔方平台")
            {
                set.Name = "xLinkServer";
                set.DisplayName = "物联网平台";

                set.Save();
            }

#if !DEBUG
            var set2 = XCode.Setting.Current;
            if (set2.IsNew)
            {
                set2.ShowSQL = false;

                set2.Save();
            }
#endif

            var set3 = Setting.Current;

            // 初始化数据库
            var n = Device.Meta.Count;

            // 设备在线管理服务
            var svc = new DeviceOnlineService();
            svc.Init();
            _online = svc;
        }
    }
}