using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WS.Music.Core.Entitys
{
    /// <summary>
    /// 数据库上下文
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

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
    }
}
