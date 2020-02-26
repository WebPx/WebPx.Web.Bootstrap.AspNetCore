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
    [HtmlTargetElement("bs:dropdown")]
    public class DropDownTagHelper : BootstrapTagHelper<DropDownOptions>
    {
        private readonly BootstrapSettings _bootstrapSettings;
        private readonly IBootstrapGenerator _boostrapGenerator;

        public DropDownTagHelper(AdapterResolver resolver, IOptions<BootstrapSettings> options, IBootstrap bootstrap) : base(resolver, options, x => x?.DropDown)
        {
            Settings = DropDownOptions.Default;
            _bootstrapSettings = options.Value;
            _boostrapGenerator = (IBootstrapGenerator)bootstrap;
        }

        public bool AsLink { get; set; }

        public string AddClass { get; set; }

        public string Caption { get; set; }

        public string Icon { get; set; }

        public Direction Direction { get; set; } = Direction.Down;

        public Alignment Alignment { get; set; } = Alignment.Left;

        public IconLocation IconLocation { get; set; }

        public bool Split { get; set; }

        public string SplitSrLabel { get; set; } = "Toggle Dropdown";

        //public string DropDownSkin { get; set; }

        public ControlSize Size { get; set; }

        protected override void ApplySkin(TagHelperContext context, TagHelperOutput output)
        {
            //base.ApplySkin(context, output);
        }

        protected override void DoProcess(TagHelperContext context, TagHelperOutput output)
        {
            base.DoProcess(context, output);

            var idAtt = context.AllAttributes["id"];
            var id = idAtt?.Value ?? context.UniqueId;
            if (idAtt!=null)
                output.Attributes.Remove(idAtt);

            var wrap = Direction != Direction.Down || Split;
            string wrapClass = null;
            if (wrap)
                switch (Direction)
                {
                    case Direction.Up: wrapClass = "dropup"; break;
                    case Direction.Left: wrapClass = "dropleft"; break;
                    case Direction.Right: wrapClass = "dropright"; break;
                }

            var classes = new List<string>
            {
                DefaultSettings?.Class ?? Settings?.Class ?? context.AllAttributes["class"]?.Value.ToString(),
            };
            if (!Split)
                classes.Add("dropdown-toggle");

            switch (Size)
            {
                case ControlSize.Small: classes.Add(Settings.SmallClass); break;
                case ControlSize.Large: classes.Add(Settings.LargeClass); break;
            }

            var isLeft = Split && Direction == Direction.Left;

            var btnClass = string.Join(" ", classes); 
            var buttonTag = AsLink ? "a" : "button";
            //_boostrapGenerator.Button(output, Caption, null, null, null, null, null, null, null, null, null, null, null);
            if (wrap)
            {
                if (isLeft)
                {
                    output.PreElement.AppendHtml($"<div class=\"btn-group\"><div class=\"btn-group {wrapClass}\" role=\"group\">");
                    output.PostElement.AppendHtml("</div>");
                }
                else
                    output.PreElement.AppendHtml($"<div class=\"btn-group {wrapClass}\">");
            }

            var buttonTarget = isLeft ? output.PostElement : output.PreElement;
            //var buttonTarget = isRight ? output.PostElement : output.PreElement;

            buttonTarget.AppendHtml($"<{buttonTag}");
            if (AsLink)
                buttonTarget.AppendHtml(" href=\"#\"");
            buttonTarget.AppendHtml($" class=\"{btnClass}\"");
            buttonTarget.AppendHtml($" id=\"{id}\"");
            if (!Split)
                buttonTarget.AppendHtml($" data-toggle=\"dropdown\" aria-haspopup=\"true\" aria-expanded=\"false\"");
            buttonTarget.AppendHtml(">");

            buttonTarget.AppendHtml(Caption);
            buttonTarget.AppendHtml($"</{buttonTag}>\r\n");

            if (Split)
            {
                classes.Add("dropdown-toggle");
                classes.Add("dropdown-toggle-split");

                output.PreElement.AppendHtml($"<{buttonTag}");
                if (AsLink)
                    output.PreElement.AppendHtml(" href=\"#\"");
                output.PreElement.AppendHtml($" class=\"{string.Join(" ", classes)}\"");
                output.PreElement.AppendHtml($" id=\"{id}\"");
                output.PreElement.AppendHtml($" data-toggle=\"dropdown\" aria-haspopup=\"true\" aria-expanded=\"false\"");
                output.PreElement.AppendHtml("><span class=\"sr-only\">");
                output.PreElement.AppendHtml(SplitSrLabel);
                output.PreElement.AppendHtml($"</span></{buttonTag}>\r\n");
            }

            if (!string.IsNullOrEmpty(Icon) && IconLocation == IconLocation.Near)
                _boostrapGenerator.Icon(output.PreElement, Icon, IconLocation);

            if (wrap)
                output.PostElement.AppendHtml("</div>");

            if (!string.IsNullOrEmpty(Icon) && IconLocation == IconLocation.Far)
                _boostrapGenerator.Icon(output.PreElement, Icon, IconLocation);

            output.TagName = "div";
            output.SetDefaultClass("dropdown-menu");

            if (Alignment == Alignment.Right)
                output.AppendClass("dropdown-menu-right");

            if (!string.IsNullOrEmpty(this.AddClass))
                output.AppendClass(AddClass);
            output.Attributes.Add("aria-labelledby", id);
        }
    }
}
