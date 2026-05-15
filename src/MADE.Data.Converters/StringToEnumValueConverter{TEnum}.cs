// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using MADE.Data.Converters.Exceptions;

namespace MADE.Data.Converters;

/// <summary>
/// Defines a value converter from <see cref="string"/> to an <see cref="Enum"/> type.
/// </summary>
/// <typeparam name="TEnum">The enum type to convert to and from.</typeparam>
public class StringToEnumValueConverter<TEnum> : IValueConverter<string, TEnum>
    where TEnum : struct, Enum
{
    /// <summary>
    /// Gets or sets a value indicating whether the conversion should ignore case. Default is true.
    /// </summary>
    public bool IgnoreCase { get; set; } = true;

    /// <summary>
    /// Converts the <paramref name="value">value</paramref> to the <typeparamref name="TEnum"/> type.
    /// </summary>
    /// <param name="value">
    /// The string value to convert.
    /// </param>
    /// <param name="parameter">
    /// The optional parameter used to help with conversion.
    /// </param>
    /// <returns>
    /// The converted <typeparamref name="TEnum"/> value.
    /// </returns>
    /// <exception cref="InvalidDataConversionException">Thrown if the <paramref name="value"/> cannot be parsed as a <typeparamref name="TEnum"/>.</exception>
    public TEnum Convert(string value, object? parameter = default)
    {
        if (Enum.TryParse<TEnum>(value, this.IgnoreCase, out var result))
        {
            return result;
        }

        throw new InvalidDataConversionException(
            nameof(StringToEnumValueConverter<TEnum>),
            value,
            $"Cannot convert '{value}' to {typeof(TEnum).Name}.");
    }

    /// <summary>
    /// Converts the <paramref name="value">value</paramref> back to a <see cref="string"/>.
    /// </summary>
    /// <param name="value">
    /// The enum value to convert.
    /// </param>
    /// <param name="parameter">
    /// The optional parameter used to help with conversion.
    /// </param>
    /// <returns>
    /// The string representation of the <typeparamref name="TEnum"/> value.
    /// </returns>
    public string ConvertBack(TEnum value, object? parameter = default)
    {
        return value.ToString();
    }
}
