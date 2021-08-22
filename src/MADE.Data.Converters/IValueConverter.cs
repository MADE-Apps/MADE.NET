// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Data.Converters
{
    /// <summary>
    /// Defines an interface for a value converter from <typeparamref name="TFrom"/> to <typeparamref name="TTo"/>.
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
        /// Converts the <paramref name="value">value</paramref> to the <typeparamref name="TTo"/> type.
        /// </summary>
        /// <param name="value">
        /// The value to convert.
        /// </param>
        /// <param name="parameter">
        /// The optional parameter used to help with conversion.
        /// </param>
        /// <returns>
        /// The converted <typeparamref name="TTo"/> object.
        /// </returns>
        TTo Convert(TFrom value, object parameter = default);

        /// <summary>
        /// Converts the <paramref name="value">value</paramref> back to the <typeparamref name="TFrom"/> type.
        /// </summary>
        /// <param name="value">
        /// The value to convert.
        /// </param>
        /// <param name="parameter">
        /// The optional parameter used to help with conversion.
        /// </param>
        /// <returns>
        /// The converted <typeparamref name="TFrom"/> object.
        /// </returns>
        TFrom ConvertBack(TTo value, object parameter = default);
    }
}