namespace MADE.App.Views.Navigation
{
    public interface IPage
    {
        void OnNavigatedFrom(NavigationEventArgs e);

        void OnNavigatedTo(NavigationEventArgs e);

        void OnNavigatingFrom(NavigatingCancelEventArgs e);
    }
}