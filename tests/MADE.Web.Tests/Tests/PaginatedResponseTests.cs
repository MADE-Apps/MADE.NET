namespace MADE.Web.Tests.Tests
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using MADE.Testing;
    using MADE.Web.Responses;
    using NUnit.Framework;
    using Shouldly;

    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class PaginatedResponseTests
    {
        public class WhenReturningPaginatedResponse
        {
            [Test]
            public void ShouldReturnPaginatedResultsWhenCountIsGreaterThanRequest()
            {
                // Arrange
                int page = 1;
                int pageSize = 3;
                int totalItemCount = 10;

                int expectedPageCount = totalItemCount / pageSize;

                var items = new List<string> { "Hello", "World", "Test" };

                // Act
                var result = new PaginatedResponse<string>(items, page, pageSize, totalItemCount);

                // Assert
                result.AvailableCount.ShouldBe(totalItemCount);
                result.Page.ShouldBe(page);
                result.PageSize.ShouldBe(pageSize);
                result.TotalPages.ShouldBe(expectedPageCount);
                result.Items.ShouldBeEquivalentTo(items);
            }
        }
    }
}
