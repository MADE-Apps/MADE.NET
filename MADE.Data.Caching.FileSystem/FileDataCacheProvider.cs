// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FileDataCacheProvider.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines a data caching provider which writes to a file stored on the local disk.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MADE.Data.Caching.FileSystem
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using System.Threading;

	using MADE.Common;

	using Newtonsoft.Json;

	/// <summary>
	/// Defines a data caching provider which writes to a file stored on the local disk.
	/// </summary>
	public class FileDataCacheProvider : IDataCacheProvider, IFileDataCacheInfo
	{
		public const string DefaultApplicationFolderName = "MADE";

		public const string DefaultCacheFolderName = "Cache";

		private readonly ReaderWriterLockSlim indexSemaphore;

		private readonly Dictionary<string, DateTime> cacheIndex;

		private readonly JsonSerializer jsonSerializer;

		private readonly JsonSerializerSettings jsonSerializerSettings;

		private string cacheIndexFilePath;

		/// <summary>
		/// Initializes a new instance of the <see cref="FileDataCacheProvider"/> class.
		/// </summary>
		public FileDataCacheProvider()
		{
			this.indexSemaphore = new ReaderWriterLockSlim();

			this.jsonSerializerSettings = new JsonSerializerSettings
				                              {
					                              ObjectCreationHandling = ObjectCreationHandling.Replace,
					                              ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
					                              TypeNameHandling = TypeNameHandling.All
				                              };

			this.jsonSerializer = JsonSerializer.Create(this.jsonSerializerSettings);

			this.cacheIndex = new Dictionary<string, DateTime>();

			this.LoadIndex();
		}

		/// <summary>
		/// Gets or sets the name of the folder storing application data.
		/// </summary>
		public string ApplicationFolderName { get; set; } = DefaultApplicationFolderName;

		/// <summary>
		/// Gets or sets the name of the folder storing the cached data.
		/// </summary>
		public string CacheFolderName { get; set; } = DefaultCacheFolderName;

		/// <summary>
		/// Adds or updates content in the data cache by the given key.
		/// </summary>
		/// <typeparam name="T">
		/// The type of content to store.
		/// </typeparam>
		/// <param name="key">
		/// The key for the content.
		/// </param>
		/// <param name="content">
		/// The content to store.
		/// </param>
		public void AddOrUpdate<T>(string key, T content)
		{
			if (string.IsNullOrWhiteSpace(key))
			{
				return;
			}

			if (content == null)
			{
				return;
			}

			bool exists = this.Contains(key);
			if (exists)
			{
				this.Remove(key);
			}

			this.indexSemaphore.EnterWriteLock();

			string filePath = this.GetCacheFilePath(key);

			string jsonContent = JsonConvert.SerializeObject(content, this.jsonSerializerSettings);

			CachedData data = new CachedData(key, jsonContent);

			using (StreamWriter sw = File.CreateText(filePath))
			{
				this.jsonSerializer.Serialize(sw, data);
			}

			this.cacheIndex.Add(key, data.CachedDate);

			this.SaveIndex();

			this.indexSemaphore.ExitWriteLock();
		}

		/// <summary>
		/// Gets the content from the cache by the given key as the given type.
		/// </summary>
		/// <param name="key">
		/// The key for the content.
		/// </param>
		/// <typeparam name="T">
		/// The type of object to return the content as.
		/// </typeparam>
		/// <returns>
		/// Returns the content for the given key, if it exists, as the given type.
		/// </returns>
		public T Get<T>(string key)
		{
			T result = default(T);

			bool exists = this.Contains(key);
			if (!exists)
			{
				return result;
			}

			this.indexSemaphore.EnterReadLock();

			string filePath = this.GetCacheFilePath(key);

			using (StreamReader sr = File.OpenText(filePath))
			{
				CachedData cachedData = (CachedData)this.jsonSerializer.Deserialize(sr, typeof(CachedData));
				if (cachedData != null)
				{
					result = cachedData.DeserializeJsonContentAs<T>();
				}
			}

			this.indexSemaphore.ExitReadLock();

			return result;
		}

		/// <summary>
		/// Determines whether the given key has existing content within the cache.
		/// </summary>
		/// <param name="key">
		/// The key to check exists.
		/// </param>
		/// <returns>
		/// Returns true if content exists for the given key.
		/// </returns>
		public bool Contains(string key)
		{
			this.indexSemaphore.EnterReadLock();

			bool exists = this.cacheIndex.ContainsKey(key);

			this.indexSemaphore.ExitReadLock();

			return exists;
		}

		/// <summary>
		/// Removes the content for the given key from the cache.
		/// </summary>
		/// <param name="key">
		/// The key to remove.
		/// </param>
		public void Remove(string key)
		{
			bool exists = this.Contains(key);
			if (!exists)
			{
				return;
			}

			this.indexSemaphore.EnterWriteLock();

			string filePath = this.GetCacheFilePath(key);

			File.Delete(filePath);
			this.cacheIndex.Remove(key);

			this.SaveIndex();

			this.indexSemaphore.ExitWriteLock();
		}

		/// <summary>
		/// Weeds/removes content from the cache that were cached before the given weed from date.
		/// </summary>
		/// <param name="weedFromDate">
		/// The date from when cached items should be removed. E.g. 24/12/2017 would remove any items cached before the 24/12/2017.
		/// </param>
		public void Weed(DateTime weedFromDate)
		{
			this.Weed(weedFromDate, false);
		}

		/// <summary>
		/// Weeds/removes content from the cache that were cached before the given weed from date/time.
		/// </summary>
		/// <param name="weedFromDateTime">
		/// The date/time from when cached items should be removed. E.g. 24/12/2017 11:00:00 would remove any items cached before the 24/12/2017 11:00:00.
		/// </param>
		/// <param name="includeTime">
		/// A value indicating whether to take into account the time component of the given weed from date/time.
		/// </param>
		public void Weed(DateTime weedFromDateTime, bool includeTime)
		{
			this.indexSemaphore.EnterWriteLock();

			IEnumerable<KeyValuePair<string, DateTime>> itemsToWeed =
				this.cacheIndex.Where(kvp => kvp.Value.IsLessThan(weedFromDateTime, includeTime));

			List<string> keys = new List<string>();

			foreach (KeyValuePair<string, DateTime> item in itemsToWeed)
			{
				string filePath = this.GetCacheFilePath(item.Key);
				File.Delete(filePath);
				keys.Add(item.Key);
			}

			foreach (string key in keys)
			{
				this.cacheIndex.Remove(key);
			}

			this.SaveIndex();

			this.indexSemaphore.ExitWriteLock();
		}

		private string GetCacheFolderPath()
		{
			string path = string.Empty;

#if __ANDROID__
			path = Android.App.Application.Context.CacheDir.AbsolutePath;
#elif __IOS__
            path =
Foundation.NSSearchPath.GetDirectories(Foundation.NSSearchPathDirectory.CachesDirectory, Foundation.NSSearchPathDomain.User)[0];

#elif WINDOWS_UWP
            path = Windows.Storage.ApplicationData.Current.LocalFolder.Path;
#else
            path =
Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), this.ApplicationFolderName);
#endif

			return Path.Combine(path, this.CacheFolderName);
		}

		private void LoadIndex()
		{
			this.UpdateCacheFileInfo();

			if (!File.Exists(this.cacheIndexFilePath))
			{
				this.SaveIndex();
			}

			this.cacheIndex.Clear();

			using (StreamReader sr = File.OpenText(this.cacheIndexFilePath))
			{
				try
				{
					Dictionary<string, DateTime> indexData =
						(Dictionary<string, DateTime>)this.jsonSerializer.Deserialize(sr, typeof(Dictionary<string, DateTime>));

					if (indexData == null || !indexData.Any())
					{
						return;
					}

					foreach (KeyValuePair<string, DateTime> indexItem in indexData)
					{
						this.cacheIndex.Add(indexItem.Key, indexItem.Value);
					}
				}
				catch (Exception)
				{
					// Ignored
				}
			}
		}

		private void SaveIndex()
		{
			this.UpdateCacheFileInfo();

			using (StreamWriter sw = File.CreateText(this.cacheIndexFilePath))
			{
				this.jsonSerializer.Serialize(sw, this.cacheIndex);
			}
		}

		private void UpdateCacheFileInfo()
		{
			string cacheFolderPath = this.GetCacheFolderPath();
			if (!Directory.Exists(cacheFolderPath))
			{
				Directory.CreateDirectory(cacheFolderPath);
			}

			if (string.IsNullOrWhiteSpace(this.cacheIndexFilePath))
			{
				this.cacheIndexFilePath = Path.Combine(cacheFolderPath, "index.store");
			}
		}

		private string GetCacheFilePath(string key)
		{
			this.UpdateCacheFileInfo();

			string cacheFolderPath = this.GetCacheFolderPath();
			return Path.Combine(cacheFolderPath, $"{key}.store");
		}
	}
}