// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPage.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines an interface for an application page.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MADE.App.Views.Navigation.Pages
{
    /// <summary>
    /// Defines an interface for an application page.
    /// </summary>
    public interface IPage
    {
        /// <summary>
        /// Gets or sets the data context for the page.
        /// </summary>
        object DataContext { get; set; }

        /// <summary>
        /// Called when the page has loaded.
        /// </summary>
        void OnPageLoaded();

        /// <summary>
        /// Called when the page has been navigated from.
        /// </summary>
        /// <param name="e">
        /// The navigation event argument for the navigation.
        /// </param>
        void OnNavigatedFrom(NavigationEventArgs e);

        /// <summary>
        /// Called when the page has been navigated to.
        /// </summary>
        /// <param name="e">
        /// The navigation event argument for the navigation.
        /// </param>
        void OnNavigatedTo(NavigationEventArgs e);

        /// <summary>
        /// Called when the page is being navigated from.
        /// </summary>
        /// <param name="e">
        /// The navigation event argument for the navigation supporting the cancellation of the navigation.
        /// </param>
        void OnNavigatingFrom(NavigatingCancelEventArgs e);
    }
}