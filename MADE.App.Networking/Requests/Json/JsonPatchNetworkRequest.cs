// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JsonPatchNetworkRequest.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines a network request for a PATCH call with a JSON response.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

#if WINDOWS_UWP
namespace MADE.App.Networking.Requests.Json
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    using Windows.Storage.Streams;
    using Windows.Web.Http;

    using MADE.App.Networking.Requests;

    using Newtonsoft.Json;

    /// <summary>
    /// Defines a network request for a PATCH call with a JSON response.
    /// </summary>
    public sealed class JsonPatchNetworkRequest : NetworkRequest
    {
        private readonly HttpClient client;

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonPatchNetworkRequest"/> class.
        /// </summary>
        /// <param name="client">
        /// The <see cref="HttpClient"/> for executing the request.
        /// </param>
        /// <param name="url">
        /// The URL for the request.
        /// </param>
        public JsonPatchNetworkRequest(HttpClient client, string url)
            : this(client, url, null, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonPatchNetworkRequest"/> class.
        /// </summary>
        /// <param name="client">
        /// The <see cref="HttpClient"/> for executing the request.
        /// </param>
        /// <param name="url">
        /// The URL for the request.
        /// </param>
        /// <param name="jsonData">
        /// The JSON data to post.
        /// </param>
        public JsonPatchNetworkRequest(HttpClient client, string url, string jsonData)
            : this(client, url, jsonData, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonPatchNetworkRequest"/> class.
        /// </summary>
        /// <param name="client">
        /// The <see cref="HttpClient"/> for executing the request.
        /// </param>
        /// <param name="url">
        /// The URL for the request.
        /// </param>
        /// <param name="jsonData">
        /// The JSON data to post.
        /// </param>
        /// <param name="headers">
        /// The additional headers.
        /// </param>
        public JsonPatchNetworkRequest(
            HttpClient client,
            string url,
            string jsonData,
            Dictionary<string, string> headers)
            : base(url, headers)
        {
            this.client = client ?? throw new ArgumentNullException(nameof(client));
            this.Data = jsonData;
        }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        /// Executes the network request.
        /// </summary>
        /// <param name="cts">
        /// The cancellation token source.
        /// </param>
        /// <typeparam name="TResponse">
        /// The type of object returned from the request.
        /// </typeparam>
        /// <returns>
        /// Returns the response of the request as the specified type.
        /// </returns>
        public override async Task<TResponse> ExecuteAsync<TResponse>(CancellationTokenSource cts = null)
        {
            string json = await this.GetJsonResponse(cts);
            return JsonConvert.DeserializeObject<TResponse>(json);
        }

        /// <summary>
        /// Executes the network request.
        /// </summary>
        /// <param name="expectedResponse">
        /// The type expected by the response of the request.
        /// </param>
        /// <param name="cts">
        /// The cancellation token source.
        /// </param>
        /// <returns>
        /// Returns the response of the request as an object.
        /// </returns>
        public override async Task<object> ExecuteAsync(Type expectedResponse, CancellationTokenSource cts = null)
        {
            string json = await this.GetJsonResponse(cts);
            return JsonConvert.DeserializeObject(json, expectedResponse);
        }

        private async Task<string> GetJsonResponse(CancellationTokenSource cts = null)
        {
            if (this.client == null)
            {
                throw new InvalidOperationException(
                    "No HttpClient has been specified for executing the network request.");
            }

            if (string.IsNullOrWhiteSpace(this.Url))
            {
                throw new InvalidOperationException("No URL has been specified for executing the network request.");
            }

            Uri uri = new Uri(this.Url);

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Patch, uri)
                              {
                                  Content = new HttpStringContent(
                                      this.Data,
                                      UnicodeEncoding.Utf8,
                                      "application/json")
                              };

            if (this.Headers != null)
            {
                foreach (KeyValuePair<string, string> header in this.Headers)
                {
                    request.Headers.Add(header.Key, header.Value);
                }
            }

            HttpResponseMessage response = cts == null
                               ? await this.client.SendRequestAsync(request, HttpCompletionOption.ResponseHeadersRead)
                               : await this.client.SendRequestAsync(request, HttpCompletionOption.ResponseHeadersRead)
                                     .AsTask(cts.Token);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}
#endif