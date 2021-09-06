namespace MADE.Samples.Features.Samples.ViewModels
{
    using System.Threading.Tasks;
    using System.Windows.Input;
    using CommunityToolkit.Mvvm.Input;
    using CommunityToolkit.Mvvm.Messaging;
    using MADE.Samples.Features.Samples.Pages;
    using MADE.UI.ViewManagement;
    using MADE.UI.Views.Navigation;
    using MADE.UI.Views.Navigation.ViewModels;

    public class WindowManagerPageViewModel : PageViewModel
    {
        public WindowManagerPageViewModel(INavigationService navigationService, IMessenger messenger)
            : base(navigationService, messenger)
        {
        }

        public ICommand ShowNewWindowCommand => new AsyncRelayCommand(this.ShowNewWindowAsync);

        private async Task ShowNewWindowAsync()
        {
            await WindowManager.CreateNewWindowForPageAsync(typeof(WindowManagerPage));
        }
    }
}