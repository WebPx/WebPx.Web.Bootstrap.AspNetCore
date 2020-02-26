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
    [HtmlTargetElement("bs:card-group")]
    public class CardGroupTagHelper : TagHelper
    {
        public string AddClass { get; set; }

        public bool IsDeck { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.SetDefaultClass(IsDeck ? "card-deck" : "card-group");
            output.AppendClass(AddClass);
        }
    }
}
