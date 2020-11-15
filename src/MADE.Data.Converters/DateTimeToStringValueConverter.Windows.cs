// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#if WINDOWS_UWP
namespace MADE.Data.Converters
{
    using System;
    using Windows.UI.Xaml.Data;

    /// <summary>
    /// Defines a Windows components for a XAML value converter from <see cref="DateTime"/> to <see cref="string"/> with an optional format string.
    /// </summary>
    public partial class DateTimeToStringValueConverter : IValueConverter
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
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            switch (value)
            {
                case DateTime dateTime:
                    return this.Convert(dateTime, parameter?.ToString());
                default:
                    return value;
            }
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
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            string dateTimeString = value?.ToString();
            return this.ConvertBack(dateTimeString);
        }
    }
}
#endif