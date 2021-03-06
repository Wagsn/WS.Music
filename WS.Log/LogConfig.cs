﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WS.Log
{
    /// <summary>
    /// 日志管理配置文件，输出路径（./cfg/config.json#"WSLog": {}）
    /// </summary
    public class LogConfig
    {
        /// <summary>
        /// 是否输出日志
        /// </summary>
        public bool IsLog { get; set; }

        /// <summary>
        /// 日志文件输出路径模板（"./log/${LoggerName}/${Date}.log"）
        /// ("./log/${LoggerName}/${LogLevel}/${Date}.log")
        /// </summary>
        public string LogOutTemplate { get; set; }

        /// <summary>
        /// 错误日志文件输出路径模板（"./log/${LoggerName}/error/${Date}.log"）
        /// </summary>
        public string ErrOutTemplate { get; set; }

        /// <summary>
        /// 致命错误输出路径（"./log/${LoggerName}/fatal/${Date}.log"）
        /// </summary>
        public string FatalOutTemplate { get; set; }

        /// <summary>
        /// 时间格式
        /// </summary>
        public string TimeFormat { get; set; }

        /// <summary>
        /// 文件名模板
        /// </summary>
        public string FileNameFormat { get; set; }

        /// <summary>
        /// 日志项模板
        /// "[${DateTime}] [${LoggerLevel}] [${LoggerName}] ${Message}" -> "[2018-11-18 17:15.452154+8:00] [Trace] [TodoController] logging content"
        /// </summary>
        public string LogItemFormat { get; set; }

        /// <summary>
        /// 保存文件天数
        /// </summary>
        public int SaveDays { get; set; }

        /// <summary>
        /// 输出日志层级（低于该层级的将不会输出）
        /// </summary>
        public LogLevels LogLevels { get; set; }

        /// <summary>
        /// 是否异步
        /// </summary>
        public bool IsAsync { get; set; }

        /// <summary>
        /// 档案文件最大限制
        /// </summary>
        public int MaxArchiveFiles { get; set; }

        /// <summary>
        /// 单个文件最大限制
        /// </summary>
        public string MaxFileSize { get; set; }


        /// <summary>
        /// 档案文件模板
        /// </summary>
        public string ArchiveFileTemplate { get; set; }

        /// <summary>
        /// 档案文件路径
        /// </summary>
        public string ArchiveBaseDir { get; set; }

        /// <summary>
        /// 档案文件写入权限码
        /// </summary>
        public string ArchiveWriteMode { get; set; }

        /// <summary>
        /// 档案文件
        /// </summary>
        public string ArchiveEvery { get; set; }

        /// <summary>
        /// 档案文件是否压缩
        /// </summary>
        public bool? ZipArchiveFile { get; set; }
    }
}
