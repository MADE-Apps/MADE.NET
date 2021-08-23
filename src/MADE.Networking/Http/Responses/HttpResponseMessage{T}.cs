// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Networking.Http.Responses
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using Newtonsoft.Json;

    /// <summary>
    /// Defines a HTTP response message that includes a deserializing option for the response data.
    /// </summary>
    /// <typeparam name="T">The type of response expected.</typeparam>
    public class HttpResponseMessage<T> : IDisposable
    {
        private HttpResponseMessage response;
        private bool disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpResponseMessage{T}"/> class with the original <see cref="HttpResponseMessage"/>.
        /// </summary>
        /// <param name="response">The original <see cref="HttpResponseMessage"/>.</param>
        public HttpResponseMessage(HttpResponseMessage response)
        {
            this.response = response;
        }

        /// <summary>
        /// Gets the content of the HTTP response message.
        /// </summary>
        public HttpContent Content => this.response.Content;

        /// <summary>
        /// Gets the collection of HTTP response headers.
        /// </summary>
        public HttpResponseHeaders Headers => this.response.Headers;

        /// <summary>
        /// Gets a value indicating whether the HTTP response was successful.
        /// </summary>
        public bool IsSuccessStatusCode => this.response.IsSuccessStatusCode;

        /// <summary>
        /// Gets the reason phrase that typically is sent by servers together with the status code.
        /// </summary>
        public string ReasonPhrase => this.response.ReasonPhrase;

        /// <summary>
        /// Gets the request message which led to this response message.
        /// </summary>
        public HttpRequestMessage RequestMessage => this.response.RequestMessage;

        /// <summary>
        /// Gets the status code of the HTTP response.
        /// </summary>
        public HttpStatusCode StatusCode => this.response.StatusCode;

        /// <summary>
        /// Gets the HTTP message version.
        /// </summary>
        public Version Version => this.response.Version;

        /// <summary>
        /// Gets the deserialized content of the original <see cref="HttpResponseMessage"/> as the specified <typeparamref name="T" /> type.
        /// <para>
        /// Note, ensure that <see cref="DeserializeAsync"/> has been called first, otherwise this value will be default.
        /// </para>
        /// </summary>
        public T DeserializedContent { get; private set; }

        /// <summary>
        /// Allows conversion of a <see cref="HttpResponseMessage"/> to the <see cref="HttpResponseMessage{T}"/> without direct casting.
        /// </summary>
        /// <param name="response">
        /// The <see cref="HttpResponseMessage"/>.
        /// </param>
        /// <returns>
        /// The <see cref="HttpResponseMessage{T}"/>.
        /// </returns>
        public static implicit operator HttpResponseMessage<T>(HttpResponseMessage response)
        {
            return new HttpResponseMessage<T>(response);
        }

        /// <summary>
        /// Deserializes the content of the <see cref="HttpResponseMessage"/> into the <see cref="DeserializedContent"/> value.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        public async Task<T> DeserializeAsync()
        {
            this.DeserializedContent = JsonConvert.DeserializeObject<T>(await this.Content.ReadAsStringAsync());
            return this.DeserializedContent;
        }

        /// <summary>
        /// Throws an exception if the <see cref="IsSuccessStatusCode"/> property for the HTTP response is false.
        /// </summary>
        /// <returns>The HTTP response message if the call is successful.</returns>
        public HttpResponseMessage<T> EnsureSuccessStatusCode()
        {
            this.response.EnsureSuccessStatusCode();
            return this;
        }

        /// <summary>
        /// Releases the unmanaged resources and disposes of unmanaged resources used by the <see cref="HttpResponseMessage{T}"/>.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases the unmanaged resources used by the <see cref="HttpResponseMessage{T}"/> and optionally disposes of the managed resources.
        /// </summary>
        /// <param name="disposing">A value indicating whether to release both managed and unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.response.Dispose();
                }

                this.response = null;
                this.DeserializedContent = default;
                this.disposed = true;
            }
        }
    }
}