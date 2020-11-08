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
        }

        public class WhenConvertingBack
        {

        }
    }
}