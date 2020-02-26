using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Options;
using WebPx.Web.Bootstrap.Configuration;
using WebPx.Web.Bootstrap.TagHelpers;
using WebPx.Web.TagHelpers;

namespace WebPx.Web.Bootstrap.TagHelpers
{
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    [HtmlTargetElement("bs:btn-group")]
    public sealed class ButtonGroupTagHelper : TagHelper
    {
        private BootstrapSettings _settings;

        public ButtonGroupTagHelper(IOptions<BootstrapSettings> options)
        {
            _settings = options.Value;
        }

        //public string AriaLabel { get; set; }

        public string AddClass { get; set; }

        public ControlSize Size { get; set; }

        public Orientation Orientation { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.SetDefaultClass(Orientation == Orientation.Horizontal ? _settings.ButtonGroupClass : _settings.VerticalButtonGroupClass);
            output.Attributes.Add("role", "group");
            switch (Size)
            {
                case ControlSize.Large: output.AppendClass("btn-group-lg"); break;
                case ControlSize.Small: output.AppendClass("btn-group-sm"); break;
            }
               
            if (!string.IsNullOrEmpty(AddClass))
                output.AppendClass(AddClass);
            //var arialLabel = this.AriaLabel;
            //if (!string.IsNullOrEmpty(AriaLabel))
            //    output.Attributes.Add("aria-label", arialLabel);
        }
    }
}
