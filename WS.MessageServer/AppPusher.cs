using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
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
                var dic = new Dictionary<string, object>();
                foreach(var item in message.KeyValues)
                {
                    dic[item.Key] = item.Value;
                }
                var messageRecord = new MessageRecord
                {
                    Id = Guid.NewGuid().ToString(),
                    Code = 1,
                    Content = message.Content?? Text.EL.Parse(message.Template, dic, "{{", "}}")
                };
                message.ReceiveUserIds.ForEach(id =>
                {
                    sends.Add(new SendRecord
                    {
                        Id = Guid.NewGuid().ToString(),
                        SendUserId = request.SendUserId,
                        ReceiveUserId = id,
                        ReceiveUserType = 1,
                        MessageId = "",
                        StatusId = "",
                        Status = 1
                    });
                });
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
