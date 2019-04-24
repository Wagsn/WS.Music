using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WS.Music.Entities;

namespace WS.Music.Dto
{
    public class SongDeleteRequest
    {
        public User User { get; set; }

        public string SongId { get; set; }
    }
}
