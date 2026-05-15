using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using Shouldly;

namespace MADE.Data.Validation.Tests.Tests;

[ExcludeFromCodeCoverage]
[TestFixture]
public class AsyncValidatorCollectionTests
{
    private class AlwaysValidAsyncValidator : IAsyncValidator
    {
        public string Key { get; set; } = "AlwaysValid";

        public bool IsInvalid { get; set; }

        public bool IsDirty { get; set; }

        public string FeedbackMessage { get; set; } = string.Empty;

        public Task ValidateAsync(object value, CancellationToken cancellationToken = default)
        {
            this.IsInvalid = false;
            this.IsDirty = true;
            return Task.CompletedTask;
        }
    }

    private class AlwaysInvalidAsyncValidator : IAsyncValidator
    {
        public string Key { get; set; } = "AlwaysInvalid";

        public bool IsInvalid { get; set; }

        public bool IsDirty { get; set; }

        public string FeedbackMessage { get; set; } = "Value is invalid.";

        public Task ValidateAsync(object value, CancellationToken cancellationToken = default)
        {
            this.IsInvalid = true;
            this.IsDirty = true;
            return Task.CompletedTask;
        }
    }

    public class WhenValidatingAsync
    {
        [Test]
        public async Task ShouldReportValidWhenAllValidatorsPass()
        {
            // Arrange
            var collection = new AsyncValidatorCollection { new AlwaysValidAsyncValidator() };

            // Act
            await collection.ValidateAsync("test");

            // Assert
            collection.IsInvalid.ShouldBeFalse();
            collection.IsDirty.ShouldBeTrue();
        }

        [Test]
        public async Task ShouldReportInvalidWhenAnyValidatorFails()
        {
            // Arrange
            var collection = new AsyncValidatorCollection
            {
                new AlwaysValidAsyncValidator(),
                new AlwaysInvalidAsyncValidator(),
            };

            // Act
            await collection.ValidateAsync("test");

            // Assert
            collection.IsInvalid.ShouldBeTrue();
        }

        [Test]
        public async Task ShouldPopulateFeedbackMessages()
        {
            // Arrange
            var collection = new AsyncValidatorCollection { new AlwaysInvalidAsyncValidator() };

            // Act
            await collection.ValidateAsync("test");

            // Assert
            collection.FeedbackMessages.ShouldContain("Value is invalid.");
        }

        [Test]
        public async Task ShouldFireValidatedEvent()
        {
            // Arrange
            var collection = new AsyncValidatorCollection { new AlwaysValidAsyncValidator() };
            bool eventFired = false;
            collection.Validated += (_, _) => eventFired = true;

            // Act
            await collection.ValidateAsync("test");

            // Assert
            eventFired.ShouldBeTrue();
        }

        [Test]
        public void ShouldSupportCancellation()
        {
            // Arrange
            var collection = new AsyncValidatorCollection { new AlwaysValidAsyncValidator() };
            using var cts = new CancellationTokenSource();
            cts.Cancel();

            // Act & Assert
            Should.ThrowAsync<OperationCanceledException>(
                () => collection.ValidateAsync("test", cts.Token));
        }
    }
}
