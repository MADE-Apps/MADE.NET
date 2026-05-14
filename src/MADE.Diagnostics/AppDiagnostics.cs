// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Threading.Tasks;
using MADE.Diagnostics.Exceptions;
using MADE.Diagnostics.Logging;

namespace MADE.Diagnostics;

/// <summary>
/// Defines a service for managing application wide event logging for exceptions.
/// </summary>
public class AppDiagnostics : IAppDiagnostics
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AppDiagnostics"/> class.
    /// </summary>
    /// <param name="eventLogger">
    /// The instance of the service for logging application event messages.
    /// </param>
    public AppDiagnostics(IEventLogger eventLogger)
    {
        this.EventLogger = eventLogger;
    }

    /// <summary>
    /// Occurs when an exception is observed.
    /// </summary>
    public event ExceptionObservedEventHandler ExceptionObserved;

    /// <summary>
    /// Gets the service for logging application event messages.
    /// </summary>
    public IEventLogger EventLogger { get; }

    /// <summary>
    /// Gets a value indicating whether application diagnostic messages are being recorded.
    /// </summary>
    public bool IsRecordingDiagnostics { get; private set; }

    /// <summary>
    /// Starts tracking and recording the application diagnostic messages.
    /// </summary>
    /// <returns>
    /// An asynchronous operation.
    /// </returns>
    public async Task StartRecordingDiagnosticsAsync()
    {
        if (this.IsRecordingDiagnostics)
        {
            return;
        }

        this.IsRecordingDiagnostics = true;

        await this.EventLogger.WriteInfo("Application diagnostics initialized.").ConfigureAwait(false);

        AppDomain.CurrentDomain.UnhandledException += this.OnAppUnhandledException;
        TaskScheduler.UnobservedTaskException += this.OnTaskUnobservedException;
    }

    /// <summary>
    /// Stops tracking and recording the application diagnostic messages.
    /// </summary>
    public void StopRecordingDiagnostics()
    {
        if (!this.IsRecordingDiagnostics)
        {
            return;
        }

        AppDomain.CurrentDomain.UnhandledException -= this.OnAppUnhandledException;
        TaskScheduler.UnobservedTaskException -= this.OnTaskUnobservedException;

        this.IsRecordingDiagnostics = false;
    }

    private async void OnTaskUnobservedException(object sender, UnobservedTaskExceptionEventArgs args)
    {
        args.SetObserved();

        var correlationId = Guid.NewGuid();

        await this.EventLogger.WriteCritical(
            args.Exception != null
                ? $"An unobserved task exception was thrown. Correlation ID: {correlationId}. Error: {args.Exception}."
                : $"An unobserved task exception was thrown. Correlation ID: {correlationId}. Error: No exception information was available.").ConfigureAwait(false);

        this.ExceptionObserved?.Invoke(this, new ExceptionObservedEventArgs(correlationId, args.Exception));
    }

    private async void OnAppUnhandledException(object sender, UnhandledExceptionEventArgs args)
    {
        if (args.IsTerminating)
        {
            await this.EventLogger.WriteCritical(
                "The application is terminating due to an unhandled exception being thrown.").ConfigureAwait(false);
        }

        if (args.ExceptionObject is not Exception ex)
        {
            return;
        }

        var correlationId = Guid.NewGuid();

        await this.EventLogger.WriteCritical($"An unhandled exception was thrown. Correlation ID: {correlationId}. Error: {ex}").ConfigureAwait(false);

        this.ExceptionObserved?.Invoke(this, new ExceptionObservedEventArgs(correlationId, ex));
    }
}
