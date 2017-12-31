// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetNetworkRequest.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines a model for a GET request with a JSON response that can be made with an <see cref="INetworkRequestManager" />.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MADE.Networking.Requests.Json
{
	using System;
	using System.Collections.Generic;
	using System.Net.Http;

	/// <summary>
	/// Defines a model for a GET request with a JSON response that can be made with an <see cref="INetworkRequestManager"/>.
	/// </summary>
	public class GetNetworkRequest : JsonResponseNetworkRequest
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="GetNetworkRequest"/> class.
		/// </summary>
		/// <param name="url">
		/// The request URL.
		/// </param>
		/// <param name="responseType">
		/// The type of response expected from the request.
		/// </param>
		public GetNetworkRequest(string url, Type responseType)
			: this(url, null, responseType)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="GetNetworkRequest"/> class.
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
		public GetNetworkRequest(string url, Dictionary<string, string> headers, Type responseType)
			: base(url, HttpMethod.Get, headers, responseType)
		{
		}
	}
}