// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EventLogger.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines a service for logging informational messages to a log file.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MADE.App.Diagnostics.Logging
{
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines a service for logging informational messages to a log file.
    /// </summary>
    public class EventLogger : IEventLogger
    {
        private const string LogFormat = "{0:G}\tLevel: {1}\tId: {2}\tMessage: '{3}'";

        private readonly SemaphoreSlim fileSemaphore = new SemaphoreSlim(1, 1);

        /// <summary>
        /// Gets or sets the path to where the log exists.
        /// </summary>
        public string LogPath { get; set; }

        /// <summary>
        /// Gets or sets the name of the folder where logs are stored locally in the application.
        /// </summary>
        public string LogsFolderName { get; set; } = "Logs";

        /// <summary>
        /// Gets or sets the format for the name of the file where a log is stored locally in the application.
        /// </summary>
        public string LogFileNameFormat { get; set; } = "Log-{0:yyyyMMdd}.txt";

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
            string log = string.Format(LogFormat, DateTime.Now, "Critical", Guid.NewGuid(), message);
            await this.WriteToFileAsync(log);
        }

        /// <summary>
        /// Writes an exception to the event log as a debug message.
        /// </summary>
        /// <param name="message">
        /// The message to write out.
        /// </param>
        /// <param name="ex">
        /// The exception to write out.
        /// </param>
        public void WriteDebug(string message, Exception ex)
        {
            this.WriteDebug($"{message} - Error: '{ex}'");
        }

        /// <summary>
        /// Writes an exception to the event log as a debug message.
        /// </summary>
        /// <param name="ex">
        /// The exception to write out.
        /// </param>
        public void WriteDebug(Exception ex)
        {
            this.WriteDebug($"Error: '{ex}'");
        }

        /// <summary>
        /// Writes an exception to the event log as a generic information message.
        /// </summary>
        /// <param name="message">
        /// The message to write out.
        /// </param>
        /// <param name="ex">
        /// The exception to write out.
        /// </param>
        public void WriteInfo(string message, Exception ex)
        {
            this.WriteInfo($"{message} - Error: '{ex}'");
        }

        /// <summary>
        /// Writes an exception to the event log as a generic information message.
        /// </summary>
        /// <param name="ex">
        /// The exception to write out.
        /// </param>
        public void WriteInfo(Exception ex)
        {
            this.WriteInfo($"Error: '{ex}'");
        }

        /// <summary>
        /// Writes an exception to the event log as a warning message.
        /// </summary>
        /// <param name="message">
        /// The message to write out.
        /// </param>
        /// <param name="ex">
        /// The exception to write out.
        /// </param>
        public void WriteWarning(string message, Exception ex)
        {
            this.WriteWarning($"{message} - Error: '{ex}'");
        }

        /// <summary>
        /// Writes an exception to the event log as a warning message.
        /// </summary>
        /// <param name="ex">
        /// The exception to write out.
        /// </param>
        public void WriteWarning(Exception ex)
        {
            this.WriteWarning($"Error: '{ex}'");
        }

        /// <summary>
        /// Writes an exception to the event log as an error message.
        /// </summary>
        /// <param name="message">
        /// The message to write out.
        /// </param>
        /// <param name="ex">
        /// The exception to write out.
        /// </param>
        public void WriteError(string message, Exception ex)
        {
            this.WriteError($"{message} - Error: '{ex}'");
        }

        /// <summary>
        /// Writes an exception to the event log as an error message.
        /// </summary>
        /// <param name="ex">
        /// The exception to write out.
        /// </param>
        public void WriteError(Exception ex)
        {
            this.WriteError($"Error: '{ex}'");
        }

        /// <summary>
        /// Writes an exception to the event log as a critical message.
        /// </summary>
        /// <param name="message">
        /// The message to write out.
        /// </param>
        /// <param name="ex">
        /// The exception to write out.
        /// </param>
        public void WriteCritical(string message, Exception ex)
        {
            this.WriteCritical($"{message} - Error: '{ex}'");
        }

        /// <summary>
        /// Writes an exception to the event log as a critical message.
        /// </summary>
        /// <param name="ex">
        /// The exception to write out.
        /// </param>
        public void WriteCritical(Exception ex)
        {
            this.WriteCritical($"Error: '{ex}'");
        }

        private async Task WriteToFileAsync(string line)
        {
            await this.fileSemaphore.WaitAsync();

            if (System.Diagnostics.Debugger.IsAttached)
            {
                System.Diagnostics.Debug.WriteLine(line);
            }

            if (string.IsNullOrWhiteSpace(this.LogPath))
            {
                await this.SetupLogFileAsync();
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

        private async Task SetupLogFileAsync()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(this.LogPath))
                {
                    string logFileName = string.Format(this.LogFileNameFormat, DateTime.Now);

                    string logFileFolderPath = string.Empty;

#if WINDOWS_UWP || __ANDROID__ || __IOS__
                    XPlat.Storage.IStorageFolder logsFolder =
                        await XPlat.Storage.ApplicationData.Current.LocalFolder.CreateFolderAsync(
                            this.LogsFolderName,
                            XPlat.Storage.CreationCollisionOption.OpenIfExists);

                    XPlat.Storage.IStorageFile logFile = await logsFolder.CreateFileAsync(
                                                             logFileName,
                                                             XPlat.Storage.CreationCollisionOption.OpenIfExists);

                    logFileFolderPath = logFile.Path;
#elif NETSTANDARD2_0
                    string appFolderPath = AppDomain.CurrentDomain.BaseDirectory;
                    string logsFolderPath = Path.Combine(appFolderPath, this.LogsFolderName);
#else
                    string appFolderPath = AppContext.BaseDirectory;
                    string logsFolderPath = Path.Combine(appFolderPath, this.LogsFolderName);
#endif

#if !(WINDOWS_UWP || __ANDROID__ || __IOS__)
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
#endif

                    this.LogPath = logFileFolderPath;
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

#if !(WINDOWS_UWP || __ANDROID__ || __IOS__)
            await Task.CompletedTask;
#endif
        }
    }
}