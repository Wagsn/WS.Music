using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WS.Music.Core.Entitys
{
    /// <summary>
    /// 歌曲实体，逻辑信息（在文件服务器上的位置就不要写在这里），歌曲信息的更新必须要知道其ID，如何处理现场或翻唱之类的行为（有些是分成两首歌，歌名或歌手不同）
    /// </summary>
    public class Song : TraceUpdate
    {
        /// <summary>
        /// 歌曲ID
        /// </summary>
        [Key]
        [MaxLength(63)]
        public string Id { get; set; }

        /// <summary>
        /// 歌曲名
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 描述介绍，可空(Empty=Blank>Null)
        /// </summary>
        [MaxLength(511)]
        public string Description { get; set; }

        /// <summary>
        /// 歌曲的持续时长，可能不需要，因为通过歌曲文件可以得到
        /// </summary>
        public long? Duration { get; set; }

        /// <summary>
        /// 发行时间
        /// </summary>
        public DateTime? ReleaseTime { get; set; }

        /// <summary>
        /// 歌曲文件的URL，标准输出，如果想听其他规格的歌曲文件，请在SongFile中查找
        /// </summary>
        [MaxLength(255)]
        public string Url { get; set; }

        // 还有个来源Link，比如来源（ID+Type：id=156&type=songlist）于专辑，歌单、排行榜、分享，这个主要是标注播放时的来源问题，方便跳转过去

        ///// <summary>
        ///// 艺人
        ///// </summary>
        //[NotMapped]
        //public List<Artist> Artists { get; set; }

        ///// <summary>
        ///// 专辑（歌曲可能属于几个专辑，不过当前歌曲只会提示属于哪个专辑）
        ///// </summary>
        //[NotMapped]
        //public Album Album { get; set; }
    }
}
