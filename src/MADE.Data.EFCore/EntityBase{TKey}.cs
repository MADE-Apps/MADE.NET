// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Data.EFCore
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Defines a base definition for an entity.
    /// </summary>
    /// <typeparam name="TKey">The type of unique identifier for the entity.</typeparam>
    public abstract class EntityBase<TKey> : IEntityBase<TKey>
    {
        /// <summary>
        /// Gets or sets the identifier of the entity.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public TKey Id { get; set; }

        /// <summary>
        /// Gets or sets the date of the entity's creation.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the date of the entity's last update.
        /// </summary>
        public DateTime? UpdatedDate { get; set; }
    }
}