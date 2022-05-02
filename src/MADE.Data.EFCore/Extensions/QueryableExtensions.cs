namespace MADE.Data.EFCore.Extensions
{
    using System;
    using System.Linq;
    using Z.EntityFramework.Plus;

    /// <summary>
    /// Defines a collection of extensions for Entity Framework queries.
    /// </summary>
    public static class QueryableExtensions
    {
        /// <summary>
        /// Skips and takes a subset of a data query based on the specified current page and page size requested.
        /// </summary>
        /// <typeparam name="T">The type of entity being queried.</typeparam>
        /// <param name="query">The current query.</param>
        /// <param name="page">The current page being requested.</param>
        /// <param name="pageSize">The size of the page being requested.</param>
        /// <returns>The paginated query.</returns>
        public static IQueryable<T> Page<T>(this IQueryable<T> query, int page, int pageSize)
        {
            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }

        /// <summary>
        /// Orders the query results by the specified property name from the entity with the option for order by ascending or descending.
        /// </summary>
        /// <typeparam name="T">The type of entity being ordered.</typeparam>
        /// <param name="query">The query to order.</param>
        /// <param name="sortName">The property/column name to sort on for the entity.</param>
        /// <param name="sortDesc">A value indicating whether to sort descending.</param>
        /// <returns>The ordered query.</returns>
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> query, string sortName, bool sortDesc)
        {
            return string.IsNullOrWhiteSpace(sortName)
                ? query
                : (!sortDesc ? query.AddOrAppendOrderBy(sortName) : query.AddOrAppendOrderByDescending(sortName));
        }
    }
}