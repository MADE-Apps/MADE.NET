namespace MADE.Samples.Features.Home.ViewModels
{
    using System.Collections.Generic;
    using System.Windows.Input;
    using CommunityToolkit.Mvvm.Input;
    using CommunityToolkit.Mvvm.Messaging;
    using MADE.Samples.Features.Samples.Data;
    using MADE.Samples.Features.Samples.Pages;
    using MADE.UI.Views.Navigation;
    using MADE.UI.Views.Navigation.ViewModels;

    public class MainPageViewModel : PageViewModel
    {
        public MainPageViewModel(INavigationService navigationService, IMessenger messenger)
            : base(navigationService, messenger)
        {
        }

        public ICommand NavigateToSampleCommand => new RelayCommand<Sample>(this.NavigateToSample);

        public ICollection<SampleGroup> Samples { get; } = new List<SampleGroup>
        {
            new SampleGroup
            {
                Name = "Controls",
                Samples = new List<Sample>
                {
                    new Sample(
                        "FilePicker",
                        typeof(FilePickerPage),
                        string.Empty,
                        "/Features/Samples/Assets/FilePicker.png")
                }
            }
        };

        private void NavigateToSample(Sample sample)
        {
            this.NavigationService.NavigateTo(sample.Page);
        }
    }
}