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
        public int PageIndex { get; set; }

        public int PageSize { get; set; } = 10;

        public string KeyWord { get; set; }

        /// <summary>
        /// ID组
        /// </summary>
        public List<string> Ids { get; set; }

        /// <summary>
        /// 专辑组
        /// </summary>
        public List<Album> Albums { get; set; }
    }
}
