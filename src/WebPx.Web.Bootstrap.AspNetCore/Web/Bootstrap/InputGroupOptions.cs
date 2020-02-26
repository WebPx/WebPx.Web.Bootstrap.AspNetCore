using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebPx.Web.Bootstrap
{
    public class InputGroupOptions : IGroupStyle
    {
        public InputGroupOptions()
        {

        }

        public string Class { get; set; } = "input-group";
        public string TextClass { get; set; } = "input-group-text";
        public string PrependClass { get; set; } = "input-group-prepend";
        public string AppendClass { get; set; } = "input-group-append";
        public string LargeClass { get; set; } = "input-group-lg";
        public string SmallClass { get; set; } = "input-group-sm";

    }
}
