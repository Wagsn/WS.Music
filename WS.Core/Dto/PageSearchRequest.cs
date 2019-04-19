using System;
using System.Collections.Generic;
using System.Text;

namespace WS.Core
{
    public class PageSearchRequest
    {
        /// <summary>
        /// 从0开始
        /// </summary>
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public string KeyWord { get; set; }
    }
}
