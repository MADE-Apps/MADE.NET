// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NavigationEventHandler.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines a delegate for an event handler of page navigation.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MADE.App.Views.Navigation
{
    /// <summary>
    /// Defines a delegate for an event handler of page navigation.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="args">
    /// The navigation event argument.
    /// </param>
    public delegate void NavigationEventHandler(object sender, NavigationEventArgs args);
}