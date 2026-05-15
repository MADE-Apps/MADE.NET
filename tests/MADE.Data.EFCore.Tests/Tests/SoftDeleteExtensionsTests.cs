using System.Diagnostics.CodeAnalysis;
using MADE.Data.EFCore.Extensions;
using NUnit.Framework;
using Shouldly;

namespace MADE.Data.EFCore.Tests.Tests;

[ExcludeFromCodeCoverage]
[TestFixture]
public class SoftDeleteExtensionsTests
{
    private class SoftDeletableEntity : EntityBase, ISoftDeletable
    {
        public string Name { get; set; } = string.Empty;

        public bool IsDeleted { get; set; }

        public DateTime? DeletedDate { get; set; }
    }

    public class WhenSoftDeleting
    {
        [Test]
        public void ShouldSetIsDeletedAndDeletedDate()
        {
            // Arrange
            var entity = new SoftDeletableEntity { Name = "Test" };

            // Act
            entity.SoftDelete();

            // Assert
            entity.IsDeleted.ShouldBeTrue();
            entity.DeletedDate.ShouldNotBeNull();
        }
    }

    public class WhenRestoring
    {
        [Test]
        public void ShouldClearIsDeletedAndDeletedDate()
        {
            // Arrange
            var entity = new SoftDeletableEntity { IsDeleted = true, DeletedDate = DateTime.UtcNow };

            // Act
            entity.Restore();

            // Assert
            entity.IsDeleted.ShouldBeFalse();
            entity.DeletedDate.ShouldBeNull();
        }
    }

    public class WhenRoundTripping
    {
        [Test]
        public void ShouldReturnToOriginalState()
        {
            // Arrange
            var entity = new SoftDeletableEntity { Name = "Test" };

            // Act
            entity.SoftDelete();
            entity.Restore();

            // Assert
            entity.IsDeleted.ShouldBeFalse();
            entity.DeletedDate.ShouldBeNull();
        }
    }
}
