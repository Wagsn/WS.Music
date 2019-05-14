using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using WS.MessageServer.Stores;

namespace WS.MessageServer
{
    public class MessageServerStartup
    {
        /// <summary>
        /// 依赖注入
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureServices(IServiceCollection services)
        {
            var configuration = new ConfigurationBuilder()
                    .AddJsonFile("config.json")
                    .AddEnvironmentVariables()
                    .Build();

            services.AddDbContext<MessageDbContext>(it =>
            {
                it.UseMySql(configuration["Data:DefaultConnection:MessageConnection"]);
                //it.UseMySql("server=localhost;database=ws_music;user=admin;password=123456;");
            });

            services.AddScoped<IMessageSender, AppPusher>();
            services.AddScoped<IMessageStore, MessageStore>();
        }

        /// <summary>
        /// 数据库初始化
        /// </summary>
        /// <param name="services"></param>
        public static void DbInitialize(IServiceProvider services)
        {
            var context = services.GetRequiredService<MessageDbContext>();

            context.Database.EnsureCreated();
        }
    }
}
