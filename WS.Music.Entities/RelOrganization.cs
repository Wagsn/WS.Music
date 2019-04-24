using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WS.Music.Entities
{
    /// <summary>
    /// 组织关系表
    /// </summary>
    public class RelOrganization
    {
        [MaxLength(36)]
        public string Id { get; set; }

        [MaxLength(36)]
        public string ParentId { get; set; }

        [MaxLength(36)]
        public string ChildId { get; set; }

        /// <summary>
        /// 是否是直接关系
        /// 说明：
        ///     若：组织A有子组织B，组织B有组织C，则组织B是组织A的直接子组织，组织C是组织A的间接子组织
        /// </summary>
        public bool? IsDirect { get; set; }
    }
}
