using System;
using System.Collections.Generic;
using System.Text;
using WS.Music.Entities;

namespace WS.Core
{
    /// <summary>
    /// 分页搜索筛选请求
    /// </summary>
    public class PageSearchRequest
    {
        /// <summary>
        /// 从0开始
        /// </summary>
        public int PageIndex { get; set; } = 0;

        public int PageSize { get; set; } = 10;

        public string Keyword { get; set; }

        /// <summary>
        /// ID组
        /// </summary>
        public List<string> Ids { get; set; }

        /// <summary>
        /// 专辑组
        /// </summary>
        public List<Album> Albums { get; set; }

        /// <summary>
        /// 歌单组
        /// </summary>
        public List<Playlist> Playlists { get; set; }

        /// <summary>
        /// 接收人ID集
        /// </summary>
        public List<string> ReceiveUserIds { get; set; }
    }
}
