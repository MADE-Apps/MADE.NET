// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.UI.ViewManagement
{
    using System;
    using System.Collections.Generic;
    using Windows.UI.ViewManagement;

    /// <summary>
    /// Defines a generic base for managing view based services, such as CoreDispatchers.
    /// </summary>
    /// <typeparam name="TService">The type of service registered.</typeparam>
    public class ViewServiceManager<TService> : IViewServiceManager<TService>
    {
        private static IViewServiceManager<TService> current;

        private readonly Dictionary<int, TService> services;

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewServiceManager{TService}"/> class.
        /// </summary>
        public ViewServiceManager()
        {
            this.services = new Dictionary<int, TService>();
        }

        /// <summary>
        /// Occurs when a view's service has changed.
        /// </summary>
        public event ViewServiceChangedEventHandler<TService> ServiceChanged;

        /// <summary>
        /// Gets an instance of the <see cref="IViewServiceManager{TService}"/>.
        /// </summary>
        public static IViewServiceManager<TService> Current => current ??= new ViewServiceManager<TService>();

        /// <summary>
        /// Gets the collection of registered view services.
        /// </summary>
        public IReadOnlyDictionary<int, TService> Services => this.services;

        /// <summary>
        /// Registers or updates a service for the current application view.
        /// </summary>
        /// <param name="service">The service to register or update.</param>
        /// <returns>The registered service.</returns>
        public static TService RegisterOrUpdateForCurrentView(TService service)
        {
            int viewId = ApplicationView.GetForCurrentView()?.Id ?? -1;
            return Current.RegisterOrUpdate(viewId, service);
        }

        /// <summary>
        /// Retrieves a service for the current application view.
        /// </summary>
        /// <returns>The registered service if exists; otherwise, null.</returns>
        public static TService GetForCurrentView()
        {
            int viewId = ApplicationView.GetForCurrentView()?.Id ?? -1;
            return Current.Get(viewId);
        }

        /// <summary>
        /// Unregisters a service for the current application view if exists.
        /// </summary>
        /// <returns>True if successfully unregistered; otherwise, false.</returns>
        public static bool UnregisterForCurrentView()
        {
            int viewId = ApplicationView.GetForCurrentView()?.Id ?? -1;
            return Current.Unregister(viewId);
        }

        /// <summary>
        /// Registers or updates a service for the specified <paramref name="viewId"/>.
        /// </summary>
        /// <param name="viewId">The view identifier.</param>
        /// <param name="service">The service to register or update.</param>
        /// <returns>The registered service.</returns>
        public TService RegisterOrUpdate(int viewId, TService service)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            bool exists = this.services.ContainsKey(viewId);
            if (exists)
            {
                TService existing = this.services[viewId];
                if (existing.Equals(service))
                {
                    return existing;
                }

                this.services.Remove(viewId);
                this.services.Add(viewId, service);

                this.ServiceChanged?.Invoke(this, new ViewServiceChangedEventArgs<TService>(viewId, service));
            }
            else
            {
                this.services.Add(viewId, service);
            }

            return service;
        }

        /// <summary>
        /// Retrieves a service for the specified <paramref name="viewId"/>.
        /// </summary>
        /// <param name="viewId">The view identifier to retrieve the service for.</param>
        /// <returns>The registered service if exists; otherwise, null.</returns>
        public TService Get(int viewId)
        {
            bool exists = this.services.ContainsKey(viewId);
            return !exists ? default : this.services[viewId];
        }

        /// <summary>
        /// Unregisters a service for the specified <paramref name="viewId"/> if exists.
        /// </summary>
        /// <param name="viewId">The view identifier to unregister the service for.</param>
        /// <returns>True if successfully unregistered; otherwise, false.</returns>
        public bool Unregister(int viewId)
        {
            bool exists = this.services.ContainsKey(viewId);
            return exists && this.services.Remove(viewId);
        }
    }
}
