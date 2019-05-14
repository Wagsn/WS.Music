namespace WS.MessageServer.Entities
{
    /// <summary>
    /// 消息记录（简单的消息，复杂的消息会封装成一个JSON放在Content里面）
    /// </summary>
    public class MessageRecord
    {
        /// <summary>
        /// 消息记录ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 消息类型码（1：私信，2：广场，3：群组）
        /// </summary>
        public int Code { get; set; }

        public enum CodeEnum
        {
            Private = 1,
            Square = 2,
            Group = 3
        }

        /// <summary>
        /// 正文（字符串与JSON对象）
        /// </summary>
        public string Content { get; set; }
    }
}
