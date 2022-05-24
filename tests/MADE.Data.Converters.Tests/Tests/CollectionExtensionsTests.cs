namespace MADE.Data.Converters.Tests.Tests
{
    using System.Diagnostics.CodeAnalysis;
    using MADE.Data.Converters.Extensions;
    using NUnit.Framework;
    using Shouldly;

    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class CollectionExtensionsTests
    {
        public class WhenConvertingToDelimitedString
        {
            [Test]
            public void ShouldReturnItemsWithCommaDelimiterByDefault()
            {
                // Arrange
                var items = new[] { "item1", "item2", "item3" };

                // Act
                var result = items.ToDelimitedString();

                // Assert
                result.ShouldBe("item1,item2,item3");
            }

            [Test]
            public void ShouldReturnItemsWithCustomDelimiter()
            {
                // Arrange
                var items = new[] { "item1", "item2", "item3" };

                // Act
                var result = items.ToDelimitedString(delimiter: " | ");

                // Assert
                result.ShouldBe("item1 | item2 | item3");
            }
        }
    }
}