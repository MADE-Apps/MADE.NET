// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JsonDeleteNetworkRequest.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines a network request for a DELETE call with a JSON response.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MADE.App.Networking.Requests.Json
{
#if WINDOWS_UWP
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    using Windows.Web.Http;

    using Newtonsoft.Json;

#else
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    using Newtonsoft.Json;
#endif

    /// <summary>
    /// Defines a network request for a DELETE call with a JSON response.
    /// </summary>
    public sealed class JsonDeleteNetworkRequest : NetworkRequest
    {
        private readonly HttpClient client;

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonDeleteNetworkRequest"/> class.
        /// </summary>
        /// <param name="client">
        /// The <see cref="HttpClient"/> for executing the request.
        /// </param>
        /// <param name="url">
        /// The URL for the request.
        /// </param>
        public JsonDeleteNetworkRequest(HttpClient client, string url)
            : this(client, url, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonDeleteNetworkRequest"/> class.
        /// </summary>
        /// <param name="client">
        /// The <see cref="HttpClient"/> for executing the request.
        /// </param>
        /// <param name="url">
        /// The URL for the request.
        /// </param>
        /// <param name="headers">
        /// The additional headers.
        /// </param>
        public JsonDeleteNetworkRequest(HttpClient client, string url, Dictionary<string, string> headers)
            : base(url, headers)
        {
            this.client = client ?? throw new ArgumentNullException(nameof(client));
        }

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
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, uri);

            if (this.Headers != null)
            {
                foreach (KeyValuePair<string, string> header in this.Headers)
                {
                    request.Headers.Add(header.Key, header.Value);
                }
            }

#if WINDOWS_UWP
            HttpResponseMessage response = cts == null
                                               ? await this.client.SendRequestAsync(
                                                     request,
                                                     HttpCompletionOption.ResponseHeadersRead)
                                               : await this.client.SendRequestAsync(
                                                     request,
                                                     HttpCompletionOption.ResponseHeadersRead).AsTask(cts.Token);
#else
            HttpResponseMessage response = cts == null
                                               ? await this.client.SendAsync(
                                                     request,
                                                     HttpCompletionOption.ResponseHeadersRead)
                                               : await this.client.SendAsync(
                                                     request,
                                                     HttpCompletionOption.ResponseHeadersRead,
                                                     cts.Token);
#endif

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}