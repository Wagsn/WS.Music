﻿using AuthorizationCenter.Entitys;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WS.Log;
using WS.Text;

namespace AuthorizationCenter.Stores
{
    /// <summary>
    /// 用户核心表存储实现
    /// </summary>
    public class UserBaseStore : StoreBase<User>, IUserBaseStore
    {
        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="dbContext"></param>
        public UserBaseStore([Required]ApplicationDbContext dbContext): base(dbContext) {  }

        /// <summary>
        /// 查询 通过ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IQueryable<User> FindById(string id)
        {
            return Find(ub => ub.Id == id);
        }

        /// <summary>
        /// 查询 -通过名称
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IQueryable<User> FindByName(string name)
        {
            return Find(ub => ub.SignName == name);
        }

        /// <summary>
        /// 删除 -通过用户ID
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <returns></returns>
        public Task<IEnumerable<User>> DeleteById(string id)
        {
            // 打印日志
            Logger.Trace($"[{nameof(DeleteById)}] 条件删除用户({id})");
            return Delete(ub => ub.Id == id);
        }

        /// <summary>
        /// 删除 -通过用户名
        /// </summary>
        /// <param name="name">用户名</param>
        /// <returns></returns>
        public Task<IEnumerable<User>> DeleteByName(string name)
        {
            // 打印日志
            Logger.Trace($"[{nameof(DeleteByName)}] 条件删除用户({name})");
            return Delete(ub => ub.SignName == name);
        }
    }
}
