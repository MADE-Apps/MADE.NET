// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.UI.Views.Navigation
{
    using System;
    using Windows.UI.Xaml.Controls;

    /// <summary>
    /// Defines an interface for frame based page-to-page navigation.
    /// </summary>
    public interface INavigationService
    {
        /// <summary>
        /// Occurs when a page has navigated.
        /// </summary>
        event EventHandler<bool> Navigated;

        /// <summary>
        /// Gets or sets the frame used for navigation.
        /// </summary>
        Frame NavigationFrame { get; set; }

        /// <summary>
        /// Gets a value indicating whether there is at least one entry in back navigation history.
        /// </summary>
        bool CanGoBack { get; }

        /// <summary>
        /// Gets the type associated with the current page.
        /// </summary>
        Type CurrentPageType { get; }

        /// <summary>
        /// Navigates the current frame to the page specified by the given page type.
        /// </summary>
        /// <param name="pageType">
        /// The type associated with the page to navigate to.
        /// </param>
        /// <returns>
        /// Returns true if the navigation service successfully navigated to the page; otherwise, false.
        /// </returns>
        bool NavigateTo(Type pageType);

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
        bool NavigateTo(Type pageType, object parameter);

        /// <summary>
        /// Attempts to navigate the current frame backwards.
        /// </summary>
        /// <returns>
        /// Returns true if the navigation service successfully navigates backwards; otherwise, false.
        /// </returns>
        bool GoBack();
    }
}