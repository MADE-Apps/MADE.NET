namespace MADE.Networking.Tests.Tests
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Net.Http;
    using System.Threading;
    using MADE.Networking.Http;
    using MADE.Networking.Http.Requests.Json;
    using Newtonsoft.Json.Linq;
    using NUnit.Framework;
    using Shouldly;

    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class NetworkRequestManagerTests
    {
        public class WhenAddingOrUpdatingQueueRequests
        {
            [Test]
            public void ShouldAddToQueue()
            {
                // Arrange
                const string query = "test";
                const bool queryValue = true;

                var requestUrl = $"https://httpbin.org/get?{query}={queryValue}";
                var request = new JsonGetNetworkRequest(new HttpClient(), requestUrl);

                var manager = new NetworkRequestManager();

                // Act
                manager.AddOrUpdate<JsonGetNetworkRequest, RequestResponse>(
                    request,
                    _ => { });

                // Assert
                manager.CurrentQueue.Count.ShouldBe(1);
                manager.CurrentQueue.Keys.ShouldContain(request.Identifier.ToString());
            }

            [Test]
            public void ShouldUpdateExistingInQueue()
            {
                // Arrange
                const string query = "test";
                const bool queryValue = true;

                var requestUrl = $"https://httpbin.org/get?{query}={queryValue}";
                var request = new JsonGetNetworkRequest(new HttpClient(), requestUrl);

                var manager = new NetworkRequestManager();
                manager.AddOrUpdate<JsonGetNetworkRequest, RequestResponse>(
                    request,
                    _ => { });

                // Act
                request.Url = $"https://httpbin.org/get?{query}={!queryValue}";
                manager.AddOrUpdate<JsonGetNetworkRequest, RequestResponse>(
                    request,
                    _ => { });

                // Assert
                manager.CurrentQueue.Count.ShouldBe(1);
                manager.CurrentQueue.Keys.ShouldContain(request.Identifier.ToString());
            }
        }

        public class WhenRemovingQueueRequests
        {
            [Test]
            public void ShouldRemoveByRequest()
            {
                // Arrange
                const string query = "test";
                const bool queryValue = true;

                var requestUrl = $"https://httpbin.org/get?{query}={queryValue}";
                var request = new JsonGetNetworkRequest(new HttpClient(), requestUrl);

                var manager = new NetworkRequestManager();

                manager.AddOrUpdate<JsonGetNetworkRequest, RequestResponse>(
                    request,
                    _ => { });

                // Act
                manager.Remove(request);

                // Assert
                manager.CurrentQueue.Count.ShouldBe(0);
            }

            [Test]
            public void ShouldRemoveByRequestId()
            {
                // Arrange
                const string query = "test";
                const bool queryValue = true;

                var requestUrl = $"https://httpbin.org/get?{query}={queryValue}";
                var request = new JsonGetNetworkRequest(new HttpClient(), requestUrl);

                var manager = new NetworkRequestManager();

                manager.AddOrUpdate<JsonGetNetworkRequest, RequestResponse>(
                    request,
                    _ => { });

                // Act
                manager.RemoveByKey(request.Identifier.ToString());

                // Assert
                manager.CurrentQueue.Count.ShouldBe(0);
            }
        }

        public class WhenProcessingQueueRequests
        {
            [Test]
            public void ShouldProcessQueue()
            {
                // Arrange
                AutoResetEvent autoResetEvent = new AutoResetEvent(false);

                const string query = "test";
                const bool queryValue = true;

                var requestUrl = $"https://httpbin.org/get?{query}={queryValue}";
                var request = new JsonGetNetworkRequest(new HttpClient(), requestUrl);

                var manager = new NetworkRequestManager();

                RequestResponse actualResponse = null;

                manager.AddOrUpdate<JsonGetNetworkRequest, RequestResponse>(request, response =>
                {
                    actualResponse = response;
                    autoResetEvent.Set();
                });

                // Act
                manager.Start();

                // Assert
                autoResetEvent.WaitOne(TimeSpan.FromSeconds(60));

                actualResponse.ShouldNotBeNull();
                actualResponse.Url.ShouldBe(requestUrl);
                actualResponse.Args.Value<bool>(query).ShouldBe(queryValue);
            }
        }

        public class WhenProcessingQueueRequestsStopped
        {
            [Test]
            public void ShouldStopProcessingQueue()
            {
                // Arrange
                AutoResetEvent autoResetEvent = new AutoResetEvent(false);

                const string query = "test";
                const bool queryValue = true;

                var requestUrl = $"https://httpbin.org/get?{query}={queryValue}";
                var request = new JsonGetNetworkRequest(new HttpClient(), requestUrl);

                var manager = new NetworkRequestManager();

                manager.AddOrUpdate<JsonGetNetworkRequest, RequestResponse>(
                    request,
                    _ =>
                    {
                        autoResetEvent.Set();
                    });

                manager.Start();

                autoResetEvent.WaitOne(TimeSpan.FromSeconds(60));

                // Act
                manager.Stop();

                manager.AddOrUpdate<JsonGetNetworkRequest, RequestResponse>(
                    request,
                    _ => { });

                // Assert
                manager.CurrentQueue.Count.ShouldBe(1);
                manager.CurrentQueue.Keys.ShouldContain(request.Identifier.ToString());
            }
        }

        public class RequestResponse
        {
            public JObject Args { get; set; }

            public string Url { get; set; }
        }
    }
}