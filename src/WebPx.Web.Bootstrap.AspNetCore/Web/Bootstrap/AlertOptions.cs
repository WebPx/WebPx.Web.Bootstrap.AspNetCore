using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebPx.Web.Bootstrap
{
    public class AlertOptions : Skin, IMergeable<AlertOptions>
    {
        public AlertOptions()
        {
            Class = "alert alert-primary";
        }

        public string DismissableClass { get; set; } = "alert-dismissible";

        public static AlertOptions Default => new AlertOptions()
        {
            Name = "Default"
        };

        public bool? Dismiss { get; internal set; }

        void IMergeable<AlertOptions>.Merge(AlertOptions instance)
        {
            if (instance.Dismiss.HasValue)
                this.Dismiss = instance.Dismiss.Value;
            this.Class = instance.Class ?? this.Class;
            this.DismissableClass = instance.DismissableClass ?? this.DismissableClass;
        }
    }
}
