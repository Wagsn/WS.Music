using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WS.Core;
using WS.Music.Entities;
using WS.Music.Stores;
using WS.Text;

namespace WS.Music.Controllers
{
    [Route("api")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        public ApiController(IMusicStore musicStore)
        {
            MusicStore = musicStore;
        }

        private IMusicStore MusicStore { get; set; }

        /// <summary>
        /// 歌曲搜索
        /// </summary>
        /// <returns></returns>
        [HttpPost("song/list/search")]
        public PagingResponseMessage<Song> SongPageSearch([FromBody]PageSearchRequest request)
        {
            Console.WriteLine($"[{nameof(SongPageSearch)}] 歌曲搜索开始\r\n请求体：{JsonUtil.ToJson(request)}");
            var response = new PagingResponseMessage<Song>();

            try
            {
                response.Data= MusicStore.Set<Song>().Where(a => a.Name.Contains(request.KeyWord)).Skip(request.PageIndex * request.PageSize).Take(request.PageSize).ToList();
                response.PageSize = request.PageSize;
                response.PageIndex = request.PageIndex;
                response.TotalCount = MusicStore.Set<Song>().Count();
            }
            catch(Exception e)
            {
                Console.WriteLine($"[{nameof(SongPageSearch)}] 歌曲搜索失败\r\n请求体：{JsonUtil.ToJson(request)}\r\n错误：{e.ToString()}");
            }
            return response;
        }
    }
}
