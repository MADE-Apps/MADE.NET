// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DeleteNetworkRequest.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines a model for a DELETE request with a JSON response that can be made with an INetworkRequestManager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MADE.Networking.Requests.Json
{
	using System;
	using System.Collections.Generic;
	using System.Net.Http;

	/// <summary>
	/// Defines a model for a DELETE request with a JSON response that can be made with an <see cref="INetworkRequestManager"/>.
	/// </summary>
	public class DeleteNetworkRequest : JsonResponseNetworkRequest
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="DeleteNetworkRequest"/> class.
		/// </summary>
		/// <param name="url">
		/// The request URL.
		/// </param>
		/// <param name="responseType">
		/// The type of response expected from the request.
		/// </param>
		protected DeleteNetworkRequest(string url, Type responseType)
			: this(url, null, responseType)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DeleteNetworkRequest"/> class.
		/// </summary>
		/// <param name="url">
		/// The request URL.
		/// </param>
		/// <param name="headers">
		/// The additional headers for the request URL.
		/// </param>
		/// <param name="responseType">
		/// The type of response expected from the request.
		/// </param>
		protected DeleteNetworkRequest(string url, Dictionary<string, string> headers, Type responseType)
			: base(url, HttpMethod.Delete, headers, responseType)
		{
		}
	}
}