// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JsonPostNetworkRequest.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines a network request for a POST call with a JSON response.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MADE.App.Networking.Requests.Json
{
#if WINDOWS_UWP
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    using Windows.Storage.Streams;
    using Windows.Web.Http;

    using Newtonsoft.Json;

#else
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    using Newtonsoft.Json;

#endif

    /// <summary>
    /// Defines a network request for a POST call with a JSON response.
    /// </summary>
    public sealed class JsonPostNetworkRequest : NetworkRequest
    {
        private readonly HttpClient client;

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonPostNetworkRequest"/> class.
        /// </summary>
        /// <param name="client">
        /// The <see cref="HttpClient"/> for executing the request.
        /// </param>
        /// <param name="url">
        /// The URL for the request.
        /// </param>
        public JsonPostNetworkRequest(HttpClient client, string url)
            : this(client, url, null, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonPostNetworkRequest"/> class.
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
        public JsonPostNetworkRequest(HttpClient client, string url, string jsonData)
            : this(client, url, jsonData, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonPostNetworkRequest"/> class.
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
        public JsonPostNetworkRequest(
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

#if WINDOWS_UWP

            var request = new HttpRequestMessage(HttpMethod.Post, uri)
                              {
                                  Content =
                                      new HttpStringContent(
                                          this.Data,
                                          UnicodeEncoding.Utf8,
                                          "application/json")
                              };
#else
            HttpRequestMessage request =
                new HttpRequestMessage(HttpMethod.Post, uri)
                    {
                        Content = new StringContent(
                            this.Data,
                            Encoding.UTF8,
                            "application/json")
                    };
#endif

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