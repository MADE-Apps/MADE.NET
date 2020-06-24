// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IEventLogger.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines an interface for a logging service.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MADE.App.Diagnostics.Logging
{
    /// <summary>
    /// Defines an interface for a logging service.
    /// </summary>
    public interface IEventLogger : IEventLoggerExtras
    {
        /// <summary>
        /// Gets or sets the path to where the log exists.
        /// </summary>
        string LogPath { get; set; }

        /// <summary>
        /// Writes a debug information message to the event log when in DEBUG mode.
        /// </summary>
        /// <param name="message">
        /// The message to write out.
        /// </param>
        void WriteDebug(string message);

        /// <summary>
        /// Writes a generic information message to the event log.
        /// </summary>
        /// <param name="message">
        /// The message to write out.
        /// </param>
        void WriteInfo(string message);

        /// <summary>
        /// Writes a warning message to the event log.
        /// </summary>
        /// <param name="message">
        /// The message to write out.
        /// </param>
        void WriteWarning(string message);

        /// <summary>
        /// Writes an error message to the event log.
        /// </summary>
        /// <param name="message">
        /// The message to write out.
        /// </param>
        void WriteError(string message);

        /// <summary>
        /// Writes a critical error message to the event log.
        /// </summary>
        /// <param name="message">
        /// The message to write out.
        /// </param>
        void WriteCritical(string message);
    }
}