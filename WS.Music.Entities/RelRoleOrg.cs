using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WS.Music.Entities
{
    /// <summary>
    /// 角色组织关系
    /// </summary>
    public class RelRoleOrg
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [MaxLength(36)]
        //[RegularExpression(Constants.GUID_REG, ErrorMessage = Constants.GUID_ERR)]
        public string Id { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        [MaxLength(36)]
        public string RoleId { get; set; }

        ///// <summary>
        ///// 角色
        ///// </summary>
        //public Role Role { get; set; }

        /// <summary>
        /// 组织ID
        /// </summary>
        [MaxLength(36)]
        public string OrgId { get; set; }

        ///// <summary>
        ///// 组织
        ///// </summary>
        //public Organization Org { get; set; }
    }
}
