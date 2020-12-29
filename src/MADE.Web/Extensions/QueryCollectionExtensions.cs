namespace MADE.Web.Extensions
{
    using System;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// Defines a collection of extensions for <see cref="IQueryCollection"/> objects.
    /// </summary>
    public static class QueryCollectionExtensions
    {
        /// <summary>
        /// Gets a string value from the <paramref name="query"/> by the specified <paramref name="key"/>.
        /// </summary>
        /// <param name="query">The query to retrieve a string value from.</param>
        /// <param name="key">The key associated with the parameter to retrieve.</param>
        /// <param name="defaultValue">The default value if the value does not exist.</param>
        /// <returns>The string value for the specified <paramref name="key"/>.</returns>
        public static string GetStringValueOrDefault(this IQueryCollection query, string key, string defaultValue = null)
        {
            string value = null;
            if (query.ContainsKey(key))
            {
                value = query[key].ToString();
            }

            return string.IsNullOrWhiteSpace(value) ? defaultValue : value;
        }

        /// <summary>
        /// Gets an integer value from the <paramref name="query"/> by the specified <paramref name="key"/>.
        /// </summary>
        /// <param name="query">The query to retrieve an integer value from.</param>
        /// <param name="key">The key associated with the parameter to retrieve.</param>
        /// <param name="defaultValue">The default value if the value does not exist.</param>
        /// <param name="treatZeroAsEmpty">A value indicating whether to treat 0 as empty. True by default.</param>
        /// <returns>The integer value for the specified <paramref name="key"/>.</returns>
        public static int GetIntValueOrDefault(this IQueryCollection query, string key, int defaultValue, bool treatZeroAsEmpty = true)
        {
            string stringValue = GetStringValueOrDefault(query, key);

            if (string.IsNullOrWhiteSpace(stringValue)
                || !int.TryParse(stringValue, out int intValue)
                || (treatZeroAsEmpty && intValue == 0))
            {
                return defaultValue;
            }

            return intValue;
        }

        /// <summary>
        /// Gets a <see cref="DateTime"/> value from the <paramref name="query"/> by the specified <paramref name="key"/>.
        /// </summary>
        /// <param name="query">The query to retrieve a <see cref="DateTime"/> value from.</param>
        /// <param name="key">The key associated with the parameter to retrieve.</param>
        /// <param name="defaultValue">The default value if the value does not exist.</param>
        /// <returns>The <see cref="DateTime"/> value for the specified <paramref name="key"/>.</returns>
        public static DateTime GetDateTimeValueOrDefault(this IQueryCollection query, string key, DateTime defaultValue)
        {
            string stringValue = GetStringValueOrDefault(query, key);

            if (string.IsNullOrWhiteSpace(stringValue) || !DateTime.TryParse(stringValue, out DateTime dateTimeValue))
            {
                return defaultValue;
            }

            return dateTimeValue;
        }
    }
}