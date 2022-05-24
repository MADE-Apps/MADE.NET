namespace MADE.Data.Converters.Tests.Tests
{
    using System.Diagnostics.CodeAnalysis;
    using MADE.Data.Converters.Exceptions;
    using NUnit.Framework;

    using Shouldly;

    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class BooleanToStringValueConverterTests
    {
        public class WhenConverting
        {
            [Test]
            public void ShouldConvertToTrueValueWhenTrue()
            {
                // Arrange
                const bool boolean = true;
                const string expected = "Yes";

                var converter = new BooleanToStringValueConverter {TrueValue = expected, FalseValue = "No"};

                // Act
                string converted = converter.Convert(boolean);

                // Assert
                converted.ShouldBe(expected);
            }

            [Test]
            public void ShouldConvertToFalseValueWhenFalse()
            {
                const bool boolean = false;
                const string expected = "No";

                var converter = new BooleanToStringValueConverter {TrueValue = "Yes", FalseValue = expected};

                // Act
                string converted = converter.Convert(boolean);

                // Assert
                converted.ShouldBe(expected);
            }
        }

        public class WhenConvertingBack
        {
            [Test]
            public void ShouldConvertToTrueWhenTrueValue()
            {
                // Arrange
                const string booleanString = "Yes";
                const bool expected = true;

                var converter = new BooleanToStringValueConverter {TrueValue = booleanString, FalseValue = "No"};

                // Act
                bool converted = converter.ConvertBack(booleanString);

                // Assert
                converted.ShouldBe(expected);
            }

            [Test]
            public void ShouldConvertToFalseWhenFalseValue()
            {
                // Arrange
                const string booleanString = "No";
                const bool expected = false;

                var converter = new BooleanToStringValueConverter {TrueValue = "Yes", FalseValue = booleanString};

                // Act
                bool converted = converter.ConvertBack(booleanString);

                // Assert
                converted.ShouldBe(expected);
            }

            [Test]
            public void ShouldThrowInvalidDataConversionExceptionIfNotTrueOrFalseValue()
            {
                // Arrange
                const string booleanString = "Not valid";

                var converter = new BooleanToStringValueConverter {TrueValue = "Yes", FalseValue = "No"};

                // Act & Assert
                Should.Throw<InvalidDataConversionException>(() => converter.ConvertBack(booleanString));
            }
        }
    }
}