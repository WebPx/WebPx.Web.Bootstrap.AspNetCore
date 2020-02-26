using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebPx.Web.Bootstrap
{
    public interface IGroupStyle
    {
        string Class { get; set; }
        string TextClass { get; set; }
        string PrependClass { get; set; }
        string AppendClass { get; set; }
        string LargeClass { get; set; }
        string SmallClass { get; set; }
    }
}
