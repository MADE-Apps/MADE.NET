using System.Diagnostics.CodeAnalysis;
using MADE.Data.Converters.Extensions;
using NUnit.Framework;
using Shouldly;

namespace MADE.Data.Converters.Tests.Tests;

[ExcludeFromCodeCoverage]
[TestFixture]
public class FileSizeExtensionsTests
{
    public class WhenConvertingToHumanReadableFileSize
    {
        [Test]
        public void ShouldReturnZeroBytesForZero()
        {
            0L.ToHumanReadableFileSize().ShouldBe("0 B");
        }

        [TestCase(512L, "512.00 B")]
        [TestCase(1024L, "1.00 KB")]
        [TestCase(1_048_576L, "1.00 MB")]
        [TestCase(1_073_741_824L, "1.00 GB")]
        [TestCase(1_572_864L, "1.50 MB")]
        public void ShouldConvertToCorrectUnit(long bytes, string expected)
        {
            bytes.ToHumanReadableFileSize().ShouldBe(expected);
        }

        [Test]
        public void ShouldRespectDecimalPlaces()
        {
            1_572_864L.ToHumanReadableFileSize(0).ShouldBe("2 MB");
            1_572_864L.ToHumanReadableFileSize(1).ShouldBe("1.5 MB");
        }

        [Test]
        public void ShouldHandleNegativeValues()
        {
            (-1024L).ToHumanReadableFileSize().ShouldBe("-1.00 KB");
        }

        [Test]
        public void ShouldWorkWithDoubleOverload()
        {
            1048576.0.ToHumanReadableFileSize().ShouldBe("1.00 MB");
        }
    }
}
