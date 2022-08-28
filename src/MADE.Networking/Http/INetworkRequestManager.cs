// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Networking.Http
{
    using System;
    using System.Collections.Concurrent;

    using MADE.Networking.Http.Requests;

    /// <summary>
    /// Defines an interface for a network request manager.
    /// </summary>
    public interface INetworkRequestManager
    {
        /// <summary>
        /// Gets the current queue of network requests.
        /// </summary>
        ConcurrentDictionary<string, NetworkRequestCallback> CurrentQueue { get; }

        /// <summary>
        /// Starts the manager processing the queue of network requests at a default time period of 1 minute.
        /// </summary>
        void Start();

        /// <summary>
        /// Starts the manager processing the queue of network requests.
        /// </summary>
        /// <param name="processPeriod">
        /// The time period between each process of the queue.
        /// </param>
        void Start(TimeSpan processPeriod);

        /// <summary>
        /// Stops the processing of the network manager queues.
        /// </summary>
        void Stop();

        /// <summary>
        /// Adds or updates a network request in the queue.
        /// </summary>
        /// <typeparam name="TRequest">
        /// The type of network request.
        /// </typeparam>
        /// <typeparam name="TResponse">
        /// The expected response type.
        /// </typeparam>
        /// <param name="request">
        /// The network request to execute.
        /// </param>
        /// <param name="successCallback">
        /// The action to execute when receiving a successful response.
        /// </param>
        void AddOrUpdate<TRequest, TResponse>(TRequest request, Action<TResponse> successCallback)
            where TRequest : NetworkRequest;

        /// <summary>
        /// Adds or updates a network request in the queue.
        /// </summary>
        /// <typeparam name="TRequest">
        /// The type of network request.
        /// </typeparam>
        /// <typeparam name="TResponse">
        /// The expected response type.
        /// </typeparam>
        /// <typeparam name="TErrorResponse">
        /// The expected error response type.
        /// </typeparam>
        /// <param name="request">
        /// The network request to execute.
        /// </param>
        /// <param name="successCallback">
        /// The action to execute when receiving a successful response.
        /// </param>
        /// <param name="errorCallback">
        /// The action to execute when receiving an error response.
        /// </param>
        void AddOrUpdate<TRequest, TResponse, TErrorResponse>(
            TRequest request,
            Action<TResponse> successCallback,
            Action<TErrorResponse> errorCallback)
            where TRequest : NetworkRequest;

        /// <summary>
        /// Removes a network request from the queue.
        /// <para>
        /// If the request is no longer in the queue, this method does nothing.
        /// </para>
        /// </summary>
        /// <param name="request">The request to remove from the queue.</param>
        void Remove(INetworkRequest request);

        /// <summary>
        /// Removes a network request from the queue by the registered key identifier.
        /// <para>
        /// If the request is no longer in the queue, this method does nothing.
        /// </para>
        /// </summary>
        /// <param name="key">The key corresponding to the network request to remove from the queue.</param>
        void RemoveByKey(string key);

        /// <summary>
        /// Processes the current queue of network requests.
        /// </summary>
        void ProcessCurrentQueue();
    }
}