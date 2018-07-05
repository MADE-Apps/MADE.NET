namespace MADE.Samples.Android
{
    using CommonServiceLocator;

    using GalaSoft.MvvmLight.Ioc;

    using MADE.App.Diagnostics;
    using MADE.App.Diagnostics.Logging;

    public class AndroidLocator
    {
        static AndroidLocator()
        {
            if (!ServiceLocator.IsLocationProviderSet)
            {
                ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            }

            SimpleIoc.Default.Register<IEventLogger, EventLogger>();
            SimpleIoc.Default.Register<IAppDiagnostics>(
                () => new AppDiagnostics(ServiceLocator.Current.GetInstance<IEventLogger>()));
        }
    }
}