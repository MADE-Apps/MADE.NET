namespace MADE.Data.EFCore.Tests.Data
{
    using System.Diagnostics.CodeAnalysis;
    using System.Threading;
    using System.Threading.Tasks;
    using Converters;
    using Extensions;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    [ExcludeFromCodeCoverage]
    public class TestDbContext : DbContext
    {
        public DbSet<TestEntity> Entities { get; set; }

        public DbSet<TestKeyedEntity> KeyEntities { get; set; }

        public TestDbContext(DbContextOptions<TestDbContext> options) : base(options) { }

        public static TestDbContext CreateInMemoryContext(string dbName)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TestDbContext>();
            DbContextOptions<TestDbContext> options = optionsBuilder.UseInMemoryDatabase(dbName).Options;
            return new TestDbContext(options);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            this.SetEntityDates();
            return base.SaveChangesAsync(cancellationToken);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            this.SetEntityDates();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TestDbContext).Assembly);
            modelBuilder.ApplyUtcDateTimeConverter();
        }
    }

    public class TestEntity : EntityBase
    {
        public string Name { get; set; }
    }

    public class TestKeyedEntity : EntityBase<int>
    {
        public string Name { get; set; }
    }

    public class TestEntityTypeConfiguration : IEntityTypeConfiguration<TestEntity>
    {
        public void Configure(EntityTypeBuilder<TestEntity> builder)
        {
            builder.Configure();
        }
    }

    public class TestKeyedEntityTypeConfiguration : IEntityTypeConfiguration<TestKeyedEntity>
    {
        public void Configure(EntityTypeBuilder<TestKeyedEntity> builder)
        {
            builder.ConfigureWithKey<TestKeyedEntity, int>();
        }
    }
}