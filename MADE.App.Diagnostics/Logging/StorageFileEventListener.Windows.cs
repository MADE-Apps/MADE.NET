// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StorageFileEventListener.Windows.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines a Windows event listener for writing event source logs to a storage file.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

#if WINDOWS_UWP
namespace MADE.App.Diagnostics.Logging
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Tracing;
    using System.Threading;
    using System.Threading.Tasks;

    using Windows.Storage;

    /// <summary>
    /// Defines a Windows event listener for writing event source logs to a storage file.
    /// </summary>
    [Obsolete("StorageFileEventListener will be removed. This component is no longer required by MADE.App.Diagnostics.")]
    public class StorageFileEventListener : EventListener, IStorageFileEventListener
    {
        private const string Format = "{0:G}\t '{1}'";

        private readonly SemaphoreSlim fileWriteSemaphore = new SemaphoreSlim(1);

        /// <summary>
        /// Initializes a new instance of the <see cref="StorageFileEventListener"/> class.
        /// </summary>
        public StorageFileEventListener()
        {
            this.CreateLogFileAsync();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StorageFileEventListener"/> class.
        /// </summary>
        /// <param name="logFile">
        /// The log file.
        /// </param>
        public StorageFileEventListener(StorageFile logFile)
        {
            this.LogFile = logFile;
        }

        /// <summary>
        /// Gets the storage file associated with current event log.
        /// </summary>
        public StorageFile LogFile { get; private set; }

        /// <summary>
        /// Called when an event has been written to the event source.
        /// </summary>
        /// <param name="eventData">
        /// The data associated with the event source log.
        /// </param>
        protected override async void OnEventWritten(EventWrittenEventArgs eventData)
        {
            if (this.LogFile == null)
            {
                return;
            }

            await this.WriteAsync(new[] { string.Format(Format, DateTime.Now, eventData.Payload[0]) });
        }

        private async Task CreateLogFileAsync()
        {
            StorageFolder logsFolder = await ApplicationData.Current.LocalFolder.CreateFolderAsync(
                                           AppDiagnostics.LogsFolderName,
                                           CreationCollisionOption.OpenIfExists);

            if (logsFolder != null)
            {
                this.LogFile = await logsFolder.CreateFileAsync(
                                   string.Format(AppDiagnostics.LogFileNameFormat, DateTime.Now),
                                   CreationCollisionOption.OpenIfExists);
            }
        }

        private async Task WriteAsync(IEnumerable<string> logs)
        {
            await this.fileWriteSemaphore.WaitAsync();

            await Task.Run(
                async () =>
                    {
                        try
                        {
                            await FileIO.AppendLinesAsync(this.LogFile, logs);
                        }
                        catch (Exception)
                        {
                            // Ignored, cannot write out the log if this isn't working.
                        }
                        finally
                        {
                            this.fileWriteSemaphore.Release();
                        }
                    });
        }
    }
}
#endif