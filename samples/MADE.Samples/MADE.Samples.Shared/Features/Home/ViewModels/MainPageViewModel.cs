namespace MADE.Samples.Features.Home.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
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

        public ICommand NavigateToSampleCommand => new RelayCommand<Sample>(NavigateToSample);

        public ICollection<SampleGroup> SampleGroups { get; } = new List<SampleGroup>
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
                        "/Features/Samples/Assets/FilePicker/FilePicker.png"),
                    new Sample(
                        "InputValidator",
                        typeof(InputValidatorPage),
                        string.Empty,
                        "/Features/Samples/Assets/InputValidator/InputValidator.png")
                }
            },
            new SampleGroup
            {
                Name = "Helpers",
                Samples = new List<Sample>
                {
                    new Sample(
                        "AppDialog",
                        typeof(AppDialogPage),
                        string.Empty,
                        "/Features/Samples/Assets/AppDialog/AppDialog.png")
                }
            }
        };

        public ICollection<Sample> Samples => SampleGroups.SelectMany(x => x.Samples).ToList();

        private void NavigateToSample(Sample sample)
        {
            NavigationService.NavigateTo(sample.Page);
        }
    }
}