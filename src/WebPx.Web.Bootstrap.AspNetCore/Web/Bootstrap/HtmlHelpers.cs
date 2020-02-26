using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using WebPx.Web.Bootstrap;
using WebPx.Web.Bootstrap.TagHelpers;
using WebPx.Web.TagHelpers;

//namespace Microsoft.AspNetCore.Mvc.Rendering
//{
//    //public static class BootstrapExtensions
//    //{
//    //    public static IHtmlContent Icon(this IHtmlHelper helper, string iconClass) => Bootstrap.Icon(iconClass);
//    //}
//}

//namespace Microsoft.AspNetCore.Razor.TagHelpers
//{
//    //public static class BootstrapExtensions
//    //{
//    //    public static void Icon(this TagHelperOutput output, string iconClass, IconLocation iconLocation = IconLocation.Near)
//    //    {
//    //        switch (iconLocation)
//    //        {
//    //            case IconLocation.Near:
//    //                output.PreContent.AppendHtml(Bootstrap.Icon(iconClass));
//    //                break;
//    //            case IconLocation.Far:
//    //                output.PostContent.AppendHtml(Bootstrap.Icon(iconClass));
//    //                break;
//    //        }
//    //    }
//    //}
//}

namespace WebPx.Web.Bootstrap
{

    class BootstrapGenerator : IBootstrap, IViewContextAware, IBootstrapGenerator
    {
        private IHtmlGenerator Generator { get; }

        public BootstrapGenerator(IHtmlGenerator generator)
        {
            Generator = generator;
        }

        private ViewContext _viewContext;

        public ViewContext ViewContext
        {
            get
            {
                if (_viewContext == null)
                {
                    throw new InvalidOperationException("HtmlHelper_NotContextualized");
                }

                return _viewContext;
            }
            private set => _viewContext = value;
        }

        public IHtmlContent Icon(string iconClass)
        {
            var tagBuilder = new TagBuilder("i");
            tagBuilder.AddCssClass(iconClass);
            return tagBuilder;
        }

        public IHtmlContent Icon(TagHelperOutput output, string iconClass, IconLocation iconLocation = IconLocation.Near)
        {
            switch (iconLocation)
            {
                case IconLocation.Near:
                    output.PreContent.AppendHtml(Icon(iconClass));
                    break;
                case IconLocation.Far:
                    output.PostContent.AppendHtml(Icon(iconClass));
                    break;
            }
            return output;
        }

        public IHtmlContent Icon(TagHelperContent output, string iconClass, IconLocation iconLocation = IconLocation.Near)
        {
            output.AppendHtml(Icon(iconClass));
            return output;
        }

        private static Task<TagHelperContent> defMethod(bool reuse, HtmlEncoder encoder) => Task.FromResult<TagHelperContent>(new DefaultTagHelperContent());
        
        public IHtmlContent PageButton(string caption, string page, string pageHandler = null, string fragment = null, string host = null, string protocol = null, string area = null, object routeValues = null)
        {
            return Button(CreateOutput(), caption, new RouteValueDictionary(routeValues), null, null, null, null, page, pageHandler, area, fragment, host, protocol);
        }

        public IHtmlContent ActionButton(string caption, string action, string controller = null, string fragment = null, string host = null, string protocol = null, string area = null, object routeValues = null)
        {
            return Button(CreateOutput(), caption, new RouteValueDictionary(routeValues), null, null, controller, action, null, null, area, fragment, host, protocol);
        }

        public IHtmlContent RouteButton(string caption, string route, string fragment = null, string host = null, string protocol = null, string area = null, object routeValues = null)
        {
            return Button(CreateOutput(), caption, new RouteValueDictionary(routeValues), route, null,  null, null, null, null, area, fragment, host, protocol);
        }

        public IHtmlContent LinkButton(string caption, string url, object htmlAttributes = null)
        {
            return Button(CreateOutput(), caption, null, url, null, null, null, null, null, null, null, null, null, htmlAttributes);
        }

