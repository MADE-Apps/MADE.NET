// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ViewLoadedEventHandler.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines a delegate for an event handler of view's having loaded.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MADE.App.Views
{
    /// <summary>
    /// Defines a delegate for an event handler of view's having loaded.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="args">
    /// The view loaded event argument.
    /// </param>
    public delegate void ViewLoadedEventHandler(object sender, ViewLoadedEventArgs args);
}