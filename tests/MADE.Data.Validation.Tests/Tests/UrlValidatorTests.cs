namespace MADE.Data.Validation.Tests.Tests
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using MADE.Data.Validation.Validators;
    using NUnit.Framework;
    using Shouldly;

    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class UrlValidatorTests
    {
        public class WhenValidating
        {
            [Test]
            public void ShouldBeDirtyOnceValidated()
            {
                // Arrange
                var validator = new UrlValidator();

                // Act
                validator.Validate("https://www.google.com");

                // Assert
                validator.IsDirty.ShouldBe(true);
            }

            [Test]
            public void ShouldBeValidIfUriValue()
            {
                // Arrange
                var validator = new UrlValidator();

                // Act
                validator.Validate(new Uri("https://www.google.com"));

                // Assert
                validator.IsInvalid.ShouldBe(false);
            }

            [Test]
            public void ShouldBeValidIfUriStringValue()
            {
                // Arrange
                var validator = new UrlValidator();

                // Act
                validator.Validate("https://www.google.com");

                // Assert
                validator.IsInvalid.ShouldBe(false);
            }

            [Test]
            public void ShouldBeInvalidIfNotUriValue()
            {
                // Arrange
                var validator = new UrlValidator();

                // Act
                validator.Validate("test");

                // Assert
                validator.IsInvalid.ShouldBe(true);
            }
        }
    }
}