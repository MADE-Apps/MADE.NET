namespace MADE.Samples.Features.Samples.Pages
{
    using CommunityToolkit.Mvvm.Messaging;
    using MADE.Samples.Features.Samples.ViewModels;
    using MADE.UI.Views.Navigation;
    using MADE.UI.Views.Navigation.Pages;
    using Microsoft.Extensions.DependencyInjection;

    public sealed partial class InputValidatorPage : MvvmPage
    {
        public InputValidatorPage()
        {
            this.InitializeComponent();
            this.DataContext = new InputValidatorPageViewModel(
                App.Services.GetService<INavigationService>(),
                App.Services.GetService<IMessenger>());
        }

        public InputValidatorPageViewModel ViewModel => this.DataContext as InputValidatorPageViewModel;
    }
}
