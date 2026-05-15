// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Runtime.CompilerServices;

namespace MADE.Threading;

/// <summary>
/// Defines a provider for lazy asynchronous initialization of a value.
/// </summary>
/// <typeparam name="T">The type of object that is being lazily initialized.</typeparam>
public class AsyncLazy<T>
{
    private readonly Lazy<Task<T>> inner;

    /// <summary>
    /// Initializes a new instance of the <see cref="AsyncLazy{T}"/> class with the specified asynchronous value factory.
    /// </summary>
    /// <param name="valueFactory">The asynchronous delegate that is invoked to produce the lazily initialized value when it is needed.</param>
    public AsyncLazy(Func<Task<T>> valueFactory)
    {
        this.inner = new Lazy<Task<T>>(valueFactory);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AsyncLazy{T}"/> class with the specified asynchronous value factory and thread safety mode.
    /// </summary>
    /// <param name="valueFactory">The asynchronous delegate that is invoked to produce the lazily initialized value when it is needed.</param>
    /// <param name="isThreadSafe">A value indicating whether the <see cref="AsyncLazy{T}"/> instance should be usable concurrently by multiple threads.</param>
    public AsyncLazy(Func<Task<T>> valueFactory, bool isThreadSafe)
    {
        this.inner = new Lazy<Task<T>>(valueFactory, isThreadSafe);
    }

    /// <summary>
    /// Gets a value indicating whether a value has been created.
    /// </summary>
    public bool IsValueCreated => this.inner.IsValueCreated;

    /// <summary>
    /// Gets the lazily initialized value as an awaitable task.
    /// </summary>
    /// <returns>An awaitable task that returns the lazily initialized value.</returns>
    public TaskAwaiter<T> GetAwaiter()
    {
        return this.inner.Value.GetAwaiter();
    }

    /// <summary>
    /// Gets the lazily initialized value as a task.
    /// </summary>
    /// <returns>A task that returns the lazily initialized value.</returns>
    public Task<T> GetValueAsync()
    {
        return this.inner.Value;
    }
}
