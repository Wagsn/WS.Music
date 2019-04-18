using Microsoft.EntityFrameworkCore;

namespace WS.Music.Stores
{
    public interface IStore
    {
        /// <summary>
        /// 数据集
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
    }
}
