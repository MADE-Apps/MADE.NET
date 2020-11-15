namespace MADE.Data.Converters.Tests.Tests
{
    using System.Diagnostics.CodeAnalysis;
    using Extensions;
    using NUnit.Framework;
    using Shouldly;

    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class StringExtensionsTests
    {
        public class WhenConvertingToTitleCase
        {
            [TestCase("", "")]
            [TestCase("HELLO, WORLD", "Hello, World")]
            [TestCase("HeLlO, WoRlD", "Hello, World")]
            [TestCase("hello, world", "Hello, World")]
            public void ShouldConvert(string value, string expected)
            {
                // Act
                string actual = value.ToTitleCase();

                // Assert
                actual.ShouldBe(expected);
            }
        }

        public class WhenConvertingToDefaultCase
        {
            [TestCase("", "")]
            [TestCase("HELLO, WORLD", "Hello, world")]
            [TestCase("HeLlO, WoRlD", "Hello, world")]
            [TestCase("hello, world", "Hello, world")]
            public void ShouldConvert(string value, string expected)
            {
                // Act
                string actual = value.ToDefaultCase();

                // Assert
                actual.ShouldBe(expected);
            }
        }

        public class WhenConvertingToInt
        {
            [TestCase(null, 0)]
            [TestCase("", 0)]
            [TestCase("10", 10)]
            [TestCase("-10", -10)]
            public void ShouldConvert(string value, int expected)
            {
                // Act
                int actual = value.ToInt();

                // Assert
                actual.ShouldBe(expected);
            }
        }

        public class WhenConvertingToNullableInt
        {
            [TestCase(null, null)]
            [TestCase("", null)]
            [TestCase("10", 10)]
            [TestCase("-10", -10)]
            public void ShouldConvert(string value, int? expected)
            {
                // Act
                int? actual = value.ToNullableInt();

                // Assert
                actual.ShouldBe(expected);
            }
        }

        public class WhenConvertingToBoolean
        {
            [TestCase(null, false)]
            [TestCase("", false)]
            [TestCase("True", true)]
            [TestCase("False", false)]
            [TestCase("true", true)]
            [TestCase("false", false)]
            public void ShouldConvert(string value, bool expected)
            {
                // Act
                bool actual = value.ToBoolean();

                // Assert
                actual.ShouldBe(expected);
            }
        }

        public class WhenConvertingToFloat
        {
            [TestCase(null, 0f)]
            [TestCase("", 0f)]
            [TestCase("10.5", 10.5f)]
            [TestCase("-10.5", -10.5f)]
            public void ShouldConvert(string value, float expected)
            {
                // Act
                float actual = value.ToFloat();

                // Assert
                actual.ShouldBe(expected);
            }
        }

        public class WhenConvertingToNullableFloat
        {
            [TestCase(null, null)]
            [TestCase("", null)]
            [TestCase("10.5", 10.5f)]
            [TestCase("-10.5", -10.5f)]
            public void ShouldConvert(string value, float? expected)
            {
                // Act
                float? actual = value.ToNullableFloat();

                // Assert
                actual.ShouldBe(expected);
            }
        }

        public class WhenConvertingToDouble
        {
            [TestCase(null, 0d)]
            [TestCase("", 0d)]
            [TestCase("10.5", 10.5d)]
            [TestCase("-10.5", -10.5d)]
            public void ShouldConvert(string value, double expected)
            {
                // Act
                double actual = value.ToDouble();

                // Assert
                actual.ShouldBe(expected);
            }
        }

        public class WhenConvertingToNullableDouble
        {
            [TestCase(null, null)]
            [TestCase("", null)]
            [TestCase("10.5", 10.5d)]
            [TestCase("-10.5", -10.5d)]
            public void ShouldConvert(string value, double? expected)
            {
                // Act
                double? actual = value.ToNullableDouble();

                // Assert
                actual.ShouldBe(expected);
            }
        }
    }
}