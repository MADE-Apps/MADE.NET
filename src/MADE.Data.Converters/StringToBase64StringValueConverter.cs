// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Data.Converters
{
    using System.Text;
    using MADE.Data.Converters.Extensions;

    /// <summary>
    /// Defines a value converter from <see cref="string"/> to Base64 <see cref="string"/> with an optional Encoding parameter.
    /// </summary>
    public partial class StringToBase64StringValueConverter : IValueConverter<string, string>
    {
        /// <summary>
        /// Converts the <paramref name="value">value</paramref> to the Base64 <see cref="string"/>.
        /// </summary>
        /// <param name="value">
        /// The value to convert.
        /// </param>
        /// <param name="parameter">
        /// The optional <see cref="Encoding"/> parameter used to help with conversion.
        /// </param>
        /// <returns>
        /// The converted Base64 <see cref="string"/> object.
        /// </returns>
        public string Convert(string value, object parameter = default)
        {
            return value.ToBase64(parameter as Encoding ?? Encoding.UTF8);
        }

        /// <summary>
        /// Converts the Base64 <paramref name="value">value</paramref> back to the original <see cref="string"/> value.
        /// </summary>
        /// <param name="value">
        /// The value to convert.
        /// </param>
        /// <param name="parameter">
        /// The optional <see cref="Encoding"/> parameter used to help with conversion.
        /// </param>
        /// <returns>
        /// The converted <see cref="string"/> object.
        /// </returns>
        public string ConvertBack(string value, object parameter = default)
        {
            return value.FromBase64(parameter as Encoding ?? Encoding.UTF8);
        }
    }
}