using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text;
using TuyenDung.Common.XBaseModel;

namespace TuyenDung.WebFramework.Controllers
{
    public static class ModelExtensions
    {
        public static void BindRequest(this SearchModel model, DataSourceRequest request)
        {
            model.PageIndex = request.Page;
            model.PageSize = request.PageSize;
        }

        public static string GetErrorsToHtml<T>(this XBaseResult<T> result)
            where T : class
        {
            var errorMessages = result.errors.SelectMany(x => x.Value);

            return GetErrorsToHtml(errorMessages, result.message);
        }

        public static string GetErrorsToHtml(this ModelStateDictionary modelState, string message = null)
        {
            var errorMessages = modelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage);

            return GetErrorsToHtml(errorMessages, message);
        }

        public static string GetErrorsToHtml(this IEnumerable<string> errorMessages, string message = null)
        {
            var sbErrors = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(message))
                sbErrors.Append($"<span>{message}</span>");

            if (errorMessages != null && errorMessages.Any())
            {
                sbErrors.Append("<ul>");
                foreach (var errorMessage in errorMessages)
                {
                    sbErrors.Append($"<li>{errorMessage}</li>");
                }
                sbErrors.Append("</ul>");
            }

            return sbErrors.ToString();
        }
    }
}