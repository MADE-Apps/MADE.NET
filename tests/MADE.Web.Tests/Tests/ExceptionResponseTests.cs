using System.Diagnostics.CodeAnalysis;
using MADE.Web.Exceptions;
using NUnit.Framework;
using Shouldly;

namespace MADE.Web.Tests.Tests;

[ExcludeFromCodeCoverage]
[TestFixture]
public class ExceptionResponseTests
{
    public class WhenCreating
    {
        [Test]
        public void ShouldSetErrorCode()
        {
            // Arrange & Act
            var response = new ExceptionResponse<InvalidOperationException>(
                "ERR001", "Something went wrong", new InvalidOperationException("test"));

            // Assert
            response.ErrorCode.ShouldBe("ERR001");
        }

        [Test]
        public void ShouldSetErrorMessage()
        {
            // Arrange & Act
            var response = new ExceptionResponse<InvalidOperationException>(
                "ERR001", "Something went wrong", new InvalidOperationException("test"));

            // Assert
            response.ErrorMessage.ShouldBe("Something went wrong");
        }

        [Test]
        public void ShouldSetException()
        {
            // Arrange
            var exception = new InvalidOperationException("test error");

            // Act
            var response = new ExceptionResponse<InvalidOperationException>("ERR001", "message", exception);

            // Assert
            response.Exception.ShouldBe(exception);
        }
    }
}
