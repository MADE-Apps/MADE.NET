using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using Shouldly;

namespace MADE.Threading.Tests.Tests;

[ExcludeFromCodeCoverage]
[TestFixture]
public class AdaptiveSemaphoreTests
{
    public class WhenConstructing
    {
        [Test]
        public void ShouldClampInitialToMinimum()
        {
            // Arrange & Act
            using var semaphore = new AdaptiveSemaphore(initial: 0, minimum: 3);

            // Assert
            semaphore.Limit.ShouldBe(3);
            semaphore.Available.ShouldBe(3);
        }

        [Test]
        public void ShouldClampInitialToMaximum()
        {
            // Arrange & Act
            using var semaphore = new AdaptiveSemaphore(initial: 20, minimum: 1, maximum: 5);

            // Assert
            semaphore.Limit.ShouldBe(5);
            semaphore.Available.ShouldBe(5);
        }

        [Test]
        public void ShouldThrowWhenMinimumIsLessThanOne()
        {
            Should.Throw<ArgumentOutOfRangeException>(() => new AdaptiveSemaphore(initial: 1, minimum: 0));
        }

        [Test]
        public void ShouldThrowWhenMaximumIsLessThanMinimum()
        {
            Should.Throw<ArgumentOutOfRangeException>(() => new AdaptiveSemaphore(initial: 5, minimum: 3, maximum: 2));
        }
    }

    public class WhenAcquiringAndReleasing
    {
        [Test]
        public async Task ShouldAcquireAndReleasePermit()
        {
            // Arrange
            using var semaphore = new AdaptiveSemaphore(initial: 2);

            // Act
            using (await semaphore.WaitAsync())
            {
                semaphore.Available.ShouldBe(1);
            }

            // Assert
            semaphore.Available.ShouldBe(2);
        }

        [Test]
        public async Task ShouldLimitConcurrency()
        {
            // Arrange
            using var semaphore = new AdaptiveSemaphore(initial: 2);
            int concurrent = 0;
            int maxConcurrent = 0;

            // Act
            var tasks = Enumerable.Range(0, 10).Select(async _ =>
            {
                using (await semaphore.WaitAsync())
                {
                    var current = Interlocked.Increment(ref concurrent);
                    InterlockedMax(ref maxConcurrent, current);
                    await Task.Delay(50);
                    Interlocked.Decrement(ref concurrent);
                }
            });

            await Task.WhenAll(tasks);

            // Assert
            maxConcurrent.ShouldBeLessThanOrEqualTo(2);
        }

        [Test]
        public void ShouldAcquireAndReleaseSynchronously()
        {
            // Arrange
            using var semaphore = new AdaptiveSemaphore(initial: 2);

            // Act
            using (semaphore.Wait())
            {
                semaphore.Available.ShouldBe(1);
            }

            // Assert
            semaphore.Available.ShouldBe(2);
        }
    }

    public class WhenShrinking
    {
        [Test]
        public async Task ShouldReduceLimitByOne()
        {
            // Arrange
            using var semaphore = new AdaptiveSemaphore(initial: 5, minimum: 1);

            // Act
            int newLimit = await semaphore.TryShrinkAsync();

            // Assert
            newLimit.ShouldBe(4);
            semaphore.Limit.ShouldBe(4);
            semaphore.Available.ShouldBe(4);
        }

        [Test]
        public async Task ShouldNotShrinkBelowMinimum()
        {
            // Arrange
            using var semaphore = new AdaptiveSemaphore(initial: 2, minimum: 2);

            // Act
            int newLimit = await semaphore.TryShrinkAsync();

            // Assert
            newLimit.ShouldBe(2);
            semaphore.Limit.ShouldBe(2);
        }

        [Test]
        public async Task ShouldReduceAvailablePermits()
        {
            // Arrange
            using var semaphore = new AdaptiveSemaphore(initial: 3, minimum: 1);

            // Act
            await semaphore.TryShrinkAsync();
            await semaphore.TryShrinkAsync();

            // Assert
            semaphore.Limit.ShouldBe(1);
            semaphore.Available.ShouldBe(1);
        }

        [Test]
        public async Task ShouldRollBackLimitWhenCancelled()
        {
            // Arrange
            using var semaphore = new AdaptiveSemaphore(initial: 1, minimum: 1, maximum: 2);
            semaphore.TryGrow(); // limit = 2, available = 2

            // Exhaust both permits so the next WaitAsync will block.
            using var hold1 = await semaphore.WaitAsync();
            using var hold2 = await semaphore.WaitAsync();

            using var cts = new CancellationTokenSource();
            cts.Cancel();

            // Act - shrink should decrement limit then fail to acquire, rolling back.
            await Should.ThrowAsync<OperationCanceledException>(
                async () => await semaphore.TryShrinkAsync(cts.Token));

            // Assert - limit should be restored to 2.
            semaphore.Limit.ShouldBe(2);
        }
    }

