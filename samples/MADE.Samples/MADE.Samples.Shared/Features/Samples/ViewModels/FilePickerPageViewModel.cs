namespace MADE.Samples.Features.Samples.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using CommunityToolkit.Mvvm.Messaging;
    using MADE.UI.Controls;
    using MADE.UI.Views.Navigation;
    using MADE.UI.Views.Navigation.ViewModels;

    public class FilePickerPageViewModel : PageViewModel
    {
        public FilePickerPageViewModel(INavigationService navigationService, IMessenger messenger)
            : base(navigationService, messenger)
        {
        }

        public ObservableCollection<FilePickerItem> FilePickerFiles { get; } =
            new ObservableCollection<FilePickerItem>();

        public ICollection<string> FilePickerTypes => new List<string> { ".jpg" };
    }
}