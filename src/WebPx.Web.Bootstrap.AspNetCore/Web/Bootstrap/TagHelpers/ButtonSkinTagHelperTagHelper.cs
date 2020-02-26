using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Options;
using WebPx.Web.Bootstrap.Configuration;
using WebPx.Web.TagHelpers;

namespace WebPx.Web.Bootstrap.TagHelpers
{
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    [HtmlTargetElement("button", Attributes = "skin")]
    [HtmlTargetElement("button", Attributes = "block")]
    [HtmlTargetElement("button", Attributes = "size")]
    [HtmlTargetElement("a", Attributes = "skin")]
    [HtmlTargetElement("a", Attributes = "block")]
    [HtmlTargetElement("a", Attributes = "size")]
    [HtmlTargetElement("input", Attributes = "skin")]
    [HtmlTargetElement("input", Attributes = "block")]
    [HtmlTargetElement("input", Attributes = "size")]
    public class ButtonSkinTagHelper : BootstrapTagHelper<ButtonOptions>
    {
        public ButtonSkinTagHelper(AdapterResolver resolver, IOptions<BootstrapSettings> options) : base(resolver, options, x => x?.Button)
        {
            Settings = ButtonOptions.Default;
        }

        public bool Block { get; set; }

        public ControlSize Size { get; set; }

        protected override string BuildClass(string @class)
        {
            if (!Block && Size == ControlSize.Normal)
                return @class;
            var list = new List<string>();
            if (!string.IsNullOrEmpty(@class))
                list.Add(@class);
            switch (Size)
            {
                case ControlSize.Large: list.Add(Settings.LargeClass); break;
                case ControlSize.Small: list.Add(Settings.SmallClass); break;
            }
            if (Block)
                list.Add(Settings.BlockClass);
            return string.Join(" ", list);
        }

        protected override void DoProcess(TagHelperContext context, TagHelperOutput output)
        {
            switch (context.TagName)
            {
                case "a":
                case "button":
                    break;
                case "input":
                    var type = context.AllAttributes["type"]?.Value.ToString().ToLower();
                    switch (type)
                    {
                        case "button":
                        case "submit":
                        case "reset":
                            break;
                        default:
                            return;
                    }
                    break;
            }
            base.DoProcess(context, output);
        }
    }
}
