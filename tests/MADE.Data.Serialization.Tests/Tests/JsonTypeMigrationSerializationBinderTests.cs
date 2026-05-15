using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using MADE.Data.Serialization.Json;
using MADE.Data.Serialization.Json.Converters;
using NUnit.Framework;
using Shouldly;

namespace MADE.Data.Serialization.Tests.Tests;

[ExcludeFromCodeCoverage]
[TestFixture]
public class JsonTypeMigrationSerializationBinderTests
{
    public class WhenMigratingFromOneTypeToAnother
    {
        [Test]
        public void ShouldMigrateFromTypeToType()
        {
            // Arrange
            var converter = new JsonTypeMigrationConverter();
            converter.AddTypeMigration(new JsonTypeMigration(typeof(OldType), typeof(NewType)));

            var oldType = new OldType();

            // Simulate JSON with $type metadata (as previously serialized by Newtonsoft.Json with TypeNameHandling.All)
            string serialized = JsonSerializer.Serialize(new
            {
                @__type = typeof(OldType).FullName + ", " + typeof(OldType).Assembly.GetName().Name,
                oldType.Name,
                oldType.Number
            });

            // Replace __type with $type since $ isn't valid in anonymous type member names
            serialized = serialized.Replace("\"__type\"", "\"$type\"");

            var options = new JsonSerializerOptions();
            options.Converters.Add(converter);

            // Act
            var deserialized = JsonSerializer.Deserialize<object>(serialized, options);

            // Assert
            deserialized.ShouldBeOfType(typeof(NewType));

            var newType = (NewType)deserialized;
            newType.Name.ShouldBe(oldType.Name);
            newType.Number.ShouldBe((double)oldType.Number);
        }

        [Test]
        public void ShouldMigrateFromAssemblyAndTypeNameToType()
        {
            // Arrange
            var converter = new JsonTypeMigrationConverter();
            converter.AddTypeMigration(new JsonTypeMigration(
                "MADE.Data.Serialization.Tests",
                "MADE.Data.Serialization.Tests.Tests.JsonTypeMigrationSerializationBinderTests+OldType",
                typeof(NewType)));

            var oldType = new OldType();

            // Simulate JSON with $type metadata
            string serialized = JsonSerializer.Serialize(new
            {
                @__type = "MADE.Data.Serialization.Tests.Tests.JsonTypeMigrationSerializationBinderTests+OldType, MADE.Data.Serialization.Tests",
                oldType.Name,
                oldType.Number
            });

            serialized = serialized.Replace("\"__type\"", "\"$type\"");

            var options = new JsonSerializerOptions();
            options.Converters.Add(converter);

            // Act
            var deserialized = JsonSerializer.Deserialize<object>(serialized, options);

            // Assert
            deserialized.ShouldBeOfType(typeof(NewType));

            var newType = (NewType)deserialized;
            newType.Name.ShouldBe(oldType.Name);
            newType.Number.ShouldBe((double)oldType.Number);
        }
    }

    private class OldType
    {
        public string Name { get; set; }

        public int Number { get; set; }
    }

    private class NewType
    {
        public string Name { get; set; }

        public double Number { get; set; }
    }
}
