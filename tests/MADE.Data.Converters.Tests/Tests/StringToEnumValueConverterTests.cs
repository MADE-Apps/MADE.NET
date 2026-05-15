using System.Diagnostics.CodeAnalysis;
using MADE.Data.Converters.Exceptions;
using NUnit.Framework;
using Shouldly;

namespace MADE.Data.Converters.Tests.Tests;

[ExcludeFromCodeCoverage]
[TestFixture]
public class StringToEnumValueConverterTests
{
    private enum TestEnum
    {
        None,
        First,
        Second,
        Third,
    }

    public class WhenConvertingStringToEnum
    {
        [TestCase("First", TestEnum.First)]
        [TestCase("second", TestEnum.Second)]
        [TestCase("THIRD", TestEnum.Third)]
        public void ShouldConvertCaseInsensitiveByDefault(string input, TestEnum expected)
        {
            var converter = new StringToEnumValueConverter<TestEnum>();
            converter.Convert(input).ShouldBe(expected);
        }

        [Test]
        public void ShouldThrowForInvalidValue()
        {
            var converter = new StringToEnumValueConverter<TestEnum>();
            Should.Throw<InvalidDataConversionException>(() => converter.Convert("Invalid"));
        }

        [Test]
        public void ShouldRespectCaseSensitiveFlag()
        {
            var converter = new StringToEnumValueConverter<TestEnum> { IgnoreCase = false };
            converter.Convert("First").ShouldBe(TestEnum.First);
            Should.Throw<InvalidDataConversionException>(() => converter.Convert("first"));
        }
    }

    public class WhenConvertingEnumToString
    {
        [Test]
        public void ShouldConvertToString()
        {
            var converter = new StringToEnumValueConverter<TestEnum>();
            converter.ConvertBack(TestEnum.Second).ShouldBe("Second");
        }
    }
}
