namespace MADE.Data.Validation.Tests.Tests
{
    using System.Diagnostics.CodeAnalysis;
    using MADE.Data.Validation.Validators;
    using NUnit.Framework;
    using Shouldly;

    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class AlphaNumericValidatorTests
    {
        public class WhenValidating
        {
            [Test]
            public void ShouldBeDirtyOnceValidated()
            {
                // Arrange
                string value = "Test";
                var validator = new AlphaNumericValidator();

                // Act
                validator.Validate(value);

                // Assert
                validator.IsDirty.ShouldBe(true);
            }

            [TestCase("Test")]
            [TestCase("Test1")]
            public void ShouldBeValidIfContainsAlphaNumericCharacters(string value)
            {
                // Arrange
                var validator = new AlphaNumericValidator();

                // Act
                validator.Validate(value);

                // Assert
                validator.IsInvalid.ShouldBe(false);
            }

            [TestCase("Test!")]
            public void ShouldBeInvalidIfContainsNonAlphaNumericCharacters(string value)
            {
                // Arrange
                var validator = new AlphaNumericValidator();

                // Act
                validator.Validate(value);

                // Assert
                validator.IsInvalid.ShouldBe(true);
            }
        }
    }
}