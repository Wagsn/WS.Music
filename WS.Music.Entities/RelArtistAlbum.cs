using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WS.Music.Entities
{
    public class RelArtistAlbum
    {
        [MaxLength(36)]
        public string Id { get; set; }

        /// <summary>
        /// 艺人ID
        /// </summary>
        [MaxLength(36)]
        public string ArtistId { get; set; }

        /// <summary>
        /// 专辑ID
        /// </summary>
        [MaxLength(36)]
        public string AlbumId { get; set; }

        ///// <summary>
        ///// 艺人
        ///// </summary>
        //public Artist Artist { get; set; }

        ///// <summary>
        ///// 专辑
        ///// </summary>
        //public Album Album { get; set; }
    }
}
