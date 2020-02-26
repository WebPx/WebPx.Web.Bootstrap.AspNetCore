using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;
using WebPx.Web.TagHelpers;

namespace WebPx.Web.Bootstrap.TagHelpers
{
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    [HtmlTargetElement("bs:header", ParentTag = "bs:card")]
    public class HeaderTagHelper : ThemeableTagHelper
    {
        public HeaderTagHelper(AdapterResolver resolver) : base(resolver)
        {

        }

        protected override async Task DoProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var container = (IContainer)context.Items[typeof(IContainer)];
            if (container != null)
            {
                container.HasHeader = true;
            }

            //card.Header = 
            var content = (await output.GetChildContentAsync());

            output.SetClass(container.HeaderClass);
            output.TagName = "div";
            //output.SuppressOutput();

            var caption = container.Caption;

            if (!string.IsNullOrEmpty(caption))
            {
                output.Content.AppendHtml(caption);
                output.Content.AppendHtml(" ");
                output.Content.AppendHtml(content);
            }
        }
    }
}
