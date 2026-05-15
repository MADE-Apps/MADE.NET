// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using MADE.Networking.Http.Requests;
using MADE.Networking.Http.Requests.Json;
using MADE.Networking.Http.Requests.Streams;

namespace MADE.Networking.Http;

/// <summary>
/// Defines an interface for creating <see cref="NetworkRequest"/> instances using a managed <see cref="HttpClient"/>.
/// </summary>
public interface INetworkRequestFactory
{
    /// <summary>
    /// Creates a <see cref="JsonGetNetworkRequest"/> for the specified URL.
    /// </summary>
    /// <param name="url">The URL for the request.</param>
    /// <param name="headers">Optional additional headers for the request.</param>
    /// <returns>A configured <see cref="JsonGetNetworkRequest"/>.</returns>
    JsonGetNetworkRequest Get(string url, Dictionary<string, string>? headers = null);

    /// <summary>
    /// Creates a <see cref="JsonPostNetworkRequest"/> for the specified URL.
    /// </summary>
    /// <param name="url">The URL for the request.</param>
    /// <param name="jsonData">The JSON data to post.</param>
    /// <param name="headers">Optional additional headers for the request.</param>
    /// <returns>A configured <see cref="JsonPostNetworkRequest"/>.</returns>
    JsonPostNetworkRequest Post(string url, string? jsonData = null, Dictionary<string, string>? headers = null);

    /// <summary>
    /// Creates a <see cref="JsonPutNetworkRequest"/> for the specified URL.
    /// </summary>
    /// <param name="url">The URL for the request.</param>
    /// <param name="jsonData">The JSON data to put.</param>
    /// <param name="headers">Optional additional headers for the request.</param>
    /// <returns>A configured <see cref="JsonPutNetworkRequest"/>.</returns>
    JsonPutNetworkRequest Put(string url, string? jsonData = null, Dictionary<string, string>? headers = null);

    /// <summary>
    /// Creates a <see cref="JsonPatchNetworkRequest"/> for the specified URL.
    /// </summary>
    /// <param name="url">The URL for the request.</param>
    /// <param name="jsonData">The JSON data to patch.</param>
    /// <param name="headers">Optional additional headers for the request.</param>
    /// <returns>A configured <see cref="JsonPatchNetworkRequest"/>.</returns>
    JsonPatchNetworkRequest Patch(string url, string? jsonData = null, Dictionary<string, string>? headers = null);

    /// <summary>
    /// Creates a <see cref="JsonDeleteNetworkRequest"/> for the specified URL.
    /// </summary>
    /// <param name="url">The URL for the request.</param>
    /// <param name="headers">Optional additional headers for the request.</param>
    /// <returns>A configured <see cref="JsonDeleteNetworkRequest"/>.</returns>
    JsonDeleteNetworkRequest Delete(string url, Dictionary<string, string>? headers = null);

    /// <summary>
    /// Creates a <see cref="StreamGetNetworkRequest"/> for the specified URL.
    /// </summary>
    /// <param name="url">The URL for the request.</param>
    /// <param name="headers">Optional additional headers for the request.</param>
    /// <returns>A configured <see cref="StreamGetNetworkRequest"/>.</returns>
    StreamGetNetworkRequest GetStream(string url, Dictionary<string, string>? headers = null);

    /// <summary>
    /// Creates a <see cref="MultipartFormDataPostNetworkRequest"/> for the specified URL.
    /// </summary>
    /// <param name="url">The URL for the request.</param>
    /// <param name="headers">Optional additional headers for the request.</param>
    /// <returns>A configured <see cref="MultipartFormDataPostNetworkRequest"/>.</returns>
    MultipartFormDataPostNetworkRequest PostMultipart(string url, Dictionary<string, string>? headers = null);

    /// <summary>
    /// Creates a new <see cref="INetworkRequestFactory"/> that uses the specified named <see cref="HttpClient"/>.
    /// </summary>
    /// <param name="clientName">The name of the <see cref="HttpClient"/> to use.</param>
    /// <returns>A new <see cref="INetworkRequestFactory"/> configured with the named client.</returns>
    INetworkRequestFactory WithClient(string clientName);
}
