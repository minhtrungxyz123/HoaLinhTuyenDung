using System.Collections.Generic;
using System.Net;

namespace TuyenDung.Common.XBaseModel
{
    public class ApiResult<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public int TotalCount { get; set; }
        public Dictionary<string, IEnumerable<string>> Errors { get; set; }
        public int httpStatusCode { get; set; }

        public ApiResult()
        {
            httpStatusCode = (int)HttpStatusCode.OK;
        }
    }
}