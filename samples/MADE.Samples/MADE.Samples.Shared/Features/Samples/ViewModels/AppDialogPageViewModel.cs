namespace MADE.Samples.Features.Samples.ViewModels
{
    using System;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using CommunityToolkit.Mvvm.Input;
    using CommunityToolkit.Mvvm.Messaging;
    using MADE.UI.Views.Dialogs;
    using MADE.UI.Views.Dialogs.Buttons;
    using MADE.UI.Views.Navigation;
    using MADE.UI.Views.Navigation.ViewModels;

    public class AppDialogPageViewModel : PageViewModel
    {
        private readonly IAppDialog appDialog;
        private string appDialogTitle = "Alert";
        private string appDialogMessage = "This is an app dialog!";
        private string appDialogConfirmText = "Confirm";
        private string appDialogCancelText = "Cancel";
        private string appDialogNeutralText = "Help";
        private string appDialogResponseMessage;

        public AppDialogPageViewModel(IAppDialog appDialog, INavigationService navigationService, IMessenger messenger)
            : base(navigationService, messenger)
        {
            this.appDialog = appDialog;
        }

        public ICommand ShowAppDialogCommand => new AsyncRelayCommand(this.ShowAppDialogAsync);

        public string AppDialogTitle
        {
            get => appDialogTitle;
            set => SetProperty(ref appDialogTitle, value);
        }

        public string AppDialogMessage
        {
            get => appDialogMessage;
            set => SetProperty(ref appDialogMessage, value);
        }

        public string AppDialogConfirmText
        {
            get => appDialogConfirmText;
            set => SetProperty(ref appDialogConfirmText, value);
        }

        public string AppDialogCancelText
        {
            get => appDialogCancelText;
            set => SetProperty(ref appDialogCancelText, value);
        }

        public string AppDialogNeutralText
        {
            get => appDialogNeutralText;
            set => SetProperty(ref appDialogNeutralText, value);
        }

        public string AppDialogResponseMessage
        {
            get => appDialogResponseMessage;
            set => SetProperty(ref appDialogResponseMessage, value);
        }

        private async Task ShowAppDialogAsync()
        {
            await appDialog.ShowAsync(
                AppDialogTitle,
                AppDialogMessage,
                OnAppDialogCancelled,
                new DialogButton(DialogButtonType.Confirm)
                {
                    Content = AppDialogConfirmText,
                    InvokeAction = this.OnDialogButtonInvoked
                },
                new DialogButton(DialogButtonType.Cancel)
                {
                    Content = AppDialogCancelText,
                    InvokeAction = this.OnDialogButtonInvoked
                },
                new DialogButton(DialogButtonType.Neutral)
                {
                    Content = AppDialogNeutralText,
                    InvokeAction = this.OnDialogButtonInvoked
                });
        }

        private void OnDialogButtonInvoked(DialogButton button)
        {
            switch (button.Type)
            {
                case DialogButtonType.Confirm:
                    this.AppDialogResponseMessage = "App dialog confirm button clicked!";
                    break;
                case DialogButtonType.Cancel:
                    this.AppDialogResponseMessage = "App dialog cancel button clicked!";
                    break;
                case DialogButtonType.Neutral:
                    this.AppDialogResponseMessage = "App dialog additional button clicked!";
                    break;
            }
        }

        private void OnAppDialogCancelled()
        {
            this.AppDialogResponseMessage = "App dialog was dismissed";
        }
    }
}