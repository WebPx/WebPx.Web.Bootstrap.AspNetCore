using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebPx.Web.Bootstrap
{
    public sealed class ButtonOptions : Skin
    {
        public ButtonOptions()
        {
            Class = "btn btn-link";
        }

        public bool Block { get; set; }

        public ControlSize Size { get; set; }

        public string SmallClass { get; set; } = "btn-sm";

        public string LargeClass { get; set; } = "btn-lg";

        public string BlockClass { get; set; } = "btn-block";

        public string ActiveClass { get; set; } = "active";

        public static ButtonOptions Default { get => new ButtonOptions(); }
    }

    public static class ButtonSkins
    {
        public static ButtonOptions Secondary { get => new ButtonOptions() { Name = "Secondary", Class = "btn btn-secondary" }; }
        public static ButtonOptions Dark { get => new ButtonOptions() { Name = "Dark", Class = "btn btn-dark" }; }
    }
}
