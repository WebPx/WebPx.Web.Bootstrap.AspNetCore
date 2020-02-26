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
    [HtmlTargetElement("bs:alert")]
    public class AlertTagHelper : BootstrapTagHelper<AlertOptions>
    {
        public AlertTagHelper(AdapterResolver resolver, IOptions<BootstrapSettings> options) : base(resolver, options, x => x?.Alert)
        {
            Settings = AlertOptions.Default;
        }

        public string DismissableClass { get => Settings.DismissableClass; set => Settings.DismissableClass = value; }

        public bool Dismiss { get => Settings.Dismiss ?? false; set => Settings.Dismiss = value; }

        protected override string BuildClass(string @class)
        {
            var dismiss = Dismiss;
            if (!dismiss)
                return base.BuildClass(@class);
            var list = new List<string>();
            if (!string.IsNullOrEmpty(@class))
                list.Add(@class);
            if (dismiss)
                list.Add(Settings.DismissableClass);
            return string.Join(" ", list);
        }

        protected override void DoProcess(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            base.DoProcess(context, output);
            output.Attributes.Add("role", "alert");

            if (Dismiss)
            {
                output.PostContent.AppendHtml("<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\">");
                output.PostContent.AppendHtml("<span aria-hidden=\"true\">&times;</span>");
                output.PostContent.AppendHtml("</button>");
            }
        }
    }
}
