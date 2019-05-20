using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace WS.Test
{
    /// <summary>
    /// 测试基类
    /// </summary>
    public class TestBase<TContext> where TContext : DbContext
    {
        public readonly IServiceProvider ServiceProvider;
        public readonly TContext Context;

        public TestBase()
        {
            var efServiceProvider = new ServiceCollection().AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();
            var service = new ServiceCollection();
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();
            var config = builder.Build();

            ServiceProvider = service.BuildServiceProvider();
            Context = ServiceProvider.GetRequiredService<TContext>();
        }
    }
}