    public class WhenGrowing
    {
        [Test]
        public void ShouldIncreaseLimitByOne()
        {
            // Arrange
            using var semaphore = new AdaptiveSemaphore(initial: 3, minimum: 1, maximum: 10);

            // Act
            int newLimit = semaphore.TryGrow();

            // Assert
            newLimit.ShouldBe(4);
            semaphore.Limit.ShouldBe(4);
            semaphore.Available.ShouldBe(4);
        }

        [Test]
        public void ShouldNotGrowAboveMaximum()
        {
            // Arrange
            using var semaphore = new AdaptiveSemaphore(initial: 5, minimum: 1, maximum: 5);

            // Act
            int newLimit = semaphore.TryGrow();

            // Assert
            newLimit.ShouldBe(5);
            semaphore.Limit.ShouldBe(5);
        }

        [Test]
        public void ShouldGrowWithoutMaximum()
        {
            // Arrange
            using var semaphore = new AdaptiveSemaphore(initial: 3, minimum: 1);

            // Act
            int newLimit = semaphore.TryGrow();

            // Assert
            newLimit.ShouldBe(4);
            semaphore.Available.ShouldBe(4);
        }
    }

    public class WhenShrinkingAndGrowing
    {
        [Test]
        public async Task ShouldRestoreLimitAfterShrinkAndGrow()
        {
            // Arrange
            using var semaphore = new AdaptiveSemaphore(initial: 5, minimum: 1, maximum: 10);

            // Act
            await semaphore.TryShrinkAsync();
            await semaphore.TryShrinkAsync();
            semaphore.Limit.ShouldBe(3);

            semaphore.TryGrow();
            semaphore.TryGrow();

            // Assert
            semaphore.Limit.ShouldBe(5);
            semaphore.Available.ShouldBe(5);
        }

        [Test]
        public async Task ShouldReduceEffectiveConcurrency()
        {
            // Arrange
            using var semaphore = new AdaptiveSemaphore(initial: 4, minimum: 1);
            int concurrent = 0;
            int maxConcurrent = 0;

            // Act - shrink to 2
            await semaphore.TryShrinkAsync();
            await semaphore.TryShrinkAsync();

            var tasks = Enumerable.Range(0, 10).Select(async _ =>
            {
                using (await semaphore.WaitAsync())
                {
                    var current = Interlocked.Increment(ref concurrent);
                    InterlockedMax(ref maxConcurrent, current);
                    await Task.Delay(50);
                    Interlocked.Decrement(ref concurrent);
                }
            });

            await Task.WhenAll(tasks);

            // Assert
            maxConcurrent.ShouldBeLessThanOrEqualTo(2);
        }
    }

    public class WhenDisposed
    {
        [Test]
        public void ShouldThrowOnWaitAfterDispose()
        {
            // Arrange
            var semaphore = new AdaptiveSemaphore(initial: 1);
            semaphore.Dispose();

            // Act & Assert
            Should.Throw<ObjectDisposedException>(() => semaphore.Wait());
        }

        [Test]
        public async Task ShouldThrowOnWaitAsyncAfterDispose()
        {
            // Arrange
            var semaphore = new AdaptiveSemaphore(initial: 1);
            semaphore.Dispose();

            // Act & Assert
            await Should.ThrowAsync<ObjectDisposedException>(async () => await semaphore.WaitAsync());
        }

        [Test]
        public async Task ShouldThrowOnShrinkAfterDispose()
        {
            // Arrange
            var semaphore = new AdaptiveSemaphore(initial: 5, minimum: 1);
            semaphore.Dispose();

            // Act & Assert
            await Should.ThrowAsync<ObjectDisposedException>(async () => await semaphore.TryShrinkAsync());
        }

        [Test]
        public void ShouldThrowOnGrowAfterDispose()
        {
            // Arrange
            var semaphore = new AdaptiveSemaphore(initial: 5, minimum: 1, maximum: 10);
            semaphore.Dispose();

            // Act & Assert
            Should.Throw<ObjectDisposedException>(() => semaphore.TryGrow());
        }
    }

    private static void InterlockedMax(ref int location, int value)
    {
        int current;
        do
        {
            current = location;
            if (value <= current)
            {
                return;
            }
        }
        while (Interlocked.CompareExchange(ref location, value, current) != current);
    }
}
