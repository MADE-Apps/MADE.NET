using System.Diagnostics.CodeAnalysis;
using MADE.Data.Converters.Constants;
using NUnit.Framework;
using Shouldly;

namespace MADE.Data.Converters.Tests.Tests;

[ExcludeFromCodeCoverage]
[TestFixture]
public class DateTimeToUnixTimestampValueConverterTests
{
    public class WhenConvertingToTimestamp
    {
        [Test]
        public void ShouldReturnZeroForUnixEpoch()
        {
            var converter = new DateTimeToUnixTimestampValueConverter();
            converter.Convert(DateTimeConstants.UnixEpoch).ShouldBe(0);
        }

        [Test]
        public void ShouldReturnCorrectTimestamp()
        {
            var converter = new DateTimeToUnixTimestampValueConverter();
            var date = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            converter.Convert(date).ShouldBe(1704067200);
        }
    }

    public class WhenConvertingFromTimestamp
    {
        [Test]
        public void ShouldReturnUnixEpochForZero()
        {
            var converter = new DateTimeToUnixTimestampValueConverter();
            converter.ConvertBack(0).ShouldBe(DateTimeConstants.UnixEpoch);
        }

        [Test]
        public void ShouldRoundTrip()
        {
            var converter = new DateTimeToUnixTimestampValueConverter();
            var date = new DateTime(2024, 6, 15, 12, 30, 0, DateTimeKind.Utc);
            converter.ConvertBack(converter.Convert(date)).ShouldBe(date, TimeSpan.FromSeconds(1));
        }
    }
}
