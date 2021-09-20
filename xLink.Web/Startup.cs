using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NewLife.Cube;

namespace xLink.Web
{
    public class Startup
    {
        /// <summary></summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration) { }

        /// <summary></summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services) => services.AddCube();

        /// <summary></summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // 使用Cube前添加自己的管道
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseExceptionHandler("/CubeHome/Error");

            app.UseStaticFiles(new StaticFileOptions { ServeUnknownFileTypes = true });

            app.UseCube(env);
        }
    }
}