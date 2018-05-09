namespace MADE.App.Views.Pages.Navigation
{
    public interface INavigationFrame
    {
        /// <summary>
        /// Gets a value that indicates whether there is at least one entry in back navigation history.
        /// </summary>
        bool CanNavigateBack { get; }
    }
}