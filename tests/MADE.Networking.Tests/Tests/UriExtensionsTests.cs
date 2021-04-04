namespace MADE.Networking.Tests.Tests
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using MADE.Networking.Extensions;
    using NUnit.Framework;
    using Shouldly;

    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class UriExtensionsTests
    {
        public class WhenRetrievingQueryValues
        {
            [TestCase("https://www.jamescroft.co.uk/api/profile?name=jamesmcroft", "name", "jamesmcroft")]
            [TestCase("https://www.jamescroft.co.uk/api/profile?name=jamescroft&age=24", "age", "24")]
            public void ShouldGetQueryValue(string url, string queryParam, string expectedValue)
            {
                // Arrange
                var uri = new Uri(url);

                // Act
                string value = uri.GetQueryValue(queryParam);

                // Assert
                value.ShouldBe(expectedValue);
            }

            [Test]
            public void ShouldReturnNullIfQueryParamDoesNotExist()
            {
                // Arrange
                var uri = new Uri("https://www.jamescroft.co.uk/api/profile?name=jamesmcroft");

                // Act
                string value = uri.GetQueryValue("age");

                // Assert
                value.ShouldBeNull();
            }
        }
    }
}