using System;
using System.Collections.Generic;
using System.Text;

namespace WebPx.Web.Bootstrap
{
    public sealed class ProgressOptions : Skin
    {
        public ProgressOptions()
        {
            Class = "progress";
            BarClass = "progress-bar";
            AnimationClass = "progress-bar-animated";
        }

        public string BarClass { get; set; }

        public string AnimationClass { get; set; }

        public static ProgressOptions Default { get => new ProgressOptions(); }
    }
}
