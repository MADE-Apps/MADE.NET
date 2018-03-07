#if WINDOWS_UWP
// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Extensions.Controls.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines a collection of extensions for UWP controls.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MADE.UI.Controls
{
    using System;

    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    /// <summary>
    /// Defines a collection of extensions for UWP controls.
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Sets the visibility of the given control by the given visible boolean value.
        /// </summary>
        /// <param name="view">
        /// The control to update visibility.
        /// </param>
        /// <param name="isVisible">
        /// A value indicating whether the control is visible.
        /// </param>
        public static void SetVisible(this UIElement view, bool isVisible)
        {
            SetVisible(view, isVisible, false);
        }

        /// <summary>
        /// Sets the visibility of the given control by the given visible boolean value.
        /// If the control can contain child control, these will also be updated if the given parameter is set to true.
        /// </summary>
        /// <param name="view">
        /// The control to update visibility.
        /// </param>
        /// <param name="isVisible">
        /// A value indicating whether the control is visible.
        /// </param>
        /// <param name="updateChildViews">
        /// A value indicating whether to update the control's child visibility states.
        /// </param>
        public static void SetVisible(this UIElement view, bool isVisible, bool updateChildViews)
        {
            if (view == null)
            {
                return;
            }

            view.Visibility = isVisible ? Visibility.Visible : Visibility.Collapsed;

            if (!updateChildViews || !(view is Panel panel))
            {
                return;
            }

            foreach (UIElement child in panel.Children)
            {
                try
                {
                    child?.SetVisible(isVisible, true);
                }
                catch (Exception)
                {
                    // Ignored
                }
            }
        }
    }
}
#endif