#if __ANDROID__
namespace MADE.App.Views
{
    using System.ComponentModel;

    using Android.Views;

    using MADE.App.Design.Color;

    /// <summary>
    /// Defines an interface for Android components of a common application user interface.
    /// </summary>
    public partial interface IView
    {
        /// <summary>
        /// Gets the view associated with the inflated layout.
        /// </summary>
        View View { get; }

        /// <summary>
        /// Gets the ID for the Android layout associated with the view.
        /// </summary>
        int LayoutId { get; }

        /// <summary>
        /// Gets or sets a color that provides the background of the view.
        /// </summary>
        Color BackgroundColor { get; set; }

        /// <summary>
        /// Retrieves the element from the instantiated view by the given resource identifier.
        /// </summary>
        /// <param name="resourceId">
        /// The view's resource identifier.
        /// </param>
        /// <typeparam name="TView">
        /// The type of view to retrieve.
        /// </typeparam>
        /// <returns>
        /// Returns the view from the layout, if the view is found.
        /// </returns>
        TView GetChildView<TView>(int resourceId)
            where TView : View;
    }
}
#endif