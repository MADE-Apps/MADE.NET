namespace MADE.Data.Converters.Tests.Tests
{
    using System.Diagnostics.CodeAnalysis;
    using MADE.Data.Converters.Extensions;
    using NUnit.Framework;
    using Shouldly;

    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class BooleanExtensionsTests
    {
        public class WhenConvertingBooleanToFormattedString
        {
            [Test]
            public void ShouldReturnTrueValueIfTrue()
            {
                // Arrange
                const bool boolean = true;
                const string expected = "Yes";

                // Act
                string formatted = boolean.ToFormattedString(expected, "No");

                // Assert
                formatted.ShouldBe(expected);
            }

            [Test]
            public void ShouldReturnFalseValueIfFalse()
            {
                // Arrange
                const bool boolean = false;
                const string expected = "No";

                // Act
                string formatted = boolean.ToFormattedString("Yes", expected);

                // Assert
                formatted.ShouldBe(expected);
            }
        }

        public class WhenConvertingNullableBooleanToFormattedString
        {
            [Test]
            public void ShouldReturnTrueValueIfTrue()
            {
                // Arrange
                bool? boolean = true;
                const string expected = "Yes";

                // Act
                string formatted = boolean.ToFormattedString(expected, "No", "N/A");

                // Assert
                formatted.ShouldBe(expected);
            }

            [Test]
            public void ShouldReturnFalseValueIfFalse()
            {
                // Arrange
                bool? boolean = false;
                const string expected = "No";

                // Act
                string formatted = boolean.ToFormattedString("Yes", expected, "N/A");

                // Assert
                formatted.ShouldBe(expected);
            }

            [Test]
            public void ShouldReturnNullValueIfNull()
            {
                // Arrange
                bool? boolean = null;
                const string expected = "N/A";

                // Act
                string formatted = boolean.ToFormattedString("Yes", "No", expected);

                // Assert
                formatted.ShouldBe(expected);
            }
        }
    }
}