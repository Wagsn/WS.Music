using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace WS.Core
{
    public interface IStore
    {
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        /// <summary>
        /// 按条件查询
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="predicate">条件表达式</param>
        /// <returns></returns>
        IQueryable<TEntity> Find<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class;

        /// <summary>
        /// 添加所有
        /// </summary>
        /// <param name="entities"></param>
        void AddAll(params object[] entities);

        /// <summary>
        /// 更新所有
        /// </summary>
        /// <param name="entities"></param>
        void UpdateAll(params object[] entities);

        /// <summary>
        /// 删除所有
        /// </summary>
        /// <param name="entities"></param>
        void DeleteAll(params object[] entities);
    }
}
