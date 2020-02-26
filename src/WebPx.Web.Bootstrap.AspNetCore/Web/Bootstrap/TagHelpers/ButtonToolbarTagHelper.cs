using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Options;
using WebPx.Web.Bootstrap.Configuration;
using WebPx.Web.TagHelpers;

namespace WebPx.Web.Bootstrap.TagHelpers
{
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    [HtmlTargetElement("bs:btn-toolbar")]
    public sealed class ButtonToolbarTagHelper : TagHelper
    {
        private BootstrapSettings _settings;

        public ButtonToolbarTagHelper(IOptions<BootstrapSettings> options)
        {
            _settings = options.Value;
        }

        public string AddClass { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.SetDefaultClass(_settings.ButtonToolbarClass);
            if (!string.IsNullOrEmpty(AddClass))
                output.AppendClass(AddClass);
            output.Attributes.Add("role", "toolbar");
        }
    }
}
