// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDataCacheProvider.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines an interface for a data caching provider.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MADE.Data.Caching
{
    using System;

    /// <summary>
    /// Defines an interface for a data caching provider.
    /// </summary>
    public interface IDataCacheProvider
    {
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
        void AddOrUpdate<T>(string key, T content);

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
        T Get<T>(string key);

        /// <summary>
        /// Determines whether the given key has existing content within the cache.
        /// </summary>
        /// <param name="key">
        /// The key to check exists.
        /// </param>
        /// <returns>
        /// Returns true if content exists for the given key.
        /// </returns>
        bool Contains(string key);

        /// <summary>
        /// Removes the content for the given key from the cache.
        /// </summary>
        /// <param name="key">
        /// The key to remove.
        /// </param>
        void Remove(string key);

        /// <summary>
        /// Weeds/removes content from the cache that were cached before the given weed from date.
        /// </summary>
        /// <param name="weedFromDate">
        /// The date from when cached items should be removed. E.g. 24/12/2017 would remove any items cached before the 24/12/2017.
        /// </param>
        void Weed(DateTime weedFromDate);

        /// <summary>
        /// Weeds/removes content from the cache that were cached before the given weed from date/time.
        /// </summary>
        /// <param name="weedFromDateTime">
        /// The date/time from when cached items should be removed. E.g. 24/12/2017 11:00:00 would remove any items cached before the 24/12/2017 11:00:00.
        /// </param>
        /// <param name="includeTime">
        /// A value indicating whether to take into account the time component of the given weed from date/time.
        /// </param>
        void Weed(DateTime weedFromDateTime, bool includeTime);
    }
}