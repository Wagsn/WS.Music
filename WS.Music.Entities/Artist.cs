using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WS.Music.Entities
{
    /// <summary>
    /// 艺人，艺人与用户是分离的，如何描述乐队组合之类的东西？
    /// </summary>
    public class Artist
    {
        /// <summary>
        /// ID
        /// </summary>
        [Key]
        [MaxLength(36)]
        public string Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [MaxLength(63)]
        public string Name { get; set; }

        /// <summary>
        /// 描述介绍，可空(Empty=Blank>Null)
        /// </summary>
        [MaxLength(511)]
        public string Description { get; set; }

        /// <summary>
        /// 出道/首秀 时间
        /// </summary>
        public DateTime? DebutTime { get; set; }

        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public DateTime? BirthTime { get; set; }
    }
}
