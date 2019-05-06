using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WS.Music.Entities
{
    /// <summary>
    /// 音乐专辑实体
    /// </summary>
    public class Album
    {
        /// <summary>
        /// 标签 Album
        /// </summary>
        [NotMapped]
        public static readonly string TAG = "Album";

        /// <summary>
        /// 专辑ID
        /// </summary>
        [Key]
        [MaxLength(36)]
        public string Id { get; set; }

        /// <summary>
        /// 专辑名称
        /// </summary>
        [MaxLength(63)]
        public string Name { get; set; }

        [NotMapped]
        public string ArtistName { get; set; }

        /// <summary>
        /// 描述介绍，可空(Empty=Blank>Null)
        /// </summary>
        //[MaxLength(2047)]
        public string Description { get; set; }

        /// <summary>
        /// 发行时间
        /// </summary>
        public DateTime? ReleaseTime { get; set; }

        ///// <summary>
        ///// 包含的歌曲
        ///// </summary>
        //[NotMapped]
        //public List<Song> Songs { get; set; }

        ///// <summary>
        ///// 演唱歌手列表（一般只有一个，用于合辑）
        ///// </summary>
        //[NotMapped]
        //public List<Artist> Artists { get; set; }
    }
}
