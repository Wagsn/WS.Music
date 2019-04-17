using System;
using System.Collections.Generic;
using System.Text;

namespace WS.Core.MessageServer
{
    /// <summary>
    /// 发送记录
    /// </summary>
    public class SendRecord
    {
        /// <summary>
        /// 消息记录ID
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 发送人ID
        /// </summary>
        public string SendUserId { get; set; }

        /// <summary>
        /// 接收人ID
        /// </summary>
        public string ReceiveUserId { get; set; }
    }
}
