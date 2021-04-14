// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.UI.ViewManagement
{
    /// <summary>
    /// Defines a delegate for an event handler of a view service having changed.
    /// </summary>
    /// <typeparam name="TService">
    /// The type of service.
    /// </typeparam>
    /// <param name="sender">
    /// The originator.
    /// </param>
    /// <param name="args">
    /// The service changed arguments containing the changed service and associated view identifier.
    /// </param>
    public delegate void ViewServiceChangedEventHandler<TService>(
        object sender,
        ViewServiceChangedEventArgs<TService> args);
}