using Microsoft.EntityFrameworkCore;

namespace WS.Music.Entities
{
    /// <summary>
    /// 数据库上下文
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public ApplicationDbContext() { }

        /// <summary>
        /// 用户
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// 专辑
        /// </summary>
        public DbSet<Album> Albums { get; set; }

        /// <summary>
        /// 艺术家
        /// </summary>
        public DbSet<Artist> Artists { get; set; }



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
