// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CachedData.cs" company="MADE Apps">
//   Copyright (c) MADE Apps.
// </copyright>
// <summary>
//   Defines a model for a cached data item.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MADE.Data.Caching
{
    using System;

    /// <summary>
    /// Defines a model for a cached data item.
    /// </summary>
    public class CachedData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CachedData"/> class.
        /// </summary>
        public CachedData()
            : this(string.Empty, string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CachedData"/> class.
        /// </summary>
        /// <param name="key">
        /// The key/identifier.
        /// </param>
        /// <param name="content">
        /// The content as a string.
        /// </param>
        public CachedData(string key, string content)
        {
            this.Key = key;
            this.Content = content;
            this.CachedDate = DateTime.UtcNow;
        }

        /// <summary>
        /// Gets or sets the key/identifier.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the content as a string.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Gets the date the item was cached in UTC.
        /// </summary>
        public DateTime CachedDate { get; }
    }
}