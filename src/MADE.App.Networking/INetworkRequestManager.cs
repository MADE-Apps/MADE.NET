// --------------------------------------------------------------------------------------------------------------------
// <copyright file="INetworkRequestManager.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines an interface for a network request manager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MADE.App.Networking
{
    using System;
    using System.Collections.Concurrent;

    using MADE.App.Networking.Requests;

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
        /// Processes the current queue of network requests.
        /// </summary>
        void ProcessCurrentQueue();
    }
}