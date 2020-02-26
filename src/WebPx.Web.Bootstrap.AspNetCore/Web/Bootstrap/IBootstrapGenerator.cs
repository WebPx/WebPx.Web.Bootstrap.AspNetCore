using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Routing;
using WebPx.Web.Bootstrap.TagHelpers;

namespace WebPx.Web.Bootstrap
{
    internal interface IBootstrapGenerator : IBootstrap
    {
        IHtmlContent Button(TagHelperOutput output, string caption, RouteValueDictionary routeValues = null, string url = null, string route = null, string controller = null,
            string action = null, string page = null, string pageHandler = null, string area = null, string fragment = null, string host = null, string protocol = null, object htmlAttributes = null);
        IHtmlContent Icon(TagHelperOutput output, string iconClass, IconLocation iconLocation = IconLocation.Near);
        IHtmlContent Icon(TagHelperContent output, string iconClass, IconLocation iconLocation = IconLocation.Near);
    }
}
