// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using MADE.Networking.Http.Requests;
using MADE.Networking.Http.Requests.Json;
using MADE.Networking.Http.Requests.Streams;

namespace MADE.Networking.Http;

/// <summary>
/// Defines a factory for creating <see cref="NetworkRequest"/> instances using a managed <see cref="HttpClient"/> from <see cref="IHttpClientFactory"/>.
/// </summary>
public class NetworkRequestFactory : INetworkRequestFactory
{
    private readonly IHttpClientFactory httpClientFactory;
    private readonly string? clientName;

    /// <summary>
    /// Initializes a new instance of the <see cref="NetworkRequestFactory"/> class.
    /// </summary>
    /// <param name="httpClientFactory">The <see cref="IHttpClientFactory"/> used to create <see cref="HttpClient"/> instances.</param>
    public NetworkRequestFactory(IHttpClientFactory httpClientFactory)
        : this(httpClientFactory, null)
    {
    }

    private NetworkRequestFactory(IHttpClientFactory httpClientFactory, string? clientName)
    {
        this.httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        this.clientName = clientName;
    }

    /// <inheritdoc/>
    public JsonGetNetworkRequest Get(string url, Dictionary<string, string>? headers = null)
    {
        return new JsonGetNetworkRequest(this.CreateClient(), url, headers!);
    }

    /// <inheritdoc/>
    public JsonPostNetworkRequest Post(string url, string? jsonData = null, Dictionary<string, string>? headers = null)
    {
        return new JsonPostNetworkRequest(this.CreateClient(), url, jsonData!, headers!);
    }

    /// <inheritdoc/>
    public JsonPutNetworkRequest Put(string url, string? jsonData = null, Dictionary<string, string>? headers = null)
    {
        return new JsonPutNetworkRequest(this.CreateClient(), url, jsonData!, headers!);
    }

    /// <inheritdoc/>
    public JsonPatchNetworkRequest Patch(string url, string? jsonData = null, Dictionary<string, string>? headers = null)
    {
        return new JsonPatchNetworkRequest(this.CreateClient(), url, jsonData!, headers!);
    }

    /// <inheritdoc/>
    public JsonDeleteNetworkRequest Delete(string url, Dictionary<string, string>? headers = null)
    {
        return new JsonDeleteNetworkRequest(this.CreateClient(), url, headers!);
    }

    /// <inheritdoc/>
    public StreamGetNetworkRequest GetStream(string url, Dictionary<string, string>? headers = null)
    {
        return new StreamGetNetworkRequest(this.CreateClient(), url, headers!);
    }

    /// <inheritdoc/>
    public MultipartFormDataPostNetworkRequest PostMultipart(string url, Dictionary<string, string>? headers = null)
    {
        return new MultipartFormDataPostNetworkRequest(this.CreateClient(), url, headers!);
    }

    /// <inheritdoc/>
    public INetworkRequestFactory WithClient(string clientName)
    {
        ArgumentNullException.ThrowIfNull(clientName);
        return new NetworkRequestFactory(this.httpClientFactory, clientName);
    }

    private HttpClient CreateClient()
    {
        return this.clientName is not null
            ? this.httpClientFactory.CreateClient(this.clientName)
            : this.httpClientFactory.CreateClient();
    }
}
