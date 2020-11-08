// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Threading
{
    using System;

    /// <summary>
    /// Defines an interface for a timer to use for performing actions on a tick.
    /// </summary>
    public interface ITimer
    {
        /// <summary>
        /// Occurs when the timer ticks over the specified <see cref="Interval"/>.
        /// </summary>
        event EventHandler<object> Tick;

        /// <summary>
        /// Gets or sets the interval between initiating the <see cref="Tick"/> event.
        /// </summary>
        TimeSpan Interval { get; set; }

        /// <summary>
        /// Gets a value indicating whether the timer is currently running.
        /// </summary>
        bool IsRunning { get; }

        /// <summary>
        /// Gets or sets the time before initiating the first <see cref="Tick"/> event.
        /// </summary>
        TimeSpan DueTime { get; set; }

        /// <summary>
        /// Starts the timer.
        /// </summary>
        void Start();

        /// <summary>
        /// Starts the timer after the given <paramref name="dueTime"/>.
        /// </summary>
        /// <param name="dueTime">
        /// The time before initiating the first <see cref="Tick"/> event.
        /// </param>
        void Start(TimeSpan dueTime);

        /// <summary>
        /// Starts the timer after the given <paramref name="dueTime"/> in milliseconds.
        /// </summary>
        /// <param name="dueTime">
        /// The time before initiating the first <see cref="Tick"/> event in milliseconds.
        /// </param>
        void Start(int dueTime);

        /// <summary>
        /// Stops the timer.
        /// </summary>
        void Stop();
    }
}