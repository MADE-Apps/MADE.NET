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

    /// <summary>
    /// Defines an interface for handling application diagnostics.
    /// </summary>
    public interface IAppDiagnostics
    {
        /// <summary>
        /// Gets the string path to the file used for capturing application diagnostic messages.
        /// </summary>
        string DiagnosticsFilePath { get; }

        /// <summary>
        /// Gets a value indicating whether application diagnostic messages are being recorded.
        /// </summary>
        bool IsRecordingDiagnostics { get; }

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