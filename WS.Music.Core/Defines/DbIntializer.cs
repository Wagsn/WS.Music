using System;
using System.Collections.Generic;
using System.Text;
using WS.Music.Core.Entitys;

namespace WS.Music.Core.Defines
{
    public class DbIntializer
    {
        /// <summary>
        /// 数据库初始化
        /// </summary>
        /// <param name="context"></param>
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
