// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExceptionObservedEventArgs.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines an event argument for an observed exception.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MADE.App.Diagnostics.Exceptions
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
        /// <param name="exceptionId">
        /// The ID associated with the exception.
        /// </param>
        /// <param name="exception">
        /// The exception that was observed.
        /// </param>
        public ExceptionObservedEventArgs(Guid exceptionId, Exception exception)
        {
            this.ExceptionId = exceptionId;
            this.Exception = exception;
        }

        /// <summary>
        /// Gets the ID associated with the exception.
        /// </summary>
        public Guid ExceptionId { get; }

        /// <summary>
        /// Gets the exception that was observed.
        /// </summary>
        public Exception Exception { get; }
    }
}