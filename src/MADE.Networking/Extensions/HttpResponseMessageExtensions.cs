// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Networking.Extensions
{
    using System.Net.Http;
    using System.Threading.Tasks;
    using MADE.Networking.Http.Responses;

    /// <summary>
    /// Defines a collection of extensions for <see cref="HttpResponseMessage"/> objects.
    /// </summary>
    public static class HttpResponseMessageExtensions
    {
        /// <summary>
        /// Deserializes the content of the specified <paramref name="responseTask">response task</paramref> to a <see cref="HttpResponseMessage{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of response expected.</typeparam>
        /// <param name="responseTask">The task associated with the <see cref="HttpResponseMessage"/>.</param>
        /// <returns>A <see cref="HttpResponseMessage{T}"/> with deserialized content.</returns>
        public static async Task<HttpResponseMessage<T>> DeserializeAsync<T>(this Task<HttpResponseMessage> responseTask)
        {
            HttpResponseMessage response = await responseTask;
            return await DeserializeAsync<T>(response);
        }

        /// <summary>
        /// Deserializes the content of the specified <paramref name="response">response</paramref> to a <see cref="HttpResponseMessage{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of response expected.</typeparam>
        /// <param name="response">The <see cref="HttpResponseMessage"/> to deserialize.</param>
        /// <returns>A <see cref="HttpResponseMessage{T}"/> with deserialized content.</returns>
        public static async Task<HttpResponseMessage<T>> DeserializeAsync<T>(this HttpResponseMessage response)
        {
            var deserializedResponse = new HttpResponseMessage<T>(response);
            await deserializedResponse.DeserializeAsync();
            return deserializedResponse;
        }
    }
}