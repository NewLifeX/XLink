using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using NewLife.Log;

namespace xLink.Web
{
    public class Program
    {
        public static void Main(String[] args)
        {
            XTrace.UseConsole();

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(String[] args)
        {
            return Host.CreateDefaultBuilder(args)
                //.ConfigureLogging(logging => { logging.AddXLog(); })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
        }
    }
}