namespace MADE.App.Views.Pages.Navigation
{
    public partial class NavigationFrame : INavigationFrame
    {
        /// <summary>
        /// Gets a value that indicates whether there is at least one entry in back navigation history.
        /// </summary>
        public bool CanNavigateBack =>
#if WINDOWS_UWP || __ANDROID__ || __IOS__
            this.CanNavigateBackForPlatform();
#else
            false;
#endif
    }
}