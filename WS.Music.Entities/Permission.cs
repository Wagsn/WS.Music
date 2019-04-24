using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WS.Music.Entities
{
    /// <summary>
    /// 权限
    /// </summary>
    public class Permission
    {
        [MaxLength(36)]
        public string Id { get; set; }

        /// <summary>
        /// 如：ROOT_USER_DELETE、ROOT_SONG_CREATE
        /// </summary>
        [MaxLength(36)]
        public string Code { get; set; }

        [MaxLength(63)]
        public string Name { get; set; }

        [MaxLength(511)]
        public string Description { get; set; }
    }
}
