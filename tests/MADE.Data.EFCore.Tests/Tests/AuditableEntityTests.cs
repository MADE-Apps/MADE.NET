using System.Diagnostics.CodeAnalysis;
using MADE.Data.EFCore.Extensions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Shouldly;

namespace MADE.Data.EFCore.Tests.Tests;

[ExcludeFromCodeCoverage]
[TestFixture]
public class AuditableEntityTests
{
    private class AuditableTestEntity : EntityBase, IAuditableEntity
    {
        public string Name { get; set; } = string.Empty;

        public string? CreatedBy { get; set; }

        public string? UpdatedBy { get; set; }
    }

    private class TestDbContext : DbContext
    {
        public TestDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<AuditableTestEntity> Entities { get; set; }
    }

    private static TestDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<TestDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new TestDbContext(options);
    }

    public class WhenSettingAuditInfo
    {
        [Test]
        public async Task ShouldSetCreatedByOnAdd()
        {
            // Arrange
            using var context = CreateContext();
            var entity = new AuditableTestEntity { Name = "Test" };
            context.Entities.Add(entity);

            // Act
            context.SetEntityAuditInfo("user-123");
            await context.SaveChangesAsync();

            // Assert
            entity.CreatedBy.ShouldBe("user-123");
            entity.UpdatedBy.ShouldBe("user-123");
        }

        [Test]
        public async Task ShouldSetUpdatedByOnModify()
        {
            // Arrange
            using var context = CreateContext();
            var entity = new AuditableTestEntity { Name = "Test", CreatedBy = "user-1" };
            context.Entities.Add(entity);
            await context.SaveChangesAsync();

            // Act
            entity.Name = "Updated";
            context.Entry(entity).State = EntityState.Modified;
            context.SetEntityAuditInfo("user-2");
            await context.SaveChangesAsync();

            // Assert
            entity.CreatedBy.ShouldBe("user-1");
            entity.UpdatedBy.ShouldBe("user-2");
        }
    }
}
