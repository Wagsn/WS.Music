using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WS.Music.Entities
{
    /// <summary>
    /// 用户与歌单的关联实体，（多对多）
    /// </summary>
    public class RelUserPlaylist
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

        public class TypeEnum
        {
            /// <summary>
            /// 收藏
            /// </summary>
            public static readonly string Collection = "Collection";
            /// <summary>
            /// 推荐
            /// </summary>
            public static readonly string Recommend = "Recommend";
            /// <summary>
            /// 创建
            /// </summary>
            public static readonly string Create = "Create";
            /// <summary>
            /// 喜欢
            /// </summary>
            public static readonly string Like = "Like";
            /// <summary>
            /// 播放历史
            /// </summary>
            public static readonly string History = "History";
        }
    }
}
