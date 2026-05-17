using System.Diagnostics.CodeAnalysis;
using MADE.Data.Converters.Extensions;
using NUnit.Framework;
using Shouldly;

namespace MADE.Data.Converters.Tests.Tests;

[ExcludeFromCodeCoverage]
[TestFixture]
public class MathExtensionsTests
{
    public class WhenConvertingToRadians
    {
        private static readonly object[] ToRadiansTestCases =
        {
            new object[] { 0, 0 }, new object[] { 90, Math.PI / 2 }, new object[] { 180, Math.PI },
            new object[] { 360, Math.PI * 2 },
        };

        [TestCaseSource(nameof(ToRadiansTestCases))]
        public void ShouldConvertToRadians(double degrees, double expected)
        {
            // Act
            double actual = degrees.ToRadians();

            // Assert
            actual.ShouldBe(expected);
        }
    }

    public class WhenConvertingToDegrees
    {
        private static readonly object[] ToDegreesTestCases =
        {
            new object[] { 0, 0 }, new object[] { Math.PI / 2, 90 }, new object[] { Math.PI, 180 },
            new object[] { Math.PI * 2, 360 },
        };

        [TestCaseSource(nameof(ToDegreesTestCases))]
        public void ShouldConvertToDegrees(double radians, double expected)
        {
            double actual = radians.ToDegrees();
            actual.ShouldBe(expected, 0.0001);
        }

        [TestCase(45.0)]
        [TestCase(90.0)]
        [TestCase(270.0)]
        public void ShouldRoundTripWithToRadians(double degrees)
        {
            degrees.ToRadians().ToDegrees().ShouldBe(degrees, 0.0001);
        }
    }
}
