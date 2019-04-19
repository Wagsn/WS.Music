using System;
using System.Collections.Generic;
using System.Text;

namespace WS.Music.Entities
{
    /// <summary>
    /// 用户组织关联
    /// </summary>
    public class RelUserOrganization
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        public string OrganizationId { get; set; }

        ///// <summary>
        ///// 关系类型（1=成员、2=所有者、3=管理者??由角色权限来控制??）
        ///// </summary>
        //public string RelType { get; set; }
    }
}
