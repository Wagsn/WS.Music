using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using WS.Music.Entities;

namespace WS.Music.Stores
{
    public class MusicStore : IMusicStore
    {
        private readonly MusicDbContext Context;

        public MusicStore(MusicDbContext context)
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
    }
}
