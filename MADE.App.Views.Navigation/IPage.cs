namespace MADE.App.Views.Navigation
{
    public interface IPage
    {
        object DataContext { get; set; }

        void OnPageLoaded();

        void OnNavigatedFrom(NavigationEventArgs e);

        void OnNavigatedTo(NavigationEventArgs e);

        void OnNavigatingFrom(NavigatingCancelEventArgs e);
    }
}