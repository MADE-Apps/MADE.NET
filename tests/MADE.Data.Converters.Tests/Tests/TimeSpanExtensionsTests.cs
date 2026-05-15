using System.Diagnostics.CodeAnalysis;
using MADE.Data.Converters.Extensions;
using NUnit.Framework;
using Shouldly;

namespace MADE.Data.Converters.Tests.Tests;

[ExcludeFromCodeCoverage]
[TestFixture]
public class TimeSpanExtensionsTests
{
    public class WhenConvertingToHumanReadableString
    {
        [Test]
        public void ShouldReturnZeroSecondsForZero()
        {
            TimeSpan.Zero.ToHumanReadableString().ShouldBe("0 seconds");
        }

        [Test]
        public void ShouldHandleSingularUnits()
        {
            new TimeSpan(1, 1, 1, 1).ToHumanReadableString().ShouldBe("1 day 1 hour 1 minute 1 second");
        }

        [Test]
        public void ShouldHandlePluralUnits()
        {
            new TimeSpan(2, 3, 30, 45).ToHumanReadableString().ShouldBe("2 days 3 hours 30 minutes 45 seconds");
        }

        [Test]
        public void ShouldOmitZeroComponents()
        {
            new TimeSpan(0, 2, 0, 0).ToHumanReadableString().ShouldBe("2 hours");
        }

        [Test]
        public void ShouldHandleNegativeTimeSpans()
        {
            new TimeSpan(-1, -2, 0, 0).ToHumanReadableString().ShouldStartWith("-");
        }
    }

    public class WhenGettingTotalWeeks
    {
        [TestCase(0, 0)]
        [TestCase(7, 1)]
        [TestCase(14, 2)]
        [TestCase(10, 1)]
        public void ShouldReturnWholeWeeks(int days, int expectedWeeks)
        {
            TimeSpan.FromDays(days).TotalWeeks().ShouldBe(expectedWeeks);
        }
    }
}
