using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using Shouldly;

namespace MADE.Threading.Tests.Tests;

[ExcludeFromCodeCoverage]
[TestFixture]
public class TimerTests
{
    public class WhenStarting
    {
        [Test]
        public async Task ShouldSetIsRunningToTrue()
        {
            // Arrange
            using var timer = new Timer { Interval = TimeSpan.FromMilliseconds(500) };

            // Act
            timer.Start();

            // Assert
            timer.IsRunning.ShouldBeTrue();

            await Task.Delay(50);
        }

        [Test]
        public async Task ShouldTickAtInterval()
        {
            // Arrange
            using var timer = new Timer { Interval = TimeSpan.FromMilliseconds(50) };
            int tickCount = 0;
            timer.Tick += (_, _) => Interlocked.Increment(ref tickCount);

            // Act
            timer.Start();
            await Task.Delay(200);
            timer.Stop();

            // Assert
            tickCount.ShouldBeGreaterThan(0);
        }
    }

    public class WhenStartingWithTimeSpanDueTime
    {
        [Test]
        public async Task ShouldStoreDueTimeProperty()
        {
            // Arrange
            using var timer = new Timer { Interval = TimeSpan.FromMilliseconds(500) };
            var dueTime = TimeSpan.FromMilliseconds(100);

            // Act
            timer.Start(dueTime);

            // Assert
            timer.DueTime.ShouldBe(dueTime);
            timer.IsRunning.ShouldBeTrue();

            await Task.Delay(50);
        }

        [Test]
        public async Task ShouldDelayFirstTickByDueTime()
        {
            // Arrange
            using var timer = new Timer { Interval = TimeSpan.FromMilliseconds(500) };
            int tickCount = 0;
            timer.Tick += (_, _) => Interlocked.Increment(ref tickCount);

            // Act
            timer.Start(TimeSpan.FromMilliseconds(150));
            await Task.Delay(50);

            // Assert - should not have ticked yet
            tickCount.ShouldBe(0);

            // Wait for the due time to pass
            await Task.Delay(200);
            tickCount.ShouldBeGreaterThan(0);
        }
    }

    public class WhenStartingWithIntDueTime
    {
        [Test]
        public async Task ShouldStoreDueTimeProperty()
        {
            // Arrange
            using var timer = new Timer { Interval = TimeSpan.FromMilliseconds(500) };

            // Act
            timer.Start(100);

            // Assert
            timer.DueTime.ShouldBe(TimeSpan.FromMilliseconds(100));
            timer.IsRunning.ShouldBeTrue();

            await Task.Delay(50);
        }

        [Test]
        public async Task ShouldDelayFirstTickByDueTime()
        {
            // Arrange
            using var timer = new Timer { Interval = TimeSpan.FromMilliseconds(500) };
            int tickCount = 0;
            timer.Tick += (_, _) => Interlocked.Increment(ref tickCount);

            // Act
            timer.Start(150);
            await Task.Delay(50);

            // Assert - should not have ticked yet
            tickCount.ShouldBe(0);

            // Wait for the due time to pass
            await Task.Delay(200);
            tickCount.ShouldBeGreaterThan(0);
        }

        [Test]
        public async Task ShouldUseDueTimeOnRestart()
        {
            // Arrange
            using var timer = new Timer { Interval = TimeSpan.FromMilliseconds(500) };
            int tickCount = 0;
            timer.Tick += (_, _) => Interlocked.Increment(ref tickCount);

            // Act - start once to create internal timer, stop, then restart with int dueTime
            timer.Start();
            await Task.Delay(50);
            timer.Stop();

            tickCount = 0;
            timer.Start(200);
            await Task.Delay(50);

            // Assert - should not have ticked yet (dueTime not elapsed)
            tickCount.ShouldBe(0);

            await Task.Delay(250);
            tickCount.ShouldBeGreaterThan(0);
        }
    }

    public class WhenStopping
    {
        [Test]
        public async Task ShouldSetIsRunningToFalse()
        {
            // Arrange
            using var timer = new Timer { Interval = TimeSpan.FromMilliseconds(500) };
            timer.Start();

            // Act
            timer.Stop();

            // Assert
            timer.IsRunning.ShouldBeFalse();

            await Task.Delay(50);
        }

        [Test]
        public async Task ShouldStopTicking()
        {
            // Arrange
            using var timer = new Timer { Interval = TimeSpan.FromMilliseconds(50) };
            int tickCount = 0;
            timer.Tick += (_, _) => Interlocked.Increment(ref tickCount);

            timer.Start();
            await Task.Delay(150);
            timer.Stop();

            int ticksAfterStop = tickCount;
            await Task.Delay(150);

            // Assert - no additional ticks after stopping
            tickCount.ShouldBe(ticksAfterStop);
        }
    }

    public class WhenDisposing
    {
        [Test]
        public void ShouldNotThrowWhenDisposedWithoutStarting()
        {
            // Arrange
            var timer = new Timer();

            // Act & Assert
            Should.NotThrow(() => timer.Dispose());
        }

        [Test]
        public async Task ShouldNotThrowWhenDisposedAfterStarting()
        {
            // Arrange
            var timer = new Timer { Interval = TimeSpan.FromMilliseconds(50) };
            timer.Start();
            await Task.Delay(100);

            // Act & Assert
            Should.NotThrow(() => timer.Dispose());
        }
    }
}
