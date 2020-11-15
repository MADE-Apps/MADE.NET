namespace MADE.Data.Converters.Tests.Tests
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;

    using NUnit.Framework;

    using Shouldly;

    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class DateTimeToStringValueConverterTests
    {
        public class WhenConverting
        {
            [Test]
            public void ShouldConvertToDefaultDateTimeStringWithoutParameter()
            {
                // Arrange
                var dateTime = new DateTime(2020, 11, 8, 9, 49, 0);
                string expected = dateTime.ToString(CultureInfo.InvariantCulture);

                var converter = new DateTimeToStringValueConverter();

                // Act
                string converted = converter.Convert(dateTime);

                // Assert
                converted.ShouldBe(expected);
            }

            [TestCase("G")]
            [TestCase("g")]
            public void ShouldConvertToFormattedDateTimeStringWithParameter(string format)
            {
                // Arrange
                var dateTime = new DateTime(2020, 11, 8, 9, 49, 0);
                string expected = dateTime.ToString(format, CultureInfo.InvariantCulture);

                var converter = new DateTimeToStringValueConverter();

                // Act
                string converted = converter.Convert(dateTime, format);

                // Assert
                converted.ShouldBe(expected);
            }
        }

        public class WhenConvertingBack
        {
            [TestCase(null)]
            [TestCase("G")]
            [TestCase("g")]
            public void ShouldConvertToDateTime(string format)
            {
                // Arrange
                var expected = new DateTime(2020, 11, 8, 9, 49, 0);
                string dateTimeString = expected.ToString(format, CultureInfo.InvariantCulture);

                var converter = new DateTimeToStringValueConverter();

                // Act
                DateTime converted = converter.ConvertBack(dateTimeString);

                // Assert
                converted.ShouldBe(expected);
            }

            [Test]
            public void ShouldReturnDateTimeMinValueIfNull()
            {
                // Arrange
                DateTime expected = DateTime.MinValue;
                string dateTimeString = null;

                var converter = new DateTimeToStringValueConverter();

                // Act
                DateTime converted = converter.ConvertBack(dateTimeString);

                // Assert
                converted.ShouldBe(expected);
            }

            [Test]
            public void ShouldReturnDateTimeMinValueIfNotDate()
            {
                // Arrange
                DateTime expected = DateTime.MinValue;
                string dateTimeString = "Hello, World!";

                var converter = new DateTimeToStringValueConverter();

                // Act
                DateTime converted = converter.ConvertBack(dateTimeString);

                // Assert
                converted.ShouldBe(expected);
            }
        }
    }
}