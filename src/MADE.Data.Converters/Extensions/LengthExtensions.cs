// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Data.Converters.Extensions;

/// <summary>
/// Defines a collection of extensions for converting length measurements.
/// </summary>
public static class LengthExtensions
{
    private const double MetersPerMile = 1609.344;
    private const double MilesPerMeter = 1.0 / MetersPerMile;
    private const double MetersPerKilometer = 1000.0;
    private const double KilometersPerMeter = 1.0 / MetersPerKilometer;
    private const double MetersPerFoot = 0.3048;
    private const double FeetPerMeter = 1.0 / MetersPerFoot;
    private const double MetersPerInch = 0.0254;
    private const double InchesPerMeter = 1.0 / MetersPerInch;

    /// <summary>
    /// Converts a distance measured in miles to a distance measured in meters.
    /// </summary>
    /// <param name="miles">The miles to convert to meters.</param>
    /// <returns>The meters that represent the miles.</returns>
    public static double ToMeters(this double miles)
    {
        return miles * MetersPerMile;
    }

    /// <summary>
    /// Converts a distance measured in meters to a distance measured in miles.
    /// </summary>
    /// <param name="meters">The meters to convert to miles.</param>
    /// <returns>The miles that represent the meters.</returns>
    public static double ToMiles(this double meters)
    {
        return meters * MilesPerMeter;
    }

    /// <summary>
    /// Converts a distance measured in kilometers to a distance measured in meters.
    /// </summary>
    /// <param name="kilometers">The kilometers to convert to meters.</param>
    /// <returns>The meters that represent the kilometers.</returns>
    public static double KilometersToMeters(this double kilometers)
    {
        return kilometers * MetersPerKilometer;
    }

    /// <summary>
    /// Converts a distance measured in meters to a distance measured in kilometers.
    /// </summary>
    /// <param name="meters">The meters to convert to kilometers.</param>
    /// <returns>The kilometers that represent the meters.</returns>
    public static double ToKilometers(this double meters)
    {
        return meters * KilometersPerMeter;
    }

    /// <summary>
    /// Converts a distance measured in feet to a distance measured in meters.
    /// </summary>
    /// <param name="feet">The feet to convert to meters.</param>
    /// <returns>The meters that represent the feet.</returns>
    public static double FeetToMeters(this double feet)
    {
        return feet * MetersPerFoot;
    }

    /// <summary>
    /// Converts a distance measured in meters to a distance measured in feet.
    /// </summary>
    /// <param name="meters">The meters to convert to feet.</param>
    /// <returns>The feet that represent the meters.</returns>
    public static double ToFeet(this double meters)
    {
        return meters * FeetPerMeter;
    }

    /// <summary>
    /// Converts a distance measured in inches to a distance measured in meters.
    /// </summary>
    /// <param name="inches">The inches to convert to meters.</param>
    /// <returns>The meters that represent the inches.</returns>
    public static double InchesToMeters(this double inches)
    {
        return inches * MetersPerInch;
    }

    /// <summary>
    /// Converts a distance measured in meters to a distance measured in inches.
    /// </summary>
    /// <param name="meters">The meters to convert to inches.</param>
    /// <returns>The inches that represent the meters.</returns>
    public static double ToInches(this double meters)
    {
        return meters * InchesPerMeter;
    }
}
