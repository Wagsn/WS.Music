using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WS.Music.Entities
{
    /// <summary>
    /// 文件描述
    /// </summary>
    public class FileInfo
    {
        /// <summary>
        /// GUID
        /// </summary>
        [MaxLength(36)]
        public string Id { get; set; }

        /// <summary>
        /// 全路径
        /// </summary>
        [MaxLength(511)]
        public string Path { get; set; }

        /// <summary>
        /// 局部路径
        /// </summary>
        [MaxLength(511)]
        public string RelPath { get; set; }

        /// <summary>
        /// 原路径
        /// </summary>
        [MaxLength(511)]
        public string SrcPath { get; set; }

        /// <summary>
        /// 内容（audio/mpeg、image/jpeg、type/format）
        /// </summary>
        [MaxLength(63)]
        public string ContentType { get; set; }

        /// <summary>
        /// 扩展名
        /// </summary>
        [MaxLength(15)]
        public string Ext { get; set; }

        /// <summary>
        /// 大小
        /// </summary>
        public long Length { get; set; }

        /// <summary>
        /// 内部路径
        /// </summary>
        [MaxLength(511)]
        public string Url { get; set; }

        //public DateTime? CreateTime { get; set; }

        //public DateTime? UpdateTime{ get; set; }

        //public DateTime? VisitTime { get; set; }

    }
}
