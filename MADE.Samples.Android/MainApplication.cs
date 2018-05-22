namespace MADE.Samples.Android
{
    using System;

    using global::Android.App;
    using global::Android.OS;
    using global::Android.Runtime;

    [Application]
    public class MainApplication : Application, Application.IActivityLifecycleCallbacks
    {
        private static Locator locator;

        public MainApplication(IntPtr handle, JniHandleOwnership transfer)
            : base(handle, transfer)
        {
        }

        public static Locator Locator => locator ?? (locator = new Locator());

        public override void OnCreate()
        {
            base.OnCreate();

            this.RegisterActivityLifecycleCallbacks(this);

            locator = new Locator();
        }

        public void OnActivityCreated(Activity activity, Bundle savedInstanceState)
        {
        }

        public void OnActivityDestroyed(Activity activity)
        {
        }

        public void OnActivityPaused(Activity activity)
        {
        }

        public void OnActivityResumed(Activity activity)
        {
        }

        public void OnActivitySaveInstanceState(Activity activity, Bundle outState)
        {
        }

        public void OnActivityStarted(Activity activity)
        {
        }

        public void OnActivityStopped(Activity activity)
        {
        }
    }
}