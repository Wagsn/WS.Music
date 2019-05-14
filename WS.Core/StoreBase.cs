using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace WS.Core
{
    public class StoreBase : IStore
    {
        public readonly DbContext Context;

        public StoreBase(DbContext context)
        {
            Context = context;
        }

        /// <summary>
        /// 数据集
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public DbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return Context.Set<TEntity>();
        }

        public void DeleteAll(params object[] entities)
        {
            Context.RemoveRange(entities);
            Context.SaveChanges();
        }

        public IQueryable<TEntity> Find<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            return Context.Set<TEntity>().Where(predicate);
        }

        public void AddAll(params object[] entities)
        {
            Context.AddRange(entities);
            Context.SaveChanges();
        }

        public void UpdateAll(params object[] entities)
        {
            Context.UpdateRange(entities);
            Context.SaveChanges();
        }
    }
}
