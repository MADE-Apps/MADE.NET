// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Data.Converters.Extensions;

/// <summary>
/// Defines a collection of extensions for converting byte values to human-readable file size representations.
/// </summary>
public static class FileSizeExtensions
{
    private static readonly string[] SizeUnits = { "B", "KB", "MB", "GB", "TB", "PB", "EB" };

    /// <summary>
    /// Converts a byte count to a human-readable file size string using binary units (1 KB = 1024 bytes).
    /// </summary>
    /// <param name="bytes">The byte count to convert.</param>
    /// <param name="decimalPlaces">The number of decimal places to display. Default is 2.</param>
    /// <returns>A human-readable file size string such as "1.50 MB" or "256 B".</returns>
    public static string ToHumanReadableFileSize(this long bytes, int decimalPlaces = 2)
    {
        if (bytes < 0)
        {
            return $"-{(-bytes).ToHumanReadableFileSize(decimalPlaces)}";
        }

        if (bytes == 0)
        {
            return "0 B";
        }

        int unitIndex = (int)Math.Floor(Math.Log(bytes, 1024));
        unitIndex = Math.Min(unitIndex, SizeUnits.Length - 1);

        double size = bytes / Math.Pow(1024, unitIndex);

        return $"{size.ToString($"F{decimalPlaces}")} {SizeUnits[unitIndex]}";
    }

    /// <summary>
    /// Converts a byte count to a human-readable file size string using binary units (1 KB = 1024 bytes).
    /// </summary>
    /// <param name="bytes">The byte count to convert.</param>
    /// <param name="decimalPlaces">The number of decimal places to display. Default is 2.</param>
    /// <returns>A human-readable file size string such as "1.50 MB" or "256 B".</returns>
    public static string ToHumanReadableFileSize(this double bytes, int decimalPlaces = 2)
    {
        return ((long)bytes).ToHumanReadableFileSize(decimalPlaces);
    }
}
