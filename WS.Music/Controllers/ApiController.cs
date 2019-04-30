using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WS.Core;
using WS.Music.Dto;
using WS.Music.Entities;
using WS.Music.Stores;
using WS.Text;

namespace WS.Music.Controllers
{
    [Route("api")]
    [ApiController]
    //[Produces("application/json")]
    public class ApiController : ControllerBase
    {
        public ApiController(IMusicStore musicStore)
        {
            MusicStore = musicStore;
        }

        private IMusicStore MusicStore { get; set; }


        #region << Artist >>
        /// <summary>
        /// 艺人 信息 列表
        /// </summary>
        /// <returns></returns>
        [HttpPost("artist/list")]
        public PagingResponseMessage<Artist> ArtistList([FromBody]PageSearchRequest request)
        {
            Console.WriteLine($"[{nameof(AlbumList)}] 艺人 信息 列表 开始\r\n请求体：{JsonUtil.ToJson(request)}");
            var response = new PagingResponseMessage<Artist>();

            try
            {
                response.Data = MusicStore.Set<Artist>().Where(a => a.Name.Contains(request.KeyWord)).Skip(request.PageIndex * request.PageSize).Take(request.PageSize).ToList();
                response.PageSize = request.PageSize;
                response.PageIndex = request.PageIndex;
                response.TotalCount = MusicStore.Set<Artist>().Count();
            }
            catch (Exception e)
            {
                Console.WriteLine($"[{nameof(ArtistList)}] 艺人 信息 列表 失败\r\n请求体：{JsonUtil.ToJson(request)}\r\n错误：{e.ToString()}");
            }
            return response;
        }

        /// <summary>
        /// 艺人 信息 删除 -管理后台接口
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("artist/delete")]
        public ResponseMessage ArtistDelete([FromBody]CommonRequest request)
        {
            Console.WriteLine($"[{nameof(AlbumDelete)}] 艺人 信息 删除 开始\r\n请求体：{JsonUtil.ToJson(request)}");
            var response = new ResponseMessage();

            try
            {
                MusicStore.DeleteAll(MusicStore.Set<Artist>().Find(request.Album.Id));
            }
            catch (Exception e)
            {
                Console.WriteLine($"[{nameof(ArtistDelete)}] 艺人 信息 删除 失败\r\n请求体：{JsonUtil.ToJson(request)}\r\n错误：{e.ToString()}");
            }
            return response;
        }

