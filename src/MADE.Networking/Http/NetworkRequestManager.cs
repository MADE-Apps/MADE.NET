// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Networking.Http
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using MADE.Networking.Http.Requests;
    using MADE.Runtime;

    using Timer = MADE.Threading.Timer;

    /// <summary>
    /// Defines a manager for executing queued network requests.
    /// </summary>
    public sealed class NetworkRequestManager : INetworkRequestManager
    {
        private readonly Timer processTimer;

        private bool isProcessingRequests;

        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkRequestManager"/> class.
        /// </summary>
        public NetworkRequestManager()
        {
            this.CurrentQueue = new ConcurrentDictionary<string, NetworkRequestCallback>();
            this.processTimer = new Timer();
            this.processTimer.Tick += this.OnProcessTimerTick;
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
            this.processTimer.Interval = processPeriod;
            this.processTimer.Start();
        }

        /// <summary>
        /// Stops the processing of the network manager queues.
        /// </summary>
        public void Stop()
        {
            this.processTimer.Stop();
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
                var cts = new CancellationTokenSource();
                var requestTasks = new List<Task>();
                var requestCallbacks = new List<NetworkRequestCallback>();

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
                    requestTasks.Add(ExecuteRequestsAsync(this.CurrentQueue, container, cts.Token));
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
        /// <exception cref="T:System.Exception">The <paramref name="successCallback"/> throws an exception acquiring method info.</exception>
        /// <exception cref="T:System.OverflowException">The <see cref="CurrentQueue"/> already contains the maximum number of elements (<see cref="F:System.Int32.MaxValue"></see>).</exception>
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
        /// <exception cref="T:System.Exception">The <paramref name="successCallback"/> or <paramref name="errorCallback"/> throws an exception acquiring method info.</exception>
        /// <exception cref="T:System.OverflowException">The <see cref="CurrentQueue"/> already contains the maximum number of elements (<see cref="F:System.Int32.MaxValue"></see>).</exception>
        public void AddOrUpdate<TRequest, TResponse, TErrorResponse>(
            TRequest request,
            Action<TResponse> successCallback,
            Action<TErrorResponse> errorCallback)
            where TRequest : NetworkRequest
        {
            var weakSuccessCallback = new WeakReferenceCallback(successCallback, typeof(TResponse));
            var weakErrorCallback = new WeakReferenceCallback(errorCallback, typeof(TErrorResponse));
            var requestCallback = new NetworkRequestCallback(request, weakSuccessCallback, weakErrorCallback);

            this.CurrentQueue.AddOrUpdate(
                request.Identifier.ToString(),
                requestCallback,
                (s, callback) => requestCallback);
        }

        private static async Task ExecuteRequestsAsync(
            ConcurrentDictionary<string, NetworkRequestCallback> queue,
            NetworkRequestCallback requestCallback,
            CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
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
                object response = await request.ExecuteAsync(successCallback.Type, cancellationToken);
                successCallback.Invoke(response);
            }
            catch (Exception ex)
            {
                successCallback.Invoke(Activator.CreateInstance(successCallback.Type));
                errorCallback.Invoke(ex);
            }
        }

        private void OnProcessTimerTick(object sender, object e)
        {
            this.ProcessCurrentQueue();
        }
    }
}