using System.Diagnostics.CodeAnalysis;
using MADE.Data.Converters.Extensions;
using NUnit.Framework;
using Shouldly;

namespace MADE.Data.Converters.Tests.Tests;

[ExcludeFromCodeCoverage]
[TestFixture]
public class LengthExtensionsTests
{
    public class WhenConvertingMilesToMeters
    {
        [TestCase(0, 0)]
        [TestCase(1, 1609.344)]
        [TestCase(5, 8046.72)]
        public void ShouldConvertCorrectly(double miles, double expectedMeters)
        {
            miles.ToMeters().ShouldBe(expectedMeters, 0.001);
        }
    }

    public class WhenConvertingMetersToMiles
    {
        [TestCase(0, 0)]
        [TestCase(1609.344, 1)]
        [TestCase(8046.72, 5)]
        public void ShouldConvertCorrectly(double meters, double expectedMiles)
        {
            meters.ToMiles().ShouldBe(expectedMiles, 0.001);
        }
    }

    public class WhenConvertingKilometersToMeters
    {
        [TestCase(0, 0)]
        [TestCase(1, 1000)]
        [TestCase(2.5, 2500)]
        public void ShouldConvertCorrectly(double km, double expectedMeters)
        {
            km.KilometersToMeters().ShouldBe(expectedMeters, 0.001);
        }
    }

    public class WhenConvertingMetersToKilometers
    {
        [TestCase(0, 0)]
        [TestCase(1000, 1)]
        [TestCase(2500, 2.5)]
        public void ShouldConvertCorrectly(double meters, double expectedKm)
        {
            meters.ToKilometers().ShouldBe(expectedKm, 0.001);
        }
    }

    public class WhenConvertingFeetToMeters
    {
        [TestCase(0, 0)]
        [TestCase(1, 0.3048)]
        [TestCase(100, 30.48)]
        public void ShouldConvertCorrectly(double feet, double expectedMeters)
        {
            feet.FeetToMeters().ShouldBe(expectedMeters, 0.001);
        }
    }

    public class WhenConvertingMetersToFeet
    {
        [TestCase(0, 0)]
        [TestCase(0.3048, 1)]
        [TestCase(30.48, 100)]
        public void ShouldConvertCorrectly(double meters, double expectedFeet)
        {
            meters.ToFeet().ShouldBe(expectedFeet, 0.001);
        }
    }

    public class WhenConvertingInchesToMeters
    {
        [TestCase(0, 0)]
        [TestCase(1, 0.0254)]
        [TestCase(100, 2.54)]
        public void ShouldConvertCorrectly(double inches, double expectedMeters)
        {
            inches.InchesToMeters().ShouldBe(expectedMeters, 0.001);
        }
    }

    public class WhenConvertingMetersToInches
    {
        [TestCase(0, 0)]
        [TestCase(0.0254, 1)]
        [TestCase(2.54, 100)]
        public void ShouldConvertCorrectly(double meters, double expectedInches)
        {
            meters.ToInches().ShouldBe(expectedInches, 0.001);
        }
    }

    public class WhenRoundTripping
    {
        [TestCase(3.7)]
        [TestCase(100.0)]
        public void MilesToMetersShouldRoundTrip(double miles)
        {
            miles.ToMeters().ToMiles().ShouldBe(miles, 0.0001);
        }

        [TestCase(5.0)]
        [TestCase(42.195)]
        public void KilometersToMetersShouldRoundTrip(double km)
        {
            km.KilometersToMeters().ToKilometers().ShouldBe(km, 0.0001);
        }

        [TestCase(6.0)]
        [TestCase(5280.0)]
        public void FeetToMetersShouldRoundTrip(double feet)
        {
            feet.FeetToMeters().ToFeet().ShouldBe(feet, 0.0001);
        }

        [TestCase(12.0)]
        [TestCase(72.0)]
        public void InchesToMetersShouldRoundTrip(double inches)
        {
            inches.InchesToMeters().ToInches().ShouldBe(inches, 0.0001);
        }
    }
}
