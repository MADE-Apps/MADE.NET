namespace MADE.Samples.Windows
{
    using MADE.App.Views.Navigation.Pages;
    using MADE.App.Views.Navigation.ViewModels;
    using MADE.Samples.ViewModels;

    public sealed partial class SecondPage : MvvmPage
    {
        public SecondPage()
        {
            this.InitializeComponent();
            this.DataContext = new SecondPageViewModel();
        }
    }
}