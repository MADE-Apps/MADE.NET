namespace MADE.Data.Validation.Tests.Tests
{
    using System.Diagnostics.CodeAnalysis;
    using MADE.Data.Validation.Validators;
    using NUnit.Framework;
    using Shouldly;

    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class EmailValidatorTests
    {
        public class WhenValidating
        {
            [Test]
            public void ShouldBeDirtyOnceValidated()
            {
                // Arrange
                string value = "Test";
                var validator = new EmailValidator();

                // Act
                validator.Validate(value);

                // Assert
                validator.IsDirty.ShouldBe(true);
            }

            [TestCase("email@example.com")]
            [TestCase("firstname.lastname@example.com")]
            [TestCase("email@subdomain.example.com")]
            [TestCase("firstname+lastname@example.com")]
            [TestCase("1234567890@example.com")]
            [TestCase("email@example-example.com")]
            [TestCase("email@example.name")]
            public void ShouldBeValidIfValidEmailAddress(string value)
            {
                // Arrange
                var validator = new EmailValidator();

                // Act
                validator.Validate(value);

                // Assert
                validator.IsInvalid.ShouldBe(false);
            }

            [TestCase("emailaddress")]
            [TestCase("#@%^%#$@#$@#.com")]
            [TestCase("Joe Bloggs <email@example.com>")]
            [TestCase("email.example.com")]
            [TestCase("email@example@example.com")]
            [TestCase(".email@example.com")]
            [TestCase("email.@example.com")]
            [TestCase("email..email@example.com")]
            [TestCase("email@example.com (Joe Bloggs)")]
            [TestCase("email@example")]
            [TestCase("email@111.222.333.44444")]
            public void ShouldBeInvalidIfInvalidEmailAddress(string value)
            {
                // Arrange
                var validator = new EmailValidator();

                // Act
                validator.Validate(value);

                // Assert
                validator.IsInvalid.ShouldBe(true);
            }
        }
    }
}