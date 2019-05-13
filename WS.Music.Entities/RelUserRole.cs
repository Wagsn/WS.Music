using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WS.Music.Entities
{
    /// <summary>
    /// 用户与角色关联
    /// </summary>
    public class RelUserRole
    {
        /// <summary>
        /// 关联ID
        /// </summary>
        [Key]
        [MaxLength(36)]
        //[RegularExpression(Constants.GUID_REG, ErrorMessage = Constants.GUID_ERR)]
        public string Id { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        [MaxLength(36)]
        public string UserId { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        [MaxLength(36)]
        public string RoleId { get; set; }

        ///// <summary>
        ///// 用户
        ///// </summary>
        //[NotMapped]
        //[ForeignKey("UserId")]
        //public User User { get; set; }

        ///// <summary>
        ///// 角色
        ///// </summary>
        //[NotMapped]
        //[ForeignKey("RoleId")]
        //public Role Role { get; set; }
    }
}
