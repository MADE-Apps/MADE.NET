// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NavigationService.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines a service for frame based page-to-page navigation.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MADE.App.Views.Navigation
{
    using System;

    /// <summary>
    /// Defines a service for frame based page-to-page navigation.
    /// </summary>
    public partial class NavigationService : INavigationService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationService"/> class.
        /// </summary>
        public NavigationService()
        {
#if WINDOWS_UWP || __ANDROID__
            this.NavigationFrame = new NavigationFrame();
#endif

            if (this.NavigationFrame != null)
            {
                this.NavigationFrame.PageNavigated += this.OnFrameNavigated;
            }

#if WINDOWS_UWP
            this.SetupWindowsNavigationHandler();
#endif
        }

        /// <summary>
        /// Occurs when a page has navigated.
        /// </summary>
        public event EventHandler<bool> Navigated;

        /// <summary>
        /// Gets the frame used for navigation.
        /// </summary>
        public INavigationFrame NavigationFrame { get; }

        /// <summary>
        /// Gets a value that indicates whether there is at least one entry in back navigation history.
        /// </summary>
        public bool CanGoBack => this.NavigationFrame?.CanGoBack ?? false;

        /// <summary>
        /// Gets the type associated with the current page.
        /// </summary>
        public Type CurrentPageType => this.NavigationFrame.CurrentSourcePageType;

        /// <summary>
        /// Gets the parameter that was passed to the current page during navigation.
        /// </summary>
        public object CurrentPageParameter => this.NavigationFrame.CurrentSourcePageParameter;

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
    }
}
