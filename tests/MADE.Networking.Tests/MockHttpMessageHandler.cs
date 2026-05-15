// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Net;
using System.Text;

namespace MADE.Networking.Tests;

/// <summary>
/// A mock <see cref="HttpMessageHandler"/> that returns pre-configured responses for testing.
/// </summary>
internal class MockHttpMessageHandler : HttpMessageHandler
{
    private readonly Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> handler;

    public MockHttpMessageHandler(HttpStatusCode statusCode, string content = "{}", string contentType = "application/json")
        : this(_ => new HttpResponseMessage(statusCode)
        {
            Content = new StringContent(content, Encoding.UTF8, contentType),
        })
    {
    }

    public MockHttpMessageHandler(Func<HttpRequestMessage, HttpResponseMessage> handler)
    {
        this.handler = (request, _) => Task.FromResult(handler(request));
    }

    public MockHttpMessageHandler(Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> handler)
    {
        this.handler = handler;
    }

    public int CallCount { get; private set; }

    public HttpRequestMessage? LastRequest { get; private set; }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        this.CallCount++;
        this.LastRequest = request;
        return this.handler(request, cancellationToken);
    }
}
