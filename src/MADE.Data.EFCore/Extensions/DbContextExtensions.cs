namespace MADE.Data.EFCore.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;

    /// <summary>
    /// Defines a collection of extensions for <see cref="DbContext"/> types.
    /// </summary>
    public static class DbContextExtensions
    {
        /// <summary>
        /// Updates an entity within the context and saves the changes.
        /// </summary>
        /// <param name="context">The <see cref="DbContext"/>.</param>
        /// <param name="entity">The entity to update.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <typeparam name="T">The type of entity to update.</typeparam>
        /// <returns>An asynchronous operation.</returns>
        /// <exception cref="DbUpdateException">An error is encountered while saving to the database.</exception>
        /// <exception cref="DbUpdateConcurrencyException">A concurrency violation is encountered while saving to the database.
        ///                 A concurrency violation occurs when an unexpected number of rows are affected during save.
        ///                 This is usually because the data in the database has been modified since it was loaded into memory.</exception>
        public static async Task UpdateAsync<T>(
            this DbContext context,
            T entity,
            CancellationToken cancellationToken = default)
        {
            context.Update(entity);
            await context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Removes entities from a <see cref="DbSet{TEntity}"/> using the specified predicate.
        /// </summary>
        /// <param name="set">The data set to remove entities from.</param>
        /// <param name="predicate">The function for determining the items to remove.</param>
        /// <typeparam name="T">The type of entity to remove.</typeparam>
        public static void RemoveWhere<T>(this DbSet<T> set, Expression<Func<T, bool>> predicate)
            where T : class
        {
            IQueryable<T> toRemove = set.Where(predicate);
            set.RemoveRange(toRemove);
        }

        /// <summary>
        /// Sets the dates of <see cref="EntityBase"/> entities being tracked in an added or modified state.
        /// <para>
        /// It is best to call this method in an override of the DbContext.SaveChangesAsync method in your data context.
        /// </para>
        /// </summary>
        /// <param name="context">The <see cref="DbContext"/> to update entity dates for.</param>
        public static void SetEntityDates(this DbContext context)
        {
            IEnumerable<EntityEntry> entries = context.ChangeTracker
                .Entries()
                .Where(
                    entry => entry.Entity is EntityBase &&
                             (entry.State == EntityState.Added || entry.State == EntityState.Modified));

            DateTime now = DateTime.UtcNow;

            foreach (EntityEntry entry in entries)
            {
                var entity = (EntityBase)entry.Entity;
                entity.UpdatedDate = now;

                if (entry.State == EntityState.Added && entity.CreatedDate == DateTime.MinValue)
                {
                    entity.CreatedDate = now;
                }
            }
        }
    }
}