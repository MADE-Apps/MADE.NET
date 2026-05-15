using System.Diagnostics.CodeAnalysis;
using MADE.Web.Mvc.Responses;
using Microsoft.AspNetCore.Http;
using NUnit.Framework;
using Shouldly;

namespace MADE.Web.Mvc.Tests.Tests;

[ExcludeFromCodeCoverage]
[TestFixture]
public class ForbiddenObjectResultTests
{
    public class WhenCreatingWithError
    {
        [Test]
        public void ShouldReturnForbiddenStatusCode()
        {
            // Arrange & Act
            var result = new ForbiddenObjectResult("Access denied");

            // Assert
            result.StatusCode.ShouldBe(StatusCodes.Status403Forbidden);
            result.Value.ShouldBe("Access denied");
        }
    }
}
