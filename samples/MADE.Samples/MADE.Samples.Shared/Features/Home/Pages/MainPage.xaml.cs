namespace MADE.Samples.Features.Home.Pages
{
    using MADE.Samples.Features.Home.ViewModels;
    using MADE.UI.Views.Navigation.Pages;
    using Microsoft.Extensions.DependencyInjection;

    public sealed partial class MainPage : MvvmPage
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.DataContext = App.Services.GetService<MainPageViewModel>();
        }

        public MainPageViewModel ViewModel => this.DataContext as MainPageViewModel;
    }
}