// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using MADE.Data.Converters.Constants;

namespace MADE.Data.Converters;

/// <summary>
/// Defines a value converter from <see cref="DateTime"/> to a Unix timestamp represented as a <see cref="long"/>.
/// </summary>
public class DateTimeToUnixTimestampValueConverter : IValueConverter<DateTime, long>
{
    /// <summary>
    /// Converts the <paramref name="value">value</paramref> to a Unix timestamp in seconds.
    /// </summary>
    /// <param name="value">
    /// The <see cref="DateTime"/> value to convert.
    /// </param>
    /// <param name="parameter">
    /// The optional parameter used to help with conversion.
    /// </param>
    /// <returns>
    /// The Unix timestamp in seconds since the Unix epoch (1970-01-01 00:00:00 UTC).
    /// </returns>
    public long Convert(DateTime value, object? parameter = default)
    {
        return (long)(value.ToUniversalTime() - DateTimeConstants.UnixEpoch).TotalSeconds;
    }

    /// <summary>
    /// Converts a Unix timestamp in seconds back to a <see cref="DateTime"/> in UTC.
    /// </summary>
    /// <param name="value">
    /// The Unix timestamp in seconds to convert.
    /// </param>
    /// <param name="parameter">
    /// The optional parameter used to help with conversion.
    /// </param>
    /// <returns>
    /// The converted <see cref="DateTime"/> in UTC.
    /// </returns>
    public DateTime ConvertBack(long value, object? parameter = default)
    {
        return DateTimeConstants.UnixEpoch.AddSeconds(value);
    }
}
