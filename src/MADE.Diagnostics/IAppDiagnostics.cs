// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Diagnostics
{
    using System.Threading.Tasks;

    using MADE.Diagnostics.Exceptions;
    using MADE.Diagnostics.Logging;

    /// <summary>
    /// Defines an interface for handling application diagnostics.
    /// </summary>
    public interface IAppDiagnostics
    {
        /// <summary>
        /// Occurs when an exception is observed.
        /// </summary>
        event ExceptionObservedEventHandler ExceptionObserved;

        /// <summary>
        /// Gets the service for logging application event messages.
        /// </summary>
        IEventLogger EventLogger { get; }

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