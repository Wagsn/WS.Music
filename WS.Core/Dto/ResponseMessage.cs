using System;
using System.Collections.Generic;
using System.Text;

namespace WS.Core
{
    public class ResponseMessage
    {
        public string Code { get; set; }
        public string Message { get; set; }

        public ResponseMessage()
        {
            Code = ResponseCodeDefines.SuccessCode;
        }

        public bool IsSuccess() => Code == ResponseCodeDefines.SuccessCode;
        
    }

    public class ResponseMessage<TData> : ResponseMessage
    {
        public TData Extension { get; set; }
    }

    public class PagingResponseMessage<TEntity> : ResponseMessage<List<TEntity>>
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public long TotalCount { get; set; }
    }
}
