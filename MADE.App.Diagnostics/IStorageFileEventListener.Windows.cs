#if WINDOWS_UWP
namespace MADE.App.Diagnostics
{
    using System.Diagnostics.Tracing;

    using Windows.Storage;

    public interface IStorageFileEventListener
    {
        StorageFile LogFile { get; }
    }
}
#endif