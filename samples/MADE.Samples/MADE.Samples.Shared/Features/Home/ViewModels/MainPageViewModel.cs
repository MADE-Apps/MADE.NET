namespace MADE.Samples.Features.Home.ViewModels
{
    using System.Collections.Generic;
    using System.Windows.Input;
    using CommunityToolkit.Mvvm.Input;
    using CommunityToolkit.Mvvm.Messaging;
    using MADE.Samples.Features.Samples.Data;
    using MADE.UI.Views.Navigation;
    using MADE.UI.Views.Navigation.ViewModels;
    using MADE.Samples.Features.Samples.Pages;

    public class MainPageViewModel : PageViewModel
    {
        public MainPageViewModel(INavigationService navigationService, IMessenger messenger)
            : base(navigationService, messenger)
        {
        }

        public ICommand NavigateToSampleCommand => new RelayCommand<Sample>(this.NavigateToSample);

        public ICollection<Sample> Samples { get; } = new List<Sample>
        {
            new Sample
            {
                Name = "FileInput",
                Category = "Controls",
                Page = typeof(FilePickerPage)
            }
        };

        private void NavigateToSample(Sample sample)
        {
            this.NavigationService.NavigateTo(sample.Page);
        }
    }
}