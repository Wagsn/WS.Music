namespace WS.Music.Stores
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
        public static void Initialize(MusicDbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
