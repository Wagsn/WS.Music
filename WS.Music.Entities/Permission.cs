using System;
using System.Collections.Generic;
using System.Text;

namespace WS.Music.Entities
{
    /// <summary>
    /// 权限
    /// </summary>
    public class Permission
    {
        public string Id { get; set; }

        /// <summary>
        /// 如：ROOT_USER_DELETE、ROOT_SONG_CREATE
        /// </summary>
        public string Code { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
