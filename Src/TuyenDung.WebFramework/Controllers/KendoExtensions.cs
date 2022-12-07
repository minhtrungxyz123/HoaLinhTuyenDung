using Kendo.Mvc.UI.Fluent;
using System;
using System.Collections.Generic;
using System.Text;

namespace TuyenDung.WebFramework.Controllers
{
    public static class KendoExtensions
    {
        public static GridTemplateColumnBuilder<T> XBaseSelect<T>(this GridColumnFactory<T> columnFactory) where T : class
        {
            return columnFactory
                .Template("<input type=\"checkbox\" class=\"row-checkbox\" />")
                .ClientHeaderTemplate("<input type=\"checkbox\" class=\"check-all\" />")
                .HeaderHtmlAttributes(new { @class = "check-all" })
                .Width(30).Centered();
        }

        public static GridBoundColumnBuilder<T> Centered<T>(this GridBoundColumnBuilder<T> columnBuilder) where T : class
        {
            return columnBuilder.HtmlAttributes(new { align = "center" }).HeaderHtmlAttributes(new { style = "text-align:center;" });
        }
        public static GridTemplateColumnBuilder<T> Centered<T>(this GridTemplateColumnBuilder<T> columnBuilder) where T : class
        {
            return columnBuilder.HtmlAttributes(new { align = "center" }).HeaderHtmlAttributes(new { style = "text-align:center;" });
        }
        public static GridBoundColumnBuilder<T> RightAlign<T>(this GridBoundColumnBuilder<T> columnBuilder) where T : class
        {
            return columnBuilder.HtmlAttributes(new { style = "text-align:right;" }).HeaderHtmlAttributes(new { style = "text-align:right;" });
        }
        public static GridTemplateColumnBuilder<T> RightAlign<T>(this GridTemplateColumnBuilder<T> columnBuilder) where T : class
        {
            return columnBuilder.HtmlAttributes(new { style = "text-align:right;" }).HeaderHtmlAttributes(new { style = "text-align:right;" });
        }

        #region DataSource

        #endregion
    }
}
