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
    using System;

    using Windows.ApplicationModel;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Media;

    using MADE.App.Views.Extensions;
    using MADE.App.Views.Navigation.Extensions;

    using XPlat.UI;

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
            if (DesignMode.DesignModeEnabled)
            {
                return;
            }

            this.Loaded += this.OnLoaded;
        }

        /// <summary>
        /// Occurs when the view has loaded.
        /// </summary>
        public event ViewLoadedEventHandler ViewLoaded;

        /// <summary>
        /// Occurs when the <see cref="IsVisible"/> state has changed.
        /// </summary>
        public event EventHandler<bool> IsVisibleChanged;

        /// <summary>
        /// Gets or sets a color that provides the background of the view.
        /// </summary>
        public Color BackgroundColor
        {
            get => this.Background as SolidColorBrush;
            set => this.Background = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the view is visible in the UI.
        /// </summary>
        /// <exception cref="T:System.Exception" accessor="set">A delegate callback throws an exception.</exception>
        public bool IsVisible
        {
            get => this.Visibility == Windows.UI.Xaml.Visibility.Visible;
            set
            {
                this.SetVisible(value);
                this.IsVisibleChanged?.Invoke(this, value);
            }
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
        /// Called when the page is being navigated from.
        /// </summary>
        /// <param name="e">
        /// The navigation event argument for the navigation supporting the cancellation of the navigation.
        /// </param>
        protected override void OnNavigatingFrom(Windows.UI.Xaml.Navigation.NavigatingCancelEventArgs e)
        {
            this.OnNavigatingFrom(e.ToNavigatingCancelEventArgs());
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            this.Loaded -= this.OnLoaded;

            this.ViewLoaded?.Invoke(this, new ViewLoadedEventArgs());
            this.OnPageLoaded();
        }
    }
}
#endif