namespace MADE.Data.Validation.Tests.Tests
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using MADE.Data.Validation.Validators;
    using NUnit.Framework;
    using Shouldly;

    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class MaxValueValidatorTests
    {
        public class WhenValidating
        {
            [Test]
            public void ShouldBeDirtyOnceValidated()
            {
                // Arrange
                int value = 1;
                var validator = new MaxValueValidator(0);

                // Act
                validator.Validate(value);

                // Assert
                validator.IsDirty.ShouldBe(true);
            }

            [TestCase(0, 1)]
            [TestCase(1, 1)]
            [TestCase(0.0, 1.0)]
            [TestCase(1.0, 1.0)]
            [TestCase(0.0f, 1.0f)]
            [TestCase(1.0f, 1.0f)]
            public void ShouldBeValidIfValueBelowMax(IComparable value, IComparable max)
            {
                // Arrange
                var validator = new MaxValueValidator(max);

                // Act
                validator.Validate(value);

                // Assert
                validator.IsInvalid.ShouldBe(false);
            }

            [TestCase(2, 1)]
            [TestCase(2.0, 1.0)]
            [TestCase(2.0f, 1.0f)]
            public void ShouldBeInvalidIfValueAboveMax(IComparable value, IComparable max)
            {
                // Arrange
                var validator = new MaxValueValidator(max);

                // Act
                validator.Validate(value);

                // Assert
                validator.IsInvalid.ShouldBe(true);
            }
        }
    }
}