// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultHeaderedTextBlockStyle.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines the default style for the iOS HeaderedTextBlock control.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

#if __IOS__
namespace MADE.App.Views.Styles
{
    using System;
    using System.Threading;

    using MADE.App.Design.Color;
    using MADE.App.Design.Styles;

    using UIKit;

    /// <summary>
    /// Defines the default style for the iOS <see cref="HeaderedTextBlock"/> control.
    /// </summary>
    public static class DefaultHeaderedTextBlockStyle
    {
        private static readonly Lazy<UILabelStyle> LazyDefaultHeaderStyle =
            new Lazy<UILabelStyle>(GetDefaultHeaderStyle, LazyThreadSafetyMode.PublicationOnly);

        private static readonly Lazy<UILabelStyle> LazyDefaultTextStyle =
            new Lazy<UILabelStyle>(GetDefaultTextStyle, LazyThreadSafetyMode.PublicationOnly);

        /// <summary>
        /// Gets the default style associated with the header content.
        /// </summary>
        public static UILabelStyle DefaultHeaderStyle => LazyDefaultHeaderStyle.Value;

        /// <summary>
        /// Gets the default style associated with the text content.
        /// </summary>
        public static UILabelStyle DefaultTextStyle => LazyDefaultTextStyle.Value;

        private static UILabelStyle GetDefaultHeaderStyle()
        {
            return new UILabelStyle { ForegroundColor = new Color("#C41230") };
        }

        private static UILabelStyle GetDefaultTextStyle()
        {
            return new UILabelStyle { ForegroundColor = UIColor.Black };
        }
    }
}
#endif