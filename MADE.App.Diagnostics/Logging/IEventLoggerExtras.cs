// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IEventLoggerExtras.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines an interface for exception support for a logging service.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MADE.App.Diagnostics.Logging
{
    using System;

    /// <summary>
    /// Defines an interface for exception support for a logging service.
    /// </summary>
    public interface IEventLoggerExtras
    {
        /// <summary>
        /// Writes an exception to the event log as a debug message.
        /// </summary>
        /// <param name="message">
        /// The message to write out.
        /// </param>
        /// <param name="ex">
        /// The exception to write out.
        /// </param>
        void WriteDebug(string message, Exception ex);

        /// <summary>
        /// Writes an exception to the event log as a debug message.
        /// </summary>
        /// <param name="ex">
        /// The exception to write out.
        /// </param>
        void WriteDebug(Exception ex);

        /// <summary>
        /// Writes an exception to the event log as a generic information message.
        /// </summary>
        /// <param name="message">
        /// The message to write out.
        /// </param>
        /// <param name="ex">
        /// The exception to write out.
        /// </param>
        void WriteInfo(string message, Exception ex);

        /// <summary>
        /// Writes an exception to the event log as a generic information message.
        /// </summary>
        /// <param name="ex">
        /// The exception to write out.
        /// </param>
        void WriteInfo(Exception ex);

        /// <summary>
        /// Writes an exception to the event log as a warning message.
        /// </summary>
        /// <param name="message">
        /// The message to write out.
        /// </param>
        /// <param name="ex">
        /// The exception to write out.
        /// </param>
        void WriteWarning(string message, Exception ex);

        /// <summary>
        /// Writes an exception to the event log as a warning message.
        /// </summary>
        /// <param name="ex">
        /// The exception to write out.
        /// </param>
        void WriteWarning(Exception ex);

        /// <summary>
        /// Writes an exception to the event log as an error message.
        /// </summary>
        /// <param name="message">
        /// The message to write out.
        /// </param>
        /// <param name="ex">
        /// The exception to write out.
        /// </param>
        void WriteError(string message, Exception ex);

        /// <summary>
        /// Writes an exception to the event log as an error message.
        /// </summary>
        /// <param name="ex">
        /// The exception to write out.
        /// </param>
        void WriteError(Exception ex);

        /// <summary>
        /// Writes an exception to the event log as a critical message.
        /// </summary>
        /// <param name="message">
        /// The message to write out.
        /// </param>
        /// <param name="ex">
        /// The exception to write out.
        /// </param>
        void WriteCritical(string message, Exception ex);

        /// <summary>
        /// Writes an exception to the event log as a critical message.
        /// </summary>
        /// <param name="ex">
        /// The exception to write out.
        /// </param>
        void WriteCritical(Exception ex);
    }
}