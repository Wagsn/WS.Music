﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileServer;
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
        public ApiController(IMusicStore musicStore, FileServerConfig fileServerConfig)
        {
            MusicStore = musicStore;
            FileServerConfig = fileServerConfig;
        }

        private IMusicStore MusicStore { get; set; }
        private FileServerConfig FileServerConfig { get; set; }

        /// <summary>
        /// 检查服务器是否运行
        /// </summary>
        /// <returns></returns>
        [HttpGet("check")]
        public ResponseMessage Check()
        {
            return new ResponseMessage
            {
                Code ="0",
                Message = "It's work!"
            };
        }

        /// <summary>
        /// 返回歌单列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("playlist/list")]
        public PagingResponseMessage<Playlist> PlaylistSearch([FromForm]CommonRequest request)
        {
            var query = MusicStore.Set<Playlist>().AsQueryable();

            return new PagingResponseMessage<Playlist>
            {
                Code = "0",
                Data = query.ToList()
            };
        }

        /// <summary>
        /// 通用保存接口
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ResponseMessage CommonSave(CommonRequest request)
        {

            return null;
        }

        #region << Artist >>
        /// <summary>
        /// 艺人 信息 列表
        /// </summary>
        /// <returns></returns>
        [HttpPost("artist/list")]
        public PagingResponseMessage<Artist> ArtistList([FromForm]PageSearchRequest request)
        {
            Console.WriteLine($"[{nameof(AlbumList)}] 艺人 信息 列表 开始\r\n请求体：{JsonUtil.ToJson(request)}");
            var response = new PagingResponseMessage<Artist>();

            if(request == null)
            {
                response.Code = ResponseDefine.ArgumentNullError;
                response.Message = "参数错误";
                return response;
            }

            if (request.Keyword == null)
            {
                request.Keyword = "";
            }

            try
            {
                var query = MusicStore.Set<Artist>().AsQueryable();
                if (!string.IsNullOrWhiteSpace(request.Keyword))
                {
                    query = query.Where(a => a.Name.Contains(request.Keyword));
                }
                if(request.Ids != null && request.Ids.Count > 0)
                {
                    query = query.Where(a => request.Ids.Contains(a.Id));
                }
                // 根据专辑查艺人
                if(request.Albums != null)
                {
                    var albIds = request.Albums.Select(a => a.Id);
                    var artIds = MusicStore.Set<RelArtistAlbum>().Where(a => albIds.Contains(a.AlbumId)).Select(a => a.ArtistId).ToList();
                    query = query.Where(a => artIds.Contains(a.Id));
                }
                response.Data = query.Skip(request.PageIndex * request.PageSize).Take(request.PageSize).ToList();
                response.PageSize = request.PageSize;
                response.PageIndex = request.PageIndex;
                response.TotalCount = query.Count();
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
        public ResponseMessage ArtistDelete([FromForm]CommonRequest request)
        {
            Console.WriteLine($"[{nameof(AlbumDelete)}] 艺人 信息 删除 开始\r\n请求体：{JsonUtil.ToJson(request)}");
            Console.WriteLine("Form: " + JsonUtil.ToJson(Request.Form));
            var response = new ResponseMessage();

            try
            {
                var artistIds = request.Artists.Select(a => a.Id).ToList();
                MusicStore.DeleteAll(MusicStore.Set<Artist>().Where(a => artistIds.Contains(a.Id)).ToArray());
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
        public ResponseMessage ArtistSave([FromForm]CommonRequest request)
        {
            Console.WriteLine($"[{nameof(ArtistSave)}] 艺人 信息 保存 开始\r\n请求体：{JsonUtil.ToJson(request)}");
            Console.WriteLine($"Request {JsonUtil.ToJson(Request.Form)}");
            var response = new ResponseMessage();

            if (request == null || request.Artist == null)
            {
                return new ResponseMessage
                {
                    Code = ResponseDefine.ModelStateInvalid,
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
                    var entity = MusicStore.Find<Artist>(a => a.Id.Equals(request.Artist.Id)).SingleOrDefault();
                    if(entity != null)
                    {
                        entity.Name = request.Artist.Name;
                        entity.Description= request.Artist.Description;
                        entity.DebutTime= request.Artist.DebutTime;
                        MusicStore.UpdateAll(entity);
                    }
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
        public PagingResponseMessage<Album> AlbumList([FromForm]PageSearchRequest request)
        {
            Console.WriteLine($"[{nameof(AlbumList)}] 专辑 信息 列表 开始\r\n请求体：{JsonUtil.ToJson(request)}");
            var response = new PagingResponseMessage<Album>();

            if (request != null && request.Keyword == null)
            {
                request.Keyword = "";
            }

            try
            {
                var query = MusicStore.Set<Album>().AsQueryable();
                if (!string.IsNullOrWhiteSpace(request.Keyword))
                {
                    query = query.Where(a => a.Name.Contains(request.Keyword));
                }
                if (request.Ids != null && request.Ids.Count > 0)
                {
                    query = query.Where(a => request.Ids.Contains(a.Id));
                }
                var albums = query.Skip(request.PageIndex * request.PageSize).Take(request.PageSize).ToList();
                foreach(var album in albums)
                {
                    // 根据专辑ID找艺人ID，根据艺人ID找艺人名称
                    var rel = MusicStore.Set<RelArtistAlbum>().Where(a => a.AlbumId.Equals(album.Id)).SingleOrDefault();
                    if(rel != null)
                    {
                        var art = MusicStore.Set<Artist>().Find(rel.ArtistId);
                        album.ArtistId = art?.Id;
                        album.ArtistName = art?.Name;
                    }
                }
                response.Data = albums;
                response.PageSize = request.PageSize;
                response.PageIndex = request.PageIndex;
                response.TotalCount = query.Count();
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
        public ResponseMessage AlbumDelete([FromForm]CommonRequest request)
        {
            Console.WriteLine($"[{nameof(AlbumDelete)}] 专辑 信息 删除 开始\r\n请求体：{JsonUtil.ToJson(request)}");
            var response = new ResponseMessage();

            try
            {
                var albumIds = request.Albums.Select(a => a.Id).ToList();
                MusicStore.DeleteAll(MusicStore.Set<Album>().Where(a => albumIds.Contains(a.Id)).ToArray());
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
        public ResponseMessage AlbumSave([FromForm]CommonRequest request)
        {
            Console.WriteLine($"[{nameof(AlbumSave)}] 专辑 信息 保存 开始\r\n请求体：{JsonUtil.ToJson(request)}");
            var response = new ResponseMessage();

            if(request ==null || request.Album == null)
            {
                return new ResponseMessage
                {
                    Code = ResponseDefine.ModelStateInvalid,
                    Message = "模型验证失败"
                };
            }

            try
            {
                if (string.IsNullOrWhiteSpace(request.Album.Id))
                {
                    request.Album.Id = Guid.NewGuid().ToString();
                    if (request.Album.ArtistId != null)
                    {
                        // 创建关联
                        MusicStore.AddAll(new RelArtistAlbum
                        {
                            Id = Guid.NewGuid().ToString(),
                            ArtistId = request.Album.ArtistId,
                            AlbumId = request.Album.Id
                        });
                    }
                    MusicStore.AddAll(request.Album);
                }
                else
                {
                    var entity = MusicStore.Find<Album>(a => a.Id.Equals(request.Album.Id)).SingleOrDefault();
                    if (entity != null)
                    {
                        entity.Name = request.Album.Name;
                        entity.ArtistName = request.Album.ArtistName;
                        entity.Description = request.Album.Description;
                        entity.ReleaseTime = request.Album.ReleaseTime;
                        if (request.Album.ArtistId != null)
                        {
                            // 删除原关联
                            var oldRel = MusicStore.Set<RelArtistAlbum>().Where(a => a.AlbumId.Equals(entity.Id)).SingleOrDefault();
                            if(oldRel == null)
                            {
                                // 原关联不存在 创建新关联
                                MusicStore.AddAll(new RelArtistAlbum
                                {
                                    Id = Guid.NewGuid().ToString(),
                                    ArtistId = request.Album.ArtistId,
                                    AlbumId = request.Album.Id
                                });
                            }
                            else
                            {
                                if (!oldRel.ArtistId.Equals(request.Album.ArtistId))
                                {
                                    // 更改艺人 删除原关联
                                    MusicStore.DeleteAll(oldRel);
                                    // 创建新关联
                                    MusicStore.AddAll(new RelArtistAlbum
                                    {
                                        Id = Guid.NewGuid().ToString(),
                                        ArtistId = request.Album.ArtistId,
                                        AlbumId = request.Album.Id
                                    });
                                }
                            }
                            
                        }
                        MusicStore.UpdateAll(entity);
                    }
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

            if (request == null)
            {
                request = new PageSearchRequest
                {
                    PageIndex = 0,
                    PageSize = 10
                };
            }

            try
            {
                var query = MusicStore.Set<Song>().AsQueryable();

                if (!string.IsNullOrWhiteSpace(request.Keyword))
                {
                    query = query.Where(a => a.Name.Contains(request.Keyword));
                }
                if (request.Ids != null && request.Ids.Count > 0)
                {
                    query = query.Where(a => a.Id != null && request.Ids.Contains(a.Id));
                }
                if(request.Playlists != null && request.Playlists.Count > 0)
                {
                    var plIds = request.Playlists.Select(a => a.Id).ToList();
                    var sIds = MusicStore.Set<RelPlayListSong>().Where(a => a.PlayListId != null && plIds.Contains(a.PlayListId)).Select(a => a.SongId).ToList();
                    query = query.Where(a => sIds.Contains(a.Id));
                }

                var songs = query.Skip(request.PageIndex * request.PageSize).Take(request.PageSize).ToList();
                foreach (var song in songs)
                {
                    // 根据歌曲ID找专辑ID，根据专辑ID找专辑名称
                    var relSongAlbum = MusicStore.Set<RelSongAlbum>().Where(a => a.SongId.Equals(song.Id)).SingleOrDefault();
                    if (relSongAlbum != null)
                    {
                        song.AlbumName = MusicStore.Set<Album>().Find(relSongAlbum.AlbumId)?.Name;
                        // 根据专辑ID找艺人ID，根据艺人ID找艺人名称
                        var rel = MusicStore.Set<RelArtistAlbum>().Where(a => a.AlbumId.Equals(relSongAlbum.AlbumId)).SingleOrDefault();
                        if (rel != null)
                        {
                            song.ArtistName = MusicStore.Set<Artist>().Find(rel.ArtistId).Name;
                        }
                    }
                }

                response.Data = songs;
                response.PageSize = request.PageSize;
                response.PageIndex = request.PageIndex;
                response.TotalCount = query.Count();
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
        public ResponseMessage SongDelete([FromForm]CommonRequest request)
        {
            Console.WriteLine($"[{nameof(SongDelete)}] 歌曲 删除 开始\r\n请求体：{JsonUtil.ToJson(request)}");
            var response = new ResponseMessage();

            try
            {
                var songIds = request.Songs.Select(a => a.Id).ToList();
                MusicStore.DeleteAll(MusicStore.Set<Song>().Where(a => songIds.Contains(a.Id)).ToArray());
            }
            catch(Exception e)
            {
                Console.WriteLine($"[{nameof(SongDelete)}] 歌曲 删除 失败\r\n请求体：{JsonUtil.ToJson(request)}\r\n错误：{e.ToString()}");
            }
            return response;
        }

        // 保存音乐文件
        private async Task<SongFile> SaveSongFile(IFormFile file, Song song)
        {
            // audio - song  / indexs - json
            Console.WriteLine("File: " + JsonUtil.ToJson(file));
            // 存到files/audio/song下
            var root = FileServerConfig.Root;
            var fileId = Guid.NewGuid().ToString();
            var songFile = new SongFile
            {
                Id = fileId,
                SongId = song.Id,
                ContentType = file.ContentType,
                Path = System.IO.Path.Combine(root.LocalPath, $"audio\\song\\{fileId}{System.IO.Path.GetExtension(file.FileName)}"),
                Url = root.Url + $"/audio/song/{fileId}{System.IO.Path.GetExtension(file.FileName)}" 
            };
            // 保存
            string dir = System.IO.Path.GetDirectoryName(songFile.Path);
            try
            {
                System.IO.Directory.CreateDirectory(dir);
            }
            catch (Exception e)
            {
                Console.WriteLine("创建目录失败：{0}\r\n{1}", dir, e.ToString());
                // Logger.Warn("创建目录失败：{0}\r\n{1}", dir, e.ToString());
                return null;
            }
            // 保存文件
            using (System.IO.FileStream fs = new System.IO.FileStream(songFile.Path, System.IO.FileMode.Create))
            {
                await file.OpenReadStream().CopyToAsync(fs);
                // 添加描述文件
                WS.IO.File.WriteAllText(System.IO.Path.Combine(root.LocalPath, $"Indexs\\{fileId}.json"), WS.Text.JsonUtil.ToJson(songFile));
                // 保存进数据库
                MusicStore.AddAll(songFile);
            }
            return songFile;
        }

        /// <summary>
        /// 歌曲信息保存
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("song/save")]
        public ResponseMessage SongSave([FromForm]CommonRequest request)
        {
            Console.WriteLine($"[{nameof(SongSave)}] 歌曲 信息 保存 开始\r\n请求体：{JsonUtil.ToJson(request)}");
            Console.WriteLine($"Request Form: ${JsonUtil.ToJson(Request.Form)}");
            var response = new ResponseMessage();

            try
            {
                // 根据name属性获取值
                if (string.IsNullOrWhiteSpace(request.Song.Id))
                {
                    request.Song.Id = Guid.NewGuid().ToString();
                    MusicStore.AddAll(request.Song);
                }
                else
                {
                    MusicStore.UpdateAll(request.Song);
                    var entity = MusicStore.Find<Song>(a => a.Id.Equals(request.Song.Id)).SingleOrDefault();
                    if (entity != null)
                    {
                        entity.Name = request.Song.Name;
                        entity.ArtistName = request.Song.ArtistName;
                        entity.Description = request.Song.Description;
                        entity.ReleaseTime = request.Song.ReleaseTime;
                        entity.Url = request.Song.Url;
                        // audio - song
                        MusicStore.UpdateAll(entity);
                    }
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
