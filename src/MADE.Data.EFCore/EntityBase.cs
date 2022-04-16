namespace MADE.Data.EFCore
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Defines a base definition for an entity.
    /// </summary>
    public abstract class EntityBase : IEntityBase
    {
        /// <summary>
        /// Gets or sets the identifier of the entity.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the date of the entity's creation.
        /// </summary>
        public virtual DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the date of the entity's last update.
        /// </summary>
        public virtual DateTime? UpdatedDate { get; set; } = DateTime.UtcNow;
    }
}