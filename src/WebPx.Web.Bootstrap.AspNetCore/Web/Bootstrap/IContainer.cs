using Microsoft.AspNetCore.Razor.TagHelpers;

namespace WebPx.Web.Bootstrap
{
    public interface IContainer : IContainerStyle
    {
        bool HasHeader { get; set; }

        string Caption { get; }
    }

    public interface IContainerStyle
    {
        string Class { get;  }

        string HeaderClass { get; }

        string BodyClass { get; }

        string FooterClass { get; }
    }
}
