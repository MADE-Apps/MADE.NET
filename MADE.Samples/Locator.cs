namespace MADE.Samples
{
    using GalaSoft.MvvmLight.Ioc;
    using GalaSoft.MvvmLight.Messaging;

    public class Locator
    {
        static Locator()
        {
            SimpleIoc.Default.Register<IMessenger, Messenger>();
        }
    }
}