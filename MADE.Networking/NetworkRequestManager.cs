// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NetworkRequestManager.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines a manager for HTTP network requests which supports request caching.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MADE.Networking
{
	using MADE.Common.Dependency;
	using MADE.Data.Caching;
	using MADE.Data.Caching.FileSystem;

	/// <summary>
	/// Defines a manager for HTTP network requests which supports request caching.
	/// </summary>
	public class NetworkRequestManager : INetworkRequestManager, IDataCacheSupported, IFileDataCacheInfo, IDataCacheInfo
	{
		private const string NetworkCacheKey = "NetworkCache";

		/// <summary>
		/// Initializes a new instance of the <see cref="NetworkRequestManager"/> class.
		/// </summary>
		public NetworkRequestManager()
		{
			this.CacheProvider = this.GetNetworkDataCacheProvider();
			this.CacheProvider.Weed(this.DaysToWeedCache); // Cleans up older cached data.
		}

		/// <summary>
		/// Gets or sets the name of the folder storing application data.
		/// </summary>
		public string ApplicationFolderName { get; set; } = FileDataCacheProvider.DefaultApplicationFolderName;

		/// <summary>
		/// Gets or sets the name of the folder storing the cached data.
		/// </summary>
		public string CacheFolderName { get; set; } = NetworkCacheKey;

		/// <summary>
		/// Gets or sets the number of days from the current day (UTC) to weed cached data from. Default is 0 (never).
		/// </summary>
		public int DaysToWeedCache { get; set; } = 0;

		/// <summary>
		/// Gets the provider for the data caching.
		/// </summary>
		public IDataCacheProvider CacheProvider { get; }

		/// <summary>
		/// Gets or sets a value indicating whether caching is currently enabled.
		/// </summary>
		public bool IsCachingEnabled { get; set; } = false;

		private IDataCacheProvider GetNetworkDataCacheProvider()
		{
			if (!SimpleDependencyService.Instance.IsRegistered<IDataCacheProvider>(NetworkCacheKey))
			{
				SimpleDependencyService.Instance.Register<IDataCacheProvider>(
					NetworkCacheKey,
					() => new FileDataCacheProvider
						      {
							      ApplicationFolderName = this.ApplicationFolderName,
							      CacheFolderName = this.CacheFolderName
						      });
			}

			return SimpleDependencyService.Instance.GetInstance<IDataCacheProvider>(NetworkCacheKey);
		}
	}
}