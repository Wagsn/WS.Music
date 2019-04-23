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

        /// <summary>
        /// 歌曲删除 -管理后台接口
        /// TODO：包含歌曲文件的删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("song/{id}")]
        public ResponseMessage SongDelete([FromRoute] string id)
        {
            Console.WriteLine($"[{nameof(SongDelete)}] 歌曲 删除 开始\r\n请求体：{JsonUtil.ToJson(id)}");
            var response = new ResponseMessage();

            try
            {
                var song = MusicStore.Set<Song>().Find(id);
                MusicStore.DeleteAll(song);
            }
            catch(Exception e)
            {
                Console.WriteLine($"[{nameof(SongDelete)}] 歌曲 删除 失败\r\n请求体：{JsonUtil.ToJson(id)}\r\n错误：{e.ToString()}");
            }
            return response;
        }

        /// <summary>
        /// 歌曲信息保存
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("song/info/save")]
        public ResponseMessage SongInfoSave([FromBody]Song request)
        {
            Console.WriteLine($"[{nameof(SongInfoSave)}] 歌曲信息 保存 开始\r\n请求体：{JsonUtil.ToJson(request)}");
            var response = new ResponseMessage();

            try
            {
                if (string.IsNullOrWhiteSpace(request.Id))
                {
                    MusicStore.AddAll(request);
                }
                else
                {
                    request.Id = Guid.NewGuid().ToString();
                    MusicStore.UpdateAll(request);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"[{nameof(SongInfoSave)}] 歌曲信息 保存 失败\r\n请求体：{JsonUtil.ToJson(request)}\r\n错误：{e.ToString()}");
            }
            return response;
        }


    }
}
