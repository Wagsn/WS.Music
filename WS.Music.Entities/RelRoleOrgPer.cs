using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WS.Music.Entities
{
    /// <summary>
    /// 角色组织权限关系
    /// </summary>
    public class RelRoleOrgPer
    {
        /// <summary>
        /// 关联ID - 便于修改
        /// </summary>
        [Key]
        //[MaxLength(36)]
        [MaxLength(36)]
        //[RegularExpression(Constants.GUID_REG, ErrorMessage = Constants.GUID_ERR)]
        public string Id { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        [MaxLength(36)]
        public string RoleId { get; set; }

        ///// <summary>
        ///// 用户
        ///// </summary>
        //[ForeignKey("RoleId")]
        //public Role Role { get; set; }

        /// <summary>
        /// 组织ID
        /// </summary>
        [MaxLength(36)]
        public string OrgId { get; set; }

        ///// <summary>
        ///// 组织
        ///// </summary>
        //[ForeignKey("OrgId")]
        //public Organization Org { get; set; }

        /// <summary>
        /// 权限ID
        [MaxLength(36)]
        /// </summary>
        public string PerId { get; set; }

        ///// <summary>
        ///// 权限
        ///// </summary>
        //[ForeignKey("PerId")]
        //public Permission Per { get; set; }
    }
}
