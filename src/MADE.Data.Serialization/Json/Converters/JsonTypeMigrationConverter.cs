// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Text.Json;
using System.Text.Json.Serialization;
using MADE.Data.Serialization.Json.Exceptions;

namespace MADE.Data.Serialization.Json.Converters;

/// <summary>
/// Defines a JSON converter for migrating serialized <see cref="Type"/> declarations within a serialized JSON object.
/// </summary>
/// <remarks>
/// This converter reads <c>$type</c> metadata from JSON objects and resolves the target type using registered type migrations.
/// It is designed to deserialize JSON that was previously serialized with type metadata (e.g., Newtonsoft.Json's <c>TypeNameHandling.All</c>).
/// </remarks>
public class JsonTypeMigrationConverter : JsonConverter<object>
{
    private readonly object migrationLock = new();

    private readonly List<JsonTypeMigration> migrations = new();

    private JsonSerializerOptions? innerOptions;

    /// <summary>
    /// Initializes a new instance of the <see cref="JsonTypeMigrationConverter"/> class.
    /// </summary>
    /// <remarks>
    /// To add migrations, call the <see cref="AddTypeMigration"/> method.
    /// </remarks>
    public JsonTypeMigrationConverter()
        : this(Array.Empty<JsonTypeMigration>())
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="JsonTypeMigrationConverter"/> class with pre-configured type migrations.
    /// </summary>
    /// <param name="migrations">The type migrations to initialize with.</param>
    public JsonTypeMigrationConverter(params JsonTypeMigration[] migrations)
    {
        if (migrations != null && migrations.Length > 0)
        {
            this.migrations.AddRange(migrations);
        }
    }

    /// <summary>
    /// Adds a JSON type migration to the converter.
    /// </summary>
    /// <param name="migration">The type migration to add.</param>
    /// <exception cref="ArgumentNullException">Thrown if the <paramref name="migration"/> is null.</exception>
    /// <exception cref="JsonTypeMigrationException">Thrown if a <paramref name="migration"/> already exists for the from type.</exception>
    public void AddTypeMigration(JsonTypeMigration migration)
    {
        ArgumentNullException.ThrowIfNull(migration);

        lock (this.migrationLock)
        {
            JsonTypeMigration? existingMigration = this.migrations.FirstOrDefault(
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
    }

    /// <inheritdoc/>
    public override object Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using JsonDocument doc = JsonDocument.ParseValue(ref reader);
        JsonElement root = doc.RootElement;

        Type resolvedType = typeToConvert;

        if (root.ValueKind == JsonValueKind.Object && root.TryGetProperty("$type", out JsonElement typeElement))
        {
            string? typeString = typeElement.GetString();
            if (typeString != null)
            {
                resolvedType = this.ResolveType(typeString) ?? typeToConvert;
            }
        }

        return root.Deserialize(resolvedType, this.GetInnerOptions(options))!;
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

    private Type? ResolveType(string typeString)
    {
        int commaIndex = typeString.IndexOf(',');
        string typeName = commaIndex >= 0 ? typeString[..commaIndex].Trim() : typeString.Trim();
        string? assemblyName = commaIndex >= 0 ? typeString[(commaIndex + 1)..].Trim() : null;

        JsonTypeMigration? migration;
        lock (this.migrationLock)
        {
            migration = this.migrations.FirstOrDefault(
                m =>
                    m.FromTypeName == typeName &&
                    (assemblyName == null || m.FromAssemblyName == assemblyName));
        }

        if (migration != null)
        {
            return migration.ToType;
        }

        return Type.GetType(typeString);
    }
}
