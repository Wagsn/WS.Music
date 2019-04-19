using System;
using System.Collections.Generic;
using System.Text;

namespace WS.Music.Entities
{
    /// <summary>
    /// 通用榜单项
    /// </summary>
    public class TopItem
    {
        public string Id { get; set; }

        public string TopListId { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// 实际资源
        /// </summary>
        public string SrcId { get; set; }
    }
}
