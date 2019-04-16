using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using WS.Music.Core.Entitys;

namespace WS.Music
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var configuration = new ConfigurationBuilder()
                    .AddJsonFile("config.json")
                    .AddEnvironmentVariables()
                    .Build();

            var fs = configuration.GetSection("FileServer");
            FileServerConfig config = fs.Get<FileServerConfig>();
            services.AddSingleton(config);

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            // 添加登陆过滤
            //services.AddMvc(config => config.Filters.Add(typeof(SignFilter))).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddDbContext<ApplicationDbContext>(it =>
            {
                it.UseMySql(configuration["Data:DefaultConnection:ConnectionString"]);
                //it.UseMySql("server=localhost;database=ws_music;user=admin;password=123456;");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors(options =>
            {
                options.AllowAnyHeader();
                options.AllowAnyMethod();
                options.AllowAnyOrigin();
                options.AllowCredentials();
            });

            FileServerConfig config = app.ApplicationServices.GetService<FileServerConfig>();
            if (config != null && config.PathList != null)
            {
                List<PathItem> pathList = new List<PathItem>();
                foreach (PathItem pi in config.PathList)
                {
                    if (string.IsNullOrEmpty(pi.LocalPath))
                        continue;

                    try
                    {
                        if (!System.IO.Directory.Exists(pi.LocalPath))
                        {
                            System.IO.Directory.CreateDirectory(pi.LocalPath);
                        }
                        pathList.Add(pi);
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine("文件夹创建失败：\r\n{0}", e.ToString());
                    }
                }

                foreach (PathItem pi in pathList)
                {
                    app.UseStaticFiles(new StaticFileOptions()
                    {
                        FileProvider = new PhysicalFileProvider(pi.LocalPath),
                        RequestPath = pi.Url
                    });
                    Console.WriteLine("路径映射：{0}-->{1}", pi.LocalPath, pi.Url);
                }
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvcWithDefaultRoute();
            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //        name: "default",
            //        template: "{controller=Sign}/{action=Index}/{id?}");
            //});
        }
    }
}
