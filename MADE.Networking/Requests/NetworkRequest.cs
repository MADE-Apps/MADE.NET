// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NetworkRequest.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines a base model for a request that can be made with an INetworkRequestManager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MADE.Networking.Requests
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    using MADE.Data.Caching;

    /// <summary>
    /// Defines a base model for a request that can be made with an <see cref="INetworkRequestManager"/>.
    /// </summary>
    public abstract class NetworkRequest : INetworkRequest, IDataCacheSupported
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkRequest"/> class.
        /// </summary>
        /// <param name="url">
        /// The request URL.
        /// </param>
        /// <param name="method">
        /// The HTTP request method.
        /// </param>
        protected NetworkRequest(string url, HttpMethod method)
            : this(url, method, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkRequest"/> class.
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
        protected NetworkRequest(string url, HttpMethod method, Dictionary<string, string> headers)
        {
            this.Id = Guid.NewGuid();
            this.Url = url;
            this.Method = method;
            this.Headers = headers ?? new Dictionary<string, string>();
        }

        /// <summary>
        /// Gets the unique identifier.
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Gets the HTTP request method.
        /// </summary>
        public HttpMethod Method { get; }

        /// <summary>
        /// Gets or sets the request URL.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the content to send as part of the request.
        /// </summary>
        public HttpContent RequestContent { get; set; }

        /// <summary>
        /// Gets the additional headers for the request URL.
        /// </summary>
        public Dictionary<string, string> Headers { get; }

        /// <summary>
        /// Gets or sets the provider for the data caching.
        /// </summary>
        public IDataCacheProvider CacheProvider { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether caching is currently enabled.
        /// </summary>
        public bool IsCachingEnabled { get; set; }

        /// <summary>
        /// Sends the network request using a default <see cref="HttpClient"/>.
        /// </summary>
        /// <typeparam name="TResponse">
        /// The type of response returned from the request.
        /// </typeparam>
        /// <returns>
        /// Returns the response of the request as the expected type.
        /// </returns>
        public Task<TResponse> SendAsync<TResponse>()
        {
            return this.SendAsync<TResponse>(new HttpClient(), null);
        }

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
        public Task<TResponse> SendAsync<TResponse>(CancellationTokenSource cts)
        {
            return this.SendAsync<TResponse>(new HttpClient(), cts);
        }

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
        public Task<TResponse> SendAsync<TResponse>(HttpClient httpClient)
        {
            return this.SendAsync<TResponse>(httpClient, null);
        }

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
        public async Task<TResponse> SendAsync<TResponse>(HttpClient httpClient, CancellationTokenSource cts)
        {
            object response = await this.SendAsync(httpClient, cts);
            return (TResponse)response;
        }

        /// <summary>
        /// Sends the network request using a default <see cref="HttpClient"/>
        /// </summary>
        /// <returns>
        /// Returns the response of the request as an object.
        /// </returns>
        public Task<object> SendAsync()
        {
            return this.SendAsync(new HttpClient(), null);
        }

        /// <summary>
        /// Sends the network request using a default <see cref="HttpClient"/> with an optional given cancellation token.
        /// </summary>
        /// <param name="cts">
        /// The cancellation token source.
        /// </param>
        /// <returns>
        /// Returns the response of the request as an object.
        /// </returns>
        public Task<object> SendAsync(CancellationTokenSource cts)
        {
            return this.SendAsync(new HttpClient(), null);
        }

        /// <summary>
        /// Sends the network request using the given <see cref="HttpClient"/>.
        /// </summary>
        /// <param name="httpClient">
        /// The HTTP client to perform the request with.
        /// </param>
        /// <returns>
        /// Returns the response of the request as an object.
        /// </returns>
        public Task<object> SendAsync(HttpClient httpClient)
        {
            return this.SendAsync(httpClient, null);
        }

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
        public async Task<object> SendAsync(HttpClient httpClient, CancellationTokenSource cts)
        {
            if (httpClient == null || string.IsNullOrWhiteSpace(this.Url))
            {
                return null;
            }

            Uri uri = new Uri(this.Url);
            HttpRequestMessage requestMessage = new HttpRequestMessage(this.Method, uri);

            if (this.RequestContent != null)
            {
                requestMessage.Content = this.RequestContent;
            }

            if (this.Headers != null)
            {
                foreach (KeyValuePair<string, string> header in this.Headers)
                {
                    requestMessage.Headers.Add(header.Key, header.Value);
                }
            }

            HttpResponseMessage responseMessage = null;
            bool useCachedResult = false;

            try
            {
                responseMessage = cts == null
                                      ? await httpClient.SendAsync(requestMessage)
                                      : await httpClient.SendAsync(requestMessage, cts.Token);

                responseMessage.EnsureSuccessStatusCode();
            }
            catch (Exception)
            {
                if (!this.IsCachingEnabled)
                {
                    throw;
                }

                useCachedResult = true;
            }

            object response;

            if (!useCachedResult)
            {
                response = await this.HandleResponseAsync(responseMessage.Content);

                if (this.IsCachingEnabled)
                {
                    this.CacheProvider?.AddOrUpdate($"{this.Method}_{this.Url}", response);
                }
            }
            else
            {
                response = this.CacheProvider?.Get<object>($"{this.Method}_{this.Url}");
            }

            return response;
        }

        /// <summary>
        /// Handles the response from the HTTP request and returns the expected type. The <see cref="NetworkRequest"/> class always returns the content as a string.
        /// </summary>
        /// <param name="responseContent">
        /// The HTTP response content.
        /// </param>
        /// <returns>
        /// Returns the content of the response after being handled.
        /// </returns>
        public abstract Task<object> HandleResponseAsync(HttpContent responseContent);
    }
}