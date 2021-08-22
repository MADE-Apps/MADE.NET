// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Threading
{
    using System;
    using System.Collections.Generic;
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

        /// <summary>
        /// Creates a task that will complete when all of the <see cref="Task"/> objects in the collection have completed.
        /// </summary>
        /// <param name="tasks">The tasks to wait on for completion.</param>
        /// <returns>A task that represents the completion of all of the supplied tasks.</returns>
        public static async Task WhenAll(this IEnumerable<Task> tasks)
        {
            await Task.WhenAll(tasks);
        }

        /// <summary>
        /// Creates a task that will complete when any of the <see cref="Task"/> objects in the collection have completed.
        /// </summary>
        /// <param name="tasks">The tasks to wait on for completion.</param>
        /// <returns>A task that represents the completion of one of the supplied tasks.</returns>
        public static async Task WhenAny(this IEnumerable<Task> tasks)
        {
            await Task.WhenAny(tasks);
        }
    }
}