// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Networking.Http.Requests.Json
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

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
        /// <typeparam name="TResponse">
        /// The type of object returned from the request.
        /// </typeparam>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// Returns the response of the request as the specified type.
        /// </returns>
        public override async Task<TResponse> ExecuteAsync<TResponse>(CancellationToken cancellationToken = default)
        {
            string json = await this.GetJsonResponse(cancellationToken);
            return JsonConvert.DeserializeObject<TResponse>(json);
        }

        /// <summary>
        /// Executes the network request.
        /// </summary>
        /// <param name="expectedResponse">
        /// The type expected by the response of the request.
        /// </param>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// Returns the response of the request as an object.
        /// </returns>
        public override async Task<object> ExecuteAsync(
            Type expectedResponse,
            CancellationToken cancellationToken = default)
        {
            string json = await this.GetJsonResponse(cancellationToken);
            return JsonConvert.DeserializeObject(json, expectedResponse);
        }

        private async Task<string> GetJsonResponse(CancellationToken cancellationToken = default)
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

            var uri = new Uri(this.Url);

            var request = new HttpRequestMessage
            {
                Method = new HttpMethod("PATCH"),
                RequestUri = uri,
                Content = new StringContent(this.Data, Encoding.UTF8, "application/json"),
            };

            if (this.Headers != null)
            {
                foreach (KeyValuePair<string, string> header in this.Headers)
                {
                    request.Headers.Add(header.Key, header.Value);
                }
            }

            HttpResponseMessage response = await this.client.SendAsync(
                                               request,
                                               HttpCompletionOption.ResponseHeadersRead,
                                               cancellationToken);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}