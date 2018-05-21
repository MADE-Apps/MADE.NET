// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NavigationEventArgs.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines an event argument for a page navigation.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MADE.App.Views.Navigation
{
    using System;

    /// <summary>
    /// Defines an event argument for a page navigation.
    /// </summary>
    public class NavigationEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets the parameter object passed to the target page for the navigation.
        /// </summary>
        public object Parameter { get; set; }

        /// <summary>
        /// Gets or sets the data type of the source page.
        /// </summary>
        public Type SourcePageType { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates the direction of movement during navigation.
        /// </summary>
        public NavigationMode NavigationMode { get; set; }
    }
}