using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WS.Music.Entities;

namespace WS.Music.Dto
{
    /// <summary>
    /// 歌曲保存接口
    /// </summary>
    public class CommonRequest
    {
        public User User { get; set; }

        public Song Song { get; set; }

        public Album Album { get; set; }

        public Artist Artist { get; set; }
    }
}
