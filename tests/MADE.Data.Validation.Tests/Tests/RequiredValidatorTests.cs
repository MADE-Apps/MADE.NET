namespace MADE.Data.Validation.Tests.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using MADE.Data.Validation.Validators;
    using NUnit.Framework;
    using Shouldly;

    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class RequiredValidatorTests
    {
        public class WhenValidating
        {
            [Test]
            public void ShouldBeDirtyOnceValidated()
            {
                // Arrange
                int value = 1;
                var validator = new RequiredValidator();

                // Act
                validator.Validate(value);

                // Assert
                validator.IsDirty.ShouldBe(true);
            }

            [Test]
            public void ShouldNotValidateEmptyString()
            {
                // Arrange
                var validator = new RequiredValidator();

                // Act
                validator.Validate(string.Empty);

                // Assert
                validator.IsInvalid.ShouldBe(true);
            }

            [Test]
            public void ShouldNotValidateStringOnlyWithSpaces()
            {
                // Arrange
                var validator = new RequiredValidator();

                // Act
                validator.Validate("   ");

                // Assert
                validator.IsInvalid.ShouldBe(true);
            }

            [Test]
            public void ShouldValidateStringWithCharacters()
            {
                // Arrange
                var validator = new RequiredValidator();

                // Act
                validator.Validate("Hello, World");

                // Assert
                validator.IsInvalid.ShouldBe(false);
            }

            [Test]
            public void ShouldNotValidateEmptyCollection()
            {
                // Arrange
                var validator = new RequiredValidator();

                // Act
                validator.Validate(new List<string>());

                // Assert
                validator.IsInvalid.ShouldBe(true);
            }

            [Test]
            public void ShouldValidatePopulatedCollection()
            {
                // Arrange
                var validator = new RequiredValidator();

                // Act
                validator.Validate(new List<string> { "Hello", "World" });

                // Assert
                validator.IsInvalid.ShouldBe(false);
            }

            [Test]
            public void ShouldNotValidateNullObject()
            {
                // Arrange
                var validator = new RequiredValidator();

                // Act
                validator.Validate(null);

                // Assert
                validator.IsInvalid.ShouldBe(true);
            }

            [Test]
            public void ShouldValidateInitializedObject()
            {
                // Arrange
                var validator = new RequiredValidator();

                // Act
                validator.Validate(new EventArgs());

                // Assert
                validator.IsInvalid.ShouldBe(false);
            }

            [Test]
            public void ShouldNotValidateFalse()
            {
                // Arrange
                var validator = new RequiredValidator();

                // Act
                validator.Validate(false);

                // Assert
                validator.IsInvalid.ShouldBe(true);
            }

            [Test]
            public void ShouldValidateTrue()
            {
                // Arrange
                var validator = new RequiredValidator();

                // Act
                validator.Validate(true);

                // Assert
                validator.IsInvalid.ShouldBe(false);
            }
        }
    }
}