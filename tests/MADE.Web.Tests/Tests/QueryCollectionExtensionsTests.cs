using System.Diagnostics.CodeAnalysis;
using MADE.Web.Extensions;
using Microsoft.AspNetCore.Http;
using NUnit.Framework;
using Shouldly;

namespace MADE.Web.Tests.Tests;

[ExcludeFromCodeCoverage]
[TestFixture]
public class QueryCollectionExtensionsTests
{
    public class WhenGettingStringValue
    {
        [Test]
        public void ShouldReturnValueWhenKeyExists()
        {
            // Arrange
            var query = new QueryCollection(new Dictionary<string, Microsoft.Extensions.Primitives.StringValues>
            {
                { "name", "test-value" },
            });

            // Act
            string? result = query.GetStringValueOrDefault("name");

            // Assert
            result.ShouldBe("test-value");
        }

        [Test]
        public void ShouldReturnDefaultWhenKeyDoesNotExist()
        {
            // Arrange
            var query = new QueryCollection();

            // Act
            string? result = query.GetStringValueOrDefault("missing", "fallback");

            // Assert
            result.ShouldBe("fallback");
        }

        [Test]
        public void ShouldReturnNullDefaultWhenKeyDoesNotExistAndNoDefault()
        {
            // Arrange
            var query = new QueryCollection();

            // Act
            string? result = query.GetStringValueOrDefault("missing");

            // Assert
            result.ShouldBeNull();
        }
    }

    public class WhenGettingIntValue
    {
        [Test]
        public void ShouldReturnValueWhenKeyExists()
        {
            // Arrange
            var query = new QueryCollection(new Dictionary<string, Microsoft.Extensions.Primitives.StringValues>
            {
                { "page", "5" },
            });

            // Act
            int result = query.GetIntValueOrDefault("page", 1);

            // Assert
            result.ShouldBe(5);
        }

        [Test]
        public void ShouldReturnDefaultWhenKeyDoesNotExist()
        {
            // Arrange
            var query = new QueryCollection();

            // Act
            int result = query.GetIntValueOrDefault("page", 1);

            // Assert
            result.ShouldBe(1);
        }

        [Test]
        public void ShouldReturnDefaultWhenValueIsNotNumeric()
        {
            // Arrange
            var query = new QueryCollection(new Dictionary<string, Microsoft.Extensions.Primitives.StringValues>
            {
                { "page", "abc" },
            });

            // Act
            int result = query.GetIntValueOrDefault("page", 1);

            // Assert
            result.ShouldBe(1);
        }

        [Test]
        public void ShouldReturnDefaultWhenValueIsZeroAndTreatZeroAsEmpty()
        {
            // Arrange
            var query = new QueryCollection(new Dictionary<string, Microsoft.Extensions.Primitives.StringValues>
            {
                { "page", "0" },
            });

            // Act
            int result = query.GetIntValueOrDefault("page", 1, treatZeroAsEmpty: true);

            // Assert
            result.ShouldBe(1);
        }

        [Test]
        public void ShouldReturnZeroWhenValueIsZeroAndNotTreatZeroAsEmpty()
        {
            // Arrange
            var query = new QueryCollection(new Dictionary<string, Microsoft.Extensions.Primitives.StringValues>
            {
                { "page", "0" },
            });

            // Act
            int result = query.GetIntValueOrDefault("page", 1, treatZeroAsEmpty: false);

            // Assert
            result.ShouldBe(0);
        }
    }

    public class WhenGettingDateTimeValue
    {
        [Test]
        public void ShouldReturnValueWhenKeyExists()
        {
            // Arrange
            var expectedDate = new DateTime(2024, 6, 15);
            var query = new QueryCollection(new Dictionary<string, Microsoft.Extensions.Primitives.StringValues>
            {
                { "date", "2024-06-15" },
            });

            // Act
            DateTime result = query.GetDateTimeValueOrDefault("date", DateTime.MinValue);

            // Assert
            result.ShouldBe(expectedDate);
        }

        [Test]
        public void ShouldReturnDefaultWhenKeyDoesNotExist()
        {
            // Arrange
            var defaultDate = new DateTime(2024, 1, 1);
            var query = new QueryCollection();

            // Act
            DateTime result = query.GetDateTimeValueOrDefault("date", defaultDate);

            // Assert
            result.ShouldBe(defaultDate);
        }

        [Test]
        public void ShouldReturnDefaultWhenValueIsNotValidDate()
        {
            // Arrange
            var defaultDate = new DateTime(2024, 1, 1);
            var query = new QueryCollection(new Dictionary<string, Microsoft.Extensions.Primitives.StringValues>
            {
                { "date", "not-a-date" },
            });

            // Act
            DateTime result = query.GetDateTimeValueOrDefault("date", defaultDate);

            // Assert
            result.ShouldBe(defaultDate);
        }
    }
}
