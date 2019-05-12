using Microsoft.EntityFrameworkCore;
using WS.Music.Entities;

namespace WS.Music.Stores
{
    /// <summary>
    /// 数据库上下文
    /// </summary>
    public class MusicDbContext : DbContext
    {
        public MusicDbContext(DbContextOptions<MusicDbContext> options) : base(options) { }

        public MusicDbContext() { }

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

        /// <summary>
        /// 音乐
        /// </summary>
        public DbSet<Song> Songs { get; set; }

        /// <summary>
        /// 组织
        /// </summary>
        public DbSet<Organization> Organizations { get; set; }

        /// <summary>
        /// 文件信息
        /// </summary>
        public DbSet<FileInfo> FileInfos { get; set; }

        /// <summary>
        /// 歌曲信息保存
        /// </summary>
        public DbSet<SongFile> SongFiles { get; set; }

        /// <summary>
        /// 歌单
        /// </summary>
        public DbSet<Playlist> PlayLists { get; set; }

        /// <summary>
        /// 歌单歌曲关联
        /// </summary>
        public DbSet<RelPlayListSong> RelPlayListSongs { get; set; }

        public DbSet<RelArtistAlbum> RelArtistAlbums { get; set; }

        /// <summary>
        /// 歌曲专辑关联
        /// </summary>
        public DbSet<RelSongAlbum> RelSongAlbums { get; set; }

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
