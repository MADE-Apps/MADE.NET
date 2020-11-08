namespace MADE.Collections.Tests.Tests
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    using NUnit.Framework;

    using Shouldly;

    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class DictionaryExtensionsTests
    {
        public class WhenAddingOrUpdatingAnItem
        {
            [Test]
            public void ShouldAddNewKeyValuePairIfNotExisting()
            {
                // Arrange
                string key = "Hello";
                string value = "World";

                var dictionary = new Dictionary<string, string>();

                // Act
                dictionary.AddOrUpdate(key, value);

                // Assert
                string actualValue = dictionary[key];
                actualValue.ShouldBe(value);
            }

            [Test]
            public void ShouldUpdateValueForExistingKeyValuePair()
            {
                // Arrange
                string key = "Hello";
                string previousValue = "World";
                string newValue = "MADE";

                var dictionary = new Dictionary<string, string> { { key, previousValue } };

                // Act
                dictionary.AddOrUpdate(key, newValue);

                // Assert
                string actualValue = dictionary[key];
                actualValue.ShouldBe(newValue);
            }
        }
    }
}