// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Diagnostics.Logging;

/// <summary>
/// Defines a service for logging events to a log file.
/// </summary>
public class FileEventLogger : IEventLogger, IDisposable
{
    private readonly SemaphoreSlim fileSemaphore = new(1, 1);

    /// <summary>
    /// Gets or sets the full file path to where the current log exists.
    /// </summary>
    public string LogPath { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the name of the folder where log files are stored.
    /// </summary>
    public string LogsFolderName { get; set; } = "Logs";

    /// <summary>
    /// Gets or sets the format for the name of the log file.
    /// </summary>
    public string LogFileNameFormat { get; set; } = "Log-{0:yyyyMMdd}.txt";

    /// <summary>
    /// Writes a debug information message to the event log when in DEBUG mode.
    /// </summary>
    /// <param name="message">
    /// The message to write out.
    /// </param>
    public Task WriteDebug(string message)
    {
        if (!System.Diagnostics.Debugger.IsAttached)
        {
            return Task.CompletedTask;
        }

        var log = $"{DateTime.Now:G}\tLevel: Debug\tId: {Guid.NewGuid()}\tMessage: '{message}'";
        return this.WriteToFileAsync(log);
    }

    /// <summary>
    /// Writes a generic information message to the event log.
    /// </summary>
    /// <param name="message">
    /// The message to write out.
    /// </param>
    public Task WriteInfo(string message)
    {
        var log = $"{DateTime.Now:G}\tLevel: Info\tId: {Guid.NewGuid()}\tMessage: '{message}'";
        return this.WriteToFileAsync(log);
    }

    /// <summary>
    /// Writes a warning message to the event log.
    /// </summary>
    /// <param name="message">
    /// The message to write out.
    /// </param>
    public Task WriteWarning(string message)
    {
        var log = $"{DateTime.Now:G}\tLevel: Warning\tId: {Guid.NewGuid()}\tMessage: '{message}'";
        return this.WriteToFileAsync(log);
    }

    /// <summary>
    /// Writes an error message to the event log.
    /// </summary>
    /// <param name="message">
    /// The message to write out.
    /// </param>
    public Task WriteError(string message)
    {
        var log = $"{DateTime.Now:G}\tLevel: Error\tId: {Guid.NewGuid()}\tMessage: '{message}'";
        return this.WriteToFileAsync(log);
    }

    /// <summary>
    /// Writes a critical error message to the event log.
    /// </summary>
    /// <param name="message">
    /// The message to write out.
    /// </param>
    public Task WriteCritical(string message)
    {
        var log = $"{DateTime.Now:G}\tLevel: Critical\tId: {Guid.NewGuid()}\tMessage: '{message}'";
        return this.WriteToFileAsync(log);
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
    public Task WriteDebug(string message, Exception ex)
    {
        return this.WriteDebug($"{message} - Error: '{ex}'");
    }

    /// <summary>
    /// Writes an exception to the event log as a debug message.
    /// </summary>
    /// <param name="ex">
    /// The exception to write out.
    /// </param>
    public Task WriteDebug(Exception ex)
    {
        return this.WriteDebug($"Error: '{ex}'");
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
    public Task WriteInfo(string message, Exception ex)
    {
        return this.WriteInfo($"{message} - Error: '{ex}'");
    }

    /// <summary>
    /// Writes an exception to the event log as a generic information message.
    /// </summary>
    /// <param name="ex">
    /// The exception to write out.
    /// </param>
    public Task WriteInfo(Exception ex)
    {
        return this.WriteInfo($"Error: '{ex}'");
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
    public Task WriteWarning(string message, Exception ex)
    {
        return this.WriteWarning($"{message} - Error: '{ex}'");
    }

    /// <summary>
    /// Writes an exception to the event log as a warning message.
    /// </summary>
    /// <param name="ex">
    /// The exception to write out.
    /// </param>
    public Task WriteWarning(Exception ex)
    {
        return this.WriteWarning($"Error: '{ex}'");
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
    public Task WriteError(string message, Exception ex)
    {
        return this.WriteError($"{message} - Error: '{ex}'");
    }

    /// <summary>
    /// Writes an exception to the event log as an error message.
    /// </summary>
    /// <param name="ex">
    /// The exception to write out.
    /// </param>
    public Task WriteError(Exception ex)
    {
        return this.WriteError($"Error: '{ex}'");
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
    public Task WriteCritical(string message, Exception ex)
    {
        return this.WriteCritical($"{message} - Error: '{ex}'");
    }

    /// <summary>
    /// Writes an exception to the event log as a critical message.
    /// </summary>
    /// <param name="ex">
    /// The exception to write out.
    /// </param>
    public Task WriteCritical(Exception ex)
    {
        return this.WriteCritical($"Error: '{ex}'");
    }

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Releases unmanaged and optionally managed resources.
    /// </summary>
    /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            this.fileSemaphore.Dispose();
        }
    }

    private async Task WriteToFileAsync(string line)
    {
        await this.fileSemaphore.WaitAsync().ConfigureAwait(false);

        if (System.Diagnostics.Debugger.IsAttached)
        {
            System.Diagnostics.Debug.WriteLine(line);
        }

        if (string.IsNullOrWhiteSpace(this.LogPath))
        {
            await this.SetupLogFileAsync().ConfigureAwait(false);
        }

        if (!string.IsNullOrWhiteSpace(this.LogPath))
        {
            try
            {
                using StreamWriter sw = File.AppendText(this.LogPath);
                await sw.WriteLineAsync(line).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                if (System.Diagnostics.Debugger.IsAttached)
                {
                    System.Diagnostics.Debug.WriteLine(
                        $"An exception was thrown while writing to the log file. Error: {ex}");
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
        if (string.IsNullOrWhiteSpace(this.LogPath))
        {
            string logFileName = string.Format(this.LogFileNameFormat, DateTime.Now);

            string logFileFolderPath = string.Empty;

#if WINDOWS_UWP || __ANDROID__ || __IOS__
            XPlat.Storage.IStorageFolder logsFolder =
                await XPlat.Storage.ApplicationData.Current.LocalFolder.CreateFolderAsync(
                    this.LogsFolderName,
                    XPlat.Storage.CreationCollisionOption.OpenIfExists).ConfigureAwait(false);

            XPlat.Storage.IStorageFile logFile = await logsFolder.CreateFileAsync(
                                                     logFileName,
                                                     XPlat.Storage.CreationCollisionOption.OpenIfExists).ConfigureAwait(false);

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

#if !(WINDOWS_UWP || __ANDROID__ || __IOS__)
        await Task.CompletedTask.ConfigureAwait(false);
#endif
    }
}
