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
    [HtmlTargetElement("bs:card")]
    //[RestrictChildren("bs:body", "bs:header", "bs:footer", "img")]
    public class CardTagHelper : ThemeableTagHelper, IContainer
    {
        public CardTagHelper(AdapterResolver resolver, IOptions<BootstrapSettings> options) : base(resolver)
        {
            bootstrapSettings = options.Value;

            if (!sharedInited)
                SharedInit(bootstrapSettings);
        }

        private static void SharedInit(BootstrapSettings settings)
        {
            sharedInited = true;
            if (settings == null)
                return;
            //if (_defaultSettings == null)
            _defaultSettings = settings.Card.Find("Default");
        }

        private static bool sharedInited = false;
        private readonly BootstrapSettings bootstrapSettings;
        private CardOptions _settings = CardOptions.Default;
        private static CardOptions _defaultSettings = null;

        public string Caption { get; set; }

        public string BodyClass { get => _settings.BodyClass; set => _settings.BodyClass = value; }

        public string HeaderClass { get => _settings.HeaderClass; set => _settings.HeaderClass = value; }

        public string FooterClass { get => _settings.FooterClass; set => _settings.FooterClass = value; }

        public bool IsTable { get; set; }

        bool IContainer.HasHeader { get; set; }

        string IContainerStyle.Class => _settings.Class;

        string IContainerStyle.HeaderClass => _settings.HeaderClass;

        string IContainerStyle.BodyClass => _settings.BodyClass;

        string IContainerStyle.FooterClass => _settings.FooterClass;

        public string AddClass { get; set; }

        public override void Init(TagHelperContext context)
        {
            base.Init(context);
            context.Items[typeof(IContainer)] = this;

            var skinName = Skin;
            if (!string.IsNullOrEmpty(skinName))
            {
                var temp = bootstrapSettings?.Card?.Find(skinName) ?? _defaultSettings;
                if (temp != null)
                    _settings = temp;
            }
            else if (_defaultSettings != null)
                _settings = _defaultSettings;
        }

        protected override async Task DoProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var container = (IContainer)this;
            output.TagName = "div";
            //if (string.IsNullOrEmpty(output.Attributes["class"]?.Value.ToString() ?? null))
            if (!string.IsNullOrEmpty(_defaultSettings?.Class) || !string.IsNullOrEmpty(_settings?.Class))
                output.SetClass(_settings?.Class ?? _defaultSettings.Class);
            var content = await output.GetChildContentAsync();

            if (!container.HasHeader && !string.IsNullOrEmpty(Caption))
            {
                output.PreContent.AppendHtml($"<div class=\"card-header {HeaderClass}\">");
                if (!string.IsNullOrEmpty(Caption))
                    output.PreContent.AppendHtml($"{Caption}");
                //if (container.HeaderTools != null)
                //    output.PreContent.AppendHtml(container.HeaderTools.Content.GetContent());
                output.PreContent.AppendHtml("</div>");
            }
            output.Content.AppendHtml(content);
            output.AppendClass(AddClass);

            //if (!string.IsNullOrEmpty(card.Header) || !string.IsNullOrEmpty(Caption))
            //{
            //    output.PreContent.AppendHtml($"<div class=\"card-header {HeaderClass}\">");
            //    if (!string.IsNullOrEmpty(Caption))
            //        output.PreContent.AppendHtml($"{Caption}");
            //    if (!string.IsNullOrEmpty(card.Header))
            //        output.PreContent.AppendHtml($"{card.Header}");
            //    if (card.HeaderTools != null)
            //        output.PreContent.AppendHtml(card.HeaderTools.Content.GetContent());
            //    output.PreContent.AppendHtml("</div>");
            //}
            //if (!card.Content && !IsTable)
            //    output.PreContent.AppendHtml($"<div class=\"card-body {BodyClass}\">");
            ////card.HeaderTools.SuppressOutput();
            //output.Content.AppendHtml(content);
            //if (!card.Content && !IsTable)
            //    output.PostContent.AppendHtml("</div>");
        }
    }
}
