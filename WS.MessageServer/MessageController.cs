﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WS.Core;
using WS.Log;
using WS.MessageServer.Stores;
using WS.Text;

namespace WS.MessageServer
{
    /// <summary>
    /// 消息服务的控制器
    /// </summary>
    [Produces("application/json")]
    [Route("api/message")]
    public class MessageController : Controller
    {
        private readonly ILogger Logger = LoggerManager.GetLogger<MessageController>();

        private readonly IMessageSender Sender;
        private readonly MessageDbContext Context;

        public MessageController(IMessageSender messageSender, MessageDbContext context)
        {
            Sender = messageSender;
            Context = context;
        }

        [HttpGet("check")]
        public IActionResult Check()
        {
            return new JsonResult(new
            {
                Code = "0",
                Message = "Its work!"
            });
        }

        /// <summary>
        /// 消息发送
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("send")]
        public async Task<ResponseMessage> SendMessage([FromForm] SendMessageRequest request)
        {
            Logger.Trace($"[{nameof(SendMessage)}] 发送消息 开始：{JsonUtil.ToJson(request)}");
            var response = new ResponseMessage();

            if(request == null)
            {
                response.Code = ResponseDefine.ArgumentNullError;
                response.Message = "请求体不能为空";
                return response;
            }
            if (string.IsNullOrWhiteSpace(request.OsType))
            {
                response.Code = ResponseDefine.ArgumentNullError;
                response.Message = "系统类型不能为空";
                return response;
            }

            try
            {
                var senResponse = await Sender.Send(request);

                response.Code = senResponse.Code;
                response.Message = senResponse.Message;
            }
            catch(Exception e)
            {
                response.Code = ResponseDefine.ServiceError;
                response.Message = e.Message;
                Logger.Error($"发送消息，服务器发生错误：\r\n{e.ToString()}");
            }
            return response;
        }

        /// <summary>
        /// 获取消息列表
        /// </summary>
        /// <returns></returns>
        public PagingResponseMessage<SendMessage> MessageList(PageSearchRequest request)
        {
            //var query = Context.SendRecord.


            return null;
        }
    }
}
