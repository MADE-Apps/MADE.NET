#if WINDOWS_UWP
namespace MADE.App.Views.Extensions
{
    public static partial class ViewExtensions
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
    }
}
#endif