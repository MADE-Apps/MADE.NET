namespace MADE.Data.EFCore.Tests.Tests
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;
    using Data;
    using Extensions;
    using NUnit.Framework;
    using Shouldly;

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
                await dbContext.TrySaveChangesAsync();

                // Assert
                entity.CreatedDate.ShouldBeInRange(DateTime.UtcNow.AddSeconds(-1), DateTime.UtcNow.AddSeconds(1));
                entity.UpdatedDate.ShouldNotBeNull();
                entity.UpdatedDate.Value.ShouldBeInRange(DateTime.UtcNow.AddSeconds(-1), DateTime.UtcNow.AddSeconds(1));
            }

            [Test]
            public async Task ShouldSetKeyedEntityBaseDates()
            {
                // Arrange
                var dbContext = TestDbContext.CreateInMemoryContext("ShouldSetKeyedEntityBaseDates");

                var entity = new TestKeyedEntity { Id = 1, Name = "Test" };

                await dbContext.AddAsync(entity);

                // Act
                await dbContext.TrySaveChangesAsync();

                // Assert
                entity.CreatedDate.ShouldBeInRange(DateTime.UtcNow.AddSeconds(-1), DateTime.UtcNow.AddSeconds(1));
                entity.UpdatedDate.ShouldNotBeNull();
                entity.UpdatedDate.Value.ShouldBeInRange(DateTime.UtcNow.AddSeconds(-1), DateTime.UtcNow.AddSeconds(1));
            }
        }
    }
}