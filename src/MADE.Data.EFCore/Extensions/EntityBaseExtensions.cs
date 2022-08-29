// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Data.EFCore.Extensions
{
    using System;
    using MADE.Data.EFCore.Converters;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Defines a collection of extensions for the <see cref="EntityBase"/> type.
    /// </summary>
    public static class EntityBaseExtensions
    {
        /// <summary>
        /// Configures the default properties of an <typeparamref name="TEntity">entity</typeparamref>.
        /// </summary>
        /// <typeparam name="TEntity">The type of entity to configure.</typeparam>
        /// <param name="builder">The entity type builder associated with the entity.</param>
        /// <returns>The entity type builder.</returns>
        public static EntityTypeBuilder<TEntity> Configure<TEntity>(this EntityTypeBuilder<TEntity> builder)
            where TEntity : class, IEntityBase
        {
            builder.ConfigureWithKey<TEntity, Guid>();
            return builder;
        }

        /// <summary>
        /// Configures the default properties of an <typeparamref name="TEntity">entity</typeparamref>.
        /// </summary>
        /// <typeparam name="TEntity">The type of entity to configure.</typeparam>
        /// <typeparam name="TKey">The type of unique identifier for the entity.</typeparam>
        /// <param name="builder">The entity type builder associated with the entity.</param>
        /// <returns>The entity type builder.</returns>
        public static EntityTypeBuilder<TEntity> ConfigureWithKey<TEntity, TKey>(this EntityTypeBuilder<TEntity> builder)
            where TEntity : class, IEntityBase<TKey>
        {
            builder.HasKey(e => e.Id);
            builder.ConfigureDateProperties();
            return builder;
        }

        /// <summary>
        /// Configures the created and updated date properties of an <typeparamref name="TEntity">entity</typeparamref>  as UTC.
        /// </summary>
        /// <typeparam name="TEntity">The type of entity to configure.</typeparam>
        /// <param name="builder">The entity type builder associated with the entity.</param>
        /// <returns>The entity type builder.</returns>
        public static EntityTypeBuilder<TEntity> ConfigureDateProperties<TEntity>(
            this EntityTypeBuilder<TEntity> builder)
            where TEntity : class, IDatedEntity
        {
            builder.Property(x => x.CreatedDate).IsUtc();
            builder.Property(x => x.UpdatedDate).IsUtc();
            return builder;
        }
    }
}