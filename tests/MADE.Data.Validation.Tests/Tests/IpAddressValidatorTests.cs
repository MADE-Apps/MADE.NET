namespace MADE.Data.Validation.Tests.Tests
{
    using System.Diagnostics.CodeAnalysis;
    using NUnit.Framework;
    using Shouldly;
    using Validators;

    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class IpAddressValidatorTests
    {
        public class WhenValidating
        {
            [Test]
            public void ShouldBeDirtyOnceValidated()
            {
                // Arrange
                string value = "Test";
                var validator = new IpAddressValidator();

                // Act
                validator.Validate(value);

                // Assert
                validator.IsDirty.ShouldBe(true);
            }

            [TestCase("127.0.0.1")]
            [TestCase("8.8.8.8")]
            [TestCase("123.41.12.168")]
            [TestCase("10.0.0.1")]
            [TestCase("10.0.0.0")]
            public void ShouldBeValidIfValidIpAddress(string value)
            {
                // Arrange
                var validator = new IpAddressValidator();

                // Act
                validator.Validate(value);

                // Assert
                validator.IsInvalid.ShouldBe(false);
            }

            [TestCase("Test")]
            [TestCase("123123123123")]
            [TestCase(" 127.0.0.1 ")]
            [TestCase("127.0.00.1")]
            [TestCase("127.0.1")]
            [TestCase("10.0.1.2.3")]
            [TestCase("1.2.3.-4")]
            [TestCase("1.256.3.4")]
            [TestCase("10.0.0.1/24")]
            public void ShouldBeInvalidIfInvalidIpAddress(string value)
            {
                // Arrange
                var validator = new IpAddressValidator();

                // Act
                validator.Validate(value);

                // Assert
                validator.IsInvalid.ShouldBe(true);
            }
        }
    }
}