using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WS.Music.Entities
{
    /// <summary>
    /// 用户与歌单的关联实体，（多对多）
    /// </summary>
    public class RelUserPlayList
    {
        [MaxLength(36)]
        public string Id { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        [MaxLength(36)]
        public string UserId { get; set; }

        /// <summary>
        /// 歌单ID
        /// </summary>
        [MaxLength(36)]
        public string PlayListId { get; set; }

        /// <summary>
        /// 关联类型，创建：Create（喜欢：Like），收藏：Collection，推荐：Recommend（推荐歌单）
        /// </summary> 
        [MaxLength(63)]
        public string Type { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// 歌单
        /// </summary>
        public Playlist PlayList { get; set; }
    }
}
