// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Collections
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Defines a collection of extensions for enumerables, lists, and collections.
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        /// Updates an item within the collection.
        /// </summary>
        /// <typeparam name="T">
        /// The type of item within the collection.
        /// </typeparam>
        /// <param name="collection">
        /// The collection to update an item in.
        /// </param>
        /// <param name="item">
        /// The item to update.
        /// </param>
        /// <param name="predicate">
        /// The function to find the item within the <paramref name="collection"/>.
        /// </param>
        /// <returns>
        /// True if the item has been updated; otherwise, false.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="item"/> or <paramref name="collection"/> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.Exception">The <paramref name="predicate"/> delegate callback throws an exception.</exception>
        public static bool Update<T>(this IList<T> collection, T item, Func<T, T, bool> predicate)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            T existing = collection.FirstOrDefault(x => predicate.Invoke(x, item));
            if (existing == null)
            {
                return false;
            }

            int idx = collection.IndexOf(existing);

            collection.Remove(existing);
            collection.Insert(idx, item);
            return true;
        }

        /// <summary>
        /// Makes the given destination collection items equal to the items in the given source collection by adding or removing items from the destination.
        /// </summary>
        /// <param name="destination">
        /// The destination collection to add or remove items to.
        /// </param>
        /// <param name="source">
        /// The source collection to provide the items.
        /// </param>
        /// <typeparam name="T">
        /// The type of item within the collection.
        /// </typeparam>
        public static void MakeEqualTo<T>(this ICollection<T> destination, IEnumerable<T> source)
        {
            var sourceList = source.ToList();
            foreach (T item in destination.Except(sourceList).ToList())
            {
                destination.Remove(item);
            }

            foreach (T item in sourceList.Except(destination).ToList())
            {
                destination.Add(item);
            }
        }

        /// <summary>
        /// Adds a collection of items to another.
        /// </summary>
        /// <param name="collection">
        /// The collection to add to.
        /// </param>
        /// <param name="itemsToAdd">
        /// The items to add.
        /// </param>
        /// <typeparam name="T">
        /// The type of items in the collection.
        /// </typeparam>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="collection"/> or <paramref name="itemsToAdd"/> is <see langword="null"/></exception>
        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> itemsToAdd)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (itemsToAdd == null)
            {
                throw new ArgumentNullException(nameof(itemsToAdd));
            }

            foreach (T item in itemsToAdd)
            {
                collection.Add(item);
            }
        }

        /// <summary>
        /// Removes a collection of items from another.
        /// </summary>
        /// <param name="collection">
        /// The collection to remove from.
        /// </param>
        /// <param name="itemsToRemove">
        /// The items to remove from the collection.
        /// </param>
        /// <typeparam name="T">
        /// The type of items in the collection.
        /// </typeparam>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="collection"/> or <paramref name="itemsToRemove"/> is <see langword="null"/></exception>
        public static void RemoveRange<T>(this ICollection<T> collection, IEnumerable<T> itemsToRemove)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (itemsToRemove == null)
            {
                throw new ArgumentNullException(nameof(itemsToRemove));
            }

            foreach (T item in itemsToRemove)
            {
                if (collection.Contains(item))
                {
                    collection.Remove(item);
                }
            }
        }
    }
}