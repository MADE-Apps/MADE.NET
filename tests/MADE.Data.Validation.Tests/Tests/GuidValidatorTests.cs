namespace MADE.Data.Validation.Tests.Tests
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using MADE.Data.Validation.Validators;
    using NUnit.Framework;
    using Shouldly;

    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class GuidValidatorTests
    {
        public class WhenValidating
        {
            [Test]
            public void ShouldBeDirtyOnceValidated()
            {
                // Arrange
                string value = "Test";
                var validator = new GuidValidator();

                // Act
                validator.Validate(value);

                // Assert
                validator.IsDirty.ShouldBe(true);
            }

            [Test]
            public void ShouldBeValidIfGuidType()
            {
                // Arrange
                var value = Guid.NewGuid();
                var validator = new GuidValidator();

                // Act
                validator.Validate(value);

                // Assert
                validator.IsInvalid.ShouldBe(false);
            }

            [Test]
            public void ShouldBeValidIfStringGuid()
            {
                // Arrange
                var value = "f39bc65d-dcb5-47f1-a3ba-51fb5f584fd9";
                var validator = new GuidValidator();

                // Act
                validator.Validate(value);

                // Assert
                validator.IsInvalid.ShouldBe(false);
            }

            [Test]
            public void ShouldBeInvalidIfNotGuid()
            {
                // Arrange
                const string value = "Test";
                var validator = new GuidValidator();

                // Act
                validator.Validate(value);

                // Assert
                validator.IsInvalid.ShouldBe(true);
            }
        }
    }
}