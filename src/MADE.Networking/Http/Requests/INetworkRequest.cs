// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Networking.Http.Requests
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines an interface for a basic network request.
    /// </summary>
    public interface INetworkRequest
    {
        /// <summary>
        /// Gets the identifier for the request.
        /// </summary>
        Guid Identifier { get; }

        /// <summary>
        /// Gets or sets the URL for the request.
        /// </summary>
        string Url { get; set; }

        /// <summary>
        /// Gets the headers for the request.
        /// </summary>
        Dictionary<string, string> Headers { get; }

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
        Task<TResponse> ExecuteAsync<TResponse>(CancellationToken cancellationToken = default);

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
        Task<object> ExecuteAsync(Type expectedResponse, CancellationToken cancellationToken = default);
    }
}