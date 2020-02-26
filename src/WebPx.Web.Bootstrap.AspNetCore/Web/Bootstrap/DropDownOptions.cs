using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebPx.Web.Bootstrap
{
    public class DropDownOptions : Skin
    {
        public DropDownOptions()
        {
            Class = "btn btn-secondary";
        }

        public bool Block { get; set; }

        public ControlSize Size { get; set; }

        public string SmallClass { get; set; } = "btn-sm";

        public string LargeClass { get; set; } = "btn-lg";

        public string BlockClass { get; set; } = "btn-block";

        public static DropDownOptions Default { get => new DropDownOptions(); }
    }
}
