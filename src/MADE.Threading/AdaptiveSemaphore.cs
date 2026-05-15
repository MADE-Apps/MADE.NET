// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Threading;

/// <summary>
/// Defines a semaphore that allows adjusting the concurrency limit at runtime to respond to backpressure.
/// </summary>
/// <remarks>
/// This is useful for scenarios where the permitted concurrency should change dynamically,
/// such as reducing parallelism when a downstream service returns rate-limit (429) responses
/// or increasing it when conditions improve.
/// <code>
/// var semaphore = new AdaptiveSemaphore(initial: 10, minimum: 1, maximum: 20);
///
/// // Normal usage - acquire and release a permit.
/// using (await semaphore.WaitAsync())
/// {
///     await httpClient.SendAsync(request);
/// }
///
/// // Reduce concurrency on backpressure.
/// await semaphore.TryShrinkAsync();
/// </code>
/// </remarks>
public sealed class AdaptiveSemaphore : IDisposable
{
    private readonly int minimum;
    private readonly int? maximum;
    private readonly SemaphoreSlim semaphore;
    private readonly object adjustLock = new();

    private int limit;
    private bool disposed;

    /// <summary>
    /// Initializes a new instance of the <see cref="AdaptiveSemaphore"/> class.
    /// </summary>
    /// <param name="initial">The initial number of concurrent permits.</param>
    /// <param name="minimum">The minimum concurrency limit. Defaults to 1.</param>
    /// <param name="maximum">The optional maximum concurrency limit.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="minimum"/> is less than 1, or <paramref name="maximum"/> is less than <paramref name="minimum"/>.</exception>
    public AdaptiveSemaphore(int initial, int minimum = 1, int? maximum = null)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(minimum, 1);

        if (maximum.HasValue)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(maximum.Value, minimum, nameof(maximum));
        }

        this.minimum = minimum;
        this.maximum = maximum;

        var bounded = Math.Max(minimum, initial);
        if (maximum.HasValue)
        {
            bounded = Math.Min(bounded, maximum.Value);
        }

        this.limit = bounded;
        this.semaphore = new SemaphoreSlim(bounded);
    }

    /// <summary>
    /// Gets the current concurrency limit.
    /// </summary>
    public int Limit => this.limit;

    /// <summary>
    /// Gets the number of permits currently available.
    /// </summary>
    public int Available => this.semaphore.CurrentCount;

    /// <summary>
    /// Reduces the concurrency limit by one by permanently acquiring a permit.
    /// </summary>
    /// <remarks>
    /// If the limit is already at the <see cref="minimum"/>, this method does nothing.
    /// The acquired permit is not released, effectively reducing the pool of available permits.
    /// </remarks>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The new concurrency limit.</returns>
    /// <exception cref="ObjectDisposedException">Thrown if the semaphore has been disposed.</exception>
    public async Task<int> TryShrinkAsync(CancellationToken cancellationToken = default)
    {
        ObjectDisposedException.ThrowIf(this.disposed, this);

        lock (this.adjustLock)
        {
            if (this.limit <= this.minimum)
            {
                return this.limit;
            }

            this.limit--;
        }

        try
        {
            await this.semaphore.WaitAsync(cancellationToken).ConfigureAwait(false);
        }
        catch
        {
            lock (this.adjustLock)
            {
                this.limit++;
            }

            throw;
        }

        return this.limit;
    }

    /// <summary>
    /// Increases the concurrency limit by one by releasing an additional permit.
    /// </summary>
    /// <remarks>
    /// If the limit is already at the <see cref="maximum"/>, this method does nothing.
    /// This is the inverse of <see cref="TryShrinkAsync"/> and should be called when
    /// conditions improve and more concurrency is desirable.
    /// </remarks>
    /// <returns>The new concurrency limit.</returns>
    /// <exception cref="ObjectDisposedException">Thrown if the semaphore has been disposed.</exception>
    public int TryGrow()
    {
        ObjectDisposedException.ThrowIf(this.disposed, this);

        lock (this.adjustLock)
        {
            if (this.maximum.HasValue && this.limit >= this.maximum.Value)
            {
                return this.limit;
            }

            this.limit++;

            try
            {
                this.semaphore.Release();
            }
            catch
            {
                this.limit--;
                throw;
            }

            return this.limit;
        }
    }

    /// <summary>
    /// Asynchronously waits to acquire a permit from the semaphore.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>An <see cref="IDisposable"/> that releases the permit when disposed.</returns>
    /// <exception cref="ObjectDisposedException">Thrown if the semaphore has been disposed.</exception>
    public async Task<IDisposable> WaitAsync(CancellationToken cancellationToken = default)
    {
        ObjectDisposedException.ThrowIf(this.disposed, this);
        await this.semaphore.WaitAsync(cancellationToken).ConfigureAwait(false);
        return new SemaphoreReleaser(this.semaphore);
    }

    /// <summary>
    /// Synchronously waits to acquire a permit from the semaphore.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>An <see cref="IDisposable"/> that releases the permit when disposed.</returns>
    /// <exception cref="ObjectDisposedException">Thrown if the semaphore has been disposed.</exception>
    public IDisposable Wait(CancellationToken cancellationToken = default)
    {
        ObjectDisposedException.ThrowIf(this.disposed, this);
        this.semaphore.Wait(cancellationToken);
        return new SemaphoreReleaser(this.semaphore);
    }

    /// <inheritdoc />
    public void Dispose()
    {
        lock (this.adjustLock)
        {
            if (this.disposed)
            {
                return;
            }

            this.semaphore.Dispose();
            this.disposed = true;
        }
    }

    private sealed class SemaphoreReleaser : IDisposable
    {
        private SemaphoreSlim? semaphore;

        public SemaphoreReleaser(SemaphoreSlim semaphore)
        {
            this.semaphore = semaphore;
        }

        public void Dispose()
        {
            Interlocked.Exchange(ref this.semaphore, null)?.Release();
        }
    }
}
