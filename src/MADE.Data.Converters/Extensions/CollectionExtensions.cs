// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Data.Converters.Extensions
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines a collection of extensions for collection objects.
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        /// Converts a collection of items to a string separated by a delimiter.
        /// </summary>
        /// <typeparam name="T">The type of item within the collection.</typeparam>
        /// <param name="source">The source collection to convert.</param>
        /// <param name="delimiter">The delimiter to separate items by in the string. Default, comma.</param>
        /// <returns>A delimited string representing the collection.</returns>
        public static string ToDelimitedString<T>(this IEnumerable<T> source, string delimiter = ",")
        {
            return string.Join(delimiter, source);
        }
    }
}