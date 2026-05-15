using System.Diagnostics.CodeAnalysis;
using System.Net;
using MADE.Networking.Http;
using NUnit.Framework;
using Shouldly;

namespace MADE.Networking.Tests.Tests;

[ExcludeFromCodeCoverage]
[TestFixture]
public class RetryDelegatingHandlerTests
{
    private class FakeHandler : HttpMessageHandler
    {
        private readonly Queue<HttpResponseMessage> responses = new();

        public int CallCount { get; private set; }

        public void EnqueueResponse(HttpStatusCode statusCode)
        {
            this.responses.Enqueue(new HttpResponseMessage(statusCode));
        }

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            this.CallCount++;
            return Task.FromResult(this.responses.Dequeue());
        }
    }

    public class WhenRequestSucceeds
    {
        [Test]
        public async Task ShouldNotRetry()
        {
            // Arrange
            var fakeHandler = new FakeHandler();
            fakeHandler.EnqueueResponse(HttpStatusCode.OK);

            using var retryHandler = new RetryDelegatingHandler(fakeHandler, maxRetries: 3);
            using var client = new HttpClient(retryHandler);

            // Act
            var response = await client.GetAsync("http://localhost/test");

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            fakeHandler.CallCount.ShouldBe(1);
        }
    }

    public class WhenRequestFailsWithTransientError
    {
        [Test]
        public async Task ShouldRetryAndEventuallySucceed()
        {
            // Arrange
            var fakeHandler = new FakeHandler();
            fakeHandler.EnqueueResponse(HttpStatusCode.ServiceUnavailable);
            fakeHandler.EnqueueResponse(HttpStatusCode.ServiceUnavailable);
            fakeHandler.EnqueueResponse(HttpStatusCode.OK);

            using var retryHandler = new RetryDelegatingHandler(
                fakeHandler,
                maxRetries: 3,
                initialDelay: TimeSpan.FromMilliseconds(10));
            using var client = new HttpClient(retryHandler);

            // Act
            var response = await client.GetAsync("http://localhost/test");

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            fakeHandler.CallCount.ShouldBe(3);
        }

        [Test]
        public async Task ShouldReturnLastResponseWhenRetriesExhausted()
        {
            // Arrange
            var fakeHandler = new FakeHandler();
            fakeHandler.EnqueueResponse(HttpStatusCode.ServiceUnavailable);
            fakeHandler.EnqueueResponse(HttpStatusCode.ServiceUnavailable);

            using var retryHandler = new RetryDelegatingHandler(
                fakeHandler,
                maxRetries: 1,
                initialDelay: TimeSpan.FromMilliseconds(10));
            using var client = new HttpClient(retryHandler);

            // Act
            var response = await client.GetAsync("http://localhost/test");

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.ServiceUnavailable);
            fakeHandler.CallCount.ShouldBe(2);
        }
    }

    public class WhenRequestFailsWithNonTransientError
    {
        [Test]
        public async Task ShouldNotRetry()
        {
            // Arrange
            var fakeHandler = new FakeHandler();
            fakeHandler.EnqueueResponse(HttpStatusCode.BadRequest);

            using var retryHandler = new RetryDelegatingHandler(
                fakeHandler,
                maxRetries: 3,
                initialDelay: TimeSpan.FromMilliseconds(10));
            using var client = new HttpClient(retryHandler);

            // Act
            var response = await client.GetAsync("http://localhost/test");

            // Assert
            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
            fakeHandler.CallCount.ShouldBe(1);
        }
    }
}
