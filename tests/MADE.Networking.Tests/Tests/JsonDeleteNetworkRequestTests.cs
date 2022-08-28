namespace MADE.Networking.Tests.Tests
{
    using System.Diagnostics.CodeAnalysis;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using MADE.Networking.Http.Requests.Json;
    using Newtonsoft.Json.Linq;
    using NUnit.Framework;
    using Shouldly;

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
                var request = new JsonDeleteNetworkRequest(new HttpClient(), requestUrl);

                // Act
                var response = await request.ExecuteAsync<RequestResponse>();

                // Assert
                response.ShouldNotBeNull();
                response.Url.ShouldBe(requestUrl);
                response.Args.Value<bool>(query).ShouldBe(queryValue);
            }

            [Test]
            public async Task ShouldReturnErrorFromGetEndpoint()
            {
                // Arrange
                const string query = "test";
                const bool queryValue = true;

                var requestUrl = $"https://httpbin.org/get?{query}={queryValue}";
                var request = new JsonDeleteNetworkRequest(new HttpClient(), requestUrl);

                // Act
                var exception = await request.ExecuteAsync<RequestResponse>().ShouldThrowAsync<HttpRequestException>();

                // Assert
                exception.StatusCode.ShouldBe(HttpStatusCode.MethodNotAllowed);
            }
        }

        public class RequestResponse
        {
            public JObject Args { get; set; }

            public string Url { get; set; }
        }
    }
}