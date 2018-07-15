namespace MADE.Samples.Android
{
    using CommonServiceLocator;

    using global::Android.App;

    using MADE.App.Views.Dialogs;
    using MADE.App.Views.Navigation;
    using MADE.App.Views.Threading;
    using MADE.Samples.Android.Fragments;

    [Activity(Label = "MADE.Samples.Android", MainLauncher = true, Theme = "@style/AppTheme")]
    public class MainActivity : NavigationFrame
    {
        public MainActivity()
        {
            this.FrameLayoutId = Resource.Layout.Main;
            this.FrameFragmentContentId = Resource.Id.MainContent;
        }

        protected override void OnResume()
        {
            base.OnResume();

            ServiceLocator.Current.GetInstance<IUIDispatcher>()?.SetReference(this);

            this.Navigate(typeof(MainFragment), "Hello Main Fragment!");
            this.Navigate(typeof(SecondFragment), "Hey Second Fragment!");
        }
    }
}