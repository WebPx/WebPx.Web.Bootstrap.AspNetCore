using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebPx.Web.Bootstrap
{
    public class BadgeOptions : Skin, IMergeable<BadgeOptions>
    {
        public BadgeOptions()
        {
            Class = "badge badge-primary";
        }

        public bool? Pill { get; set; }

        public string PillClass { get; set; } = "badge-pill";

        public static BadgeOptions Default => new BadgeOptions()
        {
            Name = "Default"
        };

        void IMergeable<BadgeOptions>.Merge(BadgeOptions instance)
        {
            if (instance.Pill.HasValue)
                this.Pill = instance.Pill.Value;
            this.Class = instance.Class ?? this.Class;
            this.PillClass = instance.PillClass ?? this.PillClass;
        }
    }
}
