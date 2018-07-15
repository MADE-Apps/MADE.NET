namespace MADE.Samples.Windows
{
    using CommonServiceLocator;

    using GalaSoft.MvvmLight.Ioc;

    using MADE.App.Diagnostics;
    using MADE.App.Diagnostics.Logging;
    using MADE.App.Views.Dialogs;
    using MADE.App.Views.Threading;

    public class WindowsLocator
    {
        static WindowsLocator()
        {
            if (!ServiceLocator.IsLocationProviderSet)
            {
                ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            }

            SimpleIoc.Default.Register<IEventLogger, EventLogger>();
            SimpleIoc.Default.Register<IAppDiagnostics>(
                () => new AppDiagnostics(ServiceLocator.Current.GetInstance<IEventLogger>()));
            SimpleIoc.Default.Register<IUIDispatcher, UIDispatcher>();
            SimpleIoc.Default.Register<IAppDialog, AppDialog>();
        }
    }
}