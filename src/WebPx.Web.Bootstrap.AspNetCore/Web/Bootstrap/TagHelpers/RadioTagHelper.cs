using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Options;
using WebPx.Web.Bootstrap.Configuration;
using WebPx.Web.TagHelpers;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace WebPx.Web.Bootstrap.TagHelpers
{
    [HtmlTargetElement("bs:radio", ParentTag = "bs:btn-group")]
    public sealed class RadioTagHelper : TagHelper
    {
        public RadioTagHelper(IHtmlGenerator htmlGenerator)
        {
            this.HtmlGenerator = htmlGenerator;
        }

        [ViewContext]
        public ViewContext ViewContext { get; set; }

        IHtmlGenerator HtmlGenerator { get; }

        public string Skin { get; set; }

        public bool Checked { get; set; }

        public ModelExpression AspFor { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var options = ButtonSkins.Secondary;

            if (!string.IsNullOrEmpty(Skin))
                if (context.Items[typeof(IButtonGroupContext)] is IButtonGroupContext ibgc)
                {
                    options = ibgc.Settings.Button.Find(this.Skin) ?? options;
                }

            string id = null;
            string name = null;
            string value = null;
            if (context.AllAttributes.ContainsName("id"))
            {
                var idAtt = context.AllAttributes["id"];
                id = idAtt?.Value?.ToString();
                output.Attributes.Remove(idAtt);
            }
            if (context.AllAttributes.ContainsName("name"))
            {
                var nameAtt = context.AllAttributes["name"];
                name = nameAtt?.Value?.ToString();
                output.Attributes.Remove(nameAtt);
            }
            if (context.AllAttributes.ContainsName("value"))
            {
                var valueAtt = context.AllAttributes["value"];
                value = valueAtt?.Value?.ToString();
                output.Attributes.Remove(valueAtt);
            }

            //base.Process(context, output);
            output.TagName = "label";
            output.SetClass(options.Class);
            if (Checked)
                output.AppendClass(options.ActiveClass);
            var @checked = false;
            if (AspFor?.Model is bool)
                @checked = (bool)AspFor.Model;
            if (AspFor != null)
                output.PreContent.AppendHtml(HtmlGenerator.GenerateRadioButton(this.ViewContext, AspFor?.ModelExplorer, AspFor?.Name, AspFor?.Model, @checked, null));
            else
            {
                output.PreContent.AppendHtml("<input type=\"radio\"");
                if (id != null)
                    output.PreContent.AppendHtml($" id=\"{name}\"");
                if (name != null)
                    output.PreContent.AppendHtml($" name=\"{name}\"");
                if (value != null)
                    output.PreContent.AppendHtml($" value=\"{value}\"");
                if (Checked)
                    output.PreContent.AppendHtml(" checked");
                output.PreContent.AppendHtml(" />");
            }
        }
    }
}
