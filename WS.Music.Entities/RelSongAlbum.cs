using System;
using System.Collections.Generic;
using System.Text;

namespace WS.Music.Entities
{
    /// <summary>
    /// 歌曲与专辑的关联实体（多对多）
    /// </summary>
    public class RelSongAlbum
    {
        public string Id { get; set; }

        /// <summary>
        /// 歌曲ID
        /// </summary>
        public string SongId { get; set; }

        /// <summary>
        /// 专辑ID
        /// </summary>
        public string AlbumId { get; set; }

        /// <summary>
        /// 歌曲
        /// </summary>
        public Song Song { get; set; }

        /// <summary>
        /// 专辑
        /// </summary>
        public Album Album { get; set; }
    }
}
