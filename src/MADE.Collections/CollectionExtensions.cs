// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Collections
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    /// <summary>
    /// Defines a collection of extensions for enumerables, lists, and collections.
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        /// Adds the specified item to the collection based on the specified condition being true.
        /// </summary>
        /// <param name="collection">The collection to add the item to.</param>
        /// <param name="item">The item to add.</param>
        /// <param name="condition">The condition required to add the item.</param>
        /// <typeparam name="T">The type of item within the collection.</typeparam>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="collection"/> or <paramref name="condition"/> is <see langword="null"/>.</exception>
        /// <exception cref="Exception">Potentially thrown by the delegate callback.</exception>
        public static void AddIf<T>(this IList<T> collection, T item, Func<bool> condition)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (condition == null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            if (condition())
            {
                collection.Add(item);
            }
        }

        /// <summary>
        /// Removes the specified item from the collection based on the specified condition being true.
        /// </summary>
        /// <param name="collection">The collection to remove the item from.</param>
        /// <param name="item">The item to remove.</param>
        /// <param name="condition">The condition required to remove the item.</param>
        /// <typeparam name="T">The type of item within the collection.</typeparam>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="collection"/> or <paramref name="condition"/> is <see langword="null"/>.</exception>
        /// <exception cref="Exception">Potentially thrown by the delegate callback.</exception>
        public static void RemoveIf<T>(this IList<T> collection, T item, Func<bool> condition)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (condition == null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            if (condition())
            {
                collection.Remove(item);
            }
        }

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
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="collection"/> or <paramref name="itemsToAdd"/> is <see langword="null"/>.</exception>
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
        /// Adds the specified collection of items to the collection based on the specified condition being true.
        /// </summary>
        /// <param name="collection">The collection to add the items to.</param>
        /// <param name="itemsToAdd">The items to add.</param>
        /// <param name="condition">The condition required to add the items.</param>
        /// <typeparam name="T">The type of item within the collection.</typeparam>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="collection"/>, <paramref name="itemsToAdd"/> or <paramref name="condition"/> is <see langword="null"/>.</exception>
        /// <exception cref="Exception">Potentially thrown by the delegate callback.</exception>
        public static void AddRangeIf<T>(
            this ICollection<T> collection,
            IEnumerable<T> itemsToAdd,
            Func<bool> condition)
        {
            if (condition == null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            if (condition())
            {
                collection.AddRange(itemsToAdd);
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
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="collection"/> or <paramref name="itemsToRemove"/> is <see langword="null"/>.</exception>
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

        /// <summary>
        /// Removes the specified collection of items from the collection based on the specified condition being true.
        /// </summary>
        /// <param name="collection">The collection to remove the items from.</param>
        /// <param name="itemsToRemove">The items to remove.</param>
        /// <param name="condition">The condition required to remove the items.</param>
        /// <typeparam name="T">The type of item within the collection.</typeparam>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="collection"/>, <paramref name="itemsToRemove"/> or <paramref name="condition"/> is <see langword="null"/>.</exception>
        /// <exception cref="Exception">Potentially thrown by the delegate callback.</exception>
        public static void RemoveRangeIf<T>(
            this ICollection<T> collection,
            IEnumerable<T> itemsToRemove,
            Func<bool> condition)
        {
            if (condition == null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            if (condition())
            {
                collection.RemoveRange(itemsToRemove);
            }
        }

        /// <summary>
        /// Determines whether two collections are equivalent, containing all the same items with no regard to order.
        /// </summary>
        /// <typeparam name="T">The type of item.</typeparam>
        /// <param name="expected">The expected collection.</param>
        /// <param name="actual">The actual collection.</param>
        /// <returns>True if the collections are equivalent; otherwise, false.</returns>
        public static bool AreEquivalent<T>(this ICollection<T> expected, ICollection<T> actual)
        {
            return (expected == null && actual == null)
                   || (expected != null && actual != null
                                        && expected.All(actual.Contains)
                                        && expected.Count == actual.Count);
        }

        /// <summary>
        /// Takes a number of elements from the specified collection from the specified starting index.
        /// </summary>
        /// <param name="list">
        /// The <see cref="List{T}"/> to take items from.
        /// </param>
        /// <param name="startingIndex">
        /// The index to start at in the <see cref="List{T}"/>.
        /// </param>
        /// <param name="takeCount">
        /// The number of items to take from the starting index of the <see cref="List{T}"/>.
        /// </param>
        /// <typeparam name="T">
        /// The type of elements in the collection.
        /// </typeparam>
        /// <returns>
        /// A collection of <typeparamref name="T"/> items.
        /// </returns>
        public static IEnumerable<T> TakeFrom<T>(this List<T> list, int startingIndex, int takeCount)
        {
            var results = new List<T>();

            int itemsToTake = takeCount;

            if (list.Count - 1 - startingIndex > itemsToTake)
            {
                List<T> items = list.GetRange(startingIndex, itemsToTake);
                results.AddRange(items);
            }
            else
            {
                itemsToTake = list.Count - startingIndex;
                if (itemsToTake <= 0)
                {
                    return results;
                }

                List<T> items = list.GetRange(startingIndex, itemsToTake);
                results.AddRange(items);
            }

            return results;
        }

        /// <summary>
        /// Performs the specified action on each item in the collection.
        /// </summary>
        /// <typeparam name="T">
        /// The type of item in the collection.
        /// </typeparam>
        /// <param name="collection">
        /// The collection to action on.
        /// </param>
        /// <param name="action">
        /// The action to perform.
        /// </param>
        /// <exception cref="Exception">Potentially thrown by the delegate callback.</exception>
        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach (T item in collection)
            {
                action?.Invoke(item);
            }
        }

        /// <summary>
        /// Chunks a collection of items into a collection of collections grouped into the specified chunk size.
        /// </summary>
        /// <typeparam name="T">The type of item.</typeparam>
        /// <param name="source">The source collection to chunk.</param>
        /// <param name="chunkSize">The chunk size.</param>
        /// <returns>A collection of collections containing the chunked items.</returns>
        public static IEnumerable<IEnumerable<T>> Chunk<T>(this IEnumerable<T> source, int chunkSize = 25)
        {
            return source
                .Select((v, i) => new { Index = i, Value = v })
                .GroupBy(x => x.Index / chunkSize)
                .Select(x => x.Select(v => v.Value));
        }

        /// <summary>Inserts an item to the specified <paramref name="source"/> at the potential index determined by the <paramref name="predicate"/>.</summary>
        /// <param name="source">The source where the <paramref name="value" /> should be inserted.</param>
        /// <param name="value">The object to insert into the <paramref name="source"/>.</param>
        /// <param name="predicate">The action to run to determine the position of the item based on the provided <paramref name="value"/> and an item in the collection.</param>
        /// <typeparam name="T">The type of items in the collection.</typeparam>
        /// <returns>The inserted index of the item.</returns>
        /// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.IList`1" /> is read-only.</exception>
        public static int InsertAtPotentialIndex<T>(this IList<T> source, T value, Func<T, T, bool> predicate)
        {
            var potentialIndex = source.PotentialIndexOf(value, predicate);
            source.Insert(potentialIndex, value);
            return potentialIndex;
        }

        /// <summary>Gets the potential index of an item that does not currently exist within a collection based on the specified criteria.</summary>
        /// <param name="source">The collection to get the index from.</param>
        /// <param name="value">The object to determine an index for in the <paramref name="source"/>.</param>
        /// <param name="predicate">The action to run to determine the position of the item based on the provided <paramref name="value"/> and an item in the collection.</param>
        /// <typeparam name="T">The type of items in the collection.</typeparam>
        /// <returns>The potential index of the item.</returns>
        /// <exception cref="Exception">Potentially thrown by the delegate callback.</exception>
        public static int PotentialIndexOf<T>(this IList<T> source, T value, Func<T, T, bool> predicate)
        {
            var result = 0;

            foreach (var item in source)
            {
                if (predicate(value, item))
                {
                    result = source.IndexOf(item) + 1;
                    continue;
                }

                break;
            }

            return result;
        }

        /// <summary>
        /// Shuffles the elements of a sequence randomly.
        /// </summary>
        /// <param name="source">The collection to shuffle.</param>
        /// <typeparam name="T">The type of item in the collection.</typeparam>
        /// <returns>The shuffled collection of items.</returns>
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            return source.OrderBy(x => Guid.NewGuid());
        }

        /// <summary>Sorts the elements in the entire <see cref="ObservableCollection{T}"/> using the specified comparer.</summary>
        /// <param name="source">The source collection to sort.</param>
        /// <param name="comparer">The implementation to use when comparing elements.</param>
        /// <typeparam name="T">The type of item in the collection.</typeparam>
        /// <typeparam name="TKey">The key value of the item to sort on.</typeparam>
        public static void Sort<T, TKey>(this ObservableCollection<T> source, Func<T, TKey> comparer)
        {
            if (source is not { Count: > 1 })
            {
                return;
            }

            var idx = 0;
            foreach (var originalIdx in source.OrderBy(comparer).Select(source.IndexOf))
            {
                if (originalIdx != idx)
                {
                    source.Move(originalIdx, idx);
                }

                idx++;
            }
        }

        /// <summary>Sorts the elements in the entire <see cref="ObservableCollection{T}"/> using the specified comparer in descending order.</summary>
        /// <param name="source">The source collection to sort.</param>
        /// <param name="comparer">The implementation to use when comparing elements.</param>
        /// <typeparam name="T">The type of item in the collection.</typeparam>
        /// <typeparam name="TKey">The key value of the item to sort on.</typeparam>
        public static void SortDescending<T, TKey>(this ObservableCollection<T> source, Func<T, TKey> comparer)
        {
            if (source is not { Count: > 1 })
            {
                return;
            }

            var idx = 0;
            foreach (var originalIdx in source.OrderByDescending(comparer).Select(source.IndexOf))
            {
                if (originalIdx != idx)
                {
                    source.Move(originalIdx, idx);
                }

                idx++;
            }
        }

        /// <summary>Indicates whether the specified collection is <see langword="null" /> or empty (containing no items).</summary>
        /// <param name="source">The collection to test.</param>
        /// <typeparam name="T">The type of item in the collection.</typeparam>
        /// <returns>
        /// <see langword="true" /> if the <paramref name="source" /> parameter is <see langword="null" /> or empty (containing no items); otherwise, <see langword="false" />.
        /// </returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> source)
        {
            return source is null || !source.Any();
        }
    }
}