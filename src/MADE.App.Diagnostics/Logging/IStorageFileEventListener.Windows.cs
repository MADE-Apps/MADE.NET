// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IStorageFileEventListener.Windows.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines an interface for a Windows event listener for writing event source logs to a storage file.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

#if WINDOWS_UWP
namespace MADE.App.Diagnostics.Logging
{
    using System;

    using Windows.Storage;

    /// <summary>
    /// Defines an interface for a Windows event listener for writing event source logs to a storage file.
    /// </summary>
    [Obsolete("IStorageFileEventListener will be removed. This component is no longer required by MADE.App.Diagnostics.")]
    public interface IStorageFileEventListener
    {
        /// <summary>
        /// Gets the storage file associated with current event log.
        /// </summary>
        StorageFile LogFile { get; }
    }
}
#endif