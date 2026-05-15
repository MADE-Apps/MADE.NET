// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Text;

namespace MADE.Data.Converters.Extensions;

/// <summary>
/// Defines a collection of extensions for <see cref="TimeSpan"/> values.
/// </summary>
public static class TimeSpanExtensions
{
    /// <summary>
    /// Converts a <see cref="TimeSpan"/> to a human-readable string representation.
    /// </summary>
    /// <param name="timeSpan">The time span to convert.</param>
    /// <returns>
    /// A human-readable string such as "2 hours 30 minutes" or "1 day 3 hours".
    /// Returns "0 seconds" for a zero time span.
    /// </returns>
    public static string ToHumanReadableString(this TimeSpan timeSpan)
    {
        if (timeSpan == TimeSpan.Zero)
        {
            return "0 seconds";
        }

        var builder = new StringBuilder();
        bool isNegative = timeSpan < TimeSpan.Zero;
        TimeSpan absolute = isNegative ? timeSpan.Negate() : timeSpan;

        if (isNegative)
        {
            builder.Append('-');
        }

        if (absolute.Days > 0)
        {
            builder.Append($"{absolute.Days} {(absolute.Days == 1 ? "day" : "days")}");
        }

        if (absolute.Hours > 0)
        {
            if (builder.Length > (isNegative ? 1 : 0))
            {
                builder.Append(' ');
            }

            builder.Append($"{absolute.Hours} {(absolute.Hours == 1 ? "hour" : "hours")}");
        }

        if (absolute.Minutes > 0)
        {
            if (builder.Length > (isNegative ? 1 : 0))
            {
                builder.Append(' ');
            }

            builder.Append($"{absolute.Minutes} {(absolute.Minutes == 1 ? "minute" : "minutes")}");
        }

        if (absolute.Seconds > 0)
        {
            if (builder.Length > (isNegative ? 1 : 0))
            {
                builder.Append(' ');
            }

            builder.Append($"{absolute.Seconds} {(absolute.Seconds == 1 ? "second" : "seconds")}");
        }

        return builder.ToString();
    }

    /// <summary>
    /// Gets the total number of whole weeks represented by the <see cref="TimeSpan"/>.
    /// </summary>
    /// <param name="timeSpan">The time span to convert.</param>
    /// <returns>The total number of whole weeks.</returns>
    public static int TotalWeeks(this TimeSpan timeSpan)
    {
        return (int)(timeSpan.TotalDays / 7);
    }
}
