using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WS.Music.Entities
{
    /// <summary>
    /// 歌单（独立实体，不产生外键关联，关联由关联实体来做），（播放列表是歌单的特殊形式）,用户查询喜欢的歌曲时其实没有查询到PlayList这个表，而是UserPlayList中找PlayListId，再PlayListSong找SongId
    /// </summary>
    public class Playlist
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
        /// 与Name相同
        /// </summary>
        [NotMapped]
        public string Title { get; set; }

        /// <summary>
        /// 与ID相同
        /// </summary>
        [NotMapped]
        public string Type { get; set; }

        /// <summary>
        /// 封面URL（当不存在的时候可以采用第一首歌的封面）
        /// </summary>
        [NotMapped]
        public string CoverUrl { get; set; }

        /// <summary>
        /// 来源
        /// （如：BaiduMusic等，默认：Wagsn）
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// 描述介绍，可空(Empty=Blank>Null)
        /// </summary>
        [MaxLength(511)]
        public string Description { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        public DateTime? DeleteTime { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDeleted { get; set; } = false;

        /// <summary>
        /// 更新时间类型
        /// </summary>
        public int UpdateType { get; set; }

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
