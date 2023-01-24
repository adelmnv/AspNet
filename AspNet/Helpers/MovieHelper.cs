using AspNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspNet.Helpers
{
    public static class MovieHelper
    {
        public static MvcHtmlString CreateList(this HtmlHelper html, string[] items, object htmlattributes = null)
        {
            TagBuilder ul = new TagBuilder("ul");
            foreach (var item in items)
            {
                TagBuilder li = new TagBuilder("li");
                li.SetInnerText(item);
                li.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlattributes));
                ul.InnerHtml += li.ToString();
            }

            return new MvcHtmlString(ul.ToString());
        }
    }
}