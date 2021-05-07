namespace MADE.Data.EFCore.Extensions
{
    using MADE.Data.EFCore.Converters;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Defines a collection of extensions for the <see cref="EntityBase"/> type.
    /// </summary>
    public static class EntityBaseExtensions
    {
        /// <summary>
        /// Configures the created and updated date properties of an <typeparamref name="TEntity">entity</typeparamref>  as UTC.
        /// </summary>
        /// <typeparam name="TEntity">The type of entity to configure.</typeparam>
        /// <param name="builder">The entity type builder associated with the entity.</param>
        /// <returns>The entity type builder.</returns>
        public static EntityTypeBuilder<TEntity> ConfigureDateProperties<TEntity>(this EntityTypeBuilder<TEntity> builder)
            where TEntity : EntityBase
        {
            builder.Property(x => x.CreatedDate).IsUtc();
            builder.Property(x => x.UpdatedDate).IsUtc();
            return builder;
        }
    }
}
