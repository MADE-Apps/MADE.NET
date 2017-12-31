namespace MADE.Playground.Android
{
    using global::Android.App;
    using global::Android.OS;

    using MADE.UI.Controls;

    [Activity(Label = "MADE.Playground.Android", MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
	        this.SetContentView(Resource.Layout.Main);

            var htb = this.FindViewById<HeaderedTextBlock>(Resource.Id.main_htb);

            if (htb != null)
            {
                htb.Orientation = UI.Layout.Orientation.Vertical;
            }
        }
    }
}