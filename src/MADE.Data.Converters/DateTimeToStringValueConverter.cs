// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Data.Converters
{
    using System;
    using System.Globalization;

    /// <summary>
    /// Defines a value converter from <see cref="DateTime"/> to <see cref="string"/> with an optional format string.
    /// </summary>
    public class DateTimeToStringValueConverter : IValueConverter<DateTime, string>
    {
        /// <summary>
        /// Converts the <paramref name="value">value</paramref> to the <see cref="string"/> type.
        /// </summary>
        /// <param name="value">
        /// The value to convert.
        /// </param>
        /// <param name="parameter">
        /// The optional parameter used to help with conversion.
        /// </param>
        /// <returns>
        /// The converted <see cref="string"/> object.
        /// </returns>
        public string Convert(DateTime value, object parameter = default)
        {
            string format = parameter?.ToString();
            return !string.IsNullOrWhiteSpace(format)
                       ? value.ToString(format, CultureInfo.InvariantCulture)
                       : value.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Converts the <paramref name="value">value</paramref> back to the <see cref="DateTime"/> type.
        /// </summary>
        /// <param name="value">
        /// The value to convert.
        /// </param>
        /// <param name="parameter">
        /// The optional parameter used to help with conversion.
        /// </param>
        /// <returns>
        /// The converted <see cref="DateTime"/> object.
        /// </returns>
        public DateTime ConvertBack(string value, object parameter = default)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return DateTime.MinValue;
            }

            bool parsed = DateTime.TryParse(value, out DateTime dateTime);
            return parsed ? dateTime : DateTime.MinValue;
        }
    }
}