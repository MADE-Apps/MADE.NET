// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Extensions.UIStackView.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines a collection of extensions for iOS UIStackView controls.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MADE.UI.Controls
{
    using MADE.UI.Layout;

    using UIKit;

    /// <summary>
    /// Defines a collection of extensions for iOS UIStackView controls.
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Sets the current orientation of the given <see cref="UIStackView"/> layout control by the given orientation value.
        /// </summary>
        /// <param name="layout">
        /// The <see cref="UIStackView"/> to update the orientation.
        /// </param>
        /// <param name="orientation">
        /// The orientation to update with.
        /// </param>
        public static void SetOrientation(this UIStackView layout, Orientation orientation)
        {
            if (layout == null)
            {
                return;
            }

            layout.Axis = orientation == Orientation.Vertical ? UILayoutConstraintAxis.Vertical : UILayoutConstraintAxis.Horizontal;
        }
    }
}