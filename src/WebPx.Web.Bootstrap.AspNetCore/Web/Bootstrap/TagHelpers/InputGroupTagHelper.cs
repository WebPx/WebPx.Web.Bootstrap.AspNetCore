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
    [HtmlTargetElement("bs:input-group")]
    public class InputGroupTagHelper : TagHelper, IGroupStyle
    {
        private BootstrapSettings _settings;

        public InputGroupTagHelper(IOptions<Configuration.BootstrapSettings> options)
        {
            _settings = options.Value;
        }

        private InputGroupOptions _skin = new InputGroupOptions();

        public string Class { get => _skin.Class; set => _skin.Class = value; }
        public string TextClass { get => _skin.TextClass; set => _skin.TextClass = value; }
        public string PrependClass { get => _skin.PrependClass; set => _skin.PrependClass = value; }
        public string AppendClass { get => _skin.AppendClass; set => _skin.AppendClass = value; }
        public string LargeClass { get => _skin.LargeClass; set => _skin.LargeClass = value; }
        public string SmallClass { get => _skin.SmallClass; set => _skin.SmallClass = value; }

        public ControlSize Size { get; set; }

        public string AddClass { get; set; }

        public override void Init(TagHelperContext context)
        {
            _skin = _settings?.InputGroup ?? _skin;
            base.Init(context);
            context.Items[typeof(IGroupStyle)] = this;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.SetDefaultClass(Class);
            if (!string.IsNullOrEmpty(AddClass))
                output.AppendClass(AddClass);
            switch (Size)
            {
                case ControlSize.Large: output.AppendClass(LargeClass); break;
                case ControlSize.Small: output.AppendClass(SmallClass); break;
            }
        }
    }
}
