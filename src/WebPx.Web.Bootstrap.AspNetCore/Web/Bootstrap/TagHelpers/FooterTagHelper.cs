using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;
using WebPx.Web.TagHelpers;

namespace WebPx.Web.Bootstrap.TagHelpers
{
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    [HtmlTargetElement("bs:footer")]
    public class FooterTagHelper : TagHelper
    {
        public string AddClass { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            var container = (IContainer)context.Items[typeof(IContainer)];
            output.SetDefaultClass(container.FooterClass);
            output.AppendClass(AddClass);
        }
    }
}
