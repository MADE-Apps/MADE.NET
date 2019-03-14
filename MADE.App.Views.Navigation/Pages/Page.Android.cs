// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Page.Android.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines an Android support fragment that is compatible with the application NavigationFrame.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

#if __ANDROID__
namespace MADE.App.Views.Navigation.Pages
{
    using Android.Graphics.Drawables;
    using Android.OS;
    using Android.Support.V4.App;
    using Android.Views;

    using MADE.App.Views.Extensions;

    using XPlat.UI;

    /// <summary>
    /// Defines an Android support fragment that is compatible with the application NavigationFrame.
    /// </summary>
    public class Page : Fragment, IPage
    {
        /// <summary>
        /// Occurs when the view has loaded.
        /// </summary>
        public event ViewLoadedEventHandler ViewLoaded;

        /// <summary>
        /// Gets or sets the data context for the page.
        /// </summary>
        public object DataContext { get; set; }

        /// <summary>
        /// Gets the ID for the Android layout associated with the page.
        /// </summary>
        public virtual int LayoutId { get; } = 0;

        /// <summary>
        /// Gets or sets a color that provides the background of the view.
        /// </summary>
        public Color BackgroundColor
        {
            get
            {
                if (this.View == null)
                {
                    return Android.Graphics.Color.Transparent;
                }

                Drawable background = this.View.Background;
                return background is ColorDrawable drawable ? drawable.Color : Android.Graphics.Color.Transparent;
            }
            set => this.View?.SetBackgroundColor(value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the view is enabled and can be interacted with.
        /// </summary>
        public bool IsEnabled
        {
            get => this.View != null && this.View.Enabled;
            set
            {
                if (this.View != null)
                {
                    this.View.Enabled = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the view is visible in the UI.
        /// </summary>
        public new bool IsVisible
        {
            get => this.View != null && this.View.Visibility == ViewStates.Visible;
            set => this.View?.SetVisible(value);
        }

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
            this.ViewLoaded?.Invoke(this, new ViewLoadedEventArgs());
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
        public TView GetChildView<TView>(int resourceId)
            where TView : View
        {
            return this.View?.FindViewById<TView>(resourceId);
        }
    }
}
#endif