namespace MADE.Data.Validation.Tests.Tests
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using MADE.Data.Validation.Validators;
    using NUnit.Framework;
    using Shouldly;

    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class BetweenValidatorTests
    {
        public class WhenValidating
        {
            [Test]
            public void ShouldBeDirtyOnceValidated()
            {
                // Arrange
                int value = 1;
                var validator = new BetweenValidator(0, 2);

                // Act
                validator.Validate(value);

                // Assert
                validator.IsDirty.ShouldBe(true);
            }

            [TestCase(0, 0, 2)]
            [TestCase(1, 0, 2)]
            [TestCase(2, 0, 2)]
            [TestCase(0.0, 0.0, 2.0)]
            [TestCase(1.0, 0.0, 2.0)]
            [TestCase(2.0, 0.0, 2.0)]
            [TestCase(0.0f, 0.0f, 2.0f)]
            [TestCase(1.0f, 0.0f, 2.0f)]
            [TestCase(2.0f, 0.0f, 2.0f)]
            public void ShouldBeValidIfValueWithinRange(IComparable value, IComparable min, IComparable max)
            {
                // Arrange
                var validator = new BetweenValidator(min, max);

                // Act
                validator.Validate(value);

                // Assert
                validator.IsInvalid.ShouldBe(false);
            }

            [TestCase(-1, 0, 2)]
            [TestCase(3, 0, 2)]
            [TestCase(-1.0, 0.0, 2.0)]
            [TestCase(3.0, 0.0, 2.0)]
            [TestCase(-1.0f, 0.0f, 2.0f)]
            [TestCase(3.0f, 0.0f, 2.0f)]
            public void ShouldBeInvalidIfValueOutsideRange(IComparable value, IComparable min, IComparable max)
            {
                // Arrange
                var validator = new BetweenValidator(min, max);

                // Act
                validator.Validate(value);

                // Assert
                validator.IsInvalid.ShouldBe(true);
            }
        }
    }
}