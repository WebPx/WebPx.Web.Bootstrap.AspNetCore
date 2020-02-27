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
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    [HtmlTargetElement("bs:button")]
    public class ButtonTagHelper : BootstrapTagHelper<ButtonOptions>
    {
        private const string AreaAttributeName = "asp-area";
        internal const string ActionAttributeName = "asp-action";
        internal const string RouteAttributeName = "asp-route";
        internal const string ControllerAttributeName = "asp-controller";
        internal const string PageHandlerAttributeName = "asp-page-handler";
        internal const string PageAttributeName = "asp-page";
        private const string ProtocolAttributeName = "asp-protocol";
        private const string HostAttributeName = "asp-host";
        private const string FragmentAttributeName = "asp-fragment";
        private const string RouteValuesDictionaryName = "asp-all-route-data";
        private const string RouteValuesPrefix = "asp-route-";

        private readonly IBootstrapGenerator _boostrapGenerator;

        public ButtonTagHelper(AdapterResolver resolver, IOptions<BootstrapSettings> options, IHtmlGenerator generator, IBootstrap bootstrap) : base(resolver, options, x => x?.Button)
        {
            Settings = ButtonOptions.Default;
            Generator = generator;
            _boostrapGenerator = (IBootstrapGenerator)bootstrap;
        }

        /// <summary>
        /// The name of the action method.
        /// </summary>
        /// <remarks>
        /// Must be <c>null</c> if <see cref="Route"/> or <see cref="Page"/> is non-<c>null</c>.
        /// </remarks>
        [HtmlAttributeName(ActionAttributeName)]
        public string Action { get; set; }

        /// <summary>
        /// The name of the area.
        /// </summary>
        /// <remarks>
        /// Must be <c>null</c> if <see cref="Route"/> is non-<c>null</c>.
        /// </remarks>
        [HtmlAttributeName(AreaAttributeName)]
        public string Area { get; set; }

        [HtmlAttributeName(PageAttributeName)]
        public virtual string Page { get; set; }

        /// <summary>
        /// The name of the controller.
        /// </summary>
        /// <remarks>
        /// Must be <c>null</c> if <see cref="Route"/> or <see cref="Page"/> is non-<c>null</c>.
        /// </remarks>
        [HtmlAttributeName(ControllerAttributeName)]
        public string Controller { get; set; }

        /// <summary>
        /// Name of the route.
        /// </summary>
        /// <remarks>
        /// Must be <c>null</c> if one of <see cref="Action"/>, <see cref="Controller"/>, <see cref="Area"/> 
        /// or <see cref="Page"/> is non-<c>null</c>.
        /// </remarks>
        [HtmlAttributeName(RouteAttributeName)]
        public string Route { get; set; }

        /// <summary>
        /// The name of the page handler.
        /// </summary>
        /// <remarks>
        /// Must be <c>null</c> if <see cref="Route"/> or <see cref="Action"/>, or <see cref="Controller"/>
        /// is non-<c>null</c>.
        /// </remarks>
        [HtmlAttributeName(PageHandlerAttributeName)]
        public string PageHandler { get; set; }

        // <summary>
        /// The protocol for the URL, such as &quot;http&quot; or &quot;https&quot;.
        /// </summary>
        [HtmlAttributeName(ProtocolAttributeName)]
        public string Protocol { get; set; }

        /// <summary>
        /// The host name.
        /// </summary>
        [HtmlAttributeName(HostAttributeName)]
        public string Host { get; set; }

        /// <summary>
        /// The URL fragment name.
        /// </summary>
        [HtmlAttributeName(FragmentAttributeName)]
        public string Fragment { get; set; }

        private IHtmlGenerator Generator { get; }

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        private IDictionary<string, string> _routeValues;

        public string Url { get; set; }

        public string Icon { get; set; }

        public IconLocation IconLocation { get; set; }

        /// <summary>
        /// Additional parameters for the route.
        /// </summary>
        [HtmlAttributeName(RouteValuesDictionaryName, DictionaryAttributePrefix = RouteValuesPrefix)]
        public IDictionary<string, string> RouteValues
        {
            get
            {
                if (_routeValues == null)
                {
                    _routeValues = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                }

                return _routeValues;
            }
            set
            {
                _routeValues = value;
            }
        }

        public ControlSize Size { get; set; }

        public string SmallClass { get => Settings.SmallClass; set => Settings.SmallClass = value; }

        public string LargeClass { get => Settings.LargeClass; set => Settings.LargeClass = value; }

        public string BlockClass { get => Settings.BlockClass; set => Settings.BlockClass = value; }

        public string ActiveClass { get => Settings.ActiveClass; set => Settings.ActiveClass = value; }

        public bool Block { get; set; }

        public bool Active { get; set; }

        public bool Toggle { get; set; }

        public bool Pressed { get; set; }

        protected override void DoProcess(TagHelperContext context, TagHelperOutput output)
        {
            base.DoProcess(context, output);
            _boostrapGenerator.Button(output, null, BuildRouteValues(), Url, Route, Controller, Action, Page, PageHandler, Area, Fragment, Host, Protocol, null);
            if (!string.IsNullOrEmpty(Icon))
                _boostrapGenerator.Icon(output, Icon, IconLocation);
            if (Size != ControlSize.Normal)
                switch (Size)
                {
                    case ControlSize.Small: output.AppendClass(this.SmallClass); break;
                    case ControlSize.Large: output.AppendClass(this.LargeClass); break;
                }
            if (Block)
                output.AppendClass(this.BlockClass);
            if (Active)
                output.AppendClass(this.ActiveClass);
            if (context.AllAttributes.ContainsName("disabled"))
                output.AppendClass("disabled");
            if (Toggle)
            {
                output.Attributes.Add("data-toggle", "button");
                output.Attributes.Add("aria-pressed", Pressed.ToString().ToLower());
            }
        }

        private RouteValueDictionary BuildRouteValues()
        {
            var values = new List<KeyValuePair<string, object>>();
            if (_routeValues != null)
                foreach (var val in _routeValues)
                    values.Add(new KeyValuePair<string, object>(val.Key, val.Value));
            return new RouteValueDictionary(values.ToArray());
        }
    }
}
