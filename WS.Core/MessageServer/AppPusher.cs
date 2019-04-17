using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WS.Core.Dto;

namespace WS.Core.MessageServer
{
    public class AppPusher : IMessageSender
    {
        public string Code => "AppPusher";

        public string Name => "App消息推送";

        public Task<ResponseMessage<SendRecordReturn>> Send(SendMessageRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
