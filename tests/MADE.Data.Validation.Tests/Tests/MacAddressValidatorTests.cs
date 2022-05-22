namespace MADE.Data.Validation.Tests.Tests
{
    using System.Diagnostics.CodeAnalysis;
    using MADE.Data.Validation.Validators;
    using NUnit.Framework;
    using Shouldly;

    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class MacAddressValidatorTests
    {
        public class WhenValidating
        {
            [Test]
            public void ShouldBeDirtyOnceValidated()
            {
                // Arrange
                string value = "Test";
                var validator = new MacAddressValidator();

                // Act
                validator.Validate(value);

                // Assert
                validator.IsDirty.ShouldBe(true);
            }

            [TestCase("001122334455")]
            [TestCase("00-11-22-33-44-55")]
            [TestCase("0011.2233.4455")]
            [TestCase("00:11:22:33:44:55")]
            [TestCase("F0-E1-D2-C3-B4-A5")]
            [TestCase("f0-e1-d2-c3-b4-a5")]
            public void ShouldBeValidIfValidMacAddress(string value)
            {
                // Arrange
                var validator = new MacAddressValidator();

                // Act
                validator.Validate(value);

                // Assert
                validator.IsInvalid.ShouldBe(false);
            }

            [TestCase("Test")]
            [TestCase("00/11/22/33/44/55")]
            public void ShouldBeInvalidIfInvalidMacAddress(string value)
            {
                // Arrange
                var validator = new MacAddressValidator();

                // Act
                validator.Validate(value);

                // Assert
                validator.IsInvalid.ShouldBe(true);
            }
        }
    }
}