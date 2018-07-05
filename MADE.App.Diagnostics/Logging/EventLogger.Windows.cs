// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EventLogger.Windows.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines a service for logging informational messages to the Windows event log.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

#if WINDOWS_UWP
namespace MADE.App.Diagnostics.Logging
{
    using System.Diagnostics.Tracing;

    /// <summary>
    /// Defines a service for logging informational messages to the Windows event log.
    /// </summary>
    public class EventLogger : EventSource, IEventLogger
    {
        /// <summary>
        /// Gets or sets the path to where the log exists.
        /// </summary>
        public string LogPath { get; set; }

        /// <summary>
        /// Writes a debug information message to the event log when in DEBUG mode.
        /// </summary>
        /// <param name="message">
        /// The message to write out.
        /// </param>
        [Event(1, Level = EventLevel.Verbose)]
        public void WriteDebug(string message)
        {
            if (!System.Diagnostics.Debugger.IsAttached)
            {
                return;
            }

            System.Diagnostics.Debug.WriteLine(message);
            this.WriteEvent(1, message);
        }

        /// <summary>
        /// Writes a generic information message to the event log.
        /// </summary>
        /// <param name="message">
        /// The message to write out.
        /// </param>
        [Event(2, Level = EventLevel.Informational)]
        public void WriteInfo(string message)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                System.Diagnostics.Debug.WriteLine(message);
            }

            this.WriteEvent(2, message);
        }

        /// <summary>
        /// Writes a warning message to the event log.
        /// </summary>
        /// <param name="message">
        /// The message to write out.
        /// </param>
        [Event(3, Level = EventLevel.Warning)]
        public void WriteWarning(string message)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                System.Diagnostics.Debug.WriteLine(message);
            }

            this.WriteEvent(3, message);
        }

        /// <summary>
        /// Writes an error message to the event log.
        /// </summary>
        /// <param name="message">
        /// The message to write out.
        /// </param>
        [Event(4, Level = EventLevel.Error)]
        public void WriteError(string message)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                System.Diagnostics.Debug.WriteLine(message);
            }

            this.WriteEvent(4, message);
        }

        /// <summary>
        /// Writes a critical error message to the event log.
        /// </summary>
        /// <param name="message">
        /// The message to write out.
        /// </param>
        [Event(5, Level = EventLevel.Critical)]
        public void WriteCritical(string message)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                System.Diagnostics.Debug.WriteLine(message);
            }

            this.WriteEvent(5, message);
        }
    }
}
#endif