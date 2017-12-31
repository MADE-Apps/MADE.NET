// --------------------------------------------------------------------------------------------------------------------
// <copyright file="INetworkRequestManager.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines an interface for a HTTP network request manager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MADE.Networking
{
	using System;
	using System.Collections.Concurrent;
	using System.Threading.Tasks;

	using MADE.Networking.Requests;

	/// <summary>
	/// Defines an interface for a HTTP network request manager.
	/// </summary>
	public interface INetworkRequestManager
	{
		/// <summary>
		/// Gets the current queue of network requests.
		/// </summary>
		ConcurrentDictionary<string, NetworkRequestCallback> Queue { get; }

		/// <summary>
		/// Gets or sets the time between each processing of the network requests in the network queue.
		/// </summary>
		TimeSpan QueueProcessPeriod { get; set; }

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
		void AddToQueue<TRequest, TResponse>(TRequest request, Action<TResponse> successCallback)
			where TRequest : NetworkRequest;

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
		void AddToQueue<TRequest, TResponse>(TRequest request, Action<TResponse> successCallback, bool allowRetry)
			where TRequest : NetworkRequest;

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
		void AddToQueue<TRequest, TResponse, TErrorResponse>(
			TRequest request,
			Action<TResponse> successCallback,
			Action<TErrorResponse> errorCallback)
			where TRequest : NetworkRequest;

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
		void AddToQueue<TRequest, TResponse, TErrorResponse>(
			TRequest request,
			Action<TResponse> successCallback,
			Action<TErrorResponse> errorCallback,
			bool allowRetry)
			where TRequest : NetworkRequest;

		/// <summary>
		/// Processes the current network requests in the network queue.
		/// </summary>
		void ProcessQueue();
	}
}