namespace MADE.Data.Validation.Tests.Tests
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using MADE.Data.Validation.Validators;
    using NUnit.Framework;
    using Shouldly;

    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class Base64ValidatorTests
    {
        public class WhenValidating
        {
            [Test]
            public void ShouldBeDirtyOnceValidated()
            {
                // Arrange
                const string value = "Test";
                var validator = new Base64Validator();

                // Act
                validator.Validate(value);

                // Assert
                validator.IsDirty.ShouldBe(true);
            }

            [Test]
            public void ShouldBeValidIfDefinedBase64String()
            {
                // Arrange
                const string value = "VGVzdA==";
                var validator = new Base64Validator();

                // Act
                validator.Validate(value);

                // Assert
                validator.IsInvalid.ShouldBe(false);
            }

            [Test]
            public void ShouldBeValidIfConvertedToBase64String()
            {
                // Arrange
                const string decodedValue = "Test";
                string value = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(decodedValue));
                var validator = new Base64Validator();

                // Act
                validator.Validate(value);

                // Assert
                validator.IsInvalid.ShouldBe(false);
            }

            [Test]
            public void ShouldBeInvalidIfNotBase64()
            {
                // Arrange
                const string value = "Tes";
                var validator = new Base64Validator();

                // Act
                validator.Validate(value);

                // Assert
                validator.IsInvalid.ShouldBe(true);
            }
        }
    }
}