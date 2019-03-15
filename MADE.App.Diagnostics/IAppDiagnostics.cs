// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAppDiagnostics.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines an interface for handling application diagnostics.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MADE.App.Diagnostics
{
    using System.Threading.Tasks;

    using MADE.App.Diagnostics.Logging;

    /// <summary>
    /// Defines an interface for handling application diagnostics.
    /// </summary>
    public interface IAppDiagnostics
    {
        /// <summary>
        /// Gets the service for logging application event messages.
        /// </summary>
        IEventLogger EventLogger { get; }

        /// <summary>
        /// Gets the string path to the file used for capturing application diagnostic messages.
        /// </summary>
        string DiagnosticsFilePath { get; }

        /// <summary>
        /// Gets a value indicating whether application diagnostic messages are being recorded.
        /// </summary>
        bool IsRecordingDiagnostics { get; }

        /// <summary>
        /// Gets or sets the format for the name of the file where a log is stored locally in the application.
        /// </summary>
        string LogFileNameFormat { get; set; }

        /// <summary>
        /// Gets or sets the name of the folder where logs are stored locally in the application.
        /// </summary>
        string LogsFolderName { get; set; }

        /// <summary>
        /// Starts tracking and recording the application diagnostic messages.
        /// </summary>
        /// <returns>
        /// An asynchronous operation.
        /// </returns>
        Task StartRecordingDiagnosticsAsync();

        /// <summary>
        /// Stops tracking and recording the application diagnostic messages.
        /// </summary>
        void StopRecordingDiagnostics();
    }
}