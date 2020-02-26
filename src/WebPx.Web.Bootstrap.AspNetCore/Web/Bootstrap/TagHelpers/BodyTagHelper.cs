using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;
using WebPx.Web.TagHelpers;

namespace WebPx.Web.Bootstrap.TagHelpers
{
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    [HtmlTargetElement("bs:body", ParentTag = "bs:card")]
    public class BodyTagHelper : TagHelper
    {
        public BodyTagHelper()
        {

        }

        public bool Render { get; set; } = true;

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if (!Render)
            {
                var content = await output.GetChildContentAsync();
                output.Content.AppendHtml(content);
                return;
            }
            var container = (IContainer)context.Items[typeof(IContainer)];
            output.SetDefaultClass(container.BodyClass);
            output.TagName = "div";
        }
    }
}
