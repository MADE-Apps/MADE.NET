// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OrientationExtensions.Windows.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines a collection of extensions for handling control orientations in Windows applications.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

#if WINDOWS_UWP
namespace MADE.App.Views.Extensions
{
    using Windows.UI.Xaml.Controls;

    using Orientation = XPlat.UI.Controls.Orientation;

    /// <summary>
    /// Defines a collection of extensions for handling control orientations in Windows applications.
    /// </summary>
    public static partial class OrientationExtensions
    {
        /// <summary>
        /// Sets the current orientation of the given <see cref="StackPanel"/> layout control by the given orientation value.
        /// </summary>
        /// <param name="layout">
        /// The <see cref="StackPanel"/> to update the orientation.
        /// </param>
        /// <param name="orientation">
        /// The orientation to update with.
        /// </param>
        public static void SetOrientation(this StackPanel layout, Orientation orientation)
        {
            if (layout == null)
            {
                return;
            }

            layout.Orientation = orientation == Orientation.Vertical ? Windows.UI.Xaml.Controls.Orientation.Vertical : Windows.UI.Xaml.Controls.Orientation.Horizontal;
        }
    }
}
#endif