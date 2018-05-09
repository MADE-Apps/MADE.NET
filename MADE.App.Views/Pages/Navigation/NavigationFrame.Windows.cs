#if WINDOWS_UWP
namespace MADE.App.Views.Pages.Navigation
{
    using Windows.UI.Xaml.Controls;

    public partial class NavigationFrame : Frame
    {
        private bool CanNavigateBackForPlatform()
        {
            return this.CanGoBack;
        }
    }
}
#endif