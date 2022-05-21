namespace MADE.Data.Serialization.Tests.Tests
{
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;
    using MADE.Data.Serialization.Json;
    using MADE.Data.Serialization.Json.Binders;
    using Newtonsoft.Json;
    using NUnit.Framework;
    using Shouldly;

    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class JsonTypeMigrationSerializationBinderTests
    {
        public class WhenMigratingFromOneTypeToAnother
        {
            [Test]
            public async Task ShouldMigrateFromTypeToType()
            {
                // Arrange
                var binder = new JsonTypeMigrationSerializationBinder();
                await binder.AddTypeMigrationAsync(new JsonTypeMigration(typeof(OldType), typeof(NewType)));

                var oldType = new OldType();
                var serialized = JsonConvert.SerializeObject(
                    oldType,
                    new JsonSerializerSettings {TypeNameHandling = TypeNameHandling.All});

                // Act
                var deserialized = JsonConvert.DeserializeObject(
                    serialized,
                    new JsonSerializerSettings {TypeNameHandling = TypeNameHandling.All, SerializationBinder = binder});

                // Assert
                deserialized.ShouldBeOfType(typeof(NewType));

                var newType = (NewType)deserialized;
                newType.Name.ShouldBe(oldType.Name);
                newType.Number.ShouldBe((double)oldType.Number);
            }

            [Test]
            public async Task ShouldMigrateFromAssemblyAndTypeNameToType()
            {
                // Arrange
                var binder = new JsonTypeMigrationSerializationBinder();
                await binder.AddTypeMigrationAsync(new JsonTypeMigration(
                    "MADE.Data.Serialization.Tests",
                    "MADE.Data.Serialization.Tests.Tests.JsonTypeMigrationSerializationBinderTests+OldType",
                    typeof(NewType)));

                var oldType = new OldType();
                var serialized = JsonConvert.SerializeObject(
                    oldType,
                    new JsonSerializerSettings {TypeNameHandling = TypeNameHandling.All});

                // Act
                var deserialized = JsonConvert.DeserializeObject(
                    serialized,
                    new JsonSerializerSettings {TypeNameHandling = TypeNameHandling.All, SerializationBinder = binder});

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
}