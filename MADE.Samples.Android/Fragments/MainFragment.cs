namespace MADE.Samples.Android.Fragments
{
    using MADE.App.Views.Navigation.Pages;
    using MADE.App.Views.Navigation.ViewModels;

    public class MainFragment : MvvmPage
    {
        public MainFragment()
        {
            this.DataContext = new PageViewModel();
        }

        public override int LayoutId => Resource.Layout.MainFragment;
    }
}