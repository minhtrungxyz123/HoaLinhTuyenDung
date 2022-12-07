using Microsoft.AspNetCore.Http;

namespace TuyenDung.Base
{
    public static class HttpExtensions
    {
        public static string GetValue(this HttpRequest request, string key)
        {
            string value = string.Empty;

            if (request.HasFormContentType && request.Form != null && request.Form.Any())
                value = request.Form[key];
            if (value.IsEmpty())
                value = request.Query[key];
            if (value.IsEmpty())
                value = request.Cookies[key];
            if (value.IsEmpty())
                value = request.HttpContext.GetServerVariable(key);

            return value ?? string.Empty;
        }

        public static bool IsEmpty(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }
    }
}