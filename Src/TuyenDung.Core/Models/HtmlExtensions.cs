using TuyenDung.ModelName.XBaseModel;
using TuyenDung.WebFramework.Controllers;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Linq.Expressions;
using System.Text;
using System.Text.Encodings.Web;

namespace TuyenDung.Core.Models
{
    public static class HtmlExtensions
    {
        public static IHtmlContent Hint(this IHtmlHelper helper, string value)
        {
            // create a
            var a = new TagBuilder("a");
            a.MergeAttribute("href", "#");
            a.MergeAttribute("onclick", "return false;");
            //a.MergeAttribute("rel", "tooltip");
            a.MergeAttribute("title", value);
            a.MergeAttribute("tabindex", "-1");
            a.AddCssClass("hint");

            // Create img
            var img = new TagBuilder("i");
            img.AddCssClass("fa fa-question-circle");

            a.InnerHtml.AppendHtml(img);

            // Render tag
            return new HtmlString(a.ToString());
        }

        public static string FieldNameFor<T, TResult>(this IHtmlHelper<T> html, Expression<Func<T, TResult>> expression)
        {
            var modelExpressionProdiver = html.ViewContext.HttpContext.RequestServices.GetRequiredService<ModelExpressionProvider>();
            return html.ViewData.TemplateInfo.GetFullHtmlFieldName(modelExpressionProdiver.GetExpressionText(expression));
        }

        public static string FieldIdFor<T, TResult>(this IHtmlHelper<T> html, Expression<Func<T, TResult>> expression)
        {
            var modelExpressionProdiver = html.ViewContext.HttpContext.RequestServices.GetRequiredService<ModelExpressionProvider>();
            var id = html.GenerateIdFromName(html.ViewData.TemplateInfo.GetFullHtmlFieldName(modelExpressionProdiver.GetExpressionText(expression)));
            // because "[" and "]" aren't replaced with "_" in GetFullHtmlFieldId
            return id.Replace('[', '_').Replace(']', '_');
        }

        public static IHtmlContent XBaseLabelFor<TModel, TValue>(
            this IHtmlHelper<TModel> helper,
            Expression<Func<TModel, TValue>> expression,
            bool displayHint = true,
            object htmlAttributes = null,
            bool required = false)
        {
            var modelExpressionProdiver = helper.ViewContext.HttpContext.RequestServices.GetRequiredService<ModelExpressionProvider>();
            var modelExpression = modelExpressionProdiver.CreateModelExpression(helper.ViewData, expression);
            var metadata = modelExpression.Metadata;
            object resourceDisplayName = null;
            metadata.AdditionalValues.TryGetValue("XBaseResourceDisplayName", out resourceDisplayName);

            return XBaseLabelFor(helper, expression, resourceDisplayName as XBaseResourceDisplayName, metadata, displayHint, htmlAttributes, required);
        }

        public static IHtmlContent XBaseLabelFor<TModel, TValue>(
            this IHtmlHelper<TModel> helper,
            Expression<Func<TModel, TValue>> expression,
            string resourceKey,
            bool displayHint = true,
            object htmlAttributes = null)
        {
            Guard.ArgumentNotEmpty(() => resourceKey);

            var modelExpressionProdiver = helper.ViewContext.HttpContext.RequestServices.GetRequiredService<ModelExpressionProvider>();
            var modelExpression = modelExpressionProdiver.CreateModelExpression(helper.ViewData, expression);
            var metadata = modelExpression.Metadata;
            var resourceDisplayName = new XBaseResourceDisplayName(resourceKey, metadata.PropertyName);

            return XBaseLabelFor(helper, expression, resourceDisplayName, metadata, displayHint, htmlAttributes);
        }

        private static IHtmlContent XBaseLabelFor<TModel, TValue>(
            this IHtmlHelper<TModel> helper,
            Expression<Func<TModel, TValue>> expression,
            XBaseResourceDisplayName resourceDisplayName,
            ModelMetadata metadata,
            bool displayHint = true,
            object htmlAttributes = null,
            bool required = false)
        {
            var result = new StringBuilder();
            string labelText = null;
            string hint = null;

            if (resourceDisplayName != null)
            {
                // resolve label display name
                labelText = resourceDisplayName.DisplayName.NullEmpty();
                if (labelText == null)
                {
                    // take reskey as absolute fallback
                    labelText = resourceDisplayName.ResourceKey;
                }
            }

            if (labelText == null)
            {
                labelText = metadata.DisplayName.SplitPascalCase();
            }

            var label = helper.LabelFor(expression, labelText, htmlAttributes);
            var nodeLabel = HtmlNode.CreateNode(label.RenderHtmlContent());

            if (!nodeLabel.Attributes.Contains("title"))
                nodeLabel.SetAttributeValue("title", labelText);

            if (!metadata.IsNullableValueType && metadata.ModelType != typeof(Boolean))
            {
                if (required)
                {
                    var nodeRequired = HtmlNode.CreateNode("<span class=\"required\" aria-required=\"true\">*</span>");
                    nodeLabel.AppendChild(nodeRequired);
                }
            }

            result.Append(nodeLabel.OuterHtml);

            if (displayHint)
            {
                if (hint.HasValue())
                {
                    result.Append(helper.Hint(hint).RenderHtmlContent());
                }
            }

            return new HtmlString(result.ToString());
        }

        public static string RenderHtmlContent(this IHtmlContent htmlContent)
        {
            using var writer = new StringWriter();
            htmlContent.WriteTo(writer, HtmlEncoder.Default);
            return writer.ToString();
        }
    }
}