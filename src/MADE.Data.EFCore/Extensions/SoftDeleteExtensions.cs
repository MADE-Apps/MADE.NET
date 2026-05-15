// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MADE.Data.EFCore.Extensions;

/// <summary>
/// Defines a collection of extensions for supporting soft deletion of entities.
/// </summary>
public static class SoftDeleteExtensions
{
    /// <summary>
    /// Applies a global query filter to all entities implementing <see cref="ISoftDeletable"/> to exclude soft-deleted entities from queries by default.
    /// </summary>
    /// <param name="builder">The model builder to apply the filter to.</param>
    public static void ApplySoftDeleteFilter(this ModelBuilder builder)
    {
        foreach (IMutableEntityType entityType in builder.Model.GetEntityTypes())
        {
            if (!typeof(ISoftDeletable).IsAssignableFrom(entityType.ClrType))
            {
                continue;
            }

            ParameterExpression parameter = Expression.Parameter(entityType.ClrType, "e");
            MemberExpression property = Expression.Property(parameter, nameof(ISoftDeletable.IsDeleted));
            UnaryExpression filter = Expression.Not(property);
            LambdaExpression lambda = Expression.Lambda(filter, parameter);

            builder.Entity(entityType.ClrType).HasQueryFilter(lambda);
        }
    }

    /// <summary>
    /// Soft deletes an entity by setting the <see cref="ISoftDeletable.IsDeleted"/> flag to true and the <see cref="ISoftDeletable.DeletedDate"/> to the current UTC time.
    /// </summary>
    /// <typeparam name="T">The type of entity to soft delete.</typeparam>
    /// <param name="entity">The entity to soft delete.</param>
    /// <returns>The soft-deleted entity.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the <paramref name="entity"/> is <see langword="null"/>.</exception>
    public static T SoftDelete<T>(this T entity)
        where T : ISoftDeletable
    {
        ArgumentNullException.ThrowIfNull(entity);

        entity.IsDeleted = true;
        entity.DeletedDate = DateTime.UtcNow;
        return entity;
    }

    /// <summary>
    /// Restores a soft-deleted entity by clearing the <see cref="ISoftDeletable.IsDeleted"/> flag and the <see cref="ISoftDeletable.DeletedDate"/>.
    /// </summary>
    /// <typeparam name="T">The type of entity to restore.</typeparam>
    /// <param name="entity">The entity to restore.</param>
    /// <returns>The restored entity.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the <paramref name="entity"/> is <see langword="null"/>.</exception>
    public static T Restore<T>(this T entity)
        where T : ISoftDeletable
    {
        ArgumentNullException.ThrowIfNull(entity);

        entity.IsDeleted = false;
        entity.DeletedDate = null;
        return entity;
    }

    /// <summary>
    /// Intercepts save operations on the <see cref="DbContext"/> to automatically soft delete entities instead of hard deleting them.
    /// <para>
    /// Call this method in an override of the <c>SaveChangesAsync</c> method before calling the base implementation.
    /// </para>
    /// </summary>
    /// <param name="context">The <see cref="DbContext"/>.</param>
    public static void InterceptSoftDeletions(this DbContext context)
    {
        foreach (var entry in context.ChangeTracker.Entries<ISoftDeletable>())
        {
            if (entry.State != EntityState.Deleted)
            {
                continue;
            }

            entry.State = EntityState.Modified;
            entry.Entity.SoftDelete();
        }
    }
}
