// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OrientationExtensions.Android.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines a collection of extensions for handling control orientations in Android applications.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

#if __ANDROID__
namespace MADE.App.Views.Extensions
{
    /// <summary>
    /// Defines a collection of extensions for handling control orientations in Android applications.
    /// </summary>
    public static partial class OrientationExtensions
    {
        /// <summary>
        /// Sets the orientation property of the given LinearLayout by the given Orientation option.
        /// </summary>
        /// <param name="layout">
        /// The linear layout to update orientation.
        /// </param>
        /// <param name="orientation">
        /// The orientation to update with.
        /// </param>
        public static void SetOrientation(
            this Android.Widget.LinearLayout layout,
            XPlat.UI.Controls.Orientation orientation)
        {
            if (layout == null)
            {
                return;
            }

            layout.Orientation = orientation == XPlat.UI.Controls.Orientation.Vertical
                                     ? Android.Widget.Orientation.Vertical
                                     : Android.Widget.Orientation.Horizontal;

            layout.Invalidate();
        }
    }
}
#endif