// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Threading
{
    using System;
    using System.Threading;

    /// <summary>
    /// Defines a timer to use for performing actions on a tick.
    /// </summary>
    public class Timer : ITimer, IDisposable
    {
        private System.Threading.Timer timer;

        /// <summary>
        /// Occurs when the timer ticks over the specified <see cref="Interval"/>.
        /// </summary>
        public event EventHandler<object> Tick;

        /// <summary>
        /// Gets or sets the interval between initiating the <see cref="Tick"/> event.
        /// </summary>
        public TimeSpan Interval { get; set; } = Timeout.InfiniteTimeSpan;

        /// <summary>
        /// Gets a value indicating whether the timer is currently running.
        /// </summary>
        public bool IsRunning { get; private set; }

        /// <summary>
        /// Gets or sets the time before initiating the first <see cref="Tick"/> event.
        /// </summary>
        public TimeSpan DueTime { get; set; } = TimeSpan.FromSeconds(0);

        /// <summary>
        /// Starts the timer.
        /// </summary>
        public void Start()
        {
            if (this.timer == null)
            {
                this.timer = new System.Threading.Timer(
                    c => this.InvokeTick(),
                    null,
                    0,
                    (int)Math.Ceiling(this.Interval.TotalMilliseconds));
            }
            else
            {
                this.timer.Change(
                    (int)Math.Ceiling(this.DueTime.TotalMilliseconds),
                    (int)Math.Ceiling(this.Interval.TotalMilliseconds));
            }

            this.IsRunning = true;
        }

        /// <summary>
        /// Starts the timer after the given <paramref name="dueTime"/>.
        /// </summary>
        /// <param name="dueTime">
        /// The time before initiating the first <see cref="Tick"/> event.
        /// </param>
        public void Start(TimeSpan dueTime)
        {
            if (this.timer == null)
            {
                this.timer = new System.Threading.Timer(
                    c => this.InvokeTick(),
                    null,
                    dueTime.Milliseconds,
                    (int)Math.Ceiling(this.Interval.TotalMilliseconds));
            }
            else
            {
                this.timer.Change(
                    (int)Math.Ceiling(this.DueTime.TotalMilliseconds),
                    (int)Math.Ceiling(this.Interval.TotalMilliseconds));
            }

            this.IsRunning = true;
        }

        /// <summary>
        /// Starts the timer after the given <paramref name="dueTime"/> in milliseconds.
        /// </summary>
        /// <param name="dueTime">
        /// The time before initiating the first <see cref="Tick"/> event in milliseconds.
        /// </param>
        public void Start(int dueTime)
        {
            if (this.timer == null)
            {
                this.timer = new System.Threading.Timer(
                    x => this.InvokeTick(),
                    null,
                    dueTime,
                    (int)Math.Ceiling(this.Interval.TotalMilliseconds));
            }
            else
            {
                this.timer.Change(
                    (int)Math.Ceiling(this.DueTime.TotalMilliseconds),
                    (int)Math.Ceiling(this.Interval.TotalMilliseconds));
            }

            this.IsRunning = true;
        }

        /// <summary>
        /// Stops the timer.
        /// </summary>
        public void Stop()
        {
            this.timer?.Change(Timeout.InfiniteTimeSpan, Timeout.InfiniteTimeSpan);
            this.IsRunning = false;
        }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Invokes the <see cref="Tick"/> event, if attached.
        /// </summary>
        protected virtual void InvokeTick()
        {
            this.Tick?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        /// <param name="disposing">
        /// A value indicating whether the object is being disposed by the <see cref="IDisposable.Dispose"/> method.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.timer?.Dispose();
            }
        }
    }
}