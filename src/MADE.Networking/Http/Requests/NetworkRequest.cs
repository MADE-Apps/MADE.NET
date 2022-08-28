// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Networking.Http.Requests
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the model for a network request.
    /// </summary>
    public abstract class NetworkRequest : INetworkRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkRequest"/> class.
        /// </summary>
        /// <param name="url">
        /// The URL for the request.
        /// </param>
        protected NetworkRequest(string url)
            : this(url, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkRequest"/> class.
        /// </summary>
        /// <param name="url">
        /// The URL for the request.
        /// </param>
        /// <param name="headers">
        /// Additional headers for the request.
        /// </param>
        protected NetworkRequest(string url, Dictionary<string, string> headers)
        {
            this.Identifier = Guid.NewGuid();
            this.Url = url;
            this.Headers = headers ?? new Dictionary<string, string>();
        }

        /// <summary>
        /// Gets the identifier for the request.
        /// </summary>
        public Guid Identifier { get; }

        /// <summary>
        /// Gets or sets the URL for the request.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets the headers for the request.
        /// </summary>
        public Dictionary<string, string> Headers { get; }

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
        public abstract Task<TResponse> ExecuteAsync<TResponse>(CancellationToken cancellationToken = default);

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
        public abstract Task<object> ExecuteAsync(Type expectedResponse, CancellationToken cancellationToken = default);
    }
}