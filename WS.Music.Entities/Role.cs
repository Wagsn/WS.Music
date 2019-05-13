using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WS.Music.Entities
{
    /// <summary>
    /// 角色
    /// </summary>
    public class Role
    {
        /// <summary>
        /// GUID
        /// </summary>
        [Key]
        [MaxLength(36)]
        //[StringLength(36, MinimumLength = 36)]
        public string Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        //[MaxLength(15)]
        //[Required(ErrorMessage = "角色名称不能为空")]
        [MaxLength(53)]
        public string Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [MaxLength(511)]
        public string Decription { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }
    }
}
