namespace MADE.Data.Validation.Tests.Tests
{
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using MADE.Data.Validation.Extensions;
    using NUnit.Framework;
    using Shouldly;

    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class StringExtensionsTests
    {
        public class WhenValidatingIfStringIsLike
        {
            [TestCase("*", "abc", true)]
            [TestCase("a*", "abc", true)]
            [TestCase("a?c", "abc", true)]
            [TestCase("[a-z][a-z][a-z]", "abc", true)]
            [TestCase("###", "123", true)]
            [TestCase("###", "abc", false)]
            [TestCase("*###", "123abc", false)]
            [TestCase("[a-z][a-z][a-z]", "ABC", false)]
            [TestCase("a?c", "aba", false)]
            public void ShouldMatchPattern(string pattern, string input, bool expected)
            {
                // Act
                var actual = input.IsLike(pattern);

                // Assert
                actual.ShouldBe(expected);
            }
        }

        public class WhenCheckingIfStringContainsValue
        {
            [TestCase("Hello, World", "ello", CompareOptions.None)]
            [TestCase("Hello, World", "hello", CompareOptions.IgnoreCase)]
            public void ShouldReturnTrueIfDoesContain(string phrase, string value, CompareOptions compare)
            {
                // Act
                bool contains = phrase.Contains(value, compare);

                // Assert
                contains.ShouldBeTrue();
            }

            [TestCase("Hello, World", "Hey", CompareOptions.None)]
            [TestCase("Hello, World", "hello", CompareOptions.None)]
            public void ShouldReturnFalseIfDoesNotContain(string phrase, string value, CompareOptions compare)
            {
                // Act
                bool contains = phrase.Contains(value, compare);

                // Assert
                contains.ShouldBeFalse();
            }
        }

        public class WhenCheckingIfStringIsInt
        {
            [TestCase("10")]
            [TestCase("-10")]
            public void ShouldReturnTrueIfInt(string value)
            {
                // Act
                bool actual = value.IsInt();

                // Assert
                actual.ShouldBeTrue();
            }

            [TestCase(null)]
            [TestCase("")]
            [TestCase("Hello, World")]
            public void ShouldReturnFalseIfNotInt(string value)
            {
                // Act
                bool actual = value.IsInt();

                // Assert
                actual.ShouldBeFalse();
            }
        }

        public class WhenCheckingIfStringIsDouble
        {
            [TestCase("10.5")]
            [TestCase("-10.5")]
            public void ShouldReturnTrueIfDouble(string value)
            {
                // Act
                bool actual = value.IsDouble();

                // Assert
                actual.ShouldBeTrue();
            }

            [TestCase(null)]
            [TestCase("")]
            [TestCase("Hello, World")]
            public void ShouldReturnFalseIfNotDouble(string value)
            {
                // Act
                bool actual = value.IsDouble();

                // Assert
                actual.ShouldBeFalse();
            }
        }

        public class WhenCheckingIfStringIsBoolean
        {
            [TestCase("True")]
            [TestCase("true")]
            [TestCase("False")]
            [TestCase("false")]
            public void ShouldReturnTrueIfBoolean(string value)
            {
                // Act
                bool actual = value.IsBoolean();

                // Assert
                actual.ShouldBeTrue();
            }

            [TestCase(null)]
            [TestCase("")]
            [TestCase("Hello, World")]
            public void ShouldReturnFalseIfNotBoolean(string value)
            {
                // Act
                bool actual = value.IsBoolean();

                // Assert
                actual.ShouldBeFalse();
            }
        }

        public class WhenCheckingIfStringIsFloat
        {
            [TestCase("10.5")]
            [TestCase("-10.5")]
            public void ShouldReturnTrueIfFloat(string value)
            {
                // Act
                bool actual = value.IsFloat();

                // Assert
                actual.ShouldBeTrue();
            }

            [TestCase(null)]
            [TestCase("")]
            [TestCase("Hello, World")]
            public void ShouldReturnFalseIfNotFloat(string value)
            {
                // Act
                bool actual = value.IsFloat();

                // Assert
                actual.ShouldBeFalse();
            }
        }
    }
}