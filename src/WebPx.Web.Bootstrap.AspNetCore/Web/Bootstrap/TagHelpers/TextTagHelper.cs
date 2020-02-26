using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using WebPx.Web.TagHelpers;

namespace WebPx.Web.Bootstrap.TagHelpers
{
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    [HtmlTargetElement("bs:text", ParentTag = "bs:prepend")]
    [HtmlTargetElement("bs:text", ParentTag = "bs:append")]
    public class TextTagHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var style = (IGroupStyle)context.Items[typeof(IGroupStyle)];
            output.TagName = "div";
            output.SetDefaultClass(style.TextClass);
        }
    }
}
