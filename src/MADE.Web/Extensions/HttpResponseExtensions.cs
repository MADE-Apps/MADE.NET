// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Web.Extensions
{
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Newtonsoft.Json;

    /// <summary>
    /// Defines a collection of extensions for a <see cref="HttpResponse" /> object.
    /// </summary>
    public static class HttpResponseExtensions
    {
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
            await WriteJsonAsync(response, statusCode, value, null);
        }

        /// <summary>
        /// Writes an object value as JSON to the specified <paramref name="response" />.
        /// </summary>
        /// <param name="response">The HTTP response to write to.</param>
        /// <param name="statusCode">The status code of the response.</param>
        /// <param name="value">The object to serialize as JSON.</param>
        /// <param name="serializerSettings">The JSON serializer settings.</param>
        /// <returns>An asynchronous operation.</returns>
        public static async Task WriteJsonAsync(
            this HttpResponse response,
            HttpStatusCode statusCode,
            object value,
            JsonSerializerSettings serializerSettings)
        {
            response.ContentType = "application/json";
            response.StatusCode = (int)statusCode;

            string json = JsonConvert.SerializeObject(value, Formatting.Indented, serializerSettings);

            await response.WriteAsync(json, Encoding.UTF8);
        }
    }
}