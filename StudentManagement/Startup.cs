using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore.SqlServer;
using StudentManagement.Models;
using StudentManagement.Models.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudentManagement.DataRepo;

namespace StudentManagement
{
    public class Startup
    {
        private IConfiguration innerConfiguration = null;

        public Startup(IConfiguration configuration)
        {
            innerConfiguration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<AppDbContext>(optionsAction => optionsAction.UseSqlServer(innerConfiguration.GetConnectionString("MurphyStudentsDB")));

            // 添加mvc
            services.AddControllersWithViews(a => a.EnableEndpointRouting = false).
                AddXmlDataContractSerializerFormatters();
            // 添加依赖注入
            services.AddScoped<IStudentRepository, SQLStudentRepo>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// <summary>
        /// 中间件
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                DeveloperExceptionPageOptions developerExceptionPageOptions = new DeveloperExceptionPageOptions
                {
                    SourceCodeLineCount = 3
                };
                app.UseDeveloperExceptionPage(developerExceptionPageOptions);
            }

            //DefaultFilesOptions defaultFilesOptions = new DefaultFilesOptions();
            //defaultFilesOptions.DefaultFileNames.Clear();
            //defaultFilesOptions.DefaultFileNames.Add("Views/Details.cshtml");
            //app.UseDefaultFiles(defaultFilesOptions);

            app.UseStaticFiles();

            //app.UseMvcWithDefaultRoute();

            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "default", template: "{controller=Home}/{action=Index}/{id?}");
            });

            //app.Use(async (context, next) =>
            //{
            //    //logger.LogInformation("MW1:传入请求");
            //    //throw new Exception("管道异常");
            //    await next();
            //    //logger.LogInformation("MW1:传出想应");
            //});

            app.Run(async context =>
            {
                context.Response.ContentType = "text/plain;charset=utf-8";
                var pName = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
                //await context.Response.WriteAsync(innerConfiguration["MyKey"]);
                await context.Response.WriteAsync(env.EnvironmentName);
                //await context.Response.
            });

            //app.UseRouting();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapGet("/", async context =>
            //    {
            //        // 防止乱码 改为UTF-8
            //        context.Response.ContentType = "text/plain;charset=utf-8";
            //        var pName = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
            //        await context.Response.WriteAsync(innerConfiguration["MyKey"]);
            //    });
            //});
        }
    }
}
