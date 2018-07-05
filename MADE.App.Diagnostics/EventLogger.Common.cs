#if !WINDOWS_UWP
namespace MADE.App.Diagnostics
{
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    public class EventLogger : IEventLogger
    {
        private const string LogFormat = "{0:G}\tLevel: {1}\tId: {2}\tMessage: '{3}'";

        private readonly SemaphoreSlim fileSemaphore = new SemaphoreSlim(1, 1);

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
        public async void WriteDebug(string message)
        {
            if (!System.Diagnostics.Debugger.IsAttached)
            {
                return;
            }

            string log = string.Format(LogFormat, DateTime.Now, "Debug", Guid.NewGuid(), message);
            await this.WriteToFileAsync(log);
        }

        /// <summary>
        /// Writes a generic information message to the event log.
        /// </summary>
        /// <param name="message">
        /// The message to write out.
        /// </param>
        public async void WriteInfo(string message)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                System.Diagnostics.Debug.WriteLine(message);
            }

            string log = string.Format(LogFormat, DateTime.Now, "Info", Guid.NewGuid(), message);
            await this.WriteToFileAsync(log);
        }

        /// <summary>
        /// Writes a warning message to the event log.
        /// </summary>
        /// <param name="message">
        /// The message to write out.
        /// </param>
        public async void WriteWarning(string message)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                System.Diagnostics.Debug.WriteLine(message);
            }

            string log = string.Format(LogFormat, DateTime.Now, "Warning", Guid.NewGuid(), message);
            await this.WriteToFileAsync(log);
        }

        /// <summary>
        /// Writes an error message to the event log.
        /// </summary>
        /// <param name="message">
        /// The message to write out.
        /// </param>
        public async void WriteError(string message)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                System.Diagnostics.Debug.WriteLine(message);
            }

            string log = string.Format(LogFormat, DateTime.Now, "Error", Guid.NewGuid(), message);
            await this.WriteToFileAsync(log);
        }

        /// <summary>
        /// Writes a critical error message to the event log.
        /// </summary>
        /// <param name="message">
        /// The message to write out.
        /// </param>
        public async void WriteCritical(string message)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                System.Diagnostics.Debug.WriteLine(message);
            }

            string log = string.Format(LogFormat, DateTime.Now, "Critical", Guid.NewGuid(), message);
            await this.WriteToFileAsync(log);
        }

        private async Task WriteToFileAsync(string line)
        {
            await this.fileSemaphore.WaitAsync();

            if (string.IsNullOrWhiteSpace(this.LogPath))
            {
                this.SetupLogFile();
            }

            if (!string.IsNullOrWhiteSpace(this.LogPath))
            {
                try
                {
                    using (StreamWriter sw = File.AppendText(this.LogPath))
                    {
                        sw.WriteLine(line);
                    }
                }
                catch (Exception ex)
                {
                    if (System.Diagnostics.Debugger.IsAttached)
                    {
                        System.Diagnostics.Debug.WriteLine(
                            $"An exception was thrown while writing to the event log file. Error: {ex}");
                    }
                }
                finally
                {
                    this.fileSemaphore.Release();
                }
            }
        }

        private void SetupLogFile()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(this.LogPath))
                {
                    string logFileName = string.Format(AppDiagnostics.LogFileNameFormat, DateTime.Now);

                    string logFileFolderPath = string.Empty;

#if __ANDROID__
                    string appFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    string logsFolderPath = Path.Combine(appFolderPath, AppDiagnostics.LogsFolderName);
#elif __IOS__
                    string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    string appFolderPath = Path.Combine(documentsPath, "..", "Library");
                    string logsFolderPath = Path.Combine(appFolderPath, AppDiagnostics.LogsFolderName);
#else
                    string appFolderPath = AppDomain.CurrentDomain.BaseDirectory;
                    string logsFolderPath = Path.Combine(appFolderPath, AppDiagnostics.LogsFolderName);
#endif

                    if (!string.IsNullOrWhiteSpace(logsFolderPath))
                    {
                        if (!Directory.Exists(logsFolderPath))
                        {
                            Directory.CreateDirectory(logsFolderPath);
                        }

                        logFileFolderPath = Path.Combine(logsFolderPath, logFileName);
                        if (!File.Exists(logFileFolderPath))
                        {
                            File.Create(logFileFolderPath);
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(logFileFolderPath) && File.Exists(logFileFolderPath))
                    {
                        this.LogPath = logFileFolderPath;
                    }
                }
            }
            catch (Exception ex)
            {
                if (System.Diagnostics.Debugger.IsAttached)
                {
                    System.Diagnostics.Debug.WriteLine(
                        $"An exception was thrown while setting up the event log file. Error: {ex}");

                    throw;
                }
            }
        }
    }
}
#endif