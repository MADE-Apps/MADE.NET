#if __ANDROID__
namespace MADE.App.Views.Navigation.Pages
{
    using Android.Views;

    /// <summary>
    /// Defines an interface for Android components of an application page.
    /// </summary>
    public partial interface IPage
    {
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