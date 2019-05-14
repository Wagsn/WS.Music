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

        public AppPusher(MessageDbContext context)
        {
            Context = context;
        }

        public MessageDbContext Context { get; set; }

        /// <summary>
        /// 发送实现
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ResponseMessage<SendRecordReturn>> Send(SendMessageRequest request)
        {
            var response = new ResponseMessage<SendRecordReturn>();

            // 发送记录
            var sends = new List<SendRecord>();
            var messages = new List<MessageRecord>();

            foreach(var message in request.Messages)
            {
                var dic = new Dictionary<string, object>();

                // 如果该消息是模板类型
                if(message.Content != null)
                {
                    var messageRecord = new MessageRecord
                    {
                        Id = Guid.NewGuid().ToString(),
                        Code = (int)MessageRecord.CodeEnum.Private,
                        Content = message.Content
                    };
                    messages.Add(messageRecord);
                    message.ReceiveUserIds.ForEach(id =>
                    {
                        sends.Add(new SendRecord
                        {
                            Id = Guid.NewGuid().ToString(),
                            SendUserId = request.SendUserId,
                            ReceiveUserId = id,
                            // 这个外部传入
                            ReceiveUserType = (int)SendRecord.ReceiveUserTypeEnum.Private,
                            MessageId = messageRecord.Id,
                            //StatusId = "",
                            //Status = 1
                        });
                    });
                }
                if(message.Template != null && message.KeyValues != null)
                {
                    foreach (var item in message.KeyValues)
                    {
                        dic[item.Key] = item.Value;
                    }
                    var messageRecord = new MessageRecord
                    {
                        Id = Guid.NewGuid().ToString(),
                        Code = (int)MessageRecord.CodeEnum.Private,
                        Content = Text.EL.Parse(message.Template, dic, "{{", "}}")
                    };
                    messages.Add(messageRecord);
                    message.ReceiveUserIds.ForEach(id =>
                    {
                        sends.Add(new SendRecord
                        {
                            Id = Guid.NewGuid().ToString(),
                            SendUserId = request.SendUserId,
                            ReceiveUserId = id,
                            ReceiveUserType = (int)MessageRecord.CodeEnum.Private,
                            MessageId = messageRecord.Id,
                            //StatusId = "",
                            //Status = 1
                        });
                    });
                }
            }

            Context.AddRange(messages);
            Context.AddRange(sends);

            await Context.SaveChangesAsync();

            return response;
        }
    }
}
