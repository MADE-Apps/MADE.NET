namespace MADE.Data.Validation.Tests.Tests
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using MADE.Data.Validation.Validators;
    using NUnit.Framework;
    using Shouldly;

    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class DateTimeValidatorTests
    {
        public class WhenValidating
        {
            [Test]
            public void ShouldBeDirtyOnceValidated()
            {
                // Arrange
                var validator = new DateTimeValidator();

                // Act
                validator.Validate(DateTime.Now);

                // Assert
                validator.IsDirty.ShouldBe(true);
            }

            [Test]
            public void ShouldBeValidIfDateTimeValue()
            {
                // Arrange
                var validator = new DateTimeValidator();

                // Act
                validator.Validate(DateTime.Now);

                // Assert
                validator.IsInvalid.ShouldBe(false);
            }

            [Test]
            public void ShouldBeValidIfDateStringValue()
            {
                // Arrange
                var validator = new DateTimeValidator();

                // Act
                validator.Validate("1/1/0001");

                // Assert
                validator.IsInvalid.ShouldBe(false);
            }

            [Test]
            public void ShouldBeValidIfDateAndTimeStringValue()
            {
                // Arrange
                var validator = new DateTimeValidator();

                // Act
                validator.Validate("1/1/0001 12:00:00 AM");

                // Assert
                validator.IsInvalid.ShouldBe(false);
            }

            [Test]
            public void ShouldBeInvalidIfNotDateTimeValue()
            {
                // Arrange
                var validator = new DateTimeValidator();

                // Act
                validator.Validate("test");

                // Assert
                validator.IsInvalid.ShouldBe(true);
            }
        }
    }
}