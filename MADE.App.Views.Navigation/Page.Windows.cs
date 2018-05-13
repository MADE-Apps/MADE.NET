#if WINDOWS_UWP
namespace MADE.App.Views.Navigation
{
    using Windows.ApplicationModel;

    public class Page : Windows.UI.Xaml.Controls.Page, IPage
    {
        public Page()
        {
            if (DesignMode.DesignModeEnabled || DesignMode.DesignMode2Enabled)
            {
                return;
            }

            this.Loaded += (sender, args) => this.OnPageLoaded();
        }

        public virtual void OnPageLoaded()
        {
        }

        protected override void OnNavigatedFrom(Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            this.OnNavigatedFrom(e.ToNavigationEventArgs());
        }

        public virtual void OnNavigatedFrom(NavigationEventArgs e)
        {
        }

        protected override void OnNavigatedTo(Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            this.OnNavigatedTo(e.ToNavigationEventArgs());
        }

        public virtual void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        protected override void OnNavigatingFrom(Windows.UI.Xaml.Navigation.NavigatingCancelEventArgs e)
        {
            this.OnNavigatingFrom(e.ToNavigatingCancelEventArgs());
        }

        public virtual void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
        }
    }
}
#endif