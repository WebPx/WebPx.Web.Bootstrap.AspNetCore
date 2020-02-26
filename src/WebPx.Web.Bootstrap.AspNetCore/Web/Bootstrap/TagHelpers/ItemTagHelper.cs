using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Options;
using WebPx.Web.Bootstrap.Configuration;
using WebPx.Web.TagHelpers;

namespace WebPx.Web.Bootstrap.TagHelpers
{
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    [HtmlTargetElement("bs:item", ParentTag = "bs:dropdown")]
    public class ItemTagHelper : ButtonTagHelper    
    {
        public ItemTagHelper(AdapterResolver resolver, IOptions<BootstrapSettings> options, IHtmlGenerator generator, IBootstrap bootstrap) : base(resolver, options, generator, bootstrap)
        {

        }

        public bool BeginGroup { get; set; }

        public bool Disabled { get; set; }

        public bool Active { get; set; }

        public override string Page { get => base.Page; set => base.Page = value; }

        protected override void ApplySkin(TagHelperContext context, TagHelperOutput output)
        {
            //base.ApplySkin(context, output);
        }

        protected override void DoProcess(TagHelperContext context, TagHelperOutput output)
        {
            output.SetDefaultClass("dropdown-item");
            if (Active)
                output.AppendClass("active");
            if (Disabled)
                output.AppendClass("disabled");
            base.DoProcess(context, output);
            //output.TagName = "div";
            if (BeginGroup)
                output.PreElement.AppendHtml($"<div class=\"dropdown-divider\"></div>\r\n");
        }
    }
}
