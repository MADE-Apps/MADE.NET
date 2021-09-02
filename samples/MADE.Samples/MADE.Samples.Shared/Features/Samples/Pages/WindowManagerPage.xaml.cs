namespace MADE.Samples.Features.Samples.Pages
{
    using CommunityToolkit.Mvvm.Messaging;
    using MADE.Samples.Features.Samples.ViewModels;
    using MADE.UI.Views.Navigation;
    using MADE.UI.Views.Navigation.Pages;
    using Microsoft.Extensions.DependencyInjection;

    public sealed partial class WindowManagerPage : MvvmPage
    {
        public WindowManagerPage()
        {
            this.InitializeComponent();
            this.DataContext = new WindowManagerPageViewModel(
                App.Services.GetService<INavigationService>(),
                App.Services.GetService<IMessenger>());
        }

        public WindowManagerPageViewModel ViewModel => this.DataContext as WindowManagerPageViewModel;
    }
}
