// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NetworkRequestManager.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines a manager for HTTP network requests which supports request caching.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MADE.Networking
{
	using System;
	using System.Collections.Concurrent;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading;
	using System.Threading.Tasks;

	using MADE.Common.Actions;
	using MADE.Common.Dependency;
	using MADE.Data.Caching;
	using MADE.Data.Caching.FileSystem;
	using MADE.Networking.Requests;

	/// <summary>
	/// Defines a manager for HTTP network requests which supports request caching.
	/// </summary>
	public class NetworkRequestManager : INetworkRequestManager, IDataCacheSupported, IFileDataCacheInfo, IDataCacheInfo
	{
		private const string NetworkCacheKey = "NetworkCache";

		private bool isProcessing;

		private Timer processTimer;

		private TimeSpan queueProcessPeriod = TimeSpan.FromSeconds(30);

		/// <summary>
		/// Initializes a new instance of the <see cref="NetworkRequestManager"/> class.
		/// </summary>
		public NetworkRequestManager()
		{
			this.Queue = new ConcurrentDictionary<string, NetworkRequestCallback>();
		}

		/// <summary>
		/// Gets or sets the name of the folder storing application data.
		/// </summary>
		public string ApplicationFolderName { get; set; } = FileDataCacheProvider.DefaultApplicationFolderName;

		/// <summary>
		/// Gets or sets the name of the folder storing the cached data.
		/// </summary>
		public string CacheFolderName { get; set; } = NetworkCacheKey;

		/// <summary>
		/// Gets or sets the number of days from the current day (UTC) to weed cached data from. Default is 0 (never).
		/// </summary>
		public int DaysToWeedCache { get; set; } = 0;

		/// <summary>
		/// Gets the provider for the data caching.
		/// </summary>
		public IDataCacheProvider CacheProvider { get; private set; }

		/// <summary>
		/// Gets or sets a value indicating whether caching is currently enabled.
		/// </summary>
		public bool IsCachingEnabled { get; set; } = false;

		/// <summary>
		/// Gets the current queue of network requests.
		/// </summary>
		public ConcurrentDictionary<string, NetworkRequestCallback> Queue { get; }

		/// <summary>
		/// Gets or sets the time between each processing of the network requests in the network queue. Default is every 30 seconds.
		/// </summary>
		public TimeSpan QueueProcessPeriod
		{
			get => this.queueProcessPeriod;
			set
			{
				this.queueProcessPeriod = value;
				this.UpdateQueueProcessing();
			}
		}

	    /// <summary>
	    /// Starts the network request manager processing the requests added to the queue.
	    /// </summary>
	    public void StartProcessing()
	    {
	        this.CacheProvider = this.GetNetworkDataCacheProvider();
	        this.CacheProvider.Weed(this.DaysToWeedCache); // Cleans up older cached data.

	        this.UpdateQueueProcessing();
	    }

        /// <summary>
        /// Adds a network request to the current queue.
        /// </summary>
        /// <param name="request">
        /// The network request to add.
        /// </param>
        /// <param name="successCallback">
        /// The callback for if the network request was successful.
        /// </param>
        /// <typeparam name="TRequest">
        /// The type of network request.
        /// </typeparam>
        /// <typeparam name="TResponse">
        /// The type of the expected response from the network request.
        /// </typeparam>
        public void AddToQueue<TRequest, TResponse>(TRequest request, Action<TResponse> successCallback)
			where TRequest : NetworkRequest
		{
			this.AddToQueue<TRequest, TResponse, Exception>(request, successCallback, null, false);
		}

		/// <summary>
		/// Adds a network request to the current queue.
		/// </summary>
		/// <param name="request">
		/// The network request to add.
		/// </param>
		/// <param name="successCallback">
		/// The callback for if the network request was successful.
		/// </param>
		/// <param name="allowRetry">
		/// A value indicating whether to allow the network request to be retried if it is unsuccessful.
		/// </param>
		/// <typeparam name="TRequest">
		/// The type of network request.
		/// </typeparam>
		/// <typeparam name="TResponse">
		/// The type of the expected response from the network request.
		/// </typeparam>
		public void AddToQueue<TRequest, TResponse>(TRequest request, Action<TResponse> successCallback, bool allowRetry)
			where TRequest : NetworkRequest
		{
			this.AddToQueue<TRequest, TResponse, Exception>(request, successCallback, null, allowRetry);
		}

		/// <summary>
		/// Adds a network request to the current queue.
		/// </summary>
		/// <param name="request">
		/// The network request to add.
		/// </param>
		/// <param name="successCallback">
		/// The callback for if the network request was successful.
		/// </param>
		/// <param name="errorCallback">
		/// The callback for if the network request was unsuccessful.
		/// </param>
		/// <typeparam name="TRequest">
		/// The type of network request.
		/// </typeparam>
		/// <typeparam name="TResponse">
		/// The type of the expected response from the network request if it was successful.
		/// </typeparam>
		/// <typeparam name="TErrorResponse">
		/// The type of the expected response from the network request if it was unsuccessful.
		/// </typeparam>
		public void AddToQueue<TRequest, TResponse, TErrorResponse>(
			TRequest request,
			Action<TResponse> successCallback,
			Action<TErrorResponse> errorCallback)
			where TRequest : NetworkRequest
		{
			this.AddToQueue(request, successCallback, errorCallback, false);
		}

		/// <summary>
		/// Adds a network request to the current queue.
		/// </summary>
		/// <param name="request">
		/// The network request to add.
		/// </param>
		/// <param name="successCallback">
		/// The callback for if the network request was successful.
		/// </param>
		/// <param name="errorCallback">
		/// The callback for if the network request was unsuccessful.
		/// </param>
		/// <param name="allowRetry">
		/// A value indicating whether to allow the network request to be retried if it is unsuccessful.
		/// </param>
		/// <typeparam name="TRequest">
		/// The type of network request.
		/// </typeparam>
		/// <typeparam name="TResponse">
		/// The type of the expected response from the network request if it was successful.
		/// </typeparam>
		/// <typeparam name="TErrorResponse">
		/// The type of the expected response from the network request if it was unsuccessful.
		/// </typeparam>
		public void AddToQueue<TRequest, TResponse, TErrorResponse>(
			TRequest request,
			Action<TResponse> successCallback,
			Action<TErrorResponse> errorCallback,
			bool allowRetry)
			where TRequest : NetworkRequest
		{
			if (request == null)
			{
				return;
			}

			if (request.CacheProvider == null)
			{
				request.CacheProvider = this.CacheProvider;
			}

			WeakCallback successWeakCallback = null;
			if (successCallback != null)
			{
				successWeakCallback = new WeakCallback(successCallback, typeof(TResponse));
			}

			WeakCallback errorWeakCallback = null;
			if (errorCallback != null)
			{
				errorWeakCallback = new WeakCallback(errorCallback, typeof(TErrorResponse));
			}

			NetworkRequestCallback networkRequestCallback =
				new NetworkRequestCallback(request, successWeakCallback, errorWeakCallback) { AllowRetry = allowRetry };

			this.Queue.AddOrUpdate(networkRequestCallback);
		}

		/// <summary>
		/// Processes the current network requests in the network queue.
		/// </summary>
		public void ProcessQueue()
		{
			if (this.isProcessing)
			{
				return;
			}

			this.isProcessing = true;

			try
			{
				List<Task> requestTasks = new List<Task>();
				List<NetworkRequestCallback> requestCallbacks = new List<NetworkRequestCallback>();

				while (this.Queue.Count > 0)
				{
					if (this.Queue.TryRemove(this.Queue.FirstOrDefault().Key, out NetworkRequestCallback requestCallback))
					{
						requestCallbacks.Add(requestCallback);
					}
				}

				foreach (NetworkRequestCallback requestCallback in requestCallbacks)
				{
					requestTasks.Add(this.SendRequestAsync(requestCallback, null));
				}
			}
			finally
			{
				this.isProcessing = false;
			}
		}

		private void UpdateQueueProcessing()
		{
			if (this.processTimer == null)
			{
				this.processTimer = new Timer(state => this.ProcessQueue(), null, TimeSpan.FromMinutes(0), this.QueueProcessPeriod);
			}
			else
			{
				this.processTimer.Change(TimeSpan.FromMinutes(0), this.QueueProcessPeriod);
			}
		}

		private async Task SendRequestAsync(NetworkRequestCallback networkRequestCallback, CancellationTokenSource cts)
		{
			if (networkRequestCallback == null)
			{
				return;
			}

			if (cts != null && cts.IsCancellationRequested)
			{
				this.Queue.AddOrUpdate(networkRequestCallback);
				return;
			}

			try
			{
				object response = await networkRequestCallback.Request.SendAsync(cts);
				networkRequestCallback.SuccessCallback?.Invoke(response);
			}
			catch (Exception ex)
			{
				networkRequestCallback.SuccessCallback?.Invoke(null);
				networkRequestCallback.ErrorCallback?.Invoke(ex);

				if (networkRequestCallback.AllowRetry)
				{
					networkRequestCallback.OnRetry(); // Called to prevent a further retry.
					this.Queue.AddOrUpdate(networkRequestCallback);
				}
			}
		}

		private IDataCacheProvider GetNetworkDataCacheProvider()
		{
			if (!SimpleDependencyService.Instance.IsRegistered<IDataCacheProvider>(NetworkCacheKey))
			{
			    SimpleDependencyService.Instance.Register<IDataCacheProvider>(
			        NetworkCacheKey,
			        () => new FileDataCacheProvider(this.ApplicationFolderName, this.CacheFolderName));
			}

			return SimpleDependencyService.Instance.GetInstance<IDataCacheProvider>(NetworkCacheKey);
		}
	}
}