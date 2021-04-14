// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.UI.ViewManagement
{
    using System;

    /// <summary>
    /// Defines an event argument for a changed view service.
    /// </summary>
    /// <typeparam name="TService">The type of service.</typeparam>
    public class ViewServiceChangedEventArgs<TService> : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ViewServiceChangedEventArgs{TService}"/> class.
        /// </summary>
        /// <param name="viewId">
        /// The view identifier for the changed service.
        /// </param>
        /// <param name="service">
        /// The service that has changed.
        /// </param>
        public ViewServiceChangedEventArgs(int viewId, TService service)
        {
            this.ViewId = viewId;
            this.Service = service;
        }

        /// <summary>
        /// Gets the view identifier for the changed service.
        /// </summary>
        public int ViewId { get; }

        /// <summary>
        /// Gets the service that has changed.
        /// </summary>
        public TService Service { get; }
    }
}
