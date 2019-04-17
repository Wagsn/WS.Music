using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WS.Core.Dto;

namespace WS.Core.MessageServer
{
    /// <summary>
    /// 消息发送器接口
    /// </summary>
    public interface IMessageSender
    {
        /// <summary>
        /// 发送器的唯一码
        /// </summary>
        string Code { get; }

        /// <summary>
        /// 发送器名称
        /// </summary>
        string Name { get; }

        ///// <summary>
        ///// 权重
        ///// </summary>
        //int Order { get; }

        /// <summary>
        /// 发送（系统内发送就是在数据库添加一条数据，外部发送将调用第三方接口）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ResponseMessage<SendRecordReturn>> Send(SendMessageRequest request);
    }
}
