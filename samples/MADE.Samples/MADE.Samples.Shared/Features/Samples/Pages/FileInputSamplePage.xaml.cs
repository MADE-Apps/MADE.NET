namespace MADE.Samples.Features.Samples.Pages
{
    using CommunityToolkit.Mvvm.Messaging;
    using MADE.Samples.Features.Samples.ViewModels;
    using MADE.UI.Views.Navigation;
    using MADE.UI.Views.Navigation.Pages;
    using Microsoft.Extensions.DependencyInjection;

    public sealed partial class FilePickerPage : MvvmPage
    {
        public FilePickerPage()
        {
            this.InitializeComponent();
            this.DataContext = new FileInputSamplePageViewModel(
                App.Services.GetService<INavigationService>(),
                App.Services.GetService<IMessenger>());
        }

        public FileInputSamplePageViewModel ViewModel => this.DataContext as FileInputSamplePageViewModel;
    }
}