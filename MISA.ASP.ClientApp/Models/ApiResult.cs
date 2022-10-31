using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.ASP.ClientApp.Models
{
    internal class BaseResult
    {
        public bool Success { get; set; } = true;
        public string Message { get; set; }
        public string MessageDev { get; set; }

    }

    internal class ApiResult : BaseResult
    {
        public int Code { get; set; }
        public object Data { get; set; }
        public Dictionary<string, object> AdditionalData { get; set; }
        public long TotalCount { get; set; }
    }

    internal class ApiResult<T> : ApiResult
    {
        public new T Data { get; set; }
    }
}