        /// <summary>
        /// 艺人 信息 保存
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("artist/save")]
        public ResponseMessage ArtistSave([FromBody]CommonRequest request)
        {
            Console.WriteLine($"[{nameof(ArtistSave)}] 艺人 信息 保存 开始\r\n请求体：{JsonUtil.ToJson(request)}");
            var response = new ResponseMessage();

            if (request == null || request.Artist == null)
            {
                return new ResponseMessage
                {
                    Code = ResponseCodeDefines.ModelStateInvalid,
                    Message = "模型验证失败"
                };
            }

            try
            {
                if (string.IsNullOrWhiteSpace(request.Artist.Id))
                {
                    request.Artist.Id = Guid.NewGuid().ToString();
                    MusicStore.AddAll(request.Artist);
                }
                else
                {
                    MusicStore.UpdateAll(request);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"[{nameof(ArtistSave)}] 艺人 信息 保存 失败\r\n请求体：{JsonUtil.ToJson(request)}\r\n错误：{e.ToString()}");
            }
            return response;
        }
        #endregion

        #region << Album >>
        /// <summary>
        /// 专辑 信息 列表
        /// </summary>
        /// <returns></returns>
        [HttpPost("album/list")]
        public PagingResponseMessage<Album> AlbumList([FromBody]PageSearchRequest request)
        {
            Console.WriteLine($"[{nameof(AlbumList)}] 专辑 信息 列表 开始\r\n请求体：{JsonUtil.ToJson(request)}");
            var response = new PagingResponseMessage<Album>();

            try
            {
                response.Data = MusicStore.Set<Album>().Where(a => a.Name.Contains(request.KeyWord)).Skip(request.PageIndex * request.PageSize).Take(request.PageSize).ToList();
                response.PageSize = request.PageSize;
                response.PageIndex = request.PageIndex;
                response.TotalCount = MusicStore.Set<Album>().Count();
            }
            catch (Exception e)
            {
                Console.WriteLine($"[{nameof(AlbumList)}] 专辑 信息 列表 失败\r\n请求体：{JsonUtil.ToJson(request)}\r\n错误：{e.ToString()}");
            }
            return response;
        }

        /// <summary>
        /// 专辑 信息 删除 -管理后台接口
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("album/delete")]
        public ResponseMessage AlbumDelete([FromBody]CommonRequest request)
        {
            Console.WriteLine($"[{nameof(AlbumDelete)}] 专辑 信息 删除 开始\r\n请求体：{JsonUtil.ToJson(request)}");
            var response = new ResponseMessage();

            try
            {
                MusicStore.DeleteAll(MusicStore.Set<Album>().Find(request.Album.Id));
            }
            catch (Exception e)
            {
                Console.WriteLine($"[{nameof(AlbumDelete)}] 专辑 信息 删除 失败\r\n请求体：{JsonUtil.ToJson(request)}\r\n错误：{e.ToString()}");
            }
            return response;
        }

        /// <summary>
        /// 专辑 信息 保存
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("album/save")]
        public ResponseMessage AlbumSave([FromBody]CommonRequest request)
        {
            Console.WriteLine($"[{nameof(AlbumSave)}] 专辑 信息 保存 开始\r\n请求体：{JsonUtil.ToJson(request)}");
            var response = new ResponseMessage();

            if(request ==null || request.Album == null)
            {
                return new ResponseMessage
                {
                    Code = ResponseCodeDefines.ModelStateInvalid,
                    Message = "模型验证失败"
                };
            }

            try
            {
                if (string.IsNullOrWhiteSpace(request.Album.Id))
                {
                    request.Album.Id = Guid.NewGuid().ToString();
                    MusicStore.AddAll(request.Album);
                }
                else
                {
                    MusicStore.UpdateAll(request);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"[{nameof(AlbumSave)}] 专辑 信息 保存 失败\r\n请求体：{JsonUtil.ToJson(request)}\r\n错误：{e.ToString()}");
            }
            return response;
        }
        #endregion

        #region << Song >>
        /// <summary>
        /// 歌曲搜索
        /// </summary>
        /// <returns></returns>
        [HttpPost("song/list")]
        public PagingResponseMessage<Song> SongSearch([FromForm]PageSearchRequest request)
        {
            Console.WriteLine($"[{nameof(SongSearch)}] 歌曲搜索开始\r\n请求体：{JsonUtil.ToJson(request)}");
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
                Console.WriteLine($"[{nameof(SongSearch)}] 歌曲搜索失败\r\n请求体：{JsonUtil.ToJson(request)}\r\n错误：{e.ToString()}");
            }
            return response;
        }

        /// <summary>
        /// 歌曲删除 -管理后台接口
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("song/delete")]
        public ResponseMessage SongDelete([FromBody]CommonRequest request)
        {
            Console.WriteLine($"[{nameof(SongDelete)}] 歌曲 删除 开始\r\n请求体：{JsonUtil.ToJson(request)}");
            var response = new ResponseMessage();

            try
            {
                var song = MusicStore.Set<Song>().Find(request.Song.Id);
                MusicStore.DeleteAll(song);
            }
            catch(Exception e)
            {
                Console.WriteLine($"[{nameof(SongDelete)}] 歌曲 删除 失败\r\n请求体：{JsonUtil.ToJson(request)}\r\n错误：{e.ToString()}");
            }
            return response;
        }

        /// <summary>
        /// 歌曲信息保存
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("song/save")]
        public ResponseMessage SongSave([FromBody]CommonRequest request)
        {
            Console.WriteLine($"[{nameof(SongSave)}] 歌曲 信息 保存 开始\r\n请求体：{JsonUtil.ToJson(request)}");
            var response = new ResponseMessage();

            try
            {
                if (string.IsNullOrWhiteSpace(request.Song.Id))
                {
                    request.Song.Id = Guid.NewGuid().ToString();
                    MusicStore.AddAll(request.Song);
                }
                else
                {
                    MusicStore.UpdateAll(request);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"[{nameof(SongSave)}] 歌曲 信息 保存 失败\r\n请求体：{JsonUtil.ToJson(request)}\r\n错误：{e.ToString()}");
            }
            return response;
        }
        #endregion
    }
}
