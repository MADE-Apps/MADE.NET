using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Text.Json;
using System.Text.Json.Nodes;
using MADE.Networking.Http.Requests.Json;
using NUnit.Framework;
using Shouldly;

namespace MADE.Networking.Tests.Tests;

[ExcludeFromCodeCoverage]
[TestFixture]
public class JsonPutNetworkRequestTests
{
    public class WhenExecutingRequest
    {
        [Test]
        public async Task ShouldReturnSuccessFromPutEndpointWithResponse()
        {
            // Arrange
            var requestData = new RequestData { Key = "test", Enabled = true };

            const string requestUrl = "https://httpbin.org/put";
            var serializedData = JsonSerializer.Serialize(requestData);
            var responseJson = JsonSerializer.Serialize(new { data = serializedData, url = requestUrl });
            var mockHandler = new MockHttpMessageHandler(HttpStatusCode.OK, responseJson);
            var request = new JsonPutNetworkRequest(
                new HttpClient(mockHandler),
                requestUrl,
                serializedData);

            // Act
            var response = await request.ExecuteAsync<RequestResponse>();

            // Assert
            response.ShouldNotBeNull();
            response.Url.ShouldBe(requestUrl);
            response.Data.ShouldNotBeNull();

            var responseData = JsonSerializer.Deserialize<RequestData>(response.Data, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            responseData.ShouldNotBeNull();
            responseData.Key.ShouldBe(requestData.Key);
            responseData.Enabled.ShouldBe(requestData.Enabled);
        }

        [Test]
        public async Task ShouldThrowWhenMethodNotAllowed()
        {
            // Arrange
            var mockHandler = new MockHttpMessageHandler(HttpStatusCode.MethodNotAllowed);
            var request = new JsonPutNetworkRequest(
                new HttpClient(mockHandler),
                "http://localhost/get",
                JsonSerializer.Serialize(new RequestData { Key = "test" }));

            // Act
            var exception = await request.ExecuteAsync<RequestResponse>().ShouldThrowAsync<HttpRequestException>();

            // Assert
            exception.StatusCode.ShouldBe(HttpStatusCode.MethodNotAllowed);
        }
    }

    public class RequestData
    {
        public string Key { get; set; }

        public bool Enabled { get; set; }
    }

    public class RequestResponse
    {
        public JsonObject Args { get; set; }

        public string Data { get; set; }

        public string Url { get; set; }
    }
}
