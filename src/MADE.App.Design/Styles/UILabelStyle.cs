// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UILabelStyle.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines the style model for an iOS UILabel.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

#if __IOS__
namespace MADE.App.Design.Styles
{
    using UIKit;

    using XPlat.UI;

    /// <summary>
    /// Defines the style model for an iOS <see cref="UILabel"/>.
    /// </summary>
    public class UILabelStyle : BaseStyle<UILabel>
    {
        /// <summary>
        /// Gets or sets the foreground color (color of the text) of the view.
        /// </summary>
        public Color ForegroundColor { get; set; }

        /// <summary>
        /// Gets or sets the alignment of the text in the view.
        /// </summary>
        public UITextAlignment TextAlignment { get; set; }

        /// <summary>
        /// Applies the properties of the style to the given <see cref="UILabel"/>.
        /// </summary>
        /// <param name="view">
        /// The view to apply the style to.
        /// </param>
        public override void Apply(UILabel view)
        {
            base.Apply(view);

            if (this.ForegroundColor != null)
            {
                view.TextColor = this.ForegroundColor;
            }

            view.TextAlignment = this.TextAlignment;
        }
    }
}
#endif