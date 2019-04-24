using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WS.Music.Entities
{
    /// <summary>
    /// 榜单
    /// </summary>
    public class TopList
    {
        [MaxLength(36)]
        public string Id { get; set; }

        [MaxLength(63)]
        public string Name { get; set; }

        /// <summary>
        /// 图标路径
        /// </summary>
        [MaxLength(511)]
        public string IcoUrl { get; set; }

        /// <summary>
        /// URL类型：1=站内、2=站外
        /// </summary>
        public int? UrlType { get; set; }

        /// <summary>
        /// 榜单类型：1=歌曲、2=歌单、3=分享
        /// </summary>
        public int? ListType { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 刷新时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 刷新间隔（单位毫秒）
        /// </summary>
        public long Interval { get; set; }
    }
}
