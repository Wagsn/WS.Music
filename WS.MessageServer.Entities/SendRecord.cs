namespace WS.MessageServer.Entities
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

        /// <summary>
        /// 接收人类型（1：私人、2：群组、3：主页、4：广场？合并成组织）
        /// </summary>
        public int ReceiveUserType { get; set; }

        public enum ReceiveUserTypeEnum
        {
            Private = 1,
            Group = 2,
            Home = 3,
            Square = 4
        }

        /// <summary>
        /// 消息ID
        /// </summary>
        public string MessageId { get; set; }

        /// <summary>
        /// 当前状态ID
        /// </summary>
        public string StatusId { get; set; }

        /// <summary>
        /// 当前状态
        /// </summary>
        public int Status { get; set; }
    }
}
