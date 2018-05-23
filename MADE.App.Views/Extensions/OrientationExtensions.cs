namespace MADE.App.Views.Extensions
{
    public static class OrientationExtensions
    {
#if __ANDROID__
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
            MADE.App.Views.Layout.Orientation orientation)
        {
            if (layout == null)
            {
                return;
            }

            layout.Orientation = orientation == MADE.App.Views.Layout.Orientation.Vertical
                                     ? Android.Widget.Orientation.Vertical
                                     : Android.Widget.Orientation.Horizontal;

            layout.Invalidate();
        }
#endif
    }
}