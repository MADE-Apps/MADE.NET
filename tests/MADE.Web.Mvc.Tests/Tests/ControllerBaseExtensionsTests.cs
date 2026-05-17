using System.Diagnostics.CodeAnalysis;
using MADE.Web.Mvc.Extensions;
using MADE.Web.Mvc.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Moq;
using NUnit.Framework;
using Shouldly;
using JsonResult = MADE.Web.Mvc.Responses.JsonResult;

namespace MADE.Web.Mvc.Tests.Tests;

[ExcludeFromCodeCoverage]
[TestFixture]
public class ControllerBaseExtensionsTests
{
    public class WhenCallingJson
    {
        [Test]
        public void ShouldReturnJsonResult()
        {
            // Arrange
            var controller = new TestController();

            // Act
            var result = controller.Json(new { Name = "Test" });

            // Assert
            result.ShouldBeOfType<JsonResult>();
        }

        [Test]
        public void ShouldThrowWhenControllerIsNull()
        {
            // Act & Assert
            Should.Throw<ArgumentNullException>(
                () => ControllerBaseExtensions.Json(null!, new { Name = "Test" }));
        }
    }

    public class WhenCallingInternalServerError
    {
        [Test]
        public void ShouldReturnInternalServerErrorResultWithObject()
        {
            // Arrange
            var controller = new TestController();

            // Act
            var result = controller.InternalServerError("error");

            // Assert
            result.ShouldBeOfType<InternalServerErrorObjectResult>();
            var objectResult = (InternalServerErrorObjectResult)result;
            objectResult.StatusCode.ShouldBe(StatusCodes.Status500InternalServerError);
        }

        [Test]
        public void ShouldReturnInternalServerErrorResultWithModelState()
        {
            // Arrange
            var controller = new TestController();
            var modelState = new ModelStateDictionary();
            modelState.AddModelError("field", "error");

            // Act
            var result = controller.InternalServerError(modelState);

            // Assert
            result.ShouldBeOfType<InternalServerErrorObjectResult>();
        }

        [Test]
        public void ShouldThrowWhenControllerIsNull()
        {
            // Act & Assert
            Should.Throw<ArgumentNullException>(
                () => ControllerBaseExtensions.InternalServerError(null!, "error"));
        }

        [Test]
        public void ShouldThrowWhenModelStateIsNull()
        {
            // Arrange
            var controller = new TestController();

            // Act & Assert
            Should.Throw<ArgumentNullException>(
                () => ControllerBaseExtensions.InternalServerError(controller, (ModelStateDictionary)null!));
        }
    }

    public class WhenCallingForbidden
    {
        [Test]
        public void ShouldReturnForbiddenResultWithObject()
        {
            // Arrange
            var controller = new TestController();

            // Act
            var result = controller.Forbidden("access denied");

            // Assert
            result.ShouldBeOfType<ForbiddenObjectResult>();
            var objectResult = (ForbiddenObjectResult)result;
            objectResult.StatusCode.ShouldBe(StatusCodes.Status403Forbidden);
        }

        [Test]
        public void ShouldReturnForbiddenResultWithModelState()
        {
            // Arrange
            var controller = new TestController();
            var modelState = new ModelStateDictionary();
            modelState.AddModelError("field", "forbidden");

            // Act
            var result = controller.Forbidden(modelState);

            // Assert
            result.ShouldBeOfType<ForbiddenObjectResult>();
        }

        [Test]
        public void ShouldThrowWhenControllerIsNull()
        {
            // Act & Assert
            Should.Throw<ArgumentNullException>(
                () => ControllerBaseExtensions.Forbidden(null!, "denied"));
        }

        [Test]
        public void ShouldThrowWhenModelStateIsNull()
        {
            // Arrange
            var controller = new TestController();

            // Act & Assert
            Should.Throw<ArgumentNullException>(
                () => ControllerBaseExtensions.Forbidden(controller, (ModelStateDictionary)null!));
        }
    }

    public class WhenCreatingForbiddenWithModelState
    {
        [Test]
        public void ShouldReturnForbiddenStatusCode()
        {
            // Arrange
            var modelState = new ModelStateDictionary();
            modelState.AddModelError("Key", "Error");

            // Act
            var result = new ForbiddenObjectResult(modelState);

            // Assert
            result.StatusCode.ShouldBe(StatusCodes.Status403Forbidden);
        }
    }

    private class TestController : ControllerBase
    {
    }
}
