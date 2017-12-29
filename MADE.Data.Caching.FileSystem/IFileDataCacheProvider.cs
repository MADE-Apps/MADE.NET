// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IFileDataCacheProvider.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines an interface for a data caching provider which writes to a file stored on the local disk.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MADE.Data.Caching.FileSystem
{
    /// <summary>
    /// Defines an interface for a data caching provider which writes to a file stored on the local disk.
    /// </summary>
    public interface IFileDataCacheProvider : IDataCacheProvider
    {
	    /// <summary>
	    /// Gets or sets the name of the folder storing application data.
	    /// </summary>
	    string ApplicationFolderName { get; set; }

        /// <summary>
        /// Gets or sets the name of the folder storing the cached data.
        /// </summary>
        string CacheFolderName { get; set; }
    }
}