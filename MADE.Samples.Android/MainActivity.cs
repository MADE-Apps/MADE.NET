namespace MADE.Samples.Android
{
    using global::Android.App;

    using MADE.App.Views.Navigation;
    using MADE.Samples.Android.Fragments;

    [Activity(Label = "MADE.Samples.Android", MainLauncher = true)]
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
            this.Navigate(typeof(MainFragment), "Hello Main Fragment!");
            this.Navigate(typeof(SecondFragment), "Hey Second Fragment!");
        }
    }
}