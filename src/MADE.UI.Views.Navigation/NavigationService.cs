// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.UI.Views.Navigation
{
    using System;
    using Windows.Foundation.Metadata;
    using Windows.UI.Core;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Navigation;

    /// <summary>
    /// Defines a service for frame based page-to-page navigation.
    /// </summary>
    public partial class NavigationService : INavigationService
    {
        private readonly SystemNavigationManager navigationManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationService"/> class.
        /// </summary>
        /// <param name="navigationFrame">
        /// The frame used for navigation.
        /// </param>
        public NavigationService(Frame navigationFrame = default)
        {
            this.NavigationFrame = navigationFrame ?? new Frame();

            if (this.NavigationFrame != null)
            {
                this.NavigationFrame.Navigated += this.OnFrameNavigated;
            }

            if (!ApiInformation.IsTypePresent("Windows.UI.Core.SystemNavigationManager"))
            {
                return;
            }

            this.navigationManager = SystemNavigationManager.GetForCurrentView();
            if (this.navigationManager != null)
            {
                this.navigationManager.BackRequested += this.OnBackRequested;
            }
        }

        /// <summary>
        /// Occurs when a page has navigated.
        /// </summary>
        public event EventHandler<bool> Navigated;

        /// <summary>
        /// Gets or sets the frame used for navigation.
        /// </summary>
        public Frame NavigationFrame { get; set; }

        /// <summary>
        /// Gets a value indicating whether there is at least one entry in back navigation history.
        /// </summary>
        public bool CanGoBack => this.NavigationFrame?.CanGoBack ?? false;

        /// <summary>
        /// Gets the type associated with the current page.
        /// </summary>
        public Type CurrentPageType => this.NavigationFrame.CurrentSourcePageType;

        /// <summary>
        /// Navigates the current frame to the page specified by the given page type.
        /// </summary>
        /// <param name="pageType">
        /// The type associated with the page to navigate to.
        /// </param>
        /// <returns>
        /// Returns true if the navigation service successfully navigated to the page; otherwise, false.
        /// </returns>
        public virtual bool NavigateTo(Type pageType)
        {
            return this.NavigationFrame?.Navigate(pageType) ?? false;
        }

        /// <summary>
        /// Navigates the current frame to the page specified by the given page type.
        /// </summary>
        /// <param name="pageType">
        /// The type associated with the page to navigate to.
        /// </param>
        /// <param name="parameter">
        /// The parameter to pass to the page on navigation.
        /// </param>
        /// <returns>
        /// Returns true if the navigation service successfully navigated to the page; otherwise, false.
        /// </returns>
        public virtual bool NavigateTo(Type pageType, object parameter)
        {
            return this.NavigationFrame?.Navigate(pageType, parameter) ?? false;
        }

        /// <summary>
        /// Attempts to navigate the current frame backwards.
        /// </summary>
        /// <returns>
        /// Returns true if the navigation service successfully navigates backwards; otherwise, false.
        /// </returns>
        public virtual bool GoBack()
        {
            this.NavigationFrame?.GoBack();
            return true;
        }

        private void OnFrameNavigated(object sender, NavigationEventArgs args)
        {
            this.Navigated?.Invoke(this, true);
        }

        private void OnBackRequested(object sender, BackRequestedEventArgs e)
        {
            if (!this.NavigationFrame.CanGoBack)
            {
                return;
            }

            e.Handled = true;
            this.GoBack();
        }
    }
}