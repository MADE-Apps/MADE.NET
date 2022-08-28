namespace MADE.Networking.Tests.Tests
{
    using System.Diagnostics.CodeAnalysis;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using MADE.Networking.Http.Requests.Json;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using NUnit.Framework;
    using Shouldly;

    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class JsonPatchNetworkRequestTests
    {
        public class WhenExecutingRequest
        {
            [Test]
            public async Task ShouldReturnSuccessFromPatchEndpointWithResponse()
            {
                // Arrange
                var requestData = new RequestData { Key = "test", Enabled = true };

                const string requestUrl = "https://httpbin.org/patch";
                var request = new JsonPatchNetworkRequest(
                    new HttpClient(),
                    requestUrl,
                    JsonConvert.SerializeObject(requestData));

                // Act
                var response = await request.ExecuteAsync<RequestResponse>();

                // Assert
                response.ShouldNotBeNull();
                response.Url.ShouldBe(requestUrl);
                response.Data.ShouldNotBeNull();

                var responseData = JsonConvert.DeserializeObject<RequestData>(response.Data);
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
                    JsonConvert.SerializeObject(requestData));

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
            public JObject Args { get; set; }

            public string Data { get; set; }

            public string Url { get; set; }
        }
    }
}