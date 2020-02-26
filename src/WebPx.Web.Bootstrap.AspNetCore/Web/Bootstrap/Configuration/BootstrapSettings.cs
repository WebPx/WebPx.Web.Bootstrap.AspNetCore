using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebPx.Web.Bootstrap.Configuration
{
    public class BootstrapSettings
    {
        public BootstrapSettings()
        {

        }

        public string ButtonGroupClass { get; set; } = "btn-group";
        public string ButtonToolbarClass { get; set; } = "btn-toolbar";

        public string VerticalButtonGroupClass { get; set; } = "btn-group-vertical";

        public AlertOptions[] Alert { get; set; }
        public BadgeOptions[] Badge { get; set; }
        public CardOptions[] Card { get; set; }
        public ButtonOptions[] Button { get; set; }
        public DropDownOptions[] DropDown { get; set; }

        public InputGroupOptions InputGroup { get; set; }
        public ProgressOptions[] Progress { get; set; }
    }

    public static class BootstrapSettingsExtensions
    {
        public static TOption Find<TOption>(this TOption[] options, string name = "Default")
            where TOption : Skin
        {
            foreach (var option in options)
                if (string.Equals(option.Name, name, StringComparison.OrdinalIgnoreCase))
                    return option;
            return null;
        }
    }
}
