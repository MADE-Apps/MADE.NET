// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Testing
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Defines a code assertion helper for collection based scenarios.
    /// </summary>
    public static class CollectionAssertExtensions
    {
        /// <summary>
        /// Tests whether two collections contain the same elements and throws an exception if either collection contains an element not in the other collection.
        /// </summary>
        /// <param name="expected">
        /// The first collection to compare. This contains the elements the test expects.
        /// </param>
        /// <param name="actual">
        /// The second collection to compare. This is the collection produced by the code under test.
        /// </param>
        public static void ShouldBeEquivalentTo<TItem>(this IEnumerable<TItem> expected, IEnumerable<TItem> actual)
        {
            if ((expected == null) != (actual == null))
            {
                throw new AssertFailedException($"{nameof(ShouldBeEquivalentTo)} failed. Cannot compare enumerables for equivalency as {nameof(expected)} or {nameof(actual)} provided is null.");
            }

            if (Equals(expected, actual))
            {
                return;
            }

            var expectedList = expected.ToList();
            var actualList = actual.ToList();
            if (expectedList.Count != actualList.Count)
            {
                throw new AssertFailedException($"{nameof(ShouldBeEquivalentTo)} failed. The number of elements are different.");
            }

            if (expectedList.Count == 0 ||
                 !FindMismatchedElement(
                                        expectedList,
                                        actualList,
                                        out _,
                                        out _,
                                        out object mismatchedElement))
            {
                return;
            }

            throw new AssertFailedException($"{nameof(ShouldBeEquivalentTo)} failed. The collections contain mismatched elements. {mismatchedElement ?? "Element was null."}");
        }

        /// <summary>
        /// Tests whether two collections do not contain the same elements.
        /// </summary>
        /// <param name="expected">
        /// The first collection to compare. This contains the elements the test expects.
        /// </param>
        /// <param name="actual">
        /// The second collection to compare. This is the collection produced by the code under test.
        /// </param>
        public static void ShouldNotBeEquivalentTo<TItem>(this IEnumerable<TItem> expected, IEnumerable<TItem> actual)
        {
            if ((expected == null) != (actual == null))
            {
                throw new AssertFailedException($"{nameof(ShouldNotBeEquivalentTo)} failed. Cannot compare enumerables for equivalency as {nameof(expected)} or {nameof(actual)} provided is null.");
            }

            if (Equals(expected, actual))
            {
                throw new AssertFailedException($"{nameof(ShouldNotBeEquivalentTo)} failed. Cannot compare enumerables for equivalency as {nameof(expected)} and {nameof(actual)} are equal.");
            }

            var expectedList = expected.ToList();
            var actualList = actual.ToList();
            if (expectedList.Count != actualList.Count)
            {
                // The counts are different so cannot possibly be the same.
                return;
            }

            if (expectedList.Count == 0 ||
                 !FindMismatchedElement(
                                        expectedList,
                                        actualList,
                                        out _,
                                        out _,
                                        out _))
            {
                throw new AssertFailedException($"{nameof(ShouldNotBeEquivalentTo)} failed. The collections do not contain mismatched elements.");
            }
        }

        /// <summary>
        /// Finds a mismatched element between the two collections. A mismatched
        /// element is one that appears a different number of times in the
        /// expected collection than it does in the actual collection. The
        /// collections are assumed to be different non-null references with the
        /// same number of elements. The caller is responsible for this level of
        /// verification. If there is no mismatched element, the function returns
        /// false and the out parameters should not be used.
        /// </summary>
        /// <param name="expected">The first collection to compare.</param>
        /// <param name="actual">The second collection to compare.</param>
        /// <param name="expectedCount">
        /// The expected number of occurrences of
        /// <paramref name="mismatchedElement" /> or 0 if there is no mismatched
        /// element.
        /// </param>
        /// <param name="actualCount">
        /// The actual number of occurrences of
        /// <paramref name="mismatchedElement" /> or 0 if there is no mismatched
        /// element.
        /// </param>
        /// <param name="mismatchedElement">
        /// The mismatched element (may be null) or null if there is no
        /// mismatched element.
        /// </param>
        /// <returns>
        /// True if a mismatched element was found; false otherwise.
        /// </returns>
        private static bool FindMismatchedElement(
            ICollection expected,
            ICollection actual,
            out int expectedCount,
            out int actualCount,
            out object mismatchedElement)
        {
            Dictionary<object, int> elementCounts1 = GetElementCounts(expected, out int nullCount1);
            Dictionary<object, int> elementCounts2 = GetElementCounts(actual, out int nullCount2);

            if (nullCount2 != nullCount1)
            {
                expectedCount = nullCount1;
                actualCount = nullCount2;
                mismatchedElement = null;
                return true;
            }

            foreach (object key in elementCounts1.Keys)
            {
                elementCounts1.TryGetValue(key, out expectedCount);
                elementCounts2.TryGetValue(key, out actualCount);
                if (expectedCount == actualCount)
                {
                    continue;
                }

                mismatchedElement = key;
                return true;
            }

            expectedCount = 0;
            actualCount = 0;
            mismatchedElement = null;
            return false;
        }

        /// <summary>
        /// Constructs a dictionary containing the number of occurrences of each
        /// element in the specified collection.
        /// </summary>
        /// <param name="collection">The collection to process.</param>
        /// <param name="nullCount">
        /// The number of null elements in the collection.
        /// </param>
        /// <returns>
        /// A dictionary containing the number of occurrences of each element
        /// in the specified collection.
        /// </returns>
        private static Dictionary<object, int> GetElementCounts(IEnumerable collection, out int nullCount)
        {
            var dictionary = new Dictionary<object, int>();
            nullCount = 0;
            foreach (object key in collection)
            {
                if (key == null)
                {
                    ++nullCount;
                }
                else
                {
                    dictionary.TryGetValue(key, out int num);
                    ++num;
                    dictionary[key] = num;
                }
            }

            return dictionary;
        }

        private class AssertFailedException : Exception
        {
            public AssertFailedException(string message) : base(message) { }
        }
    }
}