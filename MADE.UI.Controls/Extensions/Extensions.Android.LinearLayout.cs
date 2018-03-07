#if __ANDROID__
// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Extensions.LinearLayout.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines a collection of extensions for the Android LinearLayout.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MADE.UI.Controls
{
    using Android.Widget;

    using Orientation = MADE.UI.Layout.Orientation;

    /// <summary>
    /// Defines a collection of extensions for the Android <see cref="LinearLayout"/>.
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Sets the orientation property of the given <see cref="LinearLayout"/> by the given <see cref="Orientation"/> option.
        /// </summary>
        /// <param name="layout">
        /// The linear layout to update orientation.
        /// </param>
        /// <param name="orientation">
        /// The orientation to update with.
        /// </param>
        public static void SetOrientation(this LinearLayout layout, Orientation orientation)
        {
            if (layout == null)
            {
                return;
            }

            layout.Orientation = orientation == Orientation.Vertical
                                     ? Android.Widget.Orientation.Vertical
                                     : Android.Widget.Orientation.Horizontal;

            layout.Invalidate();
        }
    }
}
#endif