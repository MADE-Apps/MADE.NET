using System;
using System.Threading;
using MADE.UI.Design.Styling;
using UIKit;

namespace MADE.UI.Controls.Design.Styling
{
    public static class DefaultHeaderedTextBlockStyle
    {
        private static readonly Lazy<UILabelStyle> LazyDefaultHeaderStyle =
            new Lazy<UILabelStyle>(GetDefaultHeaderStyle, LazyThreadSafetyMode.PublicationOnly);

        private static readonly Lazy<UILabelStyle> LazyDefaultTextStyle =
    new Lazy<UILabelStyle>(GetDefaultTextStyle, LazyThreadSafetyMode.PublicationOnly);


        public static UILabelStyle DefaultHeaderStyle => LazyDefaultHeaderStyle.Value;

        public static UILabelStyle DefaultTextStyle => LazyDefaultTextStyle.Value;

        private static UILabelStyle GetDefaultHeaderStyle()
        {
            return new UILabelStyle
            {
                ForegroundColor = UIColor.Blue
            };
        }

        private static UILabelStyle GetDefaultTextStyle()
        {
            return new UILabelStyle
            {
                ForegroundColor = UIColor.Black
            };
        }
    }
}