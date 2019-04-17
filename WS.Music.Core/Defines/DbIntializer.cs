using System;
using System.Collections.Generic;
using System.Text;
using WS.Music.Core.Entities;

namespace WS.Music.Core.Defines
{
    /// <summary>
    /// 数据库初始化器
    /// </summary>
    public class DbIntializer
    {
        /// <summary>
        /// 库初始化
        /// </summary>
        /// <param name="context"></param>
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
