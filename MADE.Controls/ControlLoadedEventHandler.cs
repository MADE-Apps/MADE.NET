// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ControlLoadedEventHandler.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines a delegate for handling when a control has loaded.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MADE.Controls
{
    /// <summary>
    /// Defines a delegate for handling when a control has loaded.
    /// </summary>
    /// <param name="sender">
    /// The originating control.
    /// </param>
    /// <param name="args">
    /// The event arguments associated with the loaded event.
    /// </param>
    public delegate void ControlLoadedEventHandler(IControl sender, ControlLoadedEventArgs args);
}