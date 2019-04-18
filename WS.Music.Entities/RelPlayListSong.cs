using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WS.Music.Entities
{
    /// <summary>
    /// 歌单与歌曲的关联实体，（多对多）,关联表不需要那么多其它数据
    /// </summary>
    public class RelPlayListSong
    {
        /// <summary>
        /// 歌单ID
        /// </summary>
        [Key]
        [MaxLength(63)]
        public string PlayListId { get; set; }

        /// <summary>
        /// 歌曲ID
        /// </summary>
        [Key]
        [MaxLength(63)]
        public string SongId { get; set; }

        ///// <summary>
        ///// 歌单
        ///// </summary>
        //[NotMapped]
        //public PlayList PlayList { get; set; }

        ///// <summary>
        ///// 歌曲
        ///// </summary>
        //[NotMapped]
        //public Song Song { get; set; }
    }
}
