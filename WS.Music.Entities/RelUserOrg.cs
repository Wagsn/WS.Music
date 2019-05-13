using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WS.Music.Entities
{
    public class RelUserOrg
    {
        /// <summary>
        /// 关联ID
        /// </summary>
        [Key]
        [StringLength(36, MinimumLength = 36)]
        //[RegularExpression(Constants.GUID_REG, ErrorMessage = Constants.GUID_ERR)]
        public string Id { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserId { get; set; }

        ///// <summary>
        ///// 用户
        ///// </summary>
        //public User User { get; set; }

        /// <summary>
        /// 组织ID
        /// </summary>
        public string OrgId { get; set; }

        ///// <summary>
        ///// 组织
        ///// </summary>
        //public Organization Org { get; set; }
    }
}
