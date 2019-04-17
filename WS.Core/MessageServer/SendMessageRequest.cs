using System;
using System.Collections.Generic;
using System.Text;
using WS.Core;

namespace WS.Core.MessageServer
{
    /// <summary>
    /// 消息发送请求（包含批量群发）
    /// </summary>
    public class SendMessageRequest
    {

        /// <summary>
        /// 操作系统类型（Web，App（Android，IOS），微信小程序）
        /// </summary>
        public string OsType { get; set; }

        /// <summary>
        /// 批量群发
        /// </summary>
        public List<MessageItem> Messages { get; set; }
    }

    /// <summary>
    /// 单条消息内容
    /// </summary>
    public class MessageItem
    {
        /// <summary>
        /// 发送用户ID（可以为空：表示未知来源）
        /// </summary>
        public string SendUserId { get; set; }

        /// <summary>
        /// 接收用户ID组
        /// </summary>
        public List<string> ReceiveUserIds { get; set; }

        #region 消息内容

        /// <summary>
        /// 消息内容（没有则采用模板，模板没有则采用消息组，消息组没有则是空消息）
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 消息发送模板（包含占位符："${UserName}死于${Date}"）
        /// </summary>
        public string Template { get; set; }

        /// <summary>
        /// 键值对（用于填充模板：{Date=2019-03-05, UserName=Wagsn}）
        /// </summary>
        public List<KeyValue> KeyValues { get; set; }

        /// <summary>
        /// 消息组
        /// </summary>
        public List<ContentItem> Contents { get; set; }

        #endregion
    }

    /// <summary>
    /// 消息内容
    /// </summary>
    public class ContentItem
    {
        /// <summary>
        /// 消息内容（没有则采用模板，模板没有则为空消息）
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 消息发送模板（包含占位符："${UserName}死于${Date}"）
        /// </summary>
        public string Template { get; set; }

        /// <summary>
        /// 键值对（用于填充模板：{Date=2019-03-05, UserName=Wagsn}）
        /// </summary>
        public List<KeyValue> KeyValues { get; set; }
    }
}