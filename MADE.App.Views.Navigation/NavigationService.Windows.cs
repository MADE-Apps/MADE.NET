#if WINDOWS_UWP
namespace MADE.App.Views.Navigation
{
    using Windows.Foundation.Metadata;
    using Windows.UI.Core;

    public partial class NavigationService
    {
        private SystemNavigationManager navigationManager;

        private void SetupWindowsNavigationHandler()
        {
            if (!ApiInformation.IsTypePresent("Windows.UI.Core.SystemNavigationManager"))
            {
                return;
            }

            this.navigationManager = SystemNavigationManager.GetForCurrentView();
            if (this.navigationManager != null)
            {
                this.navigationManager.BackRequested += this.OnBackRequested;
            }
        }

        private void OnBackRequested(object sender, BackRequestedEventArgs e)
        {
            if (!this.NavigationFrame.CanGoBack)
            {
                return;
            }

            e.Handled = true;
            this.GoBack();
        }
    }
}
#endif