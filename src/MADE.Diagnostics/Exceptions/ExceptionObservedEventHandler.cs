// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Diagnostics.Exceptions
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