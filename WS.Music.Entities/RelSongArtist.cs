using System;
using System.Collections.Generic;
using System.Text;

namespace WS.Music.Entities
{
    /// <summary>
    /// 歌曲与艺人的关联实体，（多对多）
    /// </summary>
    public class RelSongArtist
    {
        public string Id { get; set; }

        /// <summary>
        /// 歌曲ID
        /// </summary>
        public string SongId { get; set; }

        /// <summary>
        /// 艺人ID
        /// </summary>
        public string ArtistId { get; set; }

        /// <summary>
        /// 歌曲
        /// </summary>
        public Song Song { get; set; }

        /// <summary>
        /// 艺人
        /// </summary>
        public Artist Artist { get; set; }
    }
}
