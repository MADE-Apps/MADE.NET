// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Net.Http;
using System.Text.Json;

namespace MADE.Networking.Http.Requests;

/// <summary>
/// Defines a network request for a POST call with multipart form data content.
/// </summary>
public sealed class MultipartFormDataPostNetworkRequest : NetworkRequest
{
    private readonly HttpClient client;

    /// <summary>
    /// Initializes a new instance of the <see cref="MultipartFormDataPostNetworkRequest"/> class.
    /// </summary>
    /// <param name="client">The <see cref="HttpClient"/> for executing the request.</param>
    /// <param name="url">The URL for the request.</param>
    public MultipartFormDataPostNetworkRequest(HttpClient client, string url)
        : this(client, url, null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MultipartFormDataPostNetworkRequest"/> class.
    /// </summary>
    /// <param name="client">The <see cref="HttpClient"/> for executing the request.</param>
    /// <param name="url">The URL for the request.</param>
    /// <param name="headers">The additional headers.</param>
    public MultipartFormDataPostNetworkRequest(
        HttpClient client,
        string url,
        Dictionary<string, string> headers)
        : base(url, headers)
    {
        this.client = client ?? throw new ArgumentNullException(nameof(client));
        this.Content = new MultipartFormDataContent();
    }

    /// <summary>
    /// Gets the multipart form data content for the request.
    /// </summary>
    public MultipartFormDataContent Content { get; }

    /// <summary>
    /// Adds a string value to the multipart form data content.
    /// </summary>
    /// <param name="name">The name of the form field.</param>
    /// <param name="value">The value of the form field.</param>
    /// <returns>The current request for chaining.</returns>
    public MultipartFormDataPostNetworkRequest AddStringContent(string name, string value)
    {
        this.Content.Add(new StringContent(value), name);
        return this;
    }

    /// <summary>
    /// Adds a file stream to the multipart form data content.
    /// </summary>
    /// <param name="name">The name of the form field.</param>
    /// <param name="stream">The file stream.</param>
    /// <param name="fileName">The file name.</param>
    /// <param name="contentType">The content type of the file. Default is application/octet-stream.</param>
    /// <returns>The current request for chaining.</returns>
    public MultipartFormDataPostNetworkRequest AddStreamContent(
        string name,
        Stream stream,
        string fileName,
        string contentType = "application/octet-stream")
    {
        var streamContent = new StreamContent(stream);
        streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(contentType);
        this.Content.Add(streamContent, name, fileName);
        return this;
    }

    /// <summary>
    /// Adds byte array content to the multipart form data content.
    /// </summary>
    /// <param name="name">The name of the form field.</param>
    /// <param name="bytes">The byte array content.</param>
    /// <param name="fileName">The file name.</param>
    /// <param name="contentType">The content type of the file. Default is application/octet-stream.</param>
    /// <returns>The current request for chaining.</returns>
    public MultipartFormDataPostNetworkRequest AddByteArrayContent(
        string name,
        byte[] bytes,
        string fileName,
        string contentType = "application/octet-stream")
    {
        var byteContent = new ByteArrayContent(bytes);
        byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(contentType);
        this.Content.Add(byteContent, name, fileName);
        return this;
    }

    /// <inheritdoc/>
    public override async Task<TResponse> ExecuteAsync<TResponse>(CancellationToken cancellationToken = default)
    {
        string json = await this.PostAndGetJsonResponseAsync(cancellationToken).ConfigureAwait(false);
        return JsonSerializer.Deserialize<TResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }

    /// <inheritdoc/>
    public override async Task<object> ExecuteAsync(
        Type expectedResponse,
        CancellationToken cancellationToken = default)
    {
        string json = await this.PostAndGetJsonResponseAsync(cancellationToken).ConfigureAwait(false);
        return JsonSerializer.Deserialize(json, expectedResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }

    private async Task<string> PostAndGetJsonResponseAsync(CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(this.Url))
        {
            throw new InvalidOperationException("No URL has been specified for executing the network request.");
        }

        var uri = new Uri(this.Url);
        using var request = new HttpRequestMessage(HttpMethod.Post, uri) { Content = this.Content };

        if (this.Headers != null)
        {
            foreach (KeyValuePair<string, string> header in this.Headers)
            {
                request.Headers.Add(header.Key, header.Value);
            }
        }

        using HttpResponseMessage response = await this.client.SendAsync(request, cancellationToken).ConfigureAwait(false);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
    }
}
