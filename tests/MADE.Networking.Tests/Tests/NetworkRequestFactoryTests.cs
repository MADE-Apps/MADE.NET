using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Text.Json;
using MADE.Networking.Extensions;
using MADE.Networking.Http;
using MADE.Networking.Http.Requests.Json;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Shouldly;

namespace MADE.Networking.Tests.Tests;

[ExcludeFromCodeCoverage]
[TestFixture]
public class NetworkRequestFactoryTests
{
    public class WhenCreatingRequests
    {
        [Test]
        public async Task ShouldCreateGetRequest()
        {
            // Arrange
            var factory = CreateFactory();

            // Act
            var request = factory.Get("https://httpbin.org/get?key=value");
            var response = await request.ExecuteAsync<JsonGetResponse>();

            // Assert
            response.ShouldNotBeNull();
            response.Url.ShouldContain("key=value");
        }

        [Test]
        public async Task ShouldCreatePostRequest()
        {
            // Arrange
            var factory = CreateFactory();
            var data = JsonSerializer.Serialize(new { key = "value" });

            // Act
            var request = factory.Post("https://httpbin.org/post", data);
            var response = await request.ExecuteAsync<JsonPostResponse>();

            // Assert
            response.ShouldNotBeNull();
            response.Data.ShouldNotBeNull();
        }

        [Test]
        public async Task ShouldCreateDeleteRequest()
        {
            // Arrange
            var factory = CreateFactory();

            // Act
            var request = factory.Delete("https://httpbin.org/delete");
            var response = await request.ExecuteAsync<JsonGetResponse>();

            // Assert
            response.ShouldNotBeNull();
        }

        [Test]
        public void ShouldCreateMultipartRequest()
        {
            // Arrange
            var factory = CreateFactory();

            // Act
            var request = factory.PostMultipart("https://httpbin.org/post");

            // Assert
            request.ShouldNotBeNull();
            request.Content.ShouldNotBeNull();
        }
    }

    public class WhenUsingMockHandler
    {
        [Test]
        public async Task ShouldThrowForErrorStatusCode()
        {
            // Arrange
            var factory = CreateFactoryWithMock(new MockHttpMessageHandler(HttpStatusCode.NotFound));

            // Act & Assert
            var request = factory.Get("http://localhost/missing");
            await request.ExecuteAsync<object>().ShouldThrowAsync<HttpRequestException>();
        }
    }

    public class WhenUsingNamedClient
    {
        [Test]
        public async Task ShouldUseNamedClient()
        {
            // Arrange
            var expected = new JsonGetResponse { Url = "http://localhost/test" };
            var mockHandler = new MockHttpMessageHandler(HttpStatusCode.OK, JsonSerializer.Serialize(expected));

            var services = new ServiceCollection();
            services.AddNetworkRequestFactory();
            services.AddHttpClient("test")
                .ConfigurePrimaryHttpMessageHandler(() => mockHandler);

            var provider = services.BuildServiceProvider();
            var factory = provider.GetRequiredService<INetworkRequestFactory>();

            // Act
            var request = factory.WithClient("test").Get("http://localhost/test");
            var response = await request.ExecuteAsync<JsonGetResponse>();

            // Assert
            response.ShouldNotBeNull();
            response.Url.ShouldBe("http://localhost/test");
            mockHandler.CallCount.ShouldBe(1);
        }
    }

    public class WhenRegisteringWithDI
    {
        [Test]
        public void ShouldResolveFactory()
        {
            // Arrange
            var services = new ServiceCollection();
            services.AddNetworkRequestFactory();
            var provider = services.BuildServiceProvider();

            // Act
            var factory = provider.GetService<INetworkRequestFactory>();

            // Assert
            factory.ShouldNotBeNull();
        }

        [Test]
        public void ShouldResolveWithNamedClientRegistration()
        {
            // Arrange
            var services = new ServiceCollection();
            services.AddNetworkRequestFactory("MyApi", client =>
            {
                client.BaseAddress = new Uri("https://httpbin.org");
            });

            var provider = services.BuildServiceProvider();

            // Act
            var factory = provider.GetService<INetworkRequestFactory>();

            // Assert
            factory.ShouldNotBeNull();
        }
    }

    private static INetworkRequestFactory CreateFactory()
    {
        var services = new ServiceCollection();
        services.AddNetworkRequestFactory();
        return services.BuildServiceProvider().GetRequiredService<INetworkRequestFactory>();
    }

    private static INetworkRequestFactory CreateFactoryWithMock(MockHttpMessageHandler handler)
    {
        var services = new ServiceCollection();
        services.AddNetworkRequestFactory();
        services.AddHttpClient(string.Empty)
            .ConfigurePrimaryHttpMessageHandler(() => handler);
        return services.BuildServiceProvider().GetRequiredService<INetworkRequestFactory>();
    }

    public class JsonGetResponse
    {
        public string Url { get; set; } = string.Empty;
    }

    public class JsonPostResponse
    {
        public string Data { get; set; } = string.Empty;

        public string Url { get; set; } = string.Empty;
    }
}
