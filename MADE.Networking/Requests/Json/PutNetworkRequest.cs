// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PutNetworkRequest.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines a model for a PUT request with a JSON response that can be made with an INetworkRequestManager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MADE.Networking.Requests.Json
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Text;

    using Newtonsoft.Json;

    /// <summary>
    /// Defines a model for a PUT request with a JSON response that can be made with an <see cref="INetworkRequestManager"/>.
    /// </summary>
    public class PutNetworkRequest : JsonResponseNetworkRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PutNetworkRequest"/> class.
        /// </summary>
        /// <param name="url">
        /// The request URL.
        /// </param>
        /// <param name="responseType">
        /// The type of response expected from the request.
        /// </param>
        public PutNetworkRequest(string url, Type responseType)
            : this(url, null, null, responseType)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PutNetworkRequest"/> class.
        /// </summary>
        /// <param name="url">
        /// The request URL.
        /// </param>
        /// <param name="content">
        /// The content to JSONify and send.
        /// </param>
        /// <param name="responseType">
        /// The type of response expected from the request.
        /// </param>
        public PutNetworkRequest(string url, object content, Type responseType)
            : this(url, null, content, responseType)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PutNetworkRequest"/> class.
        /// </summary>
        /// <param name="url">
        /// The request URL.
        /// </param>
        /// <param name="headers">
        /// The additional headers for the request URL.
        /// </param>
        /// <param name="responseType">
        /// The type of response expected from the request.
        /// </param>
        public PutNetworkRequest(string url, Dictionary<string, string> headers, Type responseType)
            : this(url, headers, null, responseType)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PutNetworkRequest"/> class.
        /// </summary>
        /// <param name="url">
        /// The request URL.
        /// </param>
        /// <param name="headers">
        /// The additional headers for the request URL.
        /// </param>
        /// <param name="content">
        /// The content to JSONify and send.
        /// </param>
        /// <param name="responseType">
        /// The type of response expected from the request.
        /// </param>
        public PutNetworkRequest(string url, Dictionary<string, string> headers, object content, Type responseType)
            : base(url, HttpMethod.Post, headers, responseType)
        {
            if (content == null)
            {
                return;
            }

            string json = JsonConvert.SerializeObject(content);
            this.RequestContent = new StringContent(json, Encoding.UTF8, "application/json");
        }
    }
}