// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Data.Converters.Extensions
{
    /// <summary>
    /// Defines a collection of extensions for <see cref="bool"/> values.
    /// </summary>
    public static class BooleanExtensions
    {
        /// <summary>
        /// Converts a <see cref="bool"/> value to a <see cref="string"/> value with optional true/false values.
        /// </summary>
        /// <param name="value">The <see cref="bool"/> value to format.</param>
        /// <param name="trueValue">The <see cref="string"/> format for when the <paramref name="value"/> is <c>true</c>.</param>
        /// <param name="falseValue">The <see cref="string"/> format for when the <paramref name="value"/> is <c>false</c>.</param>
        /// <returns>A formatted string</returns>
        public static string ToFormattedString(this bool value, string trueValue = "True", string falseValue = "False")
        {
            return value ? trueValue : falseValue;
        }

        /// <summary>
        /// Converts a nullable <see cref="bool"/> value to a <see cref="string"/> value with optional true/false/null values.
        /// </summary>
        /// <param name="value">The <see cref="bool"/> value to format.</param>
        /// <param name="trueValue">The <see cref="string"/> format for when the <paramref name="value"/> is <c>true</c>. Default, True.</param>
        /// <param name="falseValue">The <see cref="string"/> format for when the <paramref name="value"/> is <c>false</c>. Default, False.</param>
        /// <param name="nullValue">The <see cref="string"/> format for when the <paramref name="value"/> is <c>null</c>. Default, Not set.</param>
        /// <returns>A formatted string</returns>
        public static string ToFormattedString(
            this bool? value,
            string trueValue = "True",
            string falseValue = "False",
            string nullValue = "Not set")
        {
            return value.HasValue ? value.Value ? trueValue : falseValue : nullValue;
        }
    }
}