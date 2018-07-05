namespace MADE.Samples
{
    using CommonServiceLocator;

    using GalaSoft.MvvmLight.Ioc;
    using GalaSoft.MvvmLight.Messaging;

    public class Locator
    {
        static Locator()
        {
            if (!ServiceLocator.IsLocationProviderSet)
            {
                ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            }

            SimpleIoc.Default.Register<IMessenger, Messenger>();
        }
    }
}