namespace MADE.Networking.Extensions
{
    using System;
    using System.Collections.Specialized;

    /// <summary>
    /// Defines a collection of extensions for <see cref="Uri"/> objects.
    /// </summary>
    public static class UriExtensions
    {
        /// <summary>
        /// Gets a value from a query in the specified <paramref name="uri"/> with the specified query parameter key.
        /// </summary>
        /// <param name="uri">The <see cref="Uri"/> to extract a query value from.</param>
        /// <param name="queryParam">The key of the parameter in the query to extract the value for.</param>
        /// <returns>The value for the query parameter.</returns>
        public static string GetQueryValue(this Uri uri, string queryParam)
        {
            NameValueCollection queryDictionary = System.Web.HttpUtility.ParseQueryString(uri.Query);
            return queryDictionary.Get(queryParam);
        }
    }
}
