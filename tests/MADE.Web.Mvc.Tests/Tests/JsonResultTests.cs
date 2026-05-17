using System.Diagnostics.CodeAnalysis;
using System.Net;
using MADE.Web.Mvc.Responses;
using NUnit.Framework;
using Shouldly;

namespace MADE.Web.Mvc.Tests.Tests;

[ExcludeFromCodeCoverage]
[TestFixture]
public class JsonResultTests
{
    public class WhenCreating
    {
        [Test]
        public void ShouldSetValueAndDefaultStatusCode()
        {
            // Arrange & Act
            var result = new JsonResult(new { Name = "Test" });

            // Assert
            result.StatusCode.ShouldBe((int)HttpStatusCode.OK);
            result.Value.ShouldNotBeNull();
        }

        [Test]
        public void ShouldSetCustomStatusCode()
        {
            // Arrange & Act
            var result = new JsonResult(new { Id = 1 }, HttpStatusCode.Created);

            // Assert
            result.StatusCode.ShouldBe((int)HttpStatusCode.Created);
        }

        [Test]
        public void ShouldSetSerializerOptions()
        {
            // Arrange
            var options = new System.Text.Json.JsonSerializerOptions { WriteIndented = false };

            // Act
            var result = new JsonResult(new { Id = 1 }, serializerOptions: options);

            // Assert
            result.SerializerOptions.ShouldBe(options);
        }

        [Test]
        public void ShouldHaveNullSerializerOptionsByDefault()
        {
            // Act
            var result = new JsonResult(new { Id = 1 });

            // Assert
            result.SerializerOptions.ShouldBeNull();
        }
    }
}
