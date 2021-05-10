namespace MADE.Data.Validation.Tests.Tests
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using MADE.Data.Validation.Extensions;
    using NUnit.Framework;
    using Shouldly;

    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class DateTimeExtensionsTests
    {
        public class WhenDeterminingWhetherDateIsInRange
        {
            [TestCase("07/01/2021", "01/01/2021", "12/31/2021")]
            [TestCase("07/01/2021 12:00:00", "07/01/2021 09:00:00", "07/01/2021 15:00:00")]
            public void ShouldBeTrueIfInRange(string dateVal, string fromVal, string toVal)
            {
                // Arrange
                var date = DateTime.Parse(dateVal, new CultureInfo("en-US"));
                var from = DateTime.Parse(fromVal, new CultureInfo("en-US"));
                var to = DateTime.Parse(toVal, new CultureInfo("en-US"));

                // Act
                bool isInRange = date.IsInRange(from, to);

                // Assert
                isInRange.ShouldBeTrue();
            }

            [TestCase("07/01/2021", "01/01/2020", "12/31/2020")]
            [TestCase("07/01/2021 00:00:00", "07/01/2021 09:00:00", "07/01/2021 15:00:00")]
            public void ShouldBeFalseIfNotInRange(string dateVal, string fromVal, string toVal)
            {
                // Arrange
                var date = DateTime.Parse(dateVal, new CultureInfo("en-US"));
                var from = DateTime.Parse(fromVal, new CultureInfo("en-US"));
                var to = DateTime.Parse(toVal, new CultureInfo("en-US"));

                // Act
                bool isInRange = date.IsInRange(from, to);

                // Assert
                isInRange.ShouldBeFalse();
            }
        }

        public class WhenDeterminingWeekdays
        {
            [TestCase("05/10/2021")]
            [TestCase("05/11/2021")]
            [TestCase("05/12/2021")]
            [TestCase("05/13/2021")]
            [TestCase("05/14/2021")]
            public void ShouldBeTrueIfWeekday(string dateVal)
            {
                // Arrange
                var date = DateTime.Parse(dateVal, new CultureInfo("en-US"));

                // Act
                bool isWeekday = date.IsWeekday();

                // Assert
                isWeekday.ShouldBeTrue();
            }

            [TestCase("05/15/2021")]
            [TestCase("05/16/2021")]
            public void ShouldBeFalseIfNotWeekday(string dateVal)
            {
                // Arrange
                var date = DateTime.Parse(dateVal, new CultureInfo("en-US"));

                // Act
                bool isWeekday = date.IsWeekday();

                // Assert
                isWeekday.ShouldBeFalse();
            }
        }

        public class WhenDeterminingWeekends
        {
            [TestCase("05/15/2021")]
            [TestCase("05/16/2021")]
            public void ShouldBeTrueIfWeekend(string dateVal)
            {
                // Arrange
                var date = DateTime.Parse(dateVal, new CultureInfo("en-US"));

                // Act
                bool isWeekend = date.IsWeekend();

                // Assert
                isWeekend.ShouldBeTrue();
            }

            [TestCase("05/10/2021")]
            [TestCase("05/11/2021")]
            [TestCase("05/12/2021")]
            [TestCase("05/13/2021")]
            [TestCase("05/14/2021")]
            public void ShouldBeFalseIfNotWeekend(string dateVal)
            {
                // Arrange
                var date = DateTime.Parse(dateVal, new CultureInfo("en-US"));

                // Act
                bool isWeekend = date.IsWeekend();

                // Assert
                isWeekend.ShouldBeFalse();
            }
        }
    }
}