        public IHtmlContent Button(string caption, string name = null, string value = null)
        {
            return Button(CreateOutput(), caption, null, null, null, null, null, null, null, null, null, null, null, new { name, value });
        }

        private TagHelperOutput CreateOutput(Func<bool, HtmlEncoder, Task<TagHelperContent>> getChildContentAsync = null) => new TagHelperOutput("div", new TagHelperAttributeList(), getChildContentAsync ?? defMethod);

        public IHtmlContent Button(TagHelperOutput output, string caption, RouteValueDictionary routeValues = null, string url = null, string route = null, string controller = null,
            string action = null, string page = null, string pageHandler = null, string area = null, string fragment = null, string host = null, string protocol = null, object htmlAttributes = null)
        {
            var routeLink = route != null;
            var actionLink = controller != null || action != null;
            var pageLink = page != null || pageHandler != null;
            var linkButton = !string.IsNullOrEmpty(url);

            if ((routeLink && actionLink) || (routeLink && pageLink) || (actionLink && pageLink))
            {
                var message = string.Join(
                    Environment.NewLine, ""
                    /*Resources.FormatCannotDetermineAttributeFor(Href, "<a>")*/,
                    ButtonTagHelper.RouteAttributeName,
                    ButtonTagHelper.ControllerAttributeName + ", " + ButtonTagHelper.ActionAttributeName,
                    ButtonTagHelper.PageAttributeName + ", " + ButtonTagHelper.PageHandlerAttributeName);

                throw new InvalidOperationException(message);
            }
            if (routeLink || actionLink || pageLink || linkButton)
            {
                output.TagName = "a";
                if (linkButton)
                {
                    output.Attributes.Add("href", url);
                    AddHtmlAttributes(output, htmlAttributes);
                    return output;
                }
            }
            else
            {
                output.TagName = "button";
                output.SetDefault("type", "button");
                AddHtmlAttributes(output, htmlAttributes);

                return output;
            }

            RouteValueDictionary _routeValues = null;
            if (routeValues != null && routeValues.Count > 0)
            {
                _routeValues = new RouteValueDictionary(routeValues);
            }

            if (area != null)
            {
                // Unconditionally replace any value from asp-route-area.
                if (_routeValues == null)
                {
                    _routeValues = new RouteValueDictionary();
                }
                _routeValues["area"] = area;
            }

            TagBuilder tagBuilder;
            if (pageLink)
            {
                tagBuilder = Generator.GeneratePageLink(
                    ViewContext,
                    linkText: string.Empty,
                    pageName: page,
                    pageHandler: pageHandler,
                    protocol: protocol,
                    hostname: host,
                    fragment: fragment,
                    routeValues: _routeValues,
                    htmlAttributes: htmlAttributes);
            }
            else if (routeLink)
            {
                tagBuilder = Generator.GenerateRouteLink(
                    ViewContext,
                    linkText: string.Empty,
                    routeName: route,
                    protocol: protocol,
                    hostName: host,
                    fragment: fragment,
                    routeValues: _routeValues,
                    htmlAttributes: htmlAttributes);
            }
            else
            {
                tagBuilder = Generator.GenerateActionLink(
                   ViewContext,
                   linkText: string.Empty,
                   actionName: action,
                   controllerName: controller,
                   protocol: protocol,
                   hostname: host,
                   fragment: fragment,
                   routeValues: _routeValues,
                   htmlAttributes: htmlAttributes);
            }
            output.MergeAttributes(tagBuilder);
            if (!string.IsNullOrEmpty(caption))
                output.Content.AppendHtml(caption);
            return output;
        }

        private static void AddHtmlAttributes(TagHelperOutput output, object htmlAttributes)
        {
            if (htmlAttributes != null)
            {
                var props = TypeDescriptor.GetProperties(htmlAttributes);
                foreach (PropertyDescriptor prop in props)
                {
                    object val = prop.GetValue(htmlAttributes);
                    if (val != null)
                        output.Attributes.SetAttribute(prop.Name, val);
                }
            }
        }

        void IViewContextAware.Contextualize(ViewContext viewContext)
        {
            this.ViewContext = viewContext;
        }
    }
}
