// --------------------------------------------------------------------------------------------------------------------
// <copyright file="INetworkRequest.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines an interface for a request that cane be made with an INetworkRequestManager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MADE.Networking.Requests
{
	using System;
	using System.Collections.Generic;
	using System.Net.Http;
	using System.Threading;
	using System.Threading.Tasks;

	/// <summary>
	/// Defines an interface for a request that cane be made with an <see cref="INetworkRequestManager"/>.
	/// </summary>
	internal interface INetworkRequest
	{
		/// <summary>
		/// Gets the unique identifier.
		/// </summary>
		Guid Id { get; }

		/// <summary>
		/// Gets the HTTP request method.
		/// </summary>
		HttpMethod Method { get; }

		/// <summary>
		/// Gets or sets the request URL.
		/// </summary>
		string Url { get; set; }

		/// <summary>
		/// Gets the additional headers for the request URL.
		/// </summary>
		Dictionary<string, string> Headers { get; }

		/// <summary>
		/// Sends the network request using a default <see cref="HttpClient"/>.
		/// </summary>
		/// <typeparam name="TResponse">
		/// The type of response returned from the request.
		/// </typeparam>
		/// <returns>
		/// Returns the response of the request as the expected type.
		/// </returns>
		Task<TResponse> SendAsync<TResponse>();

		/// <summary>
		/// Sends the network request using a default <see cref="HttpClient"/> with an optional given cancellation token.
		/// </summary>
		/// <param name="cts">
		/// The cancellation token source.
		/// </param>
		/// <typeparam name="TResponse">
		/// The type of response returned from the request.
		/// </typeparam>
		/// <returns>
		/// Returns the response of the request as the expected type.
		/// </returns>
		Task<TResponse> SendAsync<TResponse>(CancellationTokenSource cts);

		/// <summary>
		/// Sends the network request using the given <see cref="HttpClient"/>.
		/// </summary>
		/// <param name="httpClient">
		/// The HTTP client to perform the request with.
		/// </param>
		/// <typeparam name="TResponse">
		/// The type of response returned from the request.
		/// </typeparam>
		/// <returns>
		/// Returns the response of the request as the expected type.
		/// </returns>
		Task<TResponse> SendAsync<TResponse>(HttpClient httpClient);

		/// <summary>
		/// Sends the network request using the given <see cref="HttpClient"/> with an optional given cancellation token..
		/// </summary>
		/// <param name="httpClient">
		/// The HTTP client to perform the request with.
		/// </param>
		/// <param name="cts">
		/// The cancellation token source.
		/// </param>
		/// <typeparam name="TResponse">
		/// The type of response returned from the request.
		/// </typeparam>
		/// <returns>
		/// Returns the response of the request as the expected type.
		/// </returns>
		Task<TResponse> SendAsync<TResponse>(HttpClient httpClient, CancellationTokenSource cts);

		/// <summary>
		/// Sends the network request using a default <see cref="HttpClient"/>
		/// </summary>
		/// <returns>
		/// Returns the response of the request as an object.
		/// </returns>
		Task<object> SendAsync();

		/// <summary>
		/// Sends the network request using a default <see cref="HttpClient"/> with an optional given cancellation token.
		/// </summary>
		/// <param name="cts">
		/// The cancellation token source.
		/// </param>
		/// <returns>
		/// Returns the response of the request as an object.
		/// </returns>
		Task<object> SendAsync(CancellationTokenSource cts);

		/// <summary>
		/// Sends the network request using the given <see cref="HttpClient"/>.
		/// </summary>
		/// <param name="httpClient">
		/// The HTTP client to perform the request with.
		/// </param>
		/// <returns>
		/// Returns the response of the request as an object.
		/// </returns>
		Task<object> SendAsync(HttpClient httpClient);

		/// <summary>
		/// Sends the network request using the given <see cref="HttpClient"/> with an optional given cancellation token.
		/// </summary>
		/// <param name="httpClient">
		/// The HTTP client to perform the request with.
		/// </param>
		/// <param name="cts">
		/// The cancellation token source.
		/// </param>
		/// <returns>
		/// Returns the response of the request as an object.
		/// </returns>
		Task<object> SendAsync(HttpClient httpClient, CancellationTokenSource cts);
	}
}