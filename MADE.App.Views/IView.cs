// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IView.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines an interface for a common application user interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MADE.App.Views
{
    /// <summary>
    /// Defines an interface for a common application user interface.
    /// </summary>
    public partial interface IView
    {
        /// <summary>
        /// Occurs when the view has loaded.
        /// </summary>
        event ViewLoadedEventHandler ViewLoaded;

        /// <summary>
        /// Gets or sets a value indicating whether the view is enabled and can be interacted with.
        /// </summary>
        bool IsEnabled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the view is visible in the UI.
        /// </summary>
        bool IsVisible { get; set; }
    }
}
