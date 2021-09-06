namespace MADE.Samples.Features.Home.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Input;
    using CommunityToolkit.Mvvm.Input;
    using CommunityToolkit.Mvvm.Messaging;
    using MADE.Collections;
    using MADE.Data.Validation.Extensions;
    using MADE.Foundation.Platform;
    using MADE.Samples.Features.Samples.Data;
    using MADE.Samples.Features.Samples.Pages;
    using MADE.UI.ViewManagement;
    using MADE.UI.Views.Navigation;
    using MADE.UI.Views.Navigation.ViewModels;

    public class MainPageViewModel : PageViewModel
    {
        public MainPageViewModel(INavigationService navigationService, IMessenger messenger)
            : base(navigationService, messenger)
        {
        }

        public ICommand NavigateToSampleCommand => new RelayCommand<Sample>(NavigateToSample);

        public ICollection<SampleGroup> SampleGroups { get; } = GetSampleGroups();

        private static ICollection<SampleGroup> GetSampleGroups()
        {
            var controls = new SampleGroup
            {
                Name = "Controls",
                Samples =
                {
                    new Sample(
                        "FilePicker",
                        typeof(FilePickerPage),
                        "/Features/Samples/Assets/FilePicker/FilePicker.png"),
                    new Sample(
                        "InputValidator",
                        typeof(InputValidatorPage),
                        "/Features/Samples/Assets/InputValidator/InputValidator.png")
                }
            };

            var helpers = new SampleGroup
            {
                Name = "Helpers",
                Samples =
                {
                    new Sample(
                        "AppDialog",
                        typeof(AppDialogPage),
                        "/Features/Samples/Assets/AppDialog/AppDialog.png")
                }
            };

            if (PlatformApiHelper.IsTypeSupported(typeof(WindowManager)))
            {
                helpers.Samples.InsertAtPotentialIndex(
                    new Sample(
                        "WindowManager",
                        typeof(WindowManagerPage),
                        "/Features/Samples/Assets/WindowManager/WindowManager.png"),
                    (item, compare) => compare.Name.IsLessThanOrEqualTo(item.Name));
            }

            var list = new List<SampleGroup> { controls, helpers };

            return list;
        }

        public ICollection<Sample> Samples => SampleGroups.SelectMany(x => x.Samples).ToList();

        private void NavigateToSample(Sample sample)
        {
            NavigationService.NavigateTo(sample.Page);
        }
    }
}