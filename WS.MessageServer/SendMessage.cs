using System;
using System.Collections.Generic;
using System.Text;

namespace WS.MessageServer
{
    /// <summary>
    /// 单个消息
    /// </summary>
    public class SendMessage
    {
        public string SendUserId { get; set; }

        public string ReceiveUserId { get; set; }

        /// <summary>
        /// 消息内容
        /// </summary>
        public string Content { get; set; }
    }
}
