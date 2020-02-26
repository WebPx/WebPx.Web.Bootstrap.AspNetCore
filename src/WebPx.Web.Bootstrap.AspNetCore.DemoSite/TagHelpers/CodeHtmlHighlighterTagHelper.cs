using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace WebPx.Web.Bootstrap.AspNetCore.DemoSite.TagHelpers
{
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    [HtmlTargetElement("code", Attributes = "html")]
    public class CodeHtmlHighlighterTagHelper : TagHelper
    {
        public bool Html { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var content = await output.GetChildContentAsync();
            var doc = new HtmlDocument();
            doc.LoadHtml(content.GetContent());

            var sb = new StringBuilder();
            //sb.Append(content.GetContent());

            ProcessNode(sb, doc.DocumentNode);
            
            output.Content.SetHtmlContent(sb.ToString());
        }

        private readonly string opentag = HttpUtility.HtmlEncode("<");
        private readonly string closetag = HttpUtility.HtmlEncode("</");
        private readonly string close = HttpUtility.HtmlEncode(">");
        private readonly string selfclose = HttpUtility.HtmlEncode("/>");

        private void ProcessNode(StringBuilder sb, HtmlNode node)
        {
            switch (node.NodeType)
            {
                case HtmlNodeType.Element:
                    switch (node.Name)
                    {
                        case "#text":
                            sb.Append("$" + node.InnerText);
                            break;
                        default:
                            ProcessElement(sb, node);
                            break;
                    }
                    break;
                case HtmlNodeType.Text: sb.Append(node.InnerText); break;
                case HtmlNodeType.Document: foreach (var childNode in node.ChildNodes) ProcessNode(sb, childNode); break;
                default:
                    sb.Append("**"+node.NodeType);break;
            }
        }

        private void ProcessElement(StringBuilder sb, HtmlNode node)
        {
            var hasChild = node.ChildNodes.Count > 0;
            var hasAttributes = node.Attributes.Count > 0;
            sb.Append($"<span class=\"nt\">{opentag}{node.Name}");
            if (hasAttributes)
            {
                sb.Append($"</span>");
                foreach (var attribute in node.Attributes)
                {
                    var value = attribute.Value;
                    if (string.IsNullOrEmpty(value))
                    {
                        sb.Append($" <span class=\"na\">{attribute.Name}</span>");
                    }
                    else
                    {
                        sb.Append($" <span class=\"na\">{attribute.Name}=</span>");
                        sb.Append($"<span class=\"s\">\"{attribute.Value}\"</span>");
                    }
                }
                sb.Append($"<span class=\"nt\">{close}</span>");
            }
            else if (hasChild)
                sb.Append($"{close}</span>");
            else
                sb.Append($"{selfclose}</span>");

            if (hasChild)
            {
                foreach (var childNode in node.ChildNodes) 
                    ProcessNode(sb, childNode);
                sb.Append($"<span class=\"nt\">{closetag}{node.Name}");
                sb.Append($"{close}</span></span>");
            }
            //else
            //{
            //    if (hasAttributes)
            //        sb.Append($"<span class=\"nt\">");
            //    if (hasAttributes)
            //        sb.Append($" /></span>");
            //}
        }
    }

    //internal static class SBExt
    //{
    //    public static void AddHtmlTag(this StringBuilder sb, string tagName)
    //    {

    //    }
    //}
}
