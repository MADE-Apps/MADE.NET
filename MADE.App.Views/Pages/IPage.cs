namespace MADE.App.Views.Pages
{
    using MADE.App.Views.Pages.Navigation;

    public interface IPage
    {
        void OnNavigatedFrom(NavigationEventArgs e);

        void OnNavigatedTo(NavigationEventArgs e);

        void OnNavigatingFrom(NavigatingCancelEventArgs e);
    }
}