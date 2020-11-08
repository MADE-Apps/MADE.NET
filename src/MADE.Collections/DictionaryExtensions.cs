// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Collections
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines a collection of extensions for dictionaries.
    /// </summary>
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Adds or updates a value within a dictionary.
        /// </summary>
        /// <param name="dictionary">
        /// The dictionary to update.
        /// </param>
        /// <param name="key">
        /// The key of the value to add or update.
        /// </param>
        /// <param name="value">
        /// The value to add or update.
        /// </param>
        /// <typeparam name="TKey">
        /// The type of key item within the dictionary.
        /// </typeparam>
        /// <typeparam name="TValue">
        /// The type of value item within the dictionary.
        /// </typeparam>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="dictionary"/> or <paramref name="key"/> is <see langword="null"/></exception>
        public static void AddOrUpdate<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            if (dictionary == null)
            {
                throw new ArgumentNullException(nameof(dictionary));
            }

            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (dictionary.ContainsKey(key))
            {
                dictionary.Remove(key);
            }

            dictionary.Add(key, value);
        }
    }
}