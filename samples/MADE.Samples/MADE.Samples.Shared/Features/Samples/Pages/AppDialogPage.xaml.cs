namespace MADE.Samples.Features.Samples.Pages
{
    using CommunityToolkit.Mvvm.Messaging;
    using MADE.Samples.Features.Samples.ViewModels;
    using MADE.UI.Views.Dialogs;
    using MADE.UI.Views.Navigation;
    using MADE.UI.Views.Navigation.Pages;
    using Microsoft.Extensions.DependencyInjection;

    public sealed partial class AppDialogPage : MvvmPage
    {
        public AppDialogPage()
        {
            this.InitializeComponent();
            this.DataContext = new AppDialogPageViewModel(
                App.Services.GetService<IAppDialog>(),
                App.Services.GetService<INavigationService>(),
                App.Services.GetService<IMessenger>());
        }

        public AppDialogPageViewModel ViewModel => this.DataContext as AppDialogPageViewModel;
    }
}