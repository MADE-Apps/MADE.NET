#if WINDOWS_UWP
namespace MADE.App.Views.Navigation
{
    using Windows.UI.Xaml.Controls;

    public class NavigationFrame : Frame, INavigationFrame
    {
        public NavigationFrame()
        {
            this.Navigated += this.OnNavigated;
        }

        /// <summary>
        /// Occurs when the content that is being navigated to has been found and is available from the Content property, although it may not have completed loading.
        /// </summary>
        public event NavigationEventHandler PageNavigated;

        private void OnNavigated(object sender, Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            this.PageNavigated?.Invoke(sender, e.ToNavigationEventArgs());
        }
    }
}
#endif