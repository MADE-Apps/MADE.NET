namespace MADE.Data.EFCore.Tests.Tests
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Threading.Tasks;
    using Data;
    using Extensions;
    using NUnit.Framework;
    using Shouldly;

    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class QueryableExtensionsTests
    {
        public class WhenOrderingByPropertyName
        {
            [Test]
            public async Task ShouldOrderByNameAscending()
            {
                // Arrange
                var dbContext = TestDbContext.CreateInMemoryContext("OrderByNameAsc");

                await dbContext.Entities.AddRangeAsync(
                    new TestEntity { Id = Guid.NewGuid(), Name = "Charlie" },
                    new TestEntity { Id = Guid.NewGuid(), Name = "Alice" },
                    new TestEntity { Id = Guid.NewGuid(), Name = "Bob" });
                await dbContext.SaveChangesAsync();

                // Act
                var result = dbContext.Entities.OrderBy(nameof(TestEntity.Name), sortDesc: false).ToList();

                // Assert
                result.Count.ShouldBe(3);
                result[0].Name.ShouldBe("Alice");
                result[1].Name.ShouldBe("Bob");
                result[2].Name.ShouldBe("Charlie");
            }

            [Test]
            public async Task ShouldOrderByNameDescending()
            {
                // Arrange
                var dbContext = TestDbContext.CreateInMemoryContext("OrderByNameDesc");

                await dbContext.Entities.AddRangeAsync(
                    new TestEntity { Id = Guid.NewGuid(), Name = "Alice" },
                    new TestEntity { Id = Guid.NewGuid(), Name = "Charlie" },
                    new TestEntity { Id = Guid.NewGuid(), Name = "Bob" });
                await dbContext.SaveChangesAsync();

                // Act
                var result = dbContext.Entities.OrderBy(nameof(TestEntity.Name), sortDesc: true).ToList();

                // Assert
                result.Count.ShouldBe(3);
                result[0].Name.ShouldBe("Charlie");
                result[1].Name.ShouldBe("Bob");
                result[2].Name.ShouldBe("Alice");
            }

            [Test]
            public async Task ShouldReturnUnorderedQueryWhenSortNameIsEmpty()
            {
                // Arrange
                var dbContext = TestDbContext.CreateInMemoryContext("OrderByEmpty");

                await dbContext.Entities.AddRangeAsync(
                    new TestEntity { Id = Guid.NewGuid(), Name = "Bob" },
                    new TestEntity { Id = Guid.NewGuid(), Name = "Alice" });
                await dbContext.SaveChangesAsync();

                // Act
                var result = dbContext.Entities.OrderBy("", sortDesc: false).ToList();

                // Assert
                result.Count.ShouldBe(2);
            }

            [Test]
            public async Task ShouldReturnUnorderedQueryWhenSortNameIsNull()
            {
                // Arrange
                var dbContext = TestDbContext.CreateInMemoryContext("OrderByNull");

                await dbContext.Entities.AddRangeAsync(
                    new TestEntity { Id = Guid.NewGuid(), Name = "Bob" },
                    new TestEntity { Id = Guid.NewGuid(), Name = "Alice" });
                await dbContext.SaveChangesAsync();

                // Act
                var result = dbContext.Entities.OrderBy(null, sortDesc: false).ToList();

                // Assert
                result.Count.ShouldBe(2);
            }
        }

        public class WhenPaging
        {
            [Test]
            public async Task ShouldReturnFirstPage()
            {
                // Arrange
                var dbContext = TestDbContext.CreateInMemoryContext("PageFirst");

                for (int i = 0; i < 10; i++)
                {
                    await dbContext.Entities.AddAsync(
                        new TestEntity { Id = Guid.NewGuid(), Name = $"Entity{i:D2}" });
                }
                await dbContext.SaveChangesAsync();

                // Act
                var result = dbContext.Entities
                    .OrderBy(nameof(TestEntity.Name), sortDesc: false)
                    .Page(page: 1, pageSize: 3)
                    .ToList();

                // Assert
                result.Count.ShouldBe(3);
                result[0].Name.ShouldBe("Entity00");
                result[1].Name.ShouldBe("Entity01");
                result[2].Name.ShouldBe("Entity02");
            }

            [Test]
            public async Task ShouldReturnSecondPage()
            {
                // Arrange
                var dbContext = TestDbContext.CreateInMemoryContext("PageSecond");

                for (int i = 0; i < 10; i++)
                {
                    await dbContext.Entities.AddAsync(
                        new TestEntity { Id = Guid.NewGuid(), Name = $"Entity{i:D2}" });
                }
                await dbContext.SaveChangesAsync();

                // Act
                var result = dbContext.Entities
                    .OrderBy(nameof(TestEntity.Name), sortDesc: false)
                    .Page(page: 2, pageSize: 3)
                    .ToList();

                // Assert
                result.Count.ShouldBe(3);
                result[0].Name.ShouldBe("Entity03");
                result[1].Name.ShouldBe("Entity04");
                result[2].Name.ShouldBe("Entity05");
            }

            [Test]
            public async Task ShouldReturnPartialLastPage()
            {
                // Arrange
                var dbContext = TestDbContext.CreateInMemoryContext("PageLast");

                for (int i = 0; i < 5; i++)
                {
                    await dbContext.Entities.AddAsync(
                        new TestEntity { Id = Guid.NewGuid(), Name = $"Entity{i:D2}" });
                }
                await dbContext.SaveChangesAsync();

                // Act
                var result = dbContext.Entities
                    .OrderBy(nameof(TestEntity.Name), sortDesc: false)
                    .Page(page: 2, pageSize: 3)
                    .ToList();

                // Assert
                result.Count.ShouldBe(2);
                result[0].Name.ShouldBe("Entity03");
                result[1].Name.ShouldBe("Entity04");
            }
        }
    }
}
