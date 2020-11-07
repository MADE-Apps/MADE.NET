// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Diagnostics.Logging
{
    using System;

    /// <summary>
    /// Defines an interface for an event logging service.
    /// </summary>
    public interface IEventLogger
    {
        /// <summary>
        /// Writes a debug information message to the event log when in DEBUG mode.
        /// </summary>
        /// <param name="message">
        /// The message to write out.
        /// </param>
        void WriteDebug(string message);

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
        /// Writes a generic information message to the event log.
        /// </summary>
        /// <param name="message">
        /// The message to write out.
        /// </param>
        void WriteInfo(string message);

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
        /// Writes a warning message to the event log.
        /// </summary>
        /// <param name="message">
        /// The message to write out.
        /// </param>
        void WriteWarning(string message);

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
        /// Writes an error message to the event log.
        /// </summary>
        /// <param name="message">
        /// The message to write out.
        /// </param>
        void WriteError(string message);

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
        /// Writes a critical error message to the event log.
        /// </summary>
        /// <param name="message">
        /// The message to write out.
        /// </param>
        void WriteCritical(string message);

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
