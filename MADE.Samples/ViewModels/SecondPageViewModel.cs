namespace MADE.Samples.ViewModels
{
    using System.Windows.Input;

    using CommonServiceLocator;

    using MADE.App.Mvvm;
    using MADE.App.Views.Dialogs;
    using MADE.App.Views.Navigation;
    using MADE.App.Views.Navigation.ViewModels;

    public class SecondPageViewModel : PageViewModel
    {
        private readonly IAppDialog dialog;

        public SecondPageViewModel()
        {
            this.dialog = ServiceLocator.Current.GetInstance<IAppDialog>();

            this.ShowDialogCommand = new RelayCommand(this.ShowDialog);
        }

        public ICommand ShowDialogCommand { get; }

        public override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            this.ShowDialog();
        }

        private void ShowDialog()
        {
            this.dialog?.Show("Hello, World!");
        }
    }
}