#if __ANDROID__
namespace MADE.App.Views.Navigation
{
    using Android.Support.V4.App;

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