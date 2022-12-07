using System;
using System.Collections.Generic;
using System.Net;

namespace TuyenDung.Common.XBaseModel
{
    #region XBaseResult

    [Serializable]
    public class XBaseResult<T> where T : class
    {
        public bool success { get; set; }

        public string code { get; set; }

        public int httpStatusCode { get; set; }

        public string title { get; set; }

        public string message { get; set; }

        public T data { get; set; }

        public int totalCount { get; set; }

        public bool isRedirect { get; set; }

        public string redirectUrl { get; set; }

        public Dictionary<string, IEnumerable<string>> errors { get; set; }

        public XBaseResult()
        {
            success = true;
            httpStatusCode = (int)HttpStatusCode.OK;
            errors = new Dictionary<string, IEnumerable<string>>();
        }

        public XBaseResult(XBaseResult<T> obj)
        {
            success = obj.success;
            code = obj.code;
            httpStatusCode = obj.httpStatusCode;
            title = obj.title;
            message = obj.message;
            data = obj.data;
            totalCount = obj.totalCount;
            isRedirect = obj.isRedirect;
            redirectUrl = obj.redirectUrl;
            errors = obj.errors;
        }
    }

    [Serializable]
    public class XBaseResult : XBaseResult<dynamic>
    {
    }

    #endregion XBaseResult
}