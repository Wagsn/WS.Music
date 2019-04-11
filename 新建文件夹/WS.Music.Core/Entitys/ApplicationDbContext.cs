using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WS.Music.Entitys
{
    /// <summary>
    /// 数据库上下文
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }



        ///// <summary>
        ///// 模型创建
        ///// </summary>
        ///// <param name="builder"></param>
        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //}
    }
}
