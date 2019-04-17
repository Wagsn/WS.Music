using System;
using System.Collections.Generic;
using System.Text;

namespace WS.Core.MessageServer
{
    /// <summary>
    /// 消息记录
    /// </summary>
    public class MessageRecord
    {
        /// <summary>
        /// 消息记录ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 正文
        /// </summary>
        public string Content { get; set; }
    }
}
