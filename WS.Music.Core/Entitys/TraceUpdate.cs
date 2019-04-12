﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WS.Music.Core.Entitys
{
    /// <summary>
    /// 可追踪的实体
    /// </summary>
    public class TraceUpdate : ITraceUpdate
    {
        [MaxLength(36, ErrorMessage = "GUID最长不超过36")]
        public string _CreateUserId { get; set; }
        public DateTime? _CreateTime { get; set; }
        [MaxLength(36, ErrorMessage = "GUID最长不超过36")]
        public string _UpdateUserId { get; set; }
        public DateTime? _UpdateTime { get; set; }
        [MaxLength(36, ErrorMessage = "GUID最长不超过36")]
        public string _DeleteUserId { get; set; }
        public DateTime? _DeleteTime { get; set; }
        public bool _IsDeleted { get; set; }
    }
}