using System.Diagnostics.CodeAnalysis;
using MADE.Web.Requests;
using NUnit.Framework;
using Shouldly;

namespace MADE.Web.Tests.Tests;

[ExcludeFromCodeCoverage]
[TestFixture]
public class PaginatedRequestTests
{
    public class WhenCreating
    {
        [Test]
        public void ShouldHaveDefaultValues()
        {
            // Act
            var request = new PaginatedRequest<string>();

            // Assert
            request.Page.ShouldBe(1);
            request.PageSize.ShouldBe(10);
        }

        [Test]
        public void ShouldSetCustomPageAndPageSize()
        {
            // Act
            var request = new PaginatedRequest<string>(3, 25);

            // Assert
            request.Page.ShouldBe(3);
            request.PageSize.ShouldBe(25);
        }
    }

    public class WhenCalculatingSkip
    {
        [Test]
        public void ShouldReturnZeroForFirstPage()
        {
            // Arrange
            var request = new PaginatedRequest<string>(1, 10);

            // Act & Assert
            request.Skip.ShouldBe(0);
        }

        [Test]
        public void ShouldReturnCorrectSkipForSubsequentPages()
        {
            // Arrange
            var request = new PaginatedRequest<string>(3, 10);

            // Act & Assert
            request.Skip.ShouldBe(20);
        }

        [Test]
        public void ShouldClampPageToMinimumOfOne()
        {
            // Arrange
            var request = new PaginatedRequest<string>(0, 10);

            // Act & Assert
            request.Skip.ShouldBe(0);
        }
    }

    public class WhenCalculatingTake
    {
        [Test]
        public void ShouldReturnPageSize()
        {
            // Arrange
            var request = new PaginatedRequest<string>(1, 25);

            // Act & Assert
            request.Take.ShouldBe(25);
        }
    }
}
