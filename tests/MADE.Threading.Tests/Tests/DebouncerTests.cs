using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using Shouldly;

namespace MADE.Threading.Tests.Tests;

[ExcludeFromCodeCoverage]
[TestFixture]
public class DebouncerTests
{
    public class WhenDebouncing
    {
        [Test]
        public async Task ShouldExecuteActionAfterDelay()
        {
            // Arrange
            using var debouncer = new Debouncer { Delay = TimeSpan.FromMilliseconds(50) };
            int callCount = 0;

            // Act
            debouncer.Debounce(() => Interlocked.Increment(ref callCount));
            await Task.Delay(150);

            // Assert
            callCount.ShouldBe(1);
        }

        [Test]
        public async Task ShouldCollapseRapidInvocationsIntoOne()
        {
            // Arrange
            using var debouncer = new Debouncer { Delay = TimeSpan.FromMilliseconds(100) };
            int callCount = 0;

            // Act - rapid fire
            debouncer.Debounce(() => Interlocked.Increment(ref callCount));
            await Task.Delay(20);
            debouncer.Debounce(() => Interlocked.Increment(ref callCount));
            await Task.Delay(20);
            debouncer.Debounce(() => Interlocked.Increment(ref callCount));
            await Task.Delay(200);

            // Assert - only the last one should have executed
            callCount.ShouldBe(1);
        }

        [Test]
        public async Task ShouldNotExecuteAfterCancel()
        {
            // Arrange
            using var debouncer = new Debouncer { Delay = TimeSpan.FromMilliseconds(100) };
            int callCount = 0;

            // Act
            debouncer.Debounce(() => Interlocked.Increment(ref callCount));
            debouncer.Cancel();
            await Task.Delay(200);

            // Assert
            callCount.ShouldBe(0);
        }
    }
}
