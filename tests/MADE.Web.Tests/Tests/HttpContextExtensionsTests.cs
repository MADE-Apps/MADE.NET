using System.Diagnostics.CodeAnalysis;
using MADE.Web.Extensions;
using Microsoft.AspNetCore.Http;
using NUnit.Framework;
using Shouldly;

namespace MADE.Web.Tests.Tests;

[ExcludeFromCodeCoverage]
[TestFixture]
public class HttpContextExtensionsTests
{
    public class WhenGettingDomain
    {
        [Test]
        public void ShouldReturnHostDomain()
        {
            // Arrange
            var context = new DefaultHttpContext();
            context.Request.Host = new HostString("example.com");

            // Act
            string domain = context.GetDomain();

            // Assert
            domain.ShouldBe("example.com");
        }

        [Test]
        public void ShouldReturnHostWithoutPort()
        {
            // Arrange
            var context = new DefaultHttpContext();
            context.Request.Host = new HostString("example.com", 8080);

            // Act
            string domain = context.GetDomain();

            // Assert
            domain.ShouldBe("example.com");
        }
    }
}
