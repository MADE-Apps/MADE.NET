namespace MADE.Data.Validation.Tests.Tests
{
    using System.Diagnostics.CodeAnalysis;
    using MADE.Data.Validation.Validators;
    using NUnit.Framework;
    using Shouldly;

    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class LatitudeValidatorTests
    {
        public class WhenValidating
        {
            [Test]
            public void ShouldBeDirtyOnceValidated()
            {
                // Arrange
                string value = "Test";
                var validator = new LatitudeValidator();

                // Act
                validator.Validate(value);

                // Assert
                validator.IsDirty.ShouldBe(true);
            }

            [Test]
            public void ShouldBeValidIfInLatitudeRange()
            {
                // Arrange
                var validator = new LatitudeValidator();

                // Act & Assert
                for (var i = -90; i <= 90; i++)
                {
                    validator.Validate(i);
                    validator.IsInvalid.ShouldBe(false);
                }
            }

            [TestCase(-90.5)]
            [TestCase(90.5)]
            public void ShouldBeInvalidIfOutLatitudeRange(object value)
            {
                // Arrange
                var validator = new LatitudeValidator();

                // Act
                validator.Validate(value);

                // Assert
                validator.IsInvalid.ShouldBe(true);
            }
        }
    }
}