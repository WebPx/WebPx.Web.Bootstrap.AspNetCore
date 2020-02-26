using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebPx.Web.Bootstrap
{
    public class CardOptions : Skin, IContainerStyle
    {
        public CardOptions()
        {
            Class = "card";
        }

        public string HeaderClass { get; set; } = "card-header";

        public string BodyClass { get; set; } = "card-body";

        public string FooterClass { get; set; } = "card-footer";

        public static CardOptions Default => new CardOptions()
        {
            Name = "Default"
        };
    }
}
