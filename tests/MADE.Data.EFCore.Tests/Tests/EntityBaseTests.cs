using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using MADE.Data.EFCore.Tests.Data;
using MADE.Data.EFCore.Extensions;
using NUnit.Framework;
using Shouldly;

namespace MADE.Data.EFCore.Tests.Tests;

[TestFixture]
[ExcludeFromCodeCoverage]
public class EntityBaseTests
{
    public class WhenSavingToDbContext
    {
        [Test]
        public async Task ShouldSetEntityBaseDates()
        {
            // Arrange
            var dbContext = TestDbContext.CreateInMemoryContext("ShouldSetEntityBaseDates");

            var entity = new TestEntity { Id = Guid.NewGuid(), Name = "Test" };

            await dbContext.AddAsync(entity);

            // Act
            var before = DateTime.UtcNow;
            await dbContext.TrySaveChangesAsync();
            var after = DateTime.UtcNow;

            // Assert
            entity.CreatedDate.ShouldBeInRange(before.AddSeconds(-1), after.AddSeconds(1));
            entity.UpdatedDate.ShouldNotBeNull();
            entity.UpdatedDate.Value.ShouldBeInRange(before.AddSeconds(-1), after.AddSeconds(1));
        }

        [Test]
        public async Task ShouldSetKeyedEntityBaseDates()
        {
            // Arrange
            var dbContext = TestDbContext.CreateInMemoryContext("ShouldSetKeyedEntityBaseDates");

            var entity = new TestKeyedEntity { Id = 1, Name = "Test" };

            await dbContext.AddAsync(entity);

            // Act
            var before = DateTime.UtcNow;
            await dbContext.TrySaveChangesAsync();
            var after = DateTime.UtcNow;

            // Assert
            entity.CreatedDate.ShouldBeInRange(before.AddSeconds(-1), after.AddSeconds(1));
            entity.UpdatedDate.ShouldNotBeNull();
            entity.UpdatedDate.Value.ShouldBeInRange(before.AddSeconds(-1), after.AddSeconds(1));
        }
    }
}
