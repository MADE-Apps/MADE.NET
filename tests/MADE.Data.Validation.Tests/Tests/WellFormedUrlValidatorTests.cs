namespace MADE.Data.Validation.Tests.Tests
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using MADE.Data.Validation.Validators;
    using NUnit.Framework;
    using Shouldly;

    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class WellFormedUrlValidatorTests
    {
        public class WhenValidating
        {
            private static readonly object[] ValidUrls =
            {
                "https://www.wellformed.com", "http://www.wellformed.com", "ftp://wellformed.com",
                "https://www.wellformed.com/slug"
            };

            [Test]
            public void ShouldBeDirtyOnceValidated()
            {
                // Arrange
                string value = "www.website.com";
                var validator = new WellFormedUrlValidator();

                // Act
                validator.Validate(value);

                // Assert
                validator.IsDirty.ShouldBe(true);
            }

            [TestCaseSource(nameof(ValidUrls))]
            public void ShouldBeValidIfWellFormedUrlString(string value)
            {
                // Arrange
                var validator = new WellFormedUrlValidator();

                // Act
                validator.Validate(value);

                // Assert
                validator.IsInvalid.ShouldBe(false);
            }

            [TestCaseSource(nameof(ValidUrls))]
            public void ShouldBeValidIfWellFormedUri(string value)
            {
                // Arrange
                var uri = new Uri(value);
                var validator = new WellFormedUrlValidator();

                // Act
                validator.Validate(uri);

                // Assert
                validator.IsInvalid.ShouldBe(false);
            }

            [TestCase("NotUrl")]
            [TestCase("www.notwellformed.com")]
            [TestCase("www.notwellformed.com/slug")]
            public void ShouldBeInvalidIfNotWellFormedUrlString(string value)
            {
                // Arrange
                var validator = new WellFormedUrlValidator();

                // Act
                validator.Validate(value);

                // Assert
                validator.IsInvalid.ShouldBe(true);
            }
        }
    }
}