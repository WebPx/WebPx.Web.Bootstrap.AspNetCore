using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace WebPx.Web.Bootstrap.TagHelpers
{
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    [HtmlTargetElement("bs:icon")]
    public class IconTagHelper : TagHelper
    {
        public IconTagHelper()
        {

        }

        public string Icon { get; set; }
        public IconLocation Location { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "i";
            output.TagMode = TagMode.StartTagAndEndTag;
        }
    }

    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    [HtmlTargetElement("a", Attributes = "bs-icon")]
    [HtmlTargetElement("span", Attributes = "bs-icon")]
    [HtmlTargetElement("div", Attributes = "bs-icon")]
    [HtmlTargetElement("button", Attributes = "bs-icon")]
    public class IconExtenderTagHelper : TagHelper
    {
        public IconExtenderTagHelper()
        {

        }

        public string BsIcon { get; set; }
        public IconLocation BsIconLocation { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            switch (BsIconLocation)
            {
                case IconLocation.Near:
                    output.PreContent.AppendHtml($"<i class=\"{BsIcon}\"></i> ");
                    break;
                case IconLocation.Far:
                    output.PostContent.AppendHtml($" <i class=\"{BsIcon}\"></i>");
                    break;
            }
        }
    }

    public enum IconLocation
    {
        Near,
        Far
    }
}
