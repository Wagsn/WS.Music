using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WS.Music.Core.Entities
{
    /// <summary>
    /// 艺人，艺人与用户是分离的
    /// </summary>
    public class Artist : TraceUpdate
    {
        /// <summary>
        /// ID
        /// </summary>
        [Key]
        [MaxLength(63)]
        public string Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [MaxLength(63)]
        public string Name { get; set; }

        /// <summary>
        /// 描述介绍，可空(Empty=Blank>Null)
        /// </summary>
        [MaxLength(255)]
        public string Description { get; set; }

        /// <summary>
        /// 建立时间
        /// </summary>
        public DateTime? BirthTime { get; set; }
    }
}
