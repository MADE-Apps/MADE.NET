// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExceptionObservedEventHandler.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines a delegate for an event handler for observing exceptions that were thrown.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MADE.App.Diagnostics.Exceptions
{
    /// <summary>
    /// Defines a delegate for an event handler for observing exceptions that were thrown.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="args">
    /// The exception observed event argument.
    /// </param>
    public delegate void ExceptionObservedEventHandler(object sender, ExceptionObservedEventArgs args);
}