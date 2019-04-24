using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WS.Music.Entities
{
    /// <summary>
    /// 通用榜单项
    /// </summary>
    public class TopItem
    {
        [MaxLength(36)]
        public string Id { get; set; }

        [MaxLength(36)]
        public string TopListId { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// 实际资源
        /// </summary>
        [MaxLength(36)]
        public string SrcId { get; set; }
    }
}
