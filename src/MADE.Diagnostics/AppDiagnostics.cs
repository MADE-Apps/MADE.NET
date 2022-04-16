// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Diagnostics
{
    using System;
    using System.Threading.Tasks;

    using MADE.Diagnostics.Exceptions;
    using MADE.Diagnostics.Logging;

    /// <summary>
    /// Defines a service for managing application wide event logging for exceptions.
    /// </summary>
    public class AppDiagnostics : IAppDiagnostics
    {
#if __ANDROID__
        private UncaughtExceptionHandler javaExceptionHandler;
#endif

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
                await Task.CompletedTask;
            }

            this.IsRecordingDiagnostics = true;

            this.EventLogger.WriteInfo("Application diagnostics initialized.");

#if WINDOWS_UWP
            Windows.UI.Xaml.Application.Current.UnhandledException += this.OnAppUnhandledException;
#elif NETSTANDARD2_0 || __ANDROID__ || __IOS__
            AppDomain.CurrentDomain.UnhandledException += this.OnAppUnhandledException;
#endif

#if __ANDROID__
            Android.Runtime.AndroidEnvironment.UnhandledExceptionRaiser += this.OnAndroidAppUnhandledException;

            this.javaExceptionHandler = new UncaughtExceptionHandler(this.OnJavaUncaughtException);
            Java.Lang.Thread.DefaultUncaughtExceptionHandler = this.javaExceptionHandler;
#endif

            TaskScheduler.UnobservedTaskException += this.OnTaskUnobservedException;

            await Task.CompletedTask;
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

#if WINDOWS_UWP
            Windows.UI.Xaml.Application.Current.UnhandledException -= this.OnAppUnhandledException;
#elif NETSTANDARD2_0 || __ANDROID__ || __IOS__
            AppDomain.CurrentDomain.UnhandledException -= this.OnAppUnhandledException;
#endif

#if __ANDROID__
            Android.Runtime.AndroidEnvironment.UnhandledExceptionRaiser -= this.OnAndroidAppUnhandledException;
#endif

            TaskScheduler.UnobservedTaskException -= this.OnTaskUnobservedException;

            this.IsRecordingDiagnostics = false;
        }

        private void OnTaskUnobservedException(object sender, UnobservedTaskExceptionEventArgs args)
        {
            args.SetObserved();

            var correlationId = Guid.NewGuid();

            this.EventLogger.WriteCritical(
                args.Exception != null
                    ? $"An unobserved task exception was thrown. Correlation ID: {correlationId}. Error: {args.Exception}."
                    : $"An unobserved task exception was thrown. Correlation ID: {correlationId}. Error: No exception information was available.");

            this.ExceptionObserved?.Invoke(this, new ExceptionObservedEventArgs(correlationId, args.Exception));
        }

#if WINDOWS_UWP
        private void OnAppUnhandledException(object sender, Windows.UI.Xaml.UnhandledExceptionEventArgs args)
        {
            args.Handled = true;

            this.EventLogger.WriteCritical(
                args.Exception != null
                    ? $"An unhandled exception was thrown. Error: {args.Exception}"
                    : "An unhandled exception was thrown. Error: No exception information was available.");
        }
#elif NETSTANDARD2_0 || __ANDROID__ || __IOS__
        private void OnAppUnhandledException(object sender, UnhandledExceptionEventArgs args)
        {
            if (args.IsTerminating)
            {
                this.EventLogger.WriteCritical(
                    "The application is terminating due to an unhandled exception being thrown.");
            }

            if (args.ExceptionObject is not Exception ex)
            {
                return;
            }

            var correlationId = Guid.NewGuid();

            this.EventLogger.WriteCritical($"An unhandled exception was thrown. Correlation ID: {correlationId}. Error: {ex}");

            this.ExceptionObserved?.Invoke(this, new ExceptionObservedEventArgs(correlationId, ex));
        }
#endif

#if __ANDROID__
        private void OnAndroidAppUnhandledException(object sender, Android.Runtime.RaiseThrowableEventArgs args)
        {
            args.Handled = true;

            var correlationId = Guid.NewGuid();

            this.EventLogger.WriteCritical($"An unhandled exception was thrown. Correlation ID: {correlationId}. Error: {args.Exception}.");
            this.ExceptionObserved?.Invoke(this, new ExceptionObservedEventArgs(correlationId, args.Exception));
        }

        private void OnJavaUncaughtException(Exception ex)
        {
            var correlationId = Guid.NewGuid();

            this.EventLogger.WriteCritical(
                ex != null
                    ? $"An unhandled exception was thrown. Correlation ID: {correlationId}. Error: {ex}."
                    : $"An unhandled exception was thrown. Correlation ID: {correlationId}. Error: No exception information was available.");

            this.ExceptionObserved?.Invoke(this, new ExceptionObservedEventArgs(correlationId, ex));
        }
#endif
    }
}