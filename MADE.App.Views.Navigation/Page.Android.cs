#if __ANDROID__
namespace MADE.App.Views.Navigation
{
    using Android.OS;
    using Android.Support.V4.App;
    using Android.Views;

    /// <summary>
    /// Defines an Android support fragment that is compatible with the application NavigationFrame.
    /// </summary>
    public class Page : Fragment, IAndroidPage, IPage
    {
        /// <summary>
        /// Gets or sets the data context for the page.
        /// </summary>
        public object DataContext { get; set; }

        /// <summary>
        /// Gets the ID for the Android layout associated with the page.
        /// </summary>
        public virtual int LayoutId { get; } = 0;

        /// <summary>
        /// Gets the title for the page.
        /// </summary>
        public virtual string Title { get; } = "[TITLE NOT SET]";

        /// <summary>
        /// Gets a value indicating whether the page has options for the menu.
        /// </summary>
        public virtual bool HasMenu { get; } = false;

        /// <summary>
        /// Gets the menu associated with the current page.
        /// </summary>
        public IMenu Menu { get; private set; }

        /// <summary>
        /// Gets the view associated with the page's content.
        /// </summary>
        public new View View { get; set; }

        /// <summary>
        /// Called to have the fragment instantiate its user interface view.
        /// </summary>
        /// <param name="inflater">
        /// The LayoutInflater object that can be used to inflate any views in the fragment.
        /// </param>
        /// <param name="container">
        /// If non-null, this is the parent view that the fragment's UI should be attached to. The fragment should not add the view itself, but this can be used to generate the LayoutParams of the view.
        /// </param>
        /// <param name="savedInstanceState">
        /// If non-null, this fragment is being re-constructed from a previous saved state as given here.
        /// </param>
        /// <returns>
        /// Returns the inflated view.
        /// </returns>
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            this.View = inflater.Inflate(this.LayoutId, container, false);
            this.OnPageLoaded();

            this.HasOptionsMenu = this.HasMenu;
            return this.View;
        }

        /// <summary>
        /// Initialize the contents of the Activity's standard options menu.
        /// </summary>
        /// <param name="menu">
        /// The options menu in which you place your items.
        /// </param>
        /// <param name="inflater">
        /// The options menu inflater.
        /// </param>
        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
        {
            base.OnCreateOptionsMenu(menu, inflater);
            this.Menu = menu;
        }

        /// <summary>
        /// Called when the page has loaded.
        /// </summary>
        public virtual void OnPageLoaded()
        {
        }

        /// <summary>
        /// Called when the page has been navigated from.
        /// </summary>
        /// <param name="e">
        /// The navigation event argument for the navigation.
        /// </param>
        public virtual void OnNavigatedFrom(NavigationEventArgs e)
        {
        }

        /// <summary>
        /// Called when the page has been navigated to.
        /// </summary>
        /// <param name="e">
        /// The navigation event argument for the navigation.
        /// </param>
        public virtual void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        /// <summary>
        /// Called when the page is being navigated from.
        /// </summary>
        /// <param name="e">
        /// The navigation event argument for the navigation supporting the cancellation of the navigation.
        /// </param>
        public virtual void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
        }
    }
}
#endif