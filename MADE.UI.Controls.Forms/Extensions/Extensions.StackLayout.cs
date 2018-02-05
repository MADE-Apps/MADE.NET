// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Extensions.StackLayout.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines a collection of extensions for Xamarin.Forms StackLayout controls.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MADE.UI.Controls.Forms
{
    using MADE.UI.Layout;

    using Xamarin.Forms;

    /// <summary>
    /// Defines a collection of extensions for Xamarin.Forms StackLayout controls.
    /// </summary>
    public static partial class Extensions
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

            layout.Orientation = orientation.ToStackOrientation();
        }
    }
}