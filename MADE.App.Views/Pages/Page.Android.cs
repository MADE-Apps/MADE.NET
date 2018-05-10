#if __ANDROID__
namespace MADE.App.Views.Pages
{
    using Android.Support.V4.App;

    using MADE.App.Views.Pages.Navigation;

    public class Page : Fragment, IPage
    {
        public virtual void OnNavigatedFrom(NavigationEventArgs e)
        {
        }

        public virtual void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        public virtual void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
        }
    }
}
#endif