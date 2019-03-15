namespace MADE.Samples.ViewModels
{
    using System;
    using System.Threading.Tasks;
    using System.Windows.Input;

    using GalaSoft.MvvmLight.Ioc;

    using MADE.App.Mvvm;
    using MADE.App.Views.Dialogs;
    using MADE.App.Views.Navigation;
    using MADE.App.Views.Navigation.ViewModels;

    public class SecondPageViewModel : PageViewModel
    {
        private readonly IAppDialog dialog;

        public SecondPageViewModel()
        {
            this.dialog = SimpleIoc.Default.GetInstance<IAppDialog>();

            this.ShowDialogCommand = new RelayCommand(async () => await this.ShowDialogAsync());
        }

        public ICommand ShowDialogCommand { get; }

        public override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            await this.ShowDialogAsync();
        }

        private async Task ShowDialogAsync()
        {
            try
            {
                await this.dialog.ShowAsync("Hello, World!");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }
    }
}