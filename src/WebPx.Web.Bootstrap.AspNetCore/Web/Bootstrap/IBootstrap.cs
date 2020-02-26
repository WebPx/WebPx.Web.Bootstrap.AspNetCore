using Microsoft.AspNetCore.Html;

namespace WebPx.Web.Bootstrap
{
    public interface IBootstrap
    {
        IHtmlContent Icon(string iconClass);
        IHtmlContent PageButton(string caption, string page, string pageHandler = null, string fragment = null, string host = null, string protocol = null, string area = null, object routeValues = null);
        IHtmlContent ActionButton(string caption, string action, string controller = null, string fragment = null, string host = null, string protocol = null, string area = null, object routeValues = null);
        IHtmlContent RouteButton(string caption, string route, string fragment = null, string host = null, string protocol = null, string area = null, object routeValues = null);
        IHtmlContent Button(string caption, string name = null, string value = null);
    }

    //interface IBootstrapFactory
    //{
    //    BootstrapGenerator GetBootstrapGenerator();
    //}

    //class BoostrapFactory : IBootstrapFactory
    //{
    //    public BoostrapFactory()
    //    {

    //    }

    //    public BootstrapGenerator GetBootstrapGenerator()
    //    {
            
    //    }
    //}
}