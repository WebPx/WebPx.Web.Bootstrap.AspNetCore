using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace WebPx.Web.Bootstrap.AspNetCore.DemoSite.Pages
{
    public class NavPage
    {
        public NavPage()
        {

        }

        public string Caption { get; set; }

        public string Page { get; set; }

        public string Area { get; set; }
    }

    public interface INavPageService : ICollection<NavPage>
    {

    }

    public class NavPageService : Collection<NavPage>, INavPageService
    {

    }
}
