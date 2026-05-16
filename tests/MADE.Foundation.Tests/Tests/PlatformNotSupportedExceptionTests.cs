using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using Shouldly;

using PlatformNotSupportedException = MADE.Foundation.Platform.PlatformNotSupportedException;

namespace MADE.Foundation.Tests.Tests;

[ExcludeFromCodeCoverage]
[TestFixture]
public class PlatformNotSupportedExceptionTests
{
    [Test]
    public void ShouldConstructWithDefaultMessage()
    {
        // Arrange & Act
        var exception = new PlatformNotSupportedException();

        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<PlatformNotSupportedException>();
        exception.ShouldBeAssignableTo<NotImplementedException>();
    }

    [Test]
    public void ShouldConstructWithMessage()
    {
        // Arrange
        const string message = "This API is not supported on this platform.";

        // Act
        var exception = new PlatformNotSupportedException(message);

        // Assert
        exception.Message.ShouldBe(message);
    }

    [Test]
    public void ShouldConstructWithMessageAndInnerException()
    {
        // Arrange
        const string message = "This API is not supported on this platform.";
        var inner = new InvalidOperationException("Inner error");

        // Act
        var exception = new PlatformNotSupportedException(message, inner);

        // Assert
        exception.Message.ShouldBe(message);
        exception.InnerException.ShouldBe(inner);
    }
}
