using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WS.Text;

namespace WS.Core
{
    public class ResponseMessage
    {
        public string Code { get; set; }
        public string Message { get; set; }

        public ResponseMessage()
        {
            Code = ResponseDefine.SuccessCode;
        }

        public bool IsSuccess() => Code == ResponseDefine.SuccessCode;

        /// <summary>
        /// 响应体包装，如果code为自定义，Message则为"其它情况"
        /// </summary>
        /// <param name="code"></param>
        /// <param name="msgAppend"></param>
        public void Wrap<TAppend>([Required]string code, TAppend append)
        {
            Code = code;
            if (!string.IsNullOrWhiteSpace(JsonUtil.ToJson(append)))
            {
                Message += "\r\n" + JsonUtil.ToJson(append);
            }
        }
    }

    public class ResponseMessage<TData> : ResponseMessage
    {
        public TData Data { get; set; }
    }

    public class PagingResponseMessage<TEntity> : ResponseMessage<List<TEntity>>
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public long TotalCount { get; set; }
    }
}
