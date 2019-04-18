using System;

namespace WS.MessageServer.Entities
{
    /// <summary>
    /// 发送状态
    /// </summary>
    public class SendStatus
    {
        /// <summary>
        /// ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 状态码（1：发送中，2：已发送，3：已接收，4：已查看，5：已撤回（在时间内撤回），6：编辑中（一次编辑一次记录，产生新的MessageRecord），7：已删除，8：发送失败）
        /// </summary>
        public int Code { get; set; }
        
        /// <summary>
        /// 时间
        /// </summary>
        public DateTime? Time { get; set; }

        /// <summary>
        /// 发送ID
        /// </summary>
        public string SendId { get; set; }

        ///// <summary>
        ///// 上一步状态ID
        ///// </summary>
        //public string PreStatusId { get; set; }
    }
}
