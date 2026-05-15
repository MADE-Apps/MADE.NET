// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Testing;

/// <summary>
/// Defines a code assertion helper for exception-based scenarios.
/// </summary>
public static class ExceptionAssertExtensions
{
    /// <summary>
    /// Tests whether the specified action throws an exception of the given type and throws an assertion exception if it does not.
    /// </summary>
    /// <typeparam name="TException">The type of exception expected to be thrown.</typeparam>
    /// <param name="action">The action to execute.</param>
    /// <returns>The exception that was thrown.</returns>
    /// <exception cref="AssertFailedException">Thrown if the <paramref name="action"/> does not throw an exception of type <typeparamref name="TException"/>.</exception>
    public static TException ShouldThrow<TException>(this Action action)
        where TException : Exception
    {
        try
        {
            action();
        }
        catch (TException ex)
        {
            return ex;
        }
        catch (Exception ex)
        {
            throw new AssertFailedException($"{nameof(ShouldThrow)} failed. Expected exception of type '{typeof(TException).Name}' but '{ex.GetType().Name}' was thrown.");
        }

        throw new AssertFailedException($"{nameof(ShouldThrow)} failed. Expected exception of type '{typeof(TException).Name}' but no exception was thrown.");
    }

    /// <summary>
    /// Tests whether the specified asynchronous action throws an exception of the given type and throws an assertion exception if it does not.
    /// </summary>
    /// <typeparam name="TException">The type of exception expected to be thrown.</typeparam>
    /// <param name="action">The asynchronous action to execute.</param>
    /// <returns>The exception that was thrown.</returns>
    /// <exception cref="AssertFailedException">Thrown if the <paramref name="action"/> does not throw an exception of type <typeparamref name="TException"/>.</exception>
    public static async Task<TException> ShouldThrowAsync<TException>(this Func<Task> action)
        where TException : Exception
    {
        try
        {
            await action().ConfigureAwait(false);
        }
        catch (TException ex)
        {
            return ex;
        }
        catch (Exception ex)
        {
            throw new AssertFailedException($"{nameof(ShouldThrowAsync)} failed. Expected exception of type '{typeof(TException).Name}' but '{ex.GetType().Name}' was thrown.");
        }

        throw new AssertFailedException($"{nameof(ShouldThrowAsync)} failed. Expected exception of type '{typeof(TException).Name}' but no exception was thrown.");
    }

    /// <summary>
    /// Tests whether the specified action does not throw any exception and throws an assertion exception if it does.
    /// </summary>
    /// <param name="action">The action to execute.</param>
    /// <exception cref="AssertFailedException">Thrown if the <paramref name="action"/> throws an exception.</exception>
    public static void ShouldNotThrow(this Action action)
    {
        try
        {
            action();
        }
        catch (Exception ex)
        {
            throw new AssertFailedException($"{nameof(ShouldNotThrow)} failed. Expected no exception but '{ex.GetType().Name}' was thrown: {ex.Message}");
        }
    }

    /// <summary>
    /// Tests whether the specified asynchronous action does not throw any exception and throws an assertion exception if it does.
    /// </summary>
    /// <param name="action">The asynchronous action to execute.</param>
    /// <exception cref="AssertFailedException">Thrown if the <paramref name="action"/> throws an exception.</exception>
    public static async Task ShouldNotThrowAsync(this Func<Task> action)
    {
        try
        {
            await action().ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            throw new AssertFailedException($"{nameof(ShouldNotThrowAsync)} failed. Expected no exception but '{ex.GetType().Name}' was thrown: {ex.Message}");
        }
    }
}
