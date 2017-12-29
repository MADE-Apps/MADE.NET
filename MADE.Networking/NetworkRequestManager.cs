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
	public class NetworkRequestManager : INetworkRequestManager, IFileDataCacheInfo
	{
		private const string NetworkCacheKey = "NetworkCache";

		private readonly IDataCacheProvider networkCache;

		/// <summary>
		/// Initializes a new instance of the <see cref="NetworkRequestManager"/> class.
		/// </summary>
		public NetworkRequestManager()
		{
			this.networkCache = this.SetupDataCacheProvider();
		}

		/// <summary>
		/// Gets or sets the name of the folder storing application data.
		/// </summary>
		public string ApplicationFolderName { get; set; } = FileDataCacheProvider.DefaultApplicationFolderName;

		/// <summary>
		/// Gets or sets the name of the folder storing the cached data.
		/// </summary>
		public string CacheFolderName { get; set; } = NetworkCacheKey;

		private IDataCacheProvider SetupDataCacheProvider()
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