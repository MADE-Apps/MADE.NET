namespace MADE.Samples.Android
{
    using GalaSoft.MvvmLight.Ioc;

    using MADE.App.Diagnostics;
    using MADE.App.Diagnostics.Logging;
    using MADE.App.Views.Dialogs;
    using MADE.App.Views.Threading;

    using XPlat.UI.Core;

    public class AndroidLocator
    {
        static AndroidLocator()
        {
            SimpleIoc.Default.Register<IEventLogger, EventLogger>();
            SimpleIoc.Default.Register<IAppDiagnostics>(
                () => new AppDiagnostics(SimpleIoc.Default.GetInstance<IEventLogger>()));
            SimpleIoc.Default.Register<IUIDispatcher, UIDispatcher>();
            SimpleIoc.Default.Register<IAppDialog, AppDialog>();
        }
    }
}