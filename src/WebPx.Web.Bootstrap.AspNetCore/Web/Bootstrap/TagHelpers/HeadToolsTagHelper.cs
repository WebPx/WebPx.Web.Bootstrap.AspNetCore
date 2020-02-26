using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;
using WebPx.Web.TagHelpers;

namespace WebPx.Web.Bootstrap.TagHelpers
{
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    //[HtmlTargetElement("bs:card-tools", ParentTag = "bs:card")]
    [HtmlTargetElement("bs:head-tools", ParentTag = "bs:header")]
    public class HeadToolsTagHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            //var container = (IContainer)context.Items[typeof(IContainer)];

            //var content = await output.GetChildContentAsync();
            //card.HeaderTools = output;
            //output.SuppressOutput();
            output.TagName = "div";
            output.SetDefaultClass("card-tools");
        }
    }
}
