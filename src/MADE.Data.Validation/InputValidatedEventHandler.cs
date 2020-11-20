// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Data.Validation
{
    /// <summary>
    /// Defines a delegate for an event handler for observing when an input is validated.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="args">
    /// The input validated event argument.
    /// </param>
    public delegate void InputValidatedEventHandler(object sender, InputValidatedEventArgs args);
}