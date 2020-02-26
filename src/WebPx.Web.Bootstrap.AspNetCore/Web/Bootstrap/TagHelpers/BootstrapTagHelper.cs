using System;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Options;
using WebPx.Web.Bootstrap.Configuration;
using WebPx.Web.TagHelpers;

namespace WebPx.Web.Bootstrap.TagHelpers
{
    public abstract class BootstrapTagHelper<TOptions> : ThemeableTagHelper<TOptions>
        where TOptions: Skin
    {
        private readonly BootstrapSettings bootstrapSettings;

        protected BootstrapTagHelper(AdapterResolver resolver, IOptions<BootstrapSettings> options, Func<BootstrapSettings, TOptions[]> func) : base(resolver)
        {
            this.bootstrapSettings = options.Value;
            base.PropFunc = () => func?.Invoke(bootstrapSettings);
        }
    }
}
