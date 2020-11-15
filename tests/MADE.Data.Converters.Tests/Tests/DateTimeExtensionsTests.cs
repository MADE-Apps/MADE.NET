namespace MADE.Data.Converters.Tests.Tests
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using MADE.Data.Converters.Extensions;
    using NUnit.Framework;
    using Shouldly;

    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class DateTimeExtensionsTests
    {
        public class WhenSettingTime
        {
            [Test]
            public void ShouldSetNullableDateTimeIfProvidedTimeAsTimeSpan()
            {
                // Arrange
                var expectedTime = new TimeSpan(9, 10, 30);
                DateTime? dateTime = new DateTime(2020, 11, 15);

                // Act
                dateTime = dateTime.SetTime(expectedTime);

                // Assert
                dateTime.Value.TimeOfDay.ShouldBe(expectedTime);
            }

            [Test]
            public void ShouldSetDateTimeIfProvidedTimeAsTimeSpan()
            {
                // Arrange
                var expectedTime = new TimeSpan(9, 10, 30);
                var dateTime = new DateTime(2020, 11, 15);

                // Act
                dateTime = dateTime.SetTime(expectedTime);

                // Assert
                dateTime.TimeOfDay.ShouldBe(expectedTime);
            }

            [Test]
            public void ShouldSetNullableDateTimeIfProvidedTimeAsHoursAndMinutes()
            {
                // Arrange
                var expectedTime = new TimeSpan(9, 10, 0);
                DateTime? dateTime = new DateTime(2020, 11, 15);

                // Act
                dateTime = dateTime.SetTime(expectedTime.Hours, expectedTime.Minutes);

                // Assert
                dateTime.Value.TimeOfDay.ShouldBe(expectedTime);
            }

            [Test]
            public void ShouldSetDateTimeIfProvidedTimeAsHoursAndMinutes()
            {
                // Arrange
                var expectedTime = new TimeSpan(9, 10, 0);
                var dateTime = new DateTime(2020, 11, 15);

                // Act
                dateTime = dateTime.SetTime(expectedTime.Hours, expectedTime.Minutes);

                // Assert
                dateTime.TimeOfDay.ShouldBe(expectedTime);
            }

            [Test]
            public void ShouldSetNullableDateTimeIfProvidedTimeAsHoursMinutesAndSeconds()
            {
                // Arrange
                var expectedTime = new TimeSpan(9, 10, 30);
                DateTime? dateTime = new DateTime(2020, 11, 15);

                // Act
                dateTime = dateTime.SetTime(expectedTime.Hours, expectedTime.Minutes, expectedTime.Seconds);

                // Assert
                dateTime.Value.TimeOfDay.ShouldBe(expectedTime);
            }

            [Test]
            public void ShouldSetDateTimeIfProvidedTimeAsHoursMinutesAndSeconds()
            {
                // Arrange
                var expectedTime = new TimeSpan(9, 10, 30);
                var dateTime = new DateTime(2020, 11, 15);

                // Act
                dateTime = dateTime.SetTime(expectedTime.Hours, expectedTime.Minutes, expectedTime.Seconds);

                // Assert
                dateTime.TimeOfDay.ShouldBe(expectedTime);
            }

            [Test]
            public void ShouldSetNullableDateTimeIfProvidedTimeAsHoursMinutesSecondsAndMilliseconds()
            {
                // Arrange
                var expectedTime = new TimeSpan(0, 9, 10, 30, 10);
                DateTime? dateTime = new DateTime(2020, 11, 15);

                // Act
                dateTime = dateTime.SetTime(
                    expectedTime.Hours,
                    expectedTime.Minutes,
                    expectedTime.Seconds,
                    expectedTime.Milliseconds);

                // Assert
                dateTime.Value.TimeOfDay.ShouldBe(expectedTime);
            }

            [Test]
            public void ShouldSetDateTimeIfProvidedTimeAsHoursMinutesSecondsAndMilliseconds()
            {
                // Arrange
                var expectedTime = new TimeSpan(0, 9, 10, 30, 10);
                var dateTime = new DateTime(2020, 11, 15);

                // Act
                dateTime = dateTime.SetTime(
                    expectedTime.Hours,
                    expectedTime.Minutes,
                    expectedTime.Seconds,
                    expectedTime.Milliseconds);

                // Assert
                dateTime.TimeOfDay.ShouldBe(expectedTime);
            }
        }
    }
}