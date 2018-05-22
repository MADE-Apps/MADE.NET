#if __ANDROID__
namespace MADE.App.Views.Navigation.Pages
{
    using Android.Views;

    /// <summary>
    /// Defines an interface for an Android page.
    /// </summary>
    public interface IAndroidPage
    {
        /// <summary>
        /// Gets the view associated with the page's content.
        /// </summary>
        View View { get; }

        /// <summary>
        /// Gets the ID for the Android layout associated with the page.
        /// </summary>
        int LayoutId { get; }

        /// <summary>
        /// Gets the title for the page.
        /// </summary>
        string Title { get; }
        
        /// <summary>
        /// Gets a value indicating whether the page has options for the menu.
        /// </summary>
        bool HasMenu { get; }

        /// <summary>
        /// Gets the menu associated with the current page.
        /// </summary>
        IMenu Menu { get; }
    }
}
#endif