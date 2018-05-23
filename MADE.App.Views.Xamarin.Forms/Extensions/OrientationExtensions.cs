// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OrientationExtensions.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines a collection of extensions for Xamarin.Forms orientation supported controls.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MADE.App.Views.Extensions
{
    using MADE.App.Views.Layout;

    using Xamarin.Forms;

    /// <summary>
    /// Defines a collection of extensions for Xamarin.Forms orientation supported controls.
    /// </summary>
    public static class OrientationExtensions
    {
        /// <summary>
        /// Sets the current orientation of the given <see cref="StackLayout"/> layout control by the given orientation value.
        /// </summary>
        /// <param name="layout">
        /// The <see cref="StackLayout"/> to update the orientation.
        /// </param>
        /// <param name="orientation">
        /// The orientation to update with.
        /// </param>
        public static void SetOrientation(this StackLayout layout, Orientation orientation)
        {
            if (layout == null)
            {
                return;
            }

            layout.Orientation = orientation == Orientation.Vertical
                                     ? StackOrientation.Vertical
                                     : StackOrientation.Horizontal;
        }
    }
}