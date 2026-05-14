// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using System.Text.Json;

namespace MADE.Web.Extensions;

/// <summary>
/// Defines a collection of extensions for a <see cref="HttpResponse" /> object.
/// </summary>
public static class HttpResponseExtensions
{
    private static readonly JsonSerializerOptions DefaultSerializerOptions = new() { WriteIndented = true };

    /// <summary>
    /// Writes an object value as JSON to the specified <paramref name="response" />.
    /// </summary>
    /// <param name="response">The HTTP response to write to.</param>
    /// <param name="statusCode">The status code of the response.</param>
    /// <param name="value">The object to serialize as JSON.</param>
    /// <returns>An asynchronous operation.</returns>
    public static async Task WriteJsonAsync(
        this HttpResponse response,
        HttpStatusCode statusCode,
        object value)
    {
        await WriteJsonAsync(response, (int)statusCode, value, null).ConfigureAwait(false);
    }

    /// <summary>
    /// Writes an object value as JSON to the specified <paramref name="response" />.
    /// </summary>
    /// <param name="response">The HTTP response to write to.</param>
    /// <param name="statusCode">The status code of the response.</param>
    /// <param name="value">The object to serialize as JSON.</param>
    /// <returns>An asynchronous operation.</returns>
    public static async Task WriteJsonAsync(
        this HttpResponse response,
        int statusCode,
        object value)
    {
        await WriteJsonAsync(response, statusCode, value, null).ConfigureAwait(false);
    }

    /// <summary>
    /// Writes an object value as JSON to the specified <paramref name="response" />.
    /// </summary>
    /// <param name="response">The HTTP response to write to.</param>
    /// <param name="statusCode">The status code of the response.</param>
    /// <param name="value">The object to serialize as JSON.</param>
    /// <param name="serializerOptions">The JSON serializer options.</param>
    /// <returns>An asynchronous operation.</returns>
    public static async Task WriteJsonAsync(
        this HttpResponse response,
        HttpStatusCode statusCode,
        object value,
        JsonSerializerOptions? serializerOptions)
    {
        await WriteJsonAsync(response, (int)statusCode, value, serializerOptions).ConfigureAwait(false);
    }

    /// <summary>
    /// Writes an object value as JSON to the specified <paramref name="response" />.
    /// </summary>
    /// <param name="response">The HTTP response to write to.</param>
    /// <param name="statusCode">The status code of the response.</param>
    /// <param name="value">The object to serialize as JSON.</param>
    /// <param name="serializerOptions">The JSON serializer options.</param>
    /// <returns>An asynchronous operation.</returns>
    public static async Task WriteJsonAsync(
        this HttpResponse response,
        int statusCode,
        object value,
        JsonSerializerOptions? serializerOptions)
    {
        response.ContentType = new MediaTypeHeaderValue("application/json") { Encoding = Encoding.UTF8 }.ToString();
        response.StatusCode = statusCode;

        var options = serializerOptions ?? DefaultSerializerOptions;

        string json = JsonSerializer.Serialize(value, options);

        await response.WriteAsync(json, Encoding.UTF8).ConfigureAwait(false);
    }
}
