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

        public List<User> Users { get; set; }

        public Song Song { get; set; }

        public List<Song> Songs { get; set; }

        public Album Album { get; set; }

        public List<Album> Albums { get; set; }

        public Artist Artist { get; set; }

        public List<Artist> Artists { get; set; }
    }
}
