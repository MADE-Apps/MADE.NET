using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using MADE.Networking.Http.Requests.Json;
using System.Text.Json;
using System.Text.Json.Nodes;
using NUnit.Framework;
using Shouldly;

namespace MADE.Networking.Tests.Tests;

[ExcludeFromCodeCoverage]
[TestFixture]
public class JsonPostNetworkRequestTests
{
    public class WhenExecutingRequest
    {
        [Test]
        public async Task ShouldReturnSuccessFromPostEndpointWithResponse()
        {
            // Arrange
            var requestData = new RequestData { Key = "test", Enabled = true };

            const string requestUrl = "https://httpbin.org/post";
            var request = new JsonPostNetworkRequest(
                new HttpClient(),
                requestUrl,
                JsonSerializer.Serialize(requestData));

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
        public async Task ShouldReturnErrorFromGetEndpoint()
        {
            // Arrange
            var requestData = new RequestData { Key = "test", Enabled = true };

            const string requestUrl = "https://httpbin.org/get";
            var request = new JsonPatchNetworkRequest(
                new HttpClient(),
                requestUrl,
                JsonSerializer.Serialize(requestData));

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
