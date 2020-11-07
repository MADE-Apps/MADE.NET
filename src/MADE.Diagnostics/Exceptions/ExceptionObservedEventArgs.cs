// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Diagnostics.Exceptions
{
    using System;

    /// <summary>
    /// Defines an event argument for an observed exception.
    /// </summary>
    public class ExceptionObservedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionObservedEventArgs"/> class.
        /// </summary>
        /// <param name="correlationId">
        /// The unique identifier for correlating the exception.
        /// </param>
        /// <param name="exception">
        /// The exception that was observed.
        /// </param>
        public ExceptionObservedEventArgs(Guid correlationId, Exception exception)
        {
            this.CorrelationId = correlationId;
            this.Exception = exception;
        }

        /// <summary>
        /// Gets the unique identifier for correlating the exception.
        /// </summary>
        public Guid CorrelationId { get; }

        /// <summary>
        /// Gets the exception that was observed.
        /// </summary>
        public Exception Exception { get; }
    }
}