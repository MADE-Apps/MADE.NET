using System.Diagnostics.CodeAnalysis;
using System.Net;
using MADE.Web.Extensions;
using Microsoft.AspNetCore.Http;
using NUnit.Framework;
using Shouldly;

namespace MADE.Web.Tests.Tests;

[ExcludeFromCodeCoverage]
[TestFixture]
public class HttpResponseExtensionsTests
{
    public class WhenWritingJson
    {
        [Test]
        public async Task ShouldSetContentTypeToJson()
        {
            // Arrange
            var context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();

            // Act
            await context.Response.WriteJsonAsync(HttpStatusCode.OK, new { Name = "Test" });

            // Assert
            context.Response.ContentType.ShouldContain("application/json");
        }

        [Test]
        public async Task ShouldSetStatusCode()
        {
            // Arrange
            var context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();

            // Act
            await context.Response.WriteJsonAsync(HttpStatusCode.NotFound, new { Error = "Not found" });

            // Assert
            context.Response.StatusCode.ShouldBe(404);
        }

        [Test]
        public async Task ShouldWriteSerializedJsonToBody()
        {
            // Arrange
            var context = new DefaultHttpContext();
            var stream = new MemoryStream();
            context.Response.Body = stream;

            // Act
            await context.Response.WriteJsonAsync(200, new { Name = "Test" });

            // Assert
            stream.Position = 0;
            using var reader = new StreamReader(stream);
            string body = await reader.ReadToEndAsync();
            body.ShouldContain("\"Name\"");
            body.ShouldContain("\"Test\"");
        }

        [Test]
        public async Task ShouldAcceptIntStatusCode()
        {
            // Arrange
            var context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();

            // Act
            await context.Response.WriteJsonAsync(201, new { Id = 1 });

            // Assert
            context.Response.StatusCode.ShouldBe(201);
        }
    }
}
