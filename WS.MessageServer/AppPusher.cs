using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WS.Core;
using WS.MessageServer.Entities;
using WS.MessageServer.Stores;

namespace WS.MessageServer
{
    public class AppPusher : IMessageSender
    {
        public string Code => "AppPusher";

        public string Name => "App消息推送";

        public AppPusher(MessageServerDbContext context)
        {
            Context = context;
        }

        public MessageServerDbContext Context { get; set; }

        public async Task<ResponseMessage<SendRecordReturn>> Send(SendMessageRequest request)
        {
            var response = new ResponseMessage<SendRecordReturn>();



            var sends = new List<SendRecord>();

            foreach(var message in request.Messages)
            {

            }

            Context.Add(new SendRecord
            {
                Id = Guid.NewGuid().ToString(),
                //SendUserId = 
            });

            await Context.SaveChangesAsync();

            return response;
        }
    }
}
