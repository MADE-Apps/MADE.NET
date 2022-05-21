namespace MADE.Data.Serialization.Json
{
    using System;
    using MADE.Data.Serialization.Json.Binders;

    /// <summary>
    /// Defines the detail for migrating from one type to another using the <see cref="JsonTypeMigrationSerializationBinder"/>.
    /// </summary>
    public class JsonTypeMigration
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JsonTypeMigration"/> class with the expected from and to migration types.
        /// </summary>
        /// <param name="fromType">The type being migrated from.</param>
        /// <param name="toType">The type being migrated to.</param>
        public JsonTypeMigration(Type fromType, Type toType)
        {
            this.FromAssemblyName = fromType.Assembly.GetName().Name;
            this.FromTypeName = fromType.FullName;
            this.ToType = toType;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonTypeMigration"/> class with the expected from and to migration types.
        /// </summary>
        /// <param name="fromAssemblyName">The name of the assembly being migrated from.</param>
        /// <param name="fromTypeName">The name of the type being migrated from.</param>
        /// <param name="toType">The type being migrated to.</param>
        public JsonTypeMigration(string fromAssemblyName, string fromTypeName, Type toType)
        {
            this.FromAssemblyName = fromAssemblyName;
            this.FromTypeName = fromTypeName;
            this.ToType = toType;
        }

        /// <summary>
        /// Gets the name of the assembly being migrated from.
        /// </summary>
        public string FromAssemblyName { get; }

        /// <summary>
        /// Gets the name of the type being migrated from.
        /// </summary>
        public string FromTypeName { get; }

        /// <summary>
        /// Gets the type being migrated to.
        /// </summary>
        public Type ToType { get; }
    }
}