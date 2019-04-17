using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WS.Music.Core.Entities
{
    /// <summary>
    /// 数据库上下文
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public ApplicationDbContext() { }

        /// <summary>
        /// 用户表
        /// </summary>
        public DbSet<User> User { get; set; }


        ///// <summary>
        ///// 模型创建
        ///// </summary>
        ///// <param name="builder"></param>
        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //}

        ///// <summary>
        ///// 数据库配置 -迁移到Startup.cs -生成基架时取消注释
        ///// </summary>
        ///// <param name = "builder" > 数据库上下文选项创建器 </ param >
        //protected override void OnConfiguring(DbContextOptionsBuilder builder)
        //{
        //    base.OnConfiguring(builder);
        //    //Pomelo.EntityFrameworkCore.MySql
        //    //TODO: 采用配置文件的方式
        //    builder.UseMySql("server=192.168.100.132;database=ws_internship;user=admin;password=123456;");
        //}
}
}
