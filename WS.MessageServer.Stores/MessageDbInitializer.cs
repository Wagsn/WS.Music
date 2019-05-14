using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace WS.MessageServer.Stores
{
    public class MessageDbInitializer
    {
        public static void Initialize(IServiceProvider services)
        {
            var context = services.GetRequiredService<MessageDbContext>();

            context.Database.EnsureCreated();
        }
    }
}
