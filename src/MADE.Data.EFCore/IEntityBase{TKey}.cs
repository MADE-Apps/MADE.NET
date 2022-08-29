// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Data.EFCore
{
    /// <summary>
    /// Defines a base definition for an entity with a defined primary key type.
    /// </summary>
    /// <typeparam name="TKey">The type of unique identifier for the entity.</typeparam>
    public interface IEntityBase<TKey> : IDatedEntity
    {
        /// <summary>
        /// Gets or sets the identifier of the entity.
        /// </summary>
        TKey Id { get; set; }
    }
}