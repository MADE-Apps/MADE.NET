// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Page.Windows.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines a Windows page that is compatible with the application NavigationFrame.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

#if WINDOWS_UWP
namespace MADE.App.Views.Navigation.Pages
{
    using Windows.ApplicationModel;
    using Windows.UI.Xaml.Media;

    using MADE.App.Design.Color;
    using MADE.App.Views.Extensions;
    using MADE.App.Views.Navigation.Extensions;

    /// <summary>
    /// Defines a Windows page that is compatible with the application NavigationFrame.
    /// </summary>
    public class Page : Windows.UI.Xaml.Controls.Page, IPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Page"/> class.
        /// </summary>
        public Page()
        {
            if (DesignMode.DesignModeEnabled || DesignMode.DesignMode2Enabled)
            {
                return;
            }

            this.Loaded += (sender, args) =>
                {
                    this.ViewLoaded?.Invoke(this, new ViewLoadedEventArgs());
                    this.OnPageLoaded();
                };
        }

        /// <summary>
        /// Gets or sets a color that provides the background of the view.
        /// </summary>
        public Color BackgroundColor
        {
            get => this.Background as SolidColorBrush;
            set => this.Background = value;
        }

        /// <summary>
        /// Occurs when the view has loaded.
        /// </summary>
        public event ViewLoadedEventHandler ViewLoaded;

        /// <summary>
        /// Gets or sets a value indicating whether the view is visible in the UI.
        /// </summary>
        public bool IsVisible
        {
            get => this.Visibility == Windows.UI.Xaml.Visibility.Visible;
            set => this.SetVisible(value);
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
        protected override void OnNavigatedFrom(Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            this.OnNavigatedFrom(e.ToNavigationEventArgs());
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
        protected override void OnNavigatedTo(Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            this.OnNavigatedTo(e.ToNavigationEventArgs());
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
        protected override void OnNavigatingFrom(Windows.UI.Xaml.Navigation.NavigatingCancelEventArgs e)
        {
            this.OnNavigatingFrom(e.ToNavigatingCancelEventArgs());
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