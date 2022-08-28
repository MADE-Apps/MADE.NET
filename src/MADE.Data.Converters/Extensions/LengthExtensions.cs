// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Data.Converters.Extensions
{
    /// <summary>
    /// Defines a collection of extensions for converting length measurements.
    /// </summary>
    public static class LengthExtensions
    {
        /// <summary>
        /// Converts a distance measured in miles to a distance measured in meters.
        /// </summary>
        /// <param name="miles">The miles to convert to meters.</param>
        /// <returns>The meters that represent the miles.</returns>
        public static double ToMeters(this double miles)
        {
            return miles * 1609.344;
        }

        /// <summary>
        /// Converts a distance measured in meters to a distance measured in miles.
        /// </summary>
        /// <param name="meters">The meters to convert to miles.</param>
        /// <returns>The miles that represent the meters.</returns>
        public static double ToMiles(this double meters)
        {
            return meters / 1609.344;
        }
    }
}