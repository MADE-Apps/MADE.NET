// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace MADE.Data.Serialization.Json.Converters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using System.Threading;
    using System.Threading.Tasks;
    using MADE.Data.Serialization.Json.Exceptions;

    /// <summary>
    /// Defines a JSON converter for migrating serialized <see cref="Type"/> declarations within a serialized JSON object.
    /// </summary>
    /// <remarks>
    /// This converter reads <c>$type</c> metadata from JSON objects and resolves the target type using registered type migrations.
    /// It is designed to deserialize JSON that was previously serialized with type metadata (e.g., Newtonsoft.Json's <c>TypeNameHandling.All</c>).
    /// </remarks>
    public class JsonTypeMigrationConverter : JsonConverter<object>
    {
        private readonly SemaphoreSlim migrationSemaphore;

        private readonly List<JsonTypeMigration> migrations = new();

        private JsonSerializerOptions innerOptions;

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonTypeMigrationConverter"/> class.
        /// </summary>
        /// <remarks>
        /// To add migrations, call the <see cref="AddTypeMigrationAsync"/> method.
        /// </remarks>
        public JsonTypeMigrationConverter()
            : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonTypeMigrationConverter"/> class with pre-configured type migrations.
        /// </summary>
        /// <param name="migrations">The type migrations to initialize with.</param>
        public JsonTypeMigrationConverter(params JsonTypeMigration[] migrations)
        {
            this.migrationSemaphore = new SemaphoreSlim(1, 1);

            if (migrations != null && migrations.Any())
            {
                this.migrations.AddRange(migrations);
            }
        }

        /// <summary>
        /// Adds a JSON type migration to the converter.
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

        /// <inheritdoc/>
        public override object Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using JsonDocument doc = JsonDocument.ParseValue(ref reader);
            JsonElement root = doc.RootElement;

            Type resolvedType = typeToConvert;

            if (root.ValueKind == JsonValueKind.Object && root.TryGetProperty("$type", out JsonElement typeElement))
            {
                string typeString = typeElement.GetString();
                resolvedType = this.ResolveType(typeString) ?? typeToConvert;
            }

            return root.Deserialize(resolvedType, this.GetInnerOptions(options));
        }

        /// <inheritdoc/>
        public override void Write(Utf8JsonWriter writer, object value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, value?.GetType() ?? typeof(object), this.GetInnerOptions(options));
        }

        private JsonSerializerOptions GetInnerOptions(JsonSerializerOptions options)
        {
            if (this.innerOptions == null)
            {
                var copy = new JsonSerializerOptions(options);
                copy.Converters.Remove(this);
                this.innerOptions = copy;
            }

            return this.innerOptions;
        }

        private Type ResolveType(string typeString)
        {
            int commaIndex = typeString.IndexOf(',');
            string typeName = commaIndex >= 0 ? typeString[..commaIndex].Trim() : typeString.Trim();
            string assemblyName = commaIndex >= 0 ? typeString[(commaIndex + 1)..].Trim() : null;

            this.migrationSemaphore.Wait();

            JsonTypeMigration migration;
            try
            {
                migration = this.migrations.FirstOrDefault(
                    m =>
                        m.FromTypeName == typeName &&
                        (assemblyName == null || m.FromAssemblyName == assemblyName));
            }
            finally
            {
                this.migrationSemaphore.Release();
            }

            if (migration != null)
            {
                return migration.ToType;
            }

            return Type.GetType(typeString);
        }
    }
}
