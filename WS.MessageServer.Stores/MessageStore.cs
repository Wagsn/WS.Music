using System;
using System.Collections.Generic;
using System.Text;
using WS.Core;

namespace WS.MessageServer.Stores
{
    public class MessageStore: StoreBase, IMessageStore
    {
        public MessageStore(MessageDbContext context) : base(context) { }
    }
}
