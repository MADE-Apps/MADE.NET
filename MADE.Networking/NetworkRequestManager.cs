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
	using MADE.Data.Caching.FileSystem;

	/// <summary>
	/// Defines a manager for HTTP network requests which supports request caching.
	/// </summary>
	public class NetworkRequestManager : INetworkRequestManager
	{
		private readonly IFileDataCacheProvider requestCache;

		/// <summary>
		/// Initializes a new instance of the <see cref="NetworkRequestManager"/> class.
		/// </summary>
		public NetworkRequestManager()
		{
			this.requestCache = GetRequestCache();
			this.requestCache.CacheFolderName = "NetworkManager";
		}

		private static IFileDataCacheProvider GetRequestCache()
		{
			if (!SimpleDependencyService.Instance.IsRegistered<IFileDataCacheProvider>())
			{
				SimpleDependencyService.Instance.Register<IFileDataCacheProvider, FileDataCacheProvider>();
			}

			return SimpleDependencyService.Instance.GetInstance<IFileDataCacheProvider>();
		}
	}
}