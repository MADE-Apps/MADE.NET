// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ViewExtensions.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines a collection of extensions for application views.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MADE.App.Views.Extensions
{
    /// <summary>
    /// Defines a collection of extensions for application views.
    /// </summary>
    public static class ViewExtensions
    {
#if __ANDROID__
        /// <summary>
        /// Sets the visibility of the given control by the given visible boolean value.
        /// </summary>
        /// <param name="view">
        /// The control to update visibility.
        /// </param>
        /// <param name="isVisible">
        /// A value indicating whether the control is visible.
        /// </param>
        public static void SetVisible(this Android.Views.View view, bool isVisible)
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
        public static void SetVisible(this Android.Views.View view, bool isVisible, bool updateChildViews)
        {
            if (view == null)
            {
                return;
            }

            view.Visibility = isVisible ? Android.Views.ViewStates.Visible : Android.Views.ViewStates.Gone;

            if (!updateChildViews || !(view is Android.Views.ViewGroup viewGroup))
            {
                return;
            }

            for (int i = 0; i < viewGroup.ChildCount; i++)
            {
                try
                {
                    Android.Views.View child = viewGroup.GetChildAt(i);
                    child?.SetVisible(isVisible, true);
                }
                catch (System.Exception)
                {
                    // Ignored
                }
            }
        }
#endif

#if WINDOWS_UWP
        /// <summary>
        /// Sets the visibility of the given control by the given visible boolean value.
        /// </summary>
        /// <param name="view">
        /// The control to update visibility.
        /// </param>
        /// <param name="isVisible">
        /// A value indicating whether the control is visible.
        /// </param>
        public static void SetVisible(this Windows.UI.Xaml.UIElement view, bool isVisible)
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
        public static void SetVisible(this Windows.UI.Xaml.UIElement view, bool isVisible, bool updateChildViews)
        {
            if (view == null)
            {
                return;
            }

            view.Visibility = isVisible ? Windows.UI.Xaml.Visibility.Visible : Windows.UI.Xaml.Visibility.Collapsed;

            if (!updateChildViews || !(view is Windows.UI.Xaml.Controls.Panel viewGroup))
            {
                return;
            }

            foreach (Windows.UI.Xaml.UIElement child in viewGroup.Children)
            {
                child?.SetVisible(isVisible, true);
            }
        }
#endif
    }
}