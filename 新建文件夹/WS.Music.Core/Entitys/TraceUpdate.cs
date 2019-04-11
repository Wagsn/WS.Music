using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WS.Music.Entitys
{
    /// <summary>
    /// 可追踪的实体
    /// </summary>
    public class TraceUpdate : ITraceUpdate
    {
        public string _CreateUserId { get; set; }
        public DateTime? _CreateTime { get; set; }
        public string _UpdateUserId { get; set; }
        public DateTime? _UpdateTime { get; set; }
        public string _DeleteUserId { get; set; }
        public DateTime? _DeleteTime { get; set; }
        public bool _IsDeleted { get; set; }
    }
}
