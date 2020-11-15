namespace MADE.Data.Validation.Tests.Tests
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using NUnit.Framework;
    using Shouldly;
    using Validators;

    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class AlphaValidatorTests
    {
        public class WhenValidating
        {
            [Test]
            public void ShouldBeDirtyOnceValidated()
            {
                // Arrange
                string value = "Test";
                var validator = new AlphaValidator();

                // Act
                validator.Validate(value);

                // Assert
                validator.IsDirty.ShouldBe(true);
            }

            [Test]
            public void ShouldBeValidIfContainsOnlyAlphaCharacters()
            {
                // Arrange
                string value = "Test";
                var validator = new AlphaValidator();

                // Act
                validator.Validate(value);

                // Assert
                validator.IsInvalid.ShouldBe(false);
            }

            [TestCase("Test1")]
            [TestCase("Test!")]
            public void ShouldBeInvalidIfContainsNonAlphaCharacters(string value)
            {
                // Arrange
                var validator = new AlphaValidator();

                // Act
                validator.Validate(value);

                // Assert
                validator.IsInvalid.ShouldBe(true);
            }
        }
    }
}