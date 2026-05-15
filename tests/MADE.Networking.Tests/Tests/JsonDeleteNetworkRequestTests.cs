using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Text.Json.Nodes;
using MADE.Networking.Http.Requests.Json;
using NUnit.Framework;
using Shouldly;

namespace MADE.Networking.Tests.Tests;

[ExcludeFromCodeCoverage]
[TestFixture]
public class JsonDeleteNetworkRequestTests
{
    public class WhenExecutingRequest
    {
        [Test]
        public async Task ShouldReturnSuccessFromDeleteEndpointWithResponse()
        {
            // Arrange
            const string query = "test";
            const bool queryValue = true;

            var requestUrl = $"https://httpbin.org/delete?{query}={queryValue}";
            var responseJson = $"{{\"args\":{{\"{query}\":\"{queryValue}\"}},\"url\":\"{requestUrl}\"}}";
            var mockHandler = new MockHttpMessageHandler(HttpStatusCode.OK, responseJson);
            var request = new JsonDeleteNetworkRequest(new HttpClient(mockHandler), requestUrl);

            // Act
            var response = await request.ExecuteAsync<RequestResponse>();

            // Assert
            response.ShouldNotBeNull();
            response.Url.ShouldBe(requestUrl);
            bool.Parse(response.Args[query].ToString()).ShouldBe(queryValue);
        }

        [Test]
        public async Task ShouldThrowWhenMethodNotAllowed()
        {
            // Arrange
            var mockHandler = new MockHttpMessageHandler(HttpStatusCode.MethodNotAllowed);
            var request = new JsonDeleteNetworkRequest(new HttpClient(mockHandler), "http://localhost/get");

            // Act
            var exception = await request.ExecuteAsync<RequestResponse>().ShouldThrowAsync<HttpRequestException>();

            // Assert
            exception.StatusCode.ShouldBe(HttpStatusCode.MethodNotAllowed);
        }
    }

    public class RequestResponse
    {
        public JsonObject Args { get; set; }

        public string Url { get; set; }
    }
}
