// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Threading;

/// <summary>
/// Defines a throttler that limits execution of an action to at most once per specified time interval.
/// </summary>
/// <remarks>
/// This is useful for scenarios where you want to limit the rate of execution,
/// such as rate-limiting API calls or UI updates.
/// Unlike <see cref="Debouncer"/>, the throttler executes the first invocation immediately
/// and then suppresses subsequent invocations until the interval elapses.
/// </remarks>
public sealed class Throttler : IDisposable
{
    private readonly object lockObj = new();

    private readonly SemaphoreSlim semaphore = new(1, 1);

    private DateTime lastInvocation = DateTime.MinValue;

    private bool disposed;

    /// <summary>
    /// Gets or sets the minimum interval between executions.
    /// </summary>
    public TimeSpan Interval { get; set; } = TimeSpan.FromMilliseconds(300);

    /// <summary>
    /// Throttles the specified action. Executes immediately if the <see cref="Interval"/> has elapsed since the last execution; otherwise, the invocation is suppressed.
    /// </summary>
    /// <param name="action">The action to execute.</param>
    /// <exception cref="ArgumentNullException">Thrown if the <paramref name="action"/> is <see langword="null"/>.</exception>
    /// <exception cref="ObjectDisposedException">Thrown if the throttler has been disposed.</exception>
    public void Throttle(Action action)
    {
        ArgumentNullException.ThrowIfNull(action);
        ObjectDisposedException.ThrowIf(this.disposed, this);

        lock (this.lockObj)
        {
            var now = DateTime.UtcNow;
            if (now - this.lastInvocation < this.Interval)
            {
                return;
            }

            this.lastInvocation = now;
        }

        action();
    }

    /// <summary>
    /// Throttles the specified asynchronous action. Executes immediately if the <see cref="Interval"/> has elapsed since the last execution; otherwise, the invocation is suppressed.
    /// </summary>
    /// <param name="action">The asynchronous action to execute.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>An asynchronous operation.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the <paramref name="action"/> is <see langword="null"/>.</exception>
    /// <exception cref="ObjectDisposedException">Thrown if the throttler has been disposed.</exception>
    /// <exception cref="OperationCanceledException">Thrown if the <paramref name="cancellationToken"/> is cancelled.</exception>
    public async Task ThrottleAsync(Func<Task> action, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(action);
        ObjectDisposedException.ThrowIf(this.disposed, this);

        await this.semaphore.WaitAsync(cancellationToken).ConfigureAwait(false);

        try
        {
            var now = DateTime.UtcNow;
            if (now - this.lastInvocation < this.Interval)
            {
                return;
            }

            this.lastInvocation = now;
        }
        finally
        {
            this.semaphore.Release();
        }

        await action().ConfigureAwait(false);
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        if (this.disposed)
        {
            return;
        }

        this.semaphore.Dispose();
        this.disposed = true;
    }
}
