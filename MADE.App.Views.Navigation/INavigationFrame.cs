// --------------------------------------------------------------------------------------------------------------------
// <copyright file="INavigationFrame.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines an interface for an application frame used for navigation between pages.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MADE.App.Views.Navigation
{
    using System;

    /// <summary>
    /// Defines an interface for an application frame used for navigation between pages.
    /// </summary>
    public interface INavigationFrame
    {
        /// <summary>
        /// Occurs when the content that is being navigated to has been found and is available from the Content property, although it may not have completed loading.
        /// </summary>
        event NavigationEventHandler PageNavigated;

        /// <summary>
        /// Gets a value that indicates whether there is at least one entry in back navigation history.
        /// </summary>
        bool CanGoBack { get; }

        /// <summary>
        /// Gets a type reference for the content that is currently displayed.
        /// </summary>
        Type CurrentSourcePageType { get; }

        /// <summary>
        /// Gets the parameter passed to the current source page on navigated to.
        /// </summary>
        object CurrentSourcePageParameter { get; }

        /// <summary>
        /// Gets the number of entries in the navigation back stack.
        /// </summary>
        int BackStackDepth { get; }

        /// <summary>
        /// Navigates to the most recent item in the back navigation history.
        /// </summary>
        void GoBack();

        /// <summary>
        /// Performs a new navigation within the Frame to the Page associated by the given source page type.
        /// </summary>
        /// <param name="sourcePageType">
        /// The type associated with the source page to navigate to.
        /// </param>
        /// <returns>
        /// Returns true if the navigation is successfully; otherwise, false.
        /// </returns>
        bool Navigate(Type sourcePageType);

        /// <summary>
        /// Performs a new navigation within the Frame to the Page associated by the given source page type, passing a parameter on navigation.
        /// </summary>
        /// <param name="sourcePageType">
        /// The type associated with the source page to navigate to.
        /// </param>
        /// <param name="parameter">
        /// The parameter to pass to the page on navigated to.
        /// </param>
        /// <returns>
        /// Returns true if the navigation is successfully; otherwise, false.
        /// </returns>
        bool Navigate(Type sourcePageType, object parameter);
    }
}