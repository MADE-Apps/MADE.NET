// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Collections
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Defines a collection of extensions for queryable objects.
    /// </summary>
    public static class QueryableExtensions
    {
        /// <summary>
        /// Chunks a query of items into the specified chunk size.
        /// </summary>
        /// <typeparam name="T">The type of item.</typeparam>
        /// <param name="source">The source query to chunk.</param>
        /// <param name="chunkSize">The chunk size.</param>
        /// <returns>A collection of queries containing the chunked items.</returns>
        public static IEnumerable<IQueryable<T>> Chunk<T>(this IQueryable<T> source, int chunkSize = 25)
        {
            int idx = 0;
            while (true)
            {
                IQueryable<T> q = idx == 0
                    ? source
                    : source.Skip(idx * chunkSize);
                IQueryable<T> chunk = q.Take(chunkSize);
                if (!chunk.Any())
                {
                    yield break;
                }

                yield return chunk.AsQueryable();
                idx++;
            }
        }
    }
}