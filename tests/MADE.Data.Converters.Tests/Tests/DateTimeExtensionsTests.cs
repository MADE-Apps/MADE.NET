namespace MADE.Data.Converters.Tests.Tests
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using MADE.Data.Converters.Constants;
    using MADE.Data.Converters.Extensions;
    using NUnit.Framework;
    using Shouldly;

    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class DateTimeExtensionsTests
    {
        public class WhenConvertingDateToDaySuffix
        {
            private static object[] TestCases =
            {
                new object[] {new DateTime(2022, 3, 1), "st"}, new object[] {new DateTime(2022, 3, 2), "nd"},
                new object[] {new DateTime(2022, 3, 3), "rd"}, new object[] {new DateTime(2022, 3, 4), "th"},
                new object[] {new DateTime(2022, 3, 5), "th"}, new object[] {new DateTime(2022, 3, 6), "th"},
                new object[] {new DateTime(2022, 3, 7), "th"}, new object[] {new DateTime(2022, 3, 8), "th"},
                new object[] {new DateTime(2022, 3, 9), "th"}, new object[] {new DateTime(2022, 3, 10), "th"},
                new object[] {new DateTime(2022, 3, 11), "th"}, new object[] {new DateTime(2022, 3, 12), "th"},
                new object[] {new DateTime(2022, 3, 13), "th"}, new object[] {new DateTime(2022, 3, 14), "th"},
                new object[] {new DateTime(2022, 3, 15), "th"}, new object[] {new DateTime(2022, 3, 16), "th"},
                new object[] {new DateTime(2022, 3, 17), "th"}, new object[] {new DateTime(2022, 3, 18), "th"},
                new object[] {new DateTime(2022, 3, 19), "th"}, new object[] {new DateTime(2022, 3, 20), "th"},
                new object[] {new DateTime(2022, 3, 21), "st"}, new object[] {new DateTime(2022, 3, 22), "nd"},
                new object[] {new DateTime(2022, 3, 23), "rd"}, new object[] {new DateTime(2022, 3, 24), "th"},
                new object[] {new DateTime(2022, 3, 25), "th"}, new object[] {new DateTime(2022, 3, 26), "th"},
                new object[] {new DateTime(2022, 3, 27), "th"}, new object[] {new DateTime(2022, 3, 28), "th"},
                new object[] {new DateTime(2022, 3, 29), "th"}, new object[] {new DateTime(2022, 3, 30), "th"},
                new object[] {new DateTime(2022, 3, 31), "st"},
            };

            [TestCaseSource(nameof(TestCases))]
            public void ShouldReturnCorrectDaySuffix(DateTime dateTime, string expected)
            {
                // Act
                var result = dateTime.ToDaySuffix();

                // Assert
                result.ShouldBe(expected);
            }
        }

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

        public class WhenRoundingToNearestHour
        {
            [Test]
            public void ShouldRoundUpIfAfterHalfHour()
            {
                // Arrange
                int expectedHour = 10;
                var dateTime = new DateTime(2021, 5, 12, 9, 31, 0);

                // Act
                DateTime nearestHour = dateTime.ToNearestHour();

                // Assert
                nearestHour.Hour.ShouldBe(expectedHour);
            }

            [Test]
            public void ShouldRoundDownIfBeforeHalfHour()
            {
                // Arrange
                int expectedHour = 9;
                var dateTime = new DateTime(2021, 5, 12, 9, 29, 0);

                // Act
                DateTime nearestHour = dateTime.ToNearestHour();

                // Assert
                nearestHour.Hour.ShouldBe(expectedHour);
            }
        }

        public class WhenGettingStartOfDay
        {
            [Test]
            public void ShouldReturnDateTimeAtMidnight()
            {
                // Arrange
                var expectedTime = new TimeSpan(0, 0, 0);
                var dateTime = new DateTime(2021, 5, 12, 9, 29, 0);

                // Act
                DateTime startOfDay = dateTime.StartOfDay();

                // Assert
                startOfDay.Date.ShouldBe(dateTime.Date);
                startOfDay.TimeOfDay.ShouldBe(expectedTime);
            }
        }

        public class WhenGettingEndOfDay
        {
            [Test]
            public void ShouldReturnDateTimeAtJustBeforeMidnight()
            {
                // Arrange
                TimeSpan expectedTime = DateTimeConstants.EndOfDayTime;
                var dateTime = new DateTime(2021, 5, 12, 9, 29, 0);

                // Act
                DateTime endOfDay = dateTime.EndOfDay();

                // Assert
                endOfDay.Date.ShouldBe(dateTime.Date);
                endOfDay.TimeOfDay.ShouldBe(expectedTime);
            }
        }

        public class WhenGettingStartOfWeek
        {
            [Test]
            public void ShouldReturnFirstDayOfWeekAtMidnight()
            {
                // Arrange
                var expectedDate = new DateTime(2021, 5, 9);
                var expectedTime = new TimeSpan(0, 0, 0);
                var dateTime = new DateTime(2021, 05, 12, 9, 29, 0);

                // Act
                DateTime startOfDay = dateTime.StartOfWeek();

                // Assert
                startOfDay.Date.ShouldBe(expectedDate.Date);
                startOfDay.TimeOfDay.ShouldBe(expectedTime);
            }
        }

        public class WhenGettingEndOfWeek
        {
            [Test]
            public void ShouldReturnLastDayOfWeekAtJustBeforeMidnight()
            {
                // Arrange
                var expectedDate = new DateTime(2021, 5, 16);
                TimeSpan expectedTime = DateTimeConstants.EndOfDayTime;
                var dateTime = new DateTime(2021, 05, 12, 9, 29, 0);

                // Act
                DateTime endOfDay = dateTime.EndOfWeek();

                // Assert
                endOfDay.Date.ShouldBe(expectedDate.Date);
                endOfDay.TimeOfDay.ShouldBe(expectedTime);
            }
        }

        public class WhenGettingStartOfMonth
        {
            [Test]
            public void ShouldReturnFirstDayOfMonthAtMidnight()
            {
                // Arrange
                var expectedDate = new DateTime(2021, 5, 1);
                var expectedTime = new TimeSpan(0, 0, 0);
                var dateTime = new DateTime(2021, 05, 12, 9, 29, 0);

                // Act
                DateTime startOfDay = dateTime.StartOfMonth();

                // Assert
                startOfDay.Date.ShouldBe(expectedDate.Date);
                startOfDay.TimeOfDay.ShouldBe(expectedTime);
            }
        }

        public class WhenGettingEndOfMonth
        {
            [Test]
            public void ShouldReturnLastDayOfMonthAtJustBeforeMidnight()
            {
                // Arrange
                var expectedDate = new DateTime(2021, 5, 31);
                TimeSpan expectedTime = DateTimeConstants.EndOfDayTime;
                var dateTime = new DateTime(2021, 05, 12, 9, 29, 0);

                // Act
                DateTime endOfDay = dateTime.EndOfMonth();

                // Assert
                endOfDay.Date.ShouldBe(expectedDate.Date);
                endOfDay.TimeOfDay.ShouldBe(expectedTime);
            }
        }

        public class WhenGettingStartOfYear
        {
            [Test]
            public void ShouldReturnFirstDayOfYearAtMidnight()
            {
                // Arrange
                var expectedDate = new DateTime(2021, 1, 1);
                var expectedTime = new TimeSpan(0, 0, 0);
                var dateTime = new DateTime(2021, 05, 12, 9, 29, 0);

                // Act
                DateTime startOfDay = dateTime.StartOfYear();

                // Assert
                startOfDay.Date.ShouldBe(expectedDate.Date);
                startOfDay.TimeOfDay.ShouldBe(expectedTime);
            }
        }

        public class WhenGettingEndOfYear
        {
            [Test]
            public void ShouldReturnLastDayOfYearAtJustBeforeMidnight()
            {
                // Arrange
                var expectedDate = new DateTime(2021, 12, 31);
                TimeSpan expectedTime = DateTimeConstants.EndOfDayTime;
                var dateTime = new DateTime(2021, 05, 12, 9, 29, 0);

                // Act
                DateTime endOfDay = dateTime.EndOfYear();

                // Assert
                endOfDay.Date.ShouldBe(expectedDate.Date);
                endOfDay.TimeOfDay.ShouldBe(expectedTime);
            }
        }
    }
}