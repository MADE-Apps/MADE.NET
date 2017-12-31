// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JsonResponseNetworkRequest.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines a model for a request with a JSON response that can be made with an INetworkRequestManager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MADE.Networking.Requests.Json
{
	using System;
	using System.Collections.Generic;
	using System.Net.Http;
	using System.Threading.Tasks;

	using Newtonsoft.Json;

	/// <summary>
	/// Defines a model for a request with a JSON response that can be made with an <see cref="INetworkRequestManager"/>.
	/// </summary>
	public class JsonResponseNetworkRequest : NetworkRequest
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="JsonResponseNetworkRequest"/> class.
		/// </summary>
		/// <param name="url">
		/// The request URL.
		/// </param>
		/// <param name="method">
		/// The HTTP request method.
		/// </param>
		/// <param name="responseType">
		/// The type of response expected from the request.
		/// </param>
		public JsonResponseNetworkRequest(string url, HttpMethod method, Type responseType)
			: this(url, method, null, responseType)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="JsonResponseNetworkRequest"/> class.
		/// </summary>
		/// <param name="url">
		/// The request URL.
		/// </param>
		/// <param name="method">
		/// The HTTP request method.
		/// </param>
		/// <param name="headers">
		/// The additional headers for the request URL.
		/// </param>
		/// <param name="responseType">
		/// The type of response expected from the request.
		/// </param>
		public JsonResponseNetworkRequest(
			string url,
			HttpMethod method,
			Dictionary<string, string> headers,
			Type responseType)
			: base(url, method, headers)
		{
			this.ResponseType = responseType;
		}

		/// <summary>
		/// Gets the type of response expected from the request.
		/// </summary>
		public Type ResponseType { get; }

		/// <summary>
		/// Handles the response from the HTTP request and returns the object as the expected type.
		/// </summary>
		/// <param name="responseContent">
		/// The HTTP response content.
		/// </param>
		/// <returns>
		/// Returns the content of the response after being handled.
		/// </returns>
		public override async Task<object> HandleResponseAsync(HttpContent responseContent)
		{
			if (responseContent == null)
			{
				return null;
			}

			string json = await responseContent.ReadAsStringAsync();
			return string.IsNullOrWhiteSpace(json) ? null : JsonConvert.DeserializeObject(json, this.ResponseType);
		}
	}
}