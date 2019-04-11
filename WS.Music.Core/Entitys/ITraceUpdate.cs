using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WS.Music.Core.Entitys
{
    /// <summary>
    /// 数据库操作跟踪
    /// </summary>
    public interface ITraceUpdate
    {
        /// <summary>
        /// 创建人ID
        /// </summary>
        string _CreateUserId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        DateTime? _CreateTime { get; set; }

        /// <summary>
        /// 更新人ID
        /// </summary>
        string _UpdateUserId { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        DateTime? _UpdateTime { get; set; }

        /// <summary>
        /// 删除人ID
        /// </summary>
        string _DeleteUserId { get; set; }

        /// <summary>
        /// 删除人时间
        /// </summary>
        DateTime? _DeleteTime { get; set; }

        /// <summary>
        /// 删除状态，软删除（在数据库存在，在客户端不存在）
        /// </summary>
        bool _IsDeleted { get; set; }
    }
}
