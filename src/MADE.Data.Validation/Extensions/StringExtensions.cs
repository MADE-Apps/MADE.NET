// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Data.Validation.Extensions
{
    using System;
    using System.Globalization;

    /// <summary>
    /// Defines a collection of extensions for string values.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Indicates whether a specified string is null, empty, or consists only of white-space characters.
        /// </summary>
        /// <param name="value">The string to test.</param>
        /// <returns>
        /// True if the <paramref name="value" /> parameter is null or empty, or if <paramref name="value" /> consists exclusively of white-space characters.
        /// </returns>
        public static bool IsNullOrWhiteSpace(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        /// <summary>
        /// Checks whether a phrase contains a specified value using a comparison option.
        /// </summary>
        /// <param name="phrase">
        /// The phrase to check.
        /// </param>
        /// <param name="value">
        /// The value to find.
        /// </param>
        /// <param name="compareOption">
        /// The compare option.
        /// </param>
        /// <returns>
        /// True if the phrase contains the value; otherwise, false.
        /// </returns>
        public static bool Contains(this string phrase, string value, CompareOptions compareOption)
        {
            return CultureInfo.CurrentCulture.CompareInfo.IndexOf(phrase, value, compareOption) >= 0;
        }

        /// <summary>
        /// Checks whether a string value is an integer.
        /// </summary>
        /// <param name="value">
        /// The value to check.
        /// </param>
        /// <returns>
        /// True if safely parses to an integer; otherwise, false.
        /// </returns>
        public static bool IsInt(this string value)
        {
            return int.TryParse(value, out int _);
        }

        /// <summary>
        /// Checks whether a string value is a double.
        /// </summary>
        /// <param name="value">
        /// The value to check.
        /// </param>
        /// <returns>
        /// True if safely parses to a double; otherwise, false.
        /// </returns>
        public static bool IsDouble(this string value)
        {
            return double.TryParse(value, out double _);
        }

        /// <summary>
        /// Checks whether a string value is a boolean.
        /// </summary>
        /// <param name="value">
        /// The value to check.
        /// </param>
        /// <returns>
        /// True if safely parses to a boolean; otherwise, false.
        /// </returns>
        public static bool IsBoolean(this string value)
        {
            return bool.TryParse(value, out bool _);
        }

        /// <summary>
        /// Checks whether a string value is a float.
        /// </summary>
        /// <param name="value">
        /// The value to check.
        /// </param>
        /// <returns>
        /// True if safely parses to a float; otherwise, false.
        /// </returns>
        public static bool IsFloat(this string value)
        {
            return float.TryParse(value, out float _);
        }
    }
}