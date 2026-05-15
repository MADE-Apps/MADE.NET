using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using Shouldly;

namespace MADE.Threading.Tests.Tests;

[ExcludeFromCodeCoverage]
[TestFixture]
public class ThrottlerTests
{
    public class WhenThrottling
    {
        [Test]
        public void ShouldExecuteFirstInvocationImmediately()
        {
            // Arrange
            using var throttler = new Throttler { Interval = TimeSpan.FromMilliseconds(500) };
            int callCount = 0;

            // Act
            throttler.Throttle(() => Interlocked.Increment(ref callCount));

            // Assert
            callCount.ShouldBe(1);
        }

        [Test]
        public void ShouldSuppressRapidInvocations()
        {
            // Arrange
            using var throttler = new Throttler { Interval = TimeSpan.FromMilliseconds(500) };
            int callCount = 0;

            // Act
            throttler.Throttle(() => Interlocked.Increment(ref callCount));
            throttler.Throttle(() => Interlocked.Increment(ref callCount));
            throttler.Throttle(() => Interlocked.Increment(ref callCount));

            // Assert
            callCount.ShouldBe(1);
        }

        [Test]
        public async Task ShouldAllowExecutionAfterIntervalElapses()
        {
            // Arrange
            using var throttler = new Throttler { Interval = TimeSpan.FromMilliseconds(50) };
            int callCount = 0;

            // Act
            throttler.Throttle(() => Interlocked.Increment(ref callCount));
            await Task.Delay(100);
            throttler.Throttle(() => Interlocked.Increment(ref callCount));

            // Assert
            callCount.ShouldBe(2);
        }
    }

    public class WhenThrottlingAsync
    {
        [Test]
        public async Task ShouldSuppressRapidAsyncInvocations()
        {
            // Arrange
            using var throttler = new Throttler { Interval = TimeSpan.FromMilliseconds(500) };
            int callCount = 0;

            // Act
            await throttler.ThrottleAsync(() => { Interlocked.Increment(ref callCount); return Task.CompletedTask; });
            await throttler.ThrottleAsync(() => { Interlocked.Increment(ref callCount); return Task.CompletedTask; });

            // Assert
            callCount.ShouldBe(1);
        }
    }
}
