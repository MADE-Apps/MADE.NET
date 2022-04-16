namespace MADE.Data.Converters.Tests.Tests
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using MADE.Data.Converters.Extensions;
    using NUnit.Framework;
    using Shouldly;

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
    }
}