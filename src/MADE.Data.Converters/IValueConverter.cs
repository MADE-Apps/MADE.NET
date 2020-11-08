// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Data.Converters
{
    /// <summary>
    /// Defines an interface for a value converter from <see cref="TFrom"/> to <see cref="TTo"/>.
    /// </summary>
    /// <typeparam name="TFrom">
    /// The type of object to convert from.
    /// </typeparam>
    /// <typeparam name="TTo">
    /// The type of object to convert to.
    /// </typeparam>
    public interface IValueConverter<TFrom, TTo>
    {
        /// <summary>
        /// Converts the <paramref name="value">value</paramref> to the <see cref="TTo"/> type.
        /// </summary>
        /// <param name="value">
        /// The value to convert.
        /// </param>
        /// <param name="parameter">
        /// The optional parameter used to help with conversion.
        /// </param>
        /// <returns>
        /// The converted <see cref="TTo"/> object.
        /// </returns>
        TTo Convert(TFrom value, object parameter = default);

        /// <summary>
        /// Converts the <paramref name="value">value</paramref> back to the <see cref="TFrom"/> type.
        /// </summary>
        /// <param name="value">
        /// The value to convert.
        /// </param>
        /// <param name="parameter">
        /// The optional parameter used to help with conversion.
        /// </param>
        /// <returns>
        /// The converted <see cref="TFrom"/> object.
        /// </returns>
        TFrom ConvertBack(TTo value, object parameter = default);
    }
}