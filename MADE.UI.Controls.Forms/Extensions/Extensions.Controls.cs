// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Extensions.Controls.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines a collection of extensions for Xamarin.Forms controls.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MADE.UI.Controls.Forms
{
    using System;

    using Xamarin.Forms;

    /// <summary>
    /// Defines a collection of extensions for Xamarin.Forms controls.
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
        public static void SetVisible(this View view, bool isVisible)
        {
            SetVisible<View>(view, isVisible, false);
        }

        /// <summary>
        /// Sets the visibility of the given control by the given visible boolean value.
        /// </summary>
        /// <typeparam name="T">
        /// The type of views that are potentially children of the given view.
        /// </typeparam>
        /// <param name="view">
        /// The control to update visibility.
        /// </param>
        /// <param name="isVisible">
        /// A value indicating whether the control is visible.
        /// </param>
        public static void SetVisible<T>(this View view, bool isVisible)
            where T : View
        {
            SetVisible<T>(view, isVisible, false);
        }

        /// <summary>
        /// Sets the visibility of the given control by the given visible boolean value.
        /// If the control can contain child control, these will also be updated if the given parameter is set to true.
        /// </summary>
        /// <typeparam name="T">
        /// The type of views that are potentially children of the given view.
        /// </typeparam>
        /// <param name="view">
        /// The control to update visibility.
        /// </param>
        /// <param name="isVisible">
        /// A value indicating whether the control is visible.
        /// </param>
        /// <param name="updateChildViews">
        /// A value indicating whether to update the control's child visibility states.
        /// </param>
        public static void SetVisible<T>(this View view, bool isVisible, bool updateChildViews)
            where T : View
        {
            if (view == null)
            {
                return;
            }

            view.IsVisible = isVisible;

            if (!updateChildViews || !(view is Layout<T> panel))
            {
                return;
            }

            foreach (T child in panel.Children)
            {
                try
                {
                    child?.SetVisible<T>(isVisible, true);
                }
                catch (Exception)
                {
                    // Ignored
                }
            }
        }
    }
}