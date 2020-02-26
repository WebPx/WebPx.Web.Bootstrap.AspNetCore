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
    [HtmlTargetElement("bs:badge")]
    [HtmlTargetElement("a", Attributes = "badge")]
    public class BadgeTagHelper : BootstrapTagHelper<BadgeOptions>
    {
        public BadgeTagHelper(AdapterResolver resolver, IOptions<BootstrapSettings> options) : base(resolver, options, x => x?.Badge)
        {
            Settings = BadgeOptions.Default;
        }

        public bool Pill { get => Settings.Pill ?? false; set => Settings.Pill = value; }

        public string PillClass { get => Settings.PillClass; set => Settings.PillClass = value; }

        protected override string BuildClass(string @class)
        {
            if (Settings.Pill ?? Pill)
                return $"{@class} {(DefaultSettings ?? Settings)?.PillClass}";
            else
                return @class;
        }

        [HtmlAttributeName("sr-description")]
        public string ScreenReaderDescription { get; set; }

        protected override void DoProcess(TagHelperContext context, TagHelperOutput output)
        {
            if (context.TagName == "bs:badge")
                output.TagName = "span";
            else
                output.Attributes.Remove(output.Attributes["badge"]);
            base.DoProcess(context, output);

            if (!string.IsNullOrEmpty(ScreenReaderDescription))
                output.PostElement.AppendHtml($"<span class=\"sr-only\">{ScreenReaderDescription}</span>");
        }
    }
}
