namespace MADE.Data.Validation.Tests.Tests
{
    using System.Diagnostics.CodeAnalysis;
    using MADE.Data.Validation.Validators;
    using NUnit.Framework;
    using Shouldly;

    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class PredicateValidatorTests
    {
        public class WhenValidating
        {
            [Test]
            public void ShouldBeDirtyOnceValidated()
            {
                // Arrange
                var validator = new PredicateValidator<int>(i => i > 0);

                // Act
                validator.Validate(1);

                // Assert
                validator.IsDirty.ShouldBe(true);
            }

            [Test]
            public void ShouldBeValidIfPredicateIsTrue()
            {
                // Arrange
                var validator = new PredicateValidator<int>(i => i > 0);

                // Act
                validator.Validate(1);

                // Assert
                validator.IsInvalid.ShouldBe(false);
            }

            [Test]
            public void ShouldBeInvalidIfPredicateIsFalse()
            {
                // Arrange
                var validator = new PredicateValidator<int>(i => i > 0);

                // Act
                validator.Validate(0);

                // Assert
                validator.IsInvalid.ShouldBe(true);
            }
        }
    }
}