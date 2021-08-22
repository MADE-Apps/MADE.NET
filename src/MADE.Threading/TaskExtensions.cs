// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Threading
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines a collection of extensions for tasks.
    /// </summary>
    public static class TaskExtensions
    {
        /// <summary>
        /// Observes the exceptions of faulted tasks.
        /// </summary>
        /// <param name="task">The task to observe for exceptions.</param>
        /// <param name="onException">An action invoked when an exception is caught.</param>
        /// <returns>An asynchronous operation.</returns>
        public static Task AndObserveExceptions(this Task task, Action<Exception> onException = null)
        {
            task?.ContinueWith(
                t =>
                {
                    AggregateException aggregateException = t.Exception?.Flatten();
                    onException?.Invoke(aggregateException);
                },
                TaskContinuationOptions.OnlyOnFaulted);

            return task;
        }

        /// <summary>
        /// Observes the exceptions of faulted tasks.
        /// </summary>
        /// <typeparam name="T">
        /// The instance type for the listener.
        /// </typeparam>
        /// <param name="task">The task to observe for exceptions.</param>
        /// <param name="onException">An action invoked when an exception is caught.</param>
        /// <returns>An asynchronous operation.</returns>
        public static Task<T> AndObserveExceptions<T>(this Task<T> task, Action<Exception> onException = null)
        {
            task?.ContinueWith(
                t =>
                {
                    AggregateException aggregateException = t.Exception?.Flatten();
                    onException?.Invoke(aggregateException);
                },
                TaskContinuationOptions.OnlyOnFaulted);

            return task;
        }
    }
}