namespace MADE.Data.Validation.Tests.Tests
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using MADE.Data.Validation.Validators;
    using NUnit.Framework;
    using Shouldly;

    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class MinValueValidatorTests
    {
        public class WhenValidating
        {
            [Test]
            public void ShouldBeDirtyOnceValidated()
            {
                // Arrange
                int value = 1;
                var validator = new MinValueValidator(0);

                // Act
                validator.Validate(value);

                // Assert
                validator.IsDirty.ShouldBe(true);
            }

            [TestCase(0, 0)]
            [TestCase(1, 0)]
            [TestCase(0.0, 0.0)]
            [TestCase(1.0, 0.0)]
            [TestCase(0.0f, 0.0f)]
            [TestCase(1.0f, 0.0f)]
            public void ShouldBeValidIfValueAboveMin(IComparable value, IComparable min)
            {
                // Arrange
                var validator = new MinValueValidator(min);

                // Act
                validator.Validate(value);

                // Assert
                validator.IsInvalid.ShouldBe(false);
            }

            [TestCase(-1, 0)]
            [TestCase(-1.0, 0.0)]
            [TestCase(-1.0f, 0.0f)]
            public void ShouldBeInvalidIfValueBelowMin(IComparable value, IComparable min)
            {
                // Arrange
                var validator = new MinValueValidator(min);

                // Act
                validator.Validate(value);

                // Assert
                validator.IsInvalid.ShouldBe(true);
            }
        }
    }
}