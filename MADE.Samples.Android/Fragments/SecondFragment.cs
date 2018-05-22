namespace MADE.Samples.Android.Fragments
{
    using MADE.App.Views.Navigation;
    using MADE.App.Views.Navigation.Pages;
    using MADE.App.Views.Navigation.ViewModels;

    public class SecondFragment : Page
    {
        public SecondFragment()
        {
            this.DataContext = new PageViewModel();
        }

        public override int LayoutId => Resource.Layout.SecondFragment;
    }
}