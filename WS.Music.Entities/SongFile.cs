using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WS.Music.Entities
{
    /// <summary>
    /// 音乐文件 可以序列化称JSON文件保存下来
    /// </summary>
    public class SongFile
    {
        /// <summary>
        /// 文件ID 与保存的文件名对应 /audio/song/id.ext
        /// </summary>
        [Key]
        [MaxLength(36)]
        public string Id { get; set; }

        /// <summary>
        /// 服务器路径
        /// </summary>
        [MaxLength(511)]
        public string Path { get; set; }

        /// <summary>
        /// URL
        /// </summary>
        [MaxLength(511)]
        public string Url { get; set; }

        /// <summary>
        /// 文件类型 audio/mp3 audio/flac
        /// </summary>
        [MaxLength(63)]
        public string ContentType { get; set; }

        /// <summary>
        /// 文件长度 不存数据库
        /// </summary>
        [NotMapped]
        public long Length { get; set; }

        /// <summary>
        /// 歌曲ID
        /// </summary>
        [MaxLength(36)]
        public string SongId { get; set; }

        /// <summary>
        /// 上传用户ID
        /// </summary>
        [MaxLength(36)]
        public string UpUserId { get; set; }

        ///// <summary>
        ///// 歌曲名
        ///// </summary>
        //[MaxLength(63)]
        //public string SongName { get; set; }

        ///// <summary>
        ///// 专辑ID
        ///// </summary>
        //[MaxLength(36)]
        //public string AlbumId { get; set; }

        ///// <summary>
        ///// 专辑名
        ///// </summary>
        //[MaxLength(63)]
        //public string AlbumName { get; set; }

        ///// <summary>
        ///// 艺人ID组
        ///// </summary>
        //[NotMapped]
        //public List<Artist> Artists { get; set; }
    }
}
