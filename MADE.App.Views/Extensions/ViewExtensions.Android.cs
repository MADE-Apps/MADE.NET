#if __ANDROID__
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
    }
}
#endif