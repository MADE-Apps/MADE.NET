namespace MADE.App.Diagnostics
{
    using System.Diagnostics.Tracing;
    using System.Threading.Tasks;

    using MADE.App.Dependency;

    public class AppDiagnostics : IAppDiagnostics
    {
        /// <summary>
        /// Defines the name of the folder where logs are stored locally in the application.
        /// </summary>
        public const string LogsFolderName = "Logs";

        /// <summary>
        /// Defines the format for the name of the file where a log is stored locally in the application.
        /// </summary>
        public const string LogFileNameFormat = "Log-{0:yyyyMMdd}.txt";

#if WINDOWS_UWP
        private StorageFileEventListener listener;
#endif

        /// <summary>
        /// Initializes a new instance of the <see cref="AppDiagnostics"/> class.
        /// </summary>
        public AppDiagnostics()
        {
            if (!SimpleDependencyService.Instance.IsRegistered<IEventLogger>())
            {
                SimpleDependencyService.Instance.Register<IEventLogger, EventLogger>();
            }

            this.EventLogger = SimpleDependencyService.Instance.GetInstance<IEventLogger>();
        }

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
        /// Gets the service for logging application event messages.
        /// </summary>
        public IEventLogger EventLogger { get; }

        /// <summary>
        /// Gets the string path to the file used for capturing application diagnostic messages.
        /// </summary>
        public string DiagnosticsFilePath => this.EventLogger?.LogPath;

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

#if WINDOWS_UWP
            this.listener = new StorageFileEventListener();
            this.listener.EnableEvents(this.EventLogger as EventSource, EventLevel.Verbose);
#endif

            this.EventLogger.WriteInfo("Application diagnostics initialized.");

#if WINDOWS_UWP
            Windows.UI.Xaml.Application.Current.UnhandledException += this.OnAppUnhandledException;
#else
            System.AppDomain.CurrentDomain.UnhandledException += this.OnAppUnhandledException;
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
#else
            System.AppDomain.CurrentDomain.UnhandledException -= this.OnAppUnhandledException;
#endif
            TaskScheduler.UnobservedTaskException -= this.OnTaskUnobservedException;

            this.IsRecordingDiagnostics = false;
        }

        private void OnTaskUnobservedException(object sender, UnobservedTaskExceptionEventArgs args)
        {
            args.SetObserved();

            this.EventLogger.WriteCritical(
                args.Exception != null
                    ? $"An unobserved task exception was thrown. Error: {args.Exception}"
                    : "An unobserved task exception was thrown. Error: No exception information was available.");
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
#else
        private void OnAppUnhandledException(object sender, System.UnhandledExceptionEventArgs args)
        {
            if (args.IsTerminating)
            {
                this.EventLogger.WriteCritical("The application is terminating due to an unhandled exception being thrown.");
            }

            if (!(args.ExceptionObject is System.Exception ex))
            {
                return;
            }

            this.EventLogger.WriteCritical($"An unhandled exception was thrown. Error: {ex}");
        }
#endif
    }
}