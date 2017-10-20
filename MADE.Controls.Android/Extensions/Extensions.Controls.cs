// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Extensions.Controls.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines a collection of extensions for Android controls.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MADE.Controls
{
    using Android.Views;

    /// <summary>
    /// Defines a collection of extensions for Android controls.
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Sets the visibility of the given control by the given visible boolean value.
        /// </summary>
        /// <param name="control">
        /// The control to update visibility.
        /// </param>
        /// <param name="isVisible">
        /// A value indicating whether the control is visible.
        /// </param>
        public static void SetVisible(this View control, bool isVisible)
        {
            if (control != null)
            {
                control.Visibility = isVisible ? ViewStates.Visible : ViewStates.Gone;
            }
        }
    }
}