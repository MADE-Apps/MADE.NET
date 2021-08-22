// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.UI.ViewManagement
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines an interface for managing view based services, such as CoreDispatchers.
    /// </summary>
    /// <typeparam name="TService">The type of service registered.</typeparam>
    public interface IViewServiceManager<TService>
    {
        /// <summary>
        /// Occurs when a view's service has changed.
        /// </summary>
        event ViewServiceChangedEventHandler<TService> ServiceChanged;

        /// <summary>
        /// Gets the collection of registered view services.
        /// </summary>
        IReadOnlyDictionary<int, TService> Services { get; }

        /// <summary>
        /// Registers or updates a service for the specified <paramref name="viewId"/>.
        /// </summary>
        /// <param name="viewId">The view identifier.</param>
        /// <param name="service">The service to register or update.</param>
        /// <returns>The registered service.</returns>
        TService RegisterOrUpdate(int viewId, TService service);

        /// <summary>
        /// Retrieves a service for the specified <paramref name="viewId"/>.
        /// </summary>
        /// <param name="viewId">The view identifier to retrieve the service for.</param>
        /// <returns>The registered service if exists; otherwise, null.</returns>
        TService Get(int viewId);

        /// <summary>
        /// Unregisters a service for the specified <paramref name="viewId"/> if exists.
        /// </summary>
        /// <param name="viewId">The view identifier to unregister the service for.</param>
        /// <returns>True if successfully unregistered; otherwise, false.</returns>
        bool Unregister(int viewId);
    }
}
