namespace MADE.Data.Validation.Tests.Tests
{
    using System.Diagnostics.CodeAnalysis;
    using MADE.Data.Validation.Validators;
    using NUnit.Framework;
    using Shouldly;

    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class LongitudeValidatorTests
    {
        public class WhenValidating
        {
            [Test]
            public void ShouldBeDirtyOnceValidated()
            {
                // Arrange
                string value = "Test";
                var validator = new LongitudeValidator();

                // Act
                validator.Validate(value);

                // Assert
                validator.IsDirty.ShouldBe(true);
            }

            [Test]
            public void ShouldBeValidIfInLongitudeRange()
            {
                // Arrange
                var validator = new LongitudeValidator();

                // Act & Assert
                for (var i = -180; i <= 180; i++)
                {
                    validator.Validate(i);
                    validator.IsInvalid.ShouldBe(false);
                }
            }

            [TestCase(-180.5)]
            [TestCase(180.5)]
            public void ShouldBeInvalidIfOutLongitudeRange(object value)
            {
                // Arrange
                var validator = new LongitudeValidator();

                // Act
                validator.Validate(value);

                // Assert
                validator.IsInvalid.ShouldBe(true);
            }
        }
    }
}