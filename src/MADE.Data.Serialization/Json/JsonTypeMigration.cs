// MADE Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using MADE.Data.Serialization.Json.Converters;

namespace MADE.Data.Serialization.Json;

/// <summary>
/// Defines the detail for migrating from one type to another using the <see cref="JsonTypeMigrationConverter"/>.
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
        this.FromAssemblyName = fromType.Assembly.GetName().Name ?? string.Empty;
        this.FromTypeName = fromType.FullName ?? string.Empty;
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
