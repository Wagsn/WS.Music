using System;
using System.Collections.Generic;
using System.Text;

namespace WS.Music.Entities
{
    /// <summary>
    /// 用户与歌单的关联实体，（多对多）
    /// </summary>
    public class RelUserPlayList
    {
        public string Id { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 歌单ID
        /// </summary>
        public string PlayListId { get; set; }

        /// <summary>
        /// 关联类型，创建：Create（喜欢：Like），收藏：Collection，推荐：Recommend（推荐歌单）
        /// </summary> 
        public string Type { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// 歌单
        /// </summary>
        public PlayList PlayList { get; set; }
    }
}
