#if __ANDROID__
namespace MADE.App.Views.Navigation
{
    using Android.Views;

    public interface IAndroidPage
    {
        View View { get; }

        int LayoutId { get; }

        string Title { get; }

        bool HasMenu { get; }

        IMenu Menu { get; }
    }
}
#endif