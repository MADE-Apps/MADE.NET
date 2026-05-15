using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using Shouldly;

namespace MADE.Threading.Tests.Tests;

[ExcludeFromCodeCoverage]
[TestFixture]
public class AsyncLazyTests
{
    public class WhenGettingValue
    {
        [Test]
        public async Task ShouldReturnValueFromFactory()
        {
            // Arrange
            var lazy = new AsyncLazy<int>(() => Task.FromResult(42));

            // Act
            int result = await lazy;

            // Assert
            result.ShouldBe(42);
        }

        [Test]
        public async Task ShouldOnlyInvokeFactoryOnce()
        {
            // Arrange
            int callCount = 0;
            var lazy = new AsyncLazy<int>(() =>
            {
                Interlocked.Increment(ref callCount);
                return Task.FromResult(99);
            });

            // Act
            int result1 = await lazy;
            int result2 = await lazy;

            // Assert
            result1.ShouldBe(99);
            result2.ShouldBe(99);
            callCount.ShouldBe(1);
        }

        [Test]
        public async Task ShouldReportIsValueCreatedAfterAccess()
        {
            // Arrange
            var lazy = new AsyncLazy<string>(() => Task.FromResult("hello"));

            // Assert - before
            lazy.IsValueCreated.ShouldBeFalse();

            // Act
            await lazy;

            // Assert - after
            lazy.IsValueCreated.ShouldBeTrue();
        }

        [Test]
        public async Task ShouldReturnSameValueViaGetValueAsync()
        {
            // Arrange
            var lazy = new AsyncLazy<int>(() => Task.FromResult(7));

            // Act
            int result = await lazy.GetValueAsync();

            // Assert
            result.ShouldBe(7);
        }
    }
}
