namespace MADE.Samples.Windows
{
    using MADE.App.Views.Navigation.Pages;
    using MADE.App.Views.Navigation.ViewModels;

    public sealed partial class SecondPage : MvvmPage
    {
        public SecondPage()
        {
            this.InitializeComponent();
            this.DataContext = new PageViewModel();
        }
    }
}