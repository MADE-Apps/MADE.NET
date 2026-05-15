// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Threading;

/// <summary>
/// Defines a debouncer that delays execution of an action until a specified period of inactivity has elapsed.
/// </summary>
/// <remarks>
/// This is useful for scenarios where rapid invocations should be collapsed into a single execution, such as search-as-you-type or window resize handling.
/// </remarks>
public sealed class Debouncer : IDisposable
{
    private readonly object lockObj = new();

    private CancellationTokenSource? cts;

    private bool disposed;

    /// <summary>
    /// Gets or sets the delay period. Each invocation resets the timer.
    /// </summary>
    public TimeSpan Delay { get; set; } = TimeSpan.FromMilliseconds(300);

    /// <summary>
    /// Debounces the specified action. If called again before the <see cref="Delay"/> elapses, the previous pending invocation is cancelled.
    /// </summary>
    /// <param name="action">The action to execute after the delay.</param>
    /// <exception cref="ArgumentNullException">Thrown if the <paramref name="action"/> is <see langword="null"/>.</exception>
    /// <exception cref="ObjectDisposedException">Thrown if the debouncer has been disposed.</exception>
    public void Debounce(Action action)
    {
        ArgumentNullException.ThrowIfNull(action);
        ObjectDisposedException.ThrowIf(this.disposed, this);

        lock (this.lockObj)
        {
            this.cts?.Cancel();
            this.cts?.Dispose();
            this.cts = new CancellationTokenSource();

            var token = this.cts.Token;

            Task.Delay(this.Delay, token).ContinueWith(
                _ => action(),
                token,
                TaskContinuationOptions.OnlyOnRanToCompletion,
                TaskScheduler.Default);
        }
    }

    /// <summary>
    /// Debounces the specified asynchronous action. If called again before the <see cref="Delay"/> elapses, the previous pending invocation is cancelled.
    /// </summary>
    /// <param name="action">The asynchronous action to execute after the delay.</param>
    /// <exception cref="ArgumentNullException">Thrown if the <paramref name="action"/> is <see langword="null"/>.</exception>
    /// <exception cref="ObjectDisposedException">Thrown if the debouncer has been disposed.</exception>
    public void DebounceAsync(Func<Task> action)
    {
        ArgumentNullException.ThrowIfNull(action);
        ObjectDisposedException.ThrowIf(this.disposed, this);

        lock (this.lockObj)
        {
            this.cts?.Cancel();
            this.cts?.Dispose();
            this.cts = new CancellationTokenSource();

            var token = this.cts.Token;

            Task.Delay(this.Delay, token).ContinueWith(
                async _ => await action().ConfigureAwait(false),
                token,
                TaskContinuationOptions.OnlyOnRanToCompletion,
                TaskScheduler.Default);
        }
    }

    /// <summary>
    /// Cancels any pending debounced action.
    /// </summary>
    public void Cancel()
    {
        lock (this.lockObj)
        {
            this.cts?.Cancel();
        }
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        if (this.disposed)
        {
            return;
        }

        lock (this.lockObj)
        {
            this.cts?.Cancel();
            this.cts?.Dispose();
            this.cts = null;
        }

        this.disposed = true;
    }
}
