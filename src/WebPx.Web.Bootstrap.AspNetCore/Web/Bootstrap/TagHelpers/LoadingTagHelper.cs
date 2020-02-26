using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;
using WebPx.Web.TagHelpers;

namespace WebPx.Web.Bootstrap.TagHelpers
{
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    [HtmlTargetElement("loading")]
    public class LoadingTagHelper : TagHelper
    {

        public string Icon { get; set; } = "fas fa-2x fa-sync-alt fa-spin";
        public bool Dark { get; set; } = false;
        private string LoadingClass { get; set; } = "overlay";
        public string DarkClass { get; set; } = "overlay dark";

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            /*<div class="overlay">
  <i class="fas fa-2x fa-sync-alt fa-spin"></i>
</div>*/
            var extends = context.TagName != "loading";
            var @class = (Dark ? DarkClass : LoadingClass);
            TagHelperContent content = extends ? output.PostContent : output.Content;
            if (!extends)
            {
                output.TagName = "div";
                output.SetDefaultClass(@class);
            }
            if (extends)
                content.AppendHtml($"<div class=\"{@class}\">");
            content.AppendHtml($"<i class=\"{Icon}\"></i>");
            if (extends)
                content.AppendHtml("</div>");
        }
    }
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    [HtmlTargetElement(Attributes = "loading")]
    public class LoadingExtenderTagHelper : TagHelper
    {
        public bool Loading { get; set; }
        public string Icon { get; set; } = "fas fa-2x fa-sync-alt fa-spin";
        public bool LoadingDark { get; set; } = false;
        public string LoadingClass { get; set; } = "overlay";
        public string LoadingDarkClass { get; set; } = "overlay dark";

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            /*<div class="overlay">
  <i class="fas fa-2x fa-sync-alt fa-spin"></i>
</div>*/
            var extends = context.TagName != "loading";

            if (!Loading)
                return;

            var @class = (LoadingDark ? LoadingDarkClass : LoadingClass);
            TagHelperContent content = extends ? output.PostContent : output.Content;
            if (!extends)
            {
                output.TagName = "div";
                output.SetDefaultClass(@class);
            }
            if (extends)
                content.AppendHtml($"<div class=\"{@class}\">");
            content.AppendHtml($"<i class=\"{Icon}\"></i>");
            if (extends)
                content.AppendHtml("</div>");
        }
    }
}
