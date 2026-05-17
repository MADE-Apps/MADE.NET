using System.Diagnostics.CodeAnalysis;
using MADE.Web.Mvc.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NUnit.Framework;
using Shouldly;

namespace MADE.Web.Mvc.Tests.Tests;

[ExcludeFromCodeCoverage]
[TestFixture]
public class InternalServerErrorObjectResultTests
{
    public class WhenCreatingWithError
    {
        [Test]
        public void ShouldReturnInternalServerErrorStatusCode()
        {
            // Arrange & Act
            var result = new InternalServerErrorObjectResult("Server error");

            // Assert
            result.StatusCode.ShouldBe(StatusCodes.Status500InternalServerError);
            result.Value.ShouldBe("Server error");
        }
    }

    public class WhenCreatingWithModelState
    {
        [Test]
        public void ShouldReturnInternalServerErrorStatusCode()
        {
            // Arrange
            var modelState = new ModelStateDictionary();
            modelState.AddModelError("Field", "Error message");

            // Act
            var result = new InternalServerErrorObjectResult(modelState);

            // Assert
            result.StatusCode.ShouldBe(StatusCodes.Status500InternalServerError);
            result.Value.ShouldNotBeNull();
        }
    }
}
