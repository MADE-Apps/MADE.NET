// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Data.Serialization.Json.Binders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using MADE.Data.Serialization.Json;
    using MADE.Data.Serialization.Json.Exceptions;
    using Newtonsoft.Json.Serialization;

    /// <summary>
    /// Defines a serialization binder for JSON.NET for migrating serialized <see cref="Type"/> declarations within a serialized JSON object.
    /// </summary>
    /// <remarks>
    /// This is for migrating serialized types where <code>TypeNameHandling.All</code> has been set in the JSON serializer settings.
    /// </remarks>
    public class JsonTypeMigrationSerializationBinder : DefaultSerializationBinder
    {
        private readonly SemaphoreSlim migrationSemaphore;

        private readonly List<JsonTypeMigration> migrations = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonTypeMigrationSerializationBinder"/> class.
        /// </summary>
        /// <remarks>
        /// To add migrations, call the <see cref="AddTypeMigrationAsync"/> method.
        /// </remarks>
        public JsonTypeMigrationSerializationBinder()
            : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonTypeMigrationSerializationBinder"/> class with pre-configured type migrations.
        /// </summary>
        /// <param name="migrations">The type migrations to initialize with.</param>
        public JsonTypeMigrationSerializationBinder(params JsonTypeMigration[] migrations)
        {
            this.migrationSemaphore = new SemaphoreSlim(1, 1);

            if (migrations != null && migrations.Any())
            {
                this.migrations.AddRange(migrations);
            }
        }

        /// <summary>
        /// Adds a JSON type migration to the binder.
        /// </summary>
        /// <param name="migration">The type migration to add.</param>
        /// <returns>An asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="migration"/> is null.</exception>
        /// <exception cref="JsonTypeMigrationException">Thrown if a <paramref name="migration"/> already exists for the from type.</exception>
        public async Task AddTypeMigrationAsync(JsonTypeMigration migration)
        {
            if (migration == null)
            {
                throw new ArgumentNullException(nameof(migration));
            }

            await this.migrationSemaphore.WaitAsync();

            try
            {
                JsonTypeMigration existingMigration = this.migrations.FirstOrDefault(
                    m =>
                        m.FromAssemblyName == migration.FromAssemblyName &&
                        m.FromTypeName == migration.FromTypeName);

                if (existingMigration != null)
                {
                    throw new JsonTypeMigrationException(
                        $"A type migration is already registered for type {existingMigration.FromTypeName} in assembly {existingMigration.FromAssemblyName} to {existingMigration.ToType.FullName}");
                }

                this.migrations.Add(migration);
            }
            finally
            {
                this.migrationSemaphore.Release();
            }
        }

        /// <summary>
        /// When overridden in a derived class, controls the binding of a serialized object to a type.
        /// </summary>
        /// <param name="assemblyName">Specifies the <see cref="T:System.Reflection.Assembly" /> name of the serialized object.</param>
        /// <param name="typeName">Specifies the <see cref="T:System.Type" /> name of the serialized object.</param>
        /// <returns>
        /// The type of the object the formatter creates a new instance of.
        /// </returns>
        public override Type BindToType(string assemblyName, string typeName)
        {
            Task task = this.migrationSemaphore.WaitAsync();
            Task.WaitAll(task);

            JsonTypeMigration migration = null;
            try
            {
                migration = this.migrations.FirstOrDefault(
                    m =>
                        m.FromAssemblyName == assemblyName && m.FromTypeName == typeName);
            }
            finally
            {
                this.migrationSemaphore.Release();
            }

            return migration != null ? migration.ToType : base.BindToType(assemblyName, typeName);
        }
    }
}