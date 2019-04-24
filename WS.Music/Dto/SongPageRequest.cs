using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WS.Core;
using WS.Music.Entities;

namespace WS.Music.Dto
{
    public class SongPageRequest : PageSearchRequest
    {
        public Song Song { get; set; }
    }
}
