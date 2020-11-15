namespace MADE.Data.Validation.Tests.Tests
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using MADE.Data.Validation.Validators;
    using MADE.Testing;
    using NUnit.Framework;
    using Shouldly;

    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class ValidatorCollectionTests
    {
        public class WhenInitializing
        {
            [Test]
            public void ShouldBeEmptyIfDefaultConstructor()
            {
                // Act
                var collection = new ValidatorCollection();

                // Assert
                collection.Count.ShouldBe(0);
            }

            [Test]
            public void ShouldContainItemsIfInitializedAsEnumerable()
            {
                // Arrange
                IEnumerable<IValidator> validators = new List<IValidator>
                {
                    new AlphaValidator { Key = "AlphaOnly" },
                    new RequiredValidator { Key = "Required" },
                };

                // Act
                var collection = new ValidatorCollection(validators);

                // Assert
                collection.Count.ShouldBe(validators.Count());
                collection.ToList().ShouldBeEquivalentTo(validators);
            }
        }

        public class WhenAddingItems
        {
            [Test]
            public void ShouldAddRangeOfItems()
            {
                // Arrange
                IEnumerable<IValidator> objectsToAdd = new List<IValidator>
                {
                    new AlphaValidator { Key = "AlphaOnly" },
                    new RequiredValidator { Key = "Required" },
                };

                var collection = new ValidatorCollection();

                // Act
                collection.AddRange(objectsToAdd);

                // Assert
                foreach (IValidator item in objectsToAdd)
                {
                    collection.ShouldContain(item);
                }
            }

            [Test]
            public void ShouldAddSingleItem()
            {
                // Arrange
                var objectToAdd = new AlphaValidator { Key = "AlphaOnly" };

                // Act
                var collection = new ValidatorCollection { objectToAdd };

                // Assert
                collection.ShouldContain(objectToAdd);
            }
        }

        public class WhenValidating
        {
            [Test]
            public void ShouldBeDirtyOnceValidated()
            {
                // Arrange
                string value = "Hello";

                var collection = new ValidatorCollection
                {
                    new AlphaValidator { Key = "AlphaOnly" },
                    new RequiredValidator { Key = "Required" },
                };

                // Act
                collection.Validate(value);

                // Assert
                collection.IsDirty.ShouldBe(true);
            }

            [Test]
            public void ShouldBeValidIfValidValue()
            {
                // Arrange
                string value = "Hello";

                var collection = new ValidatorCollection
                {
                    new AlphaValidator { Key = "AlphaOnly" },
                    new RequiredValidator { Key = "Required" },
                };

                // Act
                collection.Validate(value);

                // Assert
                collection.IsInvalid.ShouldBe(false);
            }

            [Test]
            public void ShouldBeInvalidIfInvalidValue()
            {
                // Arrange
                string value = string.Empty;

                var collection = new ValidatorCollection
                {
                    new AlphaValidator { Key = "AlphaOnly" },
                    new RequiredValidator { Key = "Required" },
                };

                // Act
                collection.Validate(value);

                // Assert
                collection.IsInvalid.ShouldBe(true);
            }
        }
    }
}