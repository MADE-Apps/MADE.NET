// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NetworkRequestManager.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines a manager for executing queued network requests.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MADE.App.Networking
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using MADE.App.Networking.Requests;

    /// <summary>
    /// Defines a manager for executing queued network requests.
    /// </summary>
    public sealed class NetworkRequestManager : INetworkRequestManager
    {
#if WINDOWS_UWP
        private readonly Windows.UI.Xaml.DispatcherTimer processTimer;
#else
        private Timer processTimer;
#endif

        private bool isProcessingRequests;

        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkRequestManager"/> class.
        /// </summary>
        public NetworkRequestManager()
        {
            this.CurrentQueue = new ConcurrentDictionary<string, NetworkRequestCallback>();

#if WINDOWS_UWP
            this.processTimer = new Windows.UI.Xaml.DispatcherTimer { Interval = TimeSpan.FromMinutes(1) };
            this.processTimer.Tick += (sender, o) => this.ProcessCurrentQueue();
#endif
        }

        /// <summary>
        /// Gets the current queue of network requests.
        /// </summary>
        public ConcurrentDictionary<string, NetworkRequestCallback> CurrentQueue { get; }

        /// <summary>
        /// Starts the manager processing the queue of network requests at a default time period of 1 minute.
        /// </summary>
        public void Start()
        {
            this.Start(TimeSpan.FromMinutes(1));
        }

        /// <summary>
        /// Starts the manager processing the queue of network requests.
        /// </summary>
        /// <param name="processPeriod">
        /// The time period between each process of the queue.
        /// </param>
        public void Start(TimeSpan processPeriod)
        {
#if WINDOWS_UWP
            this.processTimer.Interval = processPeriod;

            if (!this.processTimer.IsEnabled)
            {
                this.processTimer.Start();
            }
#else
            if (this.processTimer == null)
            {
                this.processTimer = new Timer(
                    state => this.ProcessCurrentQueue(),
                    null,
                    TimeSpan.FromMinutes(0),
                    processPeriod);
            }
            else
            {
                this.processTimer.Change(TimeSpan.FromMinutes(0), processPeriod);
            }
#endif
        }

        /// <summary>
        /// Stops the processing of the network manager queues.
        /// </summary>
        public void Stop()
        {
#if WINDOWS_UWP
            if (this.processTimer.IsEnabled)
            {
                this.processTimer.Stop();
            }
#else
            this.processTimer?.Change(Timeout.InfiniteTimeSpan, Timeout.InfiniteTimeSpan);
#endif
        }

        /// <summary>
        /// Processes the current queue of network requests.
        /// </summary>
        public void ProcessCurrentQueue()
        {
            if (this.isProcessingRequests)
            {
                return;
            }

            if (this.CurrentQueue.Count > 0)
            {
                return;
            }

            this.isProcessingRequests = true;

            try
            {
                CancellationTokenSource cts = new CancellationTokenSource();
                List<Task> requestTasks = new List<Task>();
                List<NetworkRequestCallback> requestCallbacks = new List<NetworkRequestCallback>();

                while (this.CurrentQueue.Count > 0)
                {
                    if (this.CurrentQueue.TryRemove(
                        this.CurrentQueue.FirstOrDefault().Key,
                        out NetworkRequestCallback request))
                    {
                        requestCallbacks.Add(request);
                    }
                }

                foreach (NetworkRequestCallback container in requestCallbacks)
                {
                    requestTasks.Add(ExecuteRequestsAsync(this.CurrentQueue, container, cts));
                }
            }
            finally
            {
                this.isProcessingRequests = false;
            }
        }

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
        public void AddOrUpdate<TRequest, TResponse>(TRequest request, Action<TResponse> successCallback)
            where TRequest : NetworkRequest
        {
            this.AddOrUpdate<TRequest, TResponse, Exception>(request, successCallback, null);
        }

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
        public void AddOrUpdate<TRequest, TResponse, TErrorResponse>(
            TRequest request,
            Action<TResponse> successCallback,
            Action<TErrorResponse> errorCallback)
            where TRequest : NetworkRequest
        {
            WeakReferenceCallback weakSuccessCallback = new WeakReferenceCallback(successCallback, typeof(TResponse));
            WeakReferenceCallback weakErrorCallback = new WeakReferenceCallback(errorCallback, typeof(TErrorResponse));
            NetworkRequestCallback requestCallback = new NetworkRequestCallback(
                request,
                weakSuccessCallback,
                weakErrorCallback);

            this.CurrentQueue.AddOrUpdate(
                request.Identifier.ToString(),
                requestCallback,
                (s, callback) => requestCallback);
        }

        private static async Task ExecuteRequestsAsync(
            ConcurrentDictionary<string, NetworkRequestCallback> queue,
            NetworkRequestCallback requestCallback,
            CancellationTokenSource cts)
        {
            if (cts.IsCancellationRequested)
            {
                queue.AddOrUpdate(
                    requestCallback.Request.Identifier.ToString(),
                    requestCallback,
                    (s, callback) => requestCallback);

                return;
            }

            NetworkRequest request = requestCallback.Request;
            WeakReferenceCallback successCallback = requestCallback.SuccessCallback;
            WeakReferenceCallback errorCallback = requestCallback.ErrorCallback;

            try
            {
                object response = await request.ExecuteAsync(successCallback.Type);
                successCallback.Invoke(response);
            }
            catch (Exception ex)
            {
                successCallback.Invoke(Activator.CreateInstance(successCallback.Type));
                errorCallback.Invoke(ex);
            }
        }
    }
}