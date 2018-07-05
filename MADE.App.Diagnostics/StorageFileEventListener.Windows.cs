#if WINDOWS_UWP
namespace MADE.App.Diagnostics
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Tracing;
    using System.Threading;
    using System.Threading.Tasks;

    using Windows.Storage;

    public class StorageFileEventListener : EventListener, IStorageFileEventListener
    {
        private const string Format = "{0:G}\t '{1}'";

        private readonly SemaphoreSlim fileWriteSemaphore = new SemaphoreSlim(1);

        public StorageFileEventListener()
        {
            this.CreateLogFileAsync();
        }

        public StorageFileEventListener(StorageFile logFile)
        {
            this.LogFile = logFile;
        }

        public StorageFile LogFile { get; private set; }

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