// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NavigationMode.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines values associated with the mode of navigation of a page.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MADE.App.Views.Navigation
{
    /// <summary>
    /// Defines values associated with the mode of navigation of a page.
    /// </summary>
    public enum NavigationMode
    {
        /// <summary>
        /// The navigation is new.
        /// </summary>
        New,

        /// <summary>
        /// The navigation is going back.
        /// </summary>
        Back,

        /// <summary>
        /// The navigation is going forward.
        /// </summary>
        Forward,

        /// <summary>
        /// The navigation is being refreshed.
        /// </summary>
        Refresh
    }
}