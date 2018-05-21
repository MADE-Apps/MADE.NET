#if WINDOWS_UWP
namespace MADE.App.Views.Navigation
{
    using Windows.UI.Xaml.Controls;

    /// <summary>
    /// Defines a frame for navigating and displaying page content.
    /// </summary>
    public class NavigationFrame : Frame, INavigationFrame
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationFrame"/> class.
        /// </summary>
        public NavigationFrame()
        {
            this.Navigated += this.OnNavigated;
        }

        /// <summary>
        /// Occurs when the content that is being navigated to has been found and is available from the Content property, although it may not have completed loading.
        /// </summary>
        public event NavigationEventHandler PageNavigated;

        /// <summary>
        /// Gets the parameter passed to the current source page on navigated to.
        /// </summary>
        public object CurrentSourcePageParameter { get; private set; }

        private void OnNavigated(object sender, Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            this.CurrentSourcePageParameter = e.Parameter;
            this.PageNavigated?.Invoke(sender, e.ToNavigationEventArgs());
        }
    }
}
#endif