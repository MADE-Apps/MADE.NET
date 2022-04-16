// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Data.Converters.Extensions
{
    using System;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines a collection of extensions for string values.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Converts a value to title case using the case rules of the invariant culture.
        /// </summary>
        /// <param name="value">
        /// The value to convert.
        /// </param>
        /// <returns>
        /// The converted title case string.
        /// </returns>
        /// <example>
        /// string converted = "HELLO, WORLD".ToTitleCase(); // converted = "Hello, World".
        /// </example>
        public static string ToTitleCase(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }

            var result = new StringBuilder(value);
            result[0] = char.ToUpper(result[0]);
            for (int i = 1; i < result.Length; ++i)
            {
                result[i] = char.IsWhiteSpace(result[i - 1]) ? char.ToUpper(result[i]) : char.ToLower(result[i]);
            }

            return result.ToString();
        }

        /// <summary>
        /// Truncates a string value to the specified length with an ellipsis.
        /// </summary>
        /// <param name="value">The value to truncate.</param>
        /// <param name="maxLength">The maximum length of the value.</param>
        /// <returns>A truncated string with ellipsis if the value's length is greater than the <paramref name="maxLength"/>.</returns>
        public static string Truncate(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }

            if (value.Length <= maxLength)
            {
                return value;
            }

            const string suffix = "...";
            return value.Substring(0, maxLength - suffix.Length) + suffix;
        }

        /// <summary>
        /// Converts a value to default case using the case rules of the invariant culture.
        /// </summary>
        /// <param name="value">
        /// The value to convert.
        /// </param>
        /// <returns>
        /// The converted default case string.
        /// </returns>
        /// <example>
        /// string converted = "HELLO, WORLD".ToDefaultCase(); // converted = "Hello, world".
        /// </example>
        public static string ToDefaultCase(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return string.Empty;
            }

            string result = value.Substring(0, 1).ToUpperInvariant() + value.Substring(1).ToLowerInvariant();
            return result;
        }

        /// <summary>
        /// Converts a value to a Base64 string using the specified encoding.
        /// <para>
        /// Default encoding is UTF-8.
        /// </para>
        /// </summary>
        /// <param name="value">The string value to convert.</param>
        /// <param name="encoding">The encoding to get the value bytes while converting.</param>
        /// <returns>The Base64 string representing the value.</returns>
        public static string ToBase64(this string value, Encoding encoding = default)
        {
            encoding ??= Encoding.UTF8;
            return Convert.ToBase64String(encoding.GetBytes(value));
        }

        /// <summary>
        /// Converts a Base64 string to a value using the specified encoding.
        /// </summary>
        /// <param name="base64Value">The Base64 value to convert.</param>
        /// <param name="encoding">The encoding to get the value string while converting.</param>
        /// <returns>The string value representing the Base64 string.</returns>
        public static string FromBase64(this string base64Value, Encoding encoding = default)
        {
            encoding ??= Encoding.UTF8;
            return encoding.GetString(Convert.FromBase64String(base64Value));
        }

        /// <summary>
        /// Converts a string value to a <see cref="MemoryStream"/>.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>A <see cref="MemoryStream"/> representing the string value.</returns>
        public static async Task<MemoryStream> ToMemoryStreamAsync(this string value)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            await writer.WriteAsync(value);
            await writer.FlushAsync();
            stream.Position = 0;
            return stream;
        }

        /// <summary>
        /// Converts a string value to an integer.
        /// </summary>
        /// <param name="value">
        /// The value to convert.
        /// </param>
        /// <returns>
        /// The int representing the value.
        /// </returns>
        public static int ToInt(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return 0;
            }

            bool parsed = int.TryParse(value, out int intValue);
            return parsed ? intValue : 0;
        }

        /// <summary>
        /// Converts a string value to a nullable integer.
        /// </summary>
        /// <param name="value">
        /// The value to convert.
        /// </param>
        /// <returns>
        /// The nullable integer representing the value.
        /// </returns>
        public static int? ToNullableInt(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }

            bool parsed = int.TryParse(value, out int intValue);
            return parsed ? intValue : null;
        }

        /// <summary>
        /// Converts a string value to a boolean.
        /// </summary>
        /// <param name="value">
        /// The value to convert.
        /// </param>
        /// <returns>
        /// The boolean representing the value.
        /// </returns>
        public static bool ToBoolean(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return false;
            }

            bool parsed = bool.TryParse(value, out bool booleanValue);
            return parsed && booleanValue;
        }

        /// <summary>
        /// Converts a string value to a float.
        /// </summary>
        /// <param name="value">
        /// The value to convert.
        /// </param>
        /// <returns>
        /// The float representing the value.
        /// </returns>
        public static float ToFloat(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return 0;
            }

            bool parsed = float.TryParse(value, out float floatValue);
            return parsed ? floatValue : 0;
        }

        /// <summary>
        /// Converts a string value to a nullable float.
        /// </summary>
        /// <param name="value">
        /// The value to convert.
        /// </param>
        /// <returns>
        /// The nullable float representing the value.
        /// </returns>
        public static float? ToNullableFloat(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }

            bool parsed = float.TryParse(value, out float floatValue);
            return parsed ? floatValue : null;
        }

        /// <summary>
        /// Converts a string value to a double.
        /// </summary>
        /// <param name="value">
        /// The value to convert.
        /// </param>
        /// <returns>
        /// The double representing the value.
        /// </returns>
        public static double ToDouble(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return 0;
            }

            bool parsed = double.TryParse(value, out double doubleValue);
            return parsed ? doubleValue : 0;
        }

        /// <summary>
        /// Converts a string value to a nullable double.
        /// </summary>
        /// <param name="value">
        /// The value to convert.
        /// </param>
        /// <returns>
        /// The nullable double representing the value.
        /// </returns>
        public static double? ToNullableDouble(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }

            bool parsed = double.TryParse(value, out double doubleValue);
            return parsed ? doubleValue : null;
        }
    }
}