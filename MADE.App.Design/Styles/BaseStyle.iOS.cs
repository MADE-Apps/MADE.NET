// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseStyle.iOS.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines a base class for creating styles that can be applied to iOS UIView elements.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

#if __IOS__
namespace MADE.App.Design.Styles
{
    using MADE.App.Design.Color;

    using UIKit;

    /// <summary>
    /// Defines a base class for creating styles that can be applied to iOS <see cref="UIView"/> elements.
    /// </summary>
    /// <typeparam name="TView">
    /// The type of <see cref="UIView"/> to apply the style to.
    /// </typeparam>
    public abstract class BaseStyle<TView> where TView : UIView
    {
        /// <summary>
        /// Gets or sets the level of alpha transparency for the view from 0 to 1.
        /// </summary>
        public float? Opacity { get; set; }

        /// <summary>
        /// Gets or sets the background color of the view.
        /// </summary>
        public Color BackgroundColor { get; set; }

        /// <summary>
        /// Applies the properties of the style to the given <see cref="UIView"/>.
        /// </summary>
        /// <param name="view">
        /// The view to apply the style to.
        /// </param>
        public virtual void Apply(TView view)
        {
            if (view == null)
            {
                return;
            }

            if (this.BackgroundColor != null)
            {
                view.BackgroundColor = this.BackgroundColor;
            }

            if (this.Opacity != null && this.Opacity >= 0.0 && this.Opacity <= 1.0)
            {
                view.Alpha = (System.nfloat)this.Opacity;
            }
        }
    }
}
#endif