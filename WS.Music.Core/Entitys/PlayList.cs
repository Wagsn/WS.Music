using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WS.Music.Core.Entitys
{
    /// <summary>
    /// 歌单（独立实体，不产生外键关联，关联由关联实体来做），（播放列表是歌单的特殊形式）,用户查询喜欢的歌曲时其实没有查询到PlayList这个表，而是UserPlayList中找PlayListId，再PlayListSong找SongId
    /// </summary>
    public class PlayList : TraceUpdate
    {
        /// <summary>
        /// 歌单ID
        /// </summary>
        [MaxLength(36)]
        public string Id { get; set; }

        /// <summary>
        /// 歌单名称
        /// </summary>
        [MaxLength(63)]
        public string Name { get; set; }

        /// <summary>
        /// 描述介绍，可空(Empty=Blank>Null)
        /// </summary>
        [MaxLength(511)]
        public string Description { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? BuildTime { get; set; }

        /// <summary>
        /// 歌曲数量，动态数据，默认值0
        /// </summary>
        public long SongNum { get; set; }

        /// <summary>
        /// 播放次数，动态数据，默认值0
        /// </summary>
        public long PlayNum { get; set; }

        ///// <summary>
        ///// 创建用户，不映射数据库（是将UserId和UserName写在这个表中还是通过关联表（PlayListUser）关联）
        ///// </summary>
        //[NotMapped]
        //public User User { get; set; }

        ///// <summary>
        ///// 歌曲列表，不映射数据库（通过PlayListSong表关联）
        ///// </summary>
        //[NotMapped]
        //public List<Song> SongList { get; set; }
    }
}
