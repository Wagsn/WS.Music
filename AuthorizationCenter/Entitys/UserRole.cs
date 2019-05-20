using AuthorizationCenter.Define;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationCenter.Entitys
{
    /// <summary>
    /// 用户与角色关联
    /// </summary>
    public class UserRole
    {
        /// <summary>
        /// 关联ID
        /// </summary>
        [Key]
        [StringLength(36, MinimumLength = 36)]
        [RegularExpression(Constants.GUID_REG, ErrorMessage = Constants.GUID_ERR)]
        public string Id { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        public string RoleId { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        [NotMapped]
        [ForeignKey("UserId")]
        public User User { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        [NotMapped]
        [ForeignKey("RoleId")]
        public Role Role { get; set; }
    }
}
