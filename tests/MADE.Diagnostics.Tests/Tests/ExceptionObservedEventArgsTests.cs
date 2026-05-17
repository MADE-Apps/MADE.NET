using System.Diagnostics.CodeAnalysis;
using MADE.Diagnostics.Exceptions;
using NUnit.Framework;
using Shouldly;

namespace MADE.Diagnostics.Tests.Tests;

[ExcludeFromCodeCoverage]
[TestFixture]
public class ExceptionObservedEventArgsTests
{
    public class WhenCreating
    {
        [Test]
        public void ShouldSetCorrelationId()
        {
            // Arrange
            var correlationId = Guid.NewGuid();
            var exception = new InvalidOperationException("Test");

            // Act
            var args = new ExceptionObservedEventArgs(correlationId, exception);

            // Assert
            args.CorrelationId.ShouldBe(correlationId);
        }

        [Test]
        public void ShouldSetException()
        {
            // Arrange
            var correlationId = Guid.NewGuid();
            var exception = new InvalidOperationException("Test error");

            // Act
            var args = new ExceptionObservedEventArgs(correlationId, exception);

            // Assert
            args.Exception.ShouldBe(exception);
            args.Exception.Message.ShouldBe("Test error");
        }
    }
}
