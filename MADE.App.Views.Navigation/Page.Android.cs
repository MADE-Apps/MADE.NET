#if __ANDROID__
namespace MADE.App.Views.Navigation
{
    using Android.OS;
    using Android.Support.V4.App;
    using Android.Views;

    public class Page : Fragment, IAndroidPage, IPage
    {
        public object DataContext { get; set; }

        public virtual int LayoutId { get; } = 0;

        public virtual string Title { get; } = "MyPage";

        public virtual bool HasMenu { get; } = false;

        public IMenu Menu { get; private set; }

        public new View View { get; set; }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            this.View = inflater.Inflate(this.LayoutId, container, false);
            this.OnPageLoaded();

            this.HasOptionsMenu = this.HasMenu;
            return this.View;
        }

        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
        {
            base.OnCreateOptionsMenu(menu, inflater);
            this.Menu = menu;
        }

        public virtual void OnPageLoaded()
        {
        }

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