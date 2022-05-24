namespace MADE.Collections.Tests.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using MADE.Collections.Tests.Fakes;
    using MADE.Testing;
    using NUnit.Framework;
    using Shouldly;
    using CollectionExtensions = MADE.Collections.CollectionExtensions;

    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class CollectionExtensionsTests
    {
        public class WhenShufflingItems
        {
            [Test]
            public void ShouldShuffleItemOrderRandomly()
            {
                // Arrange
                var items = new List<int>
                {
                    1,
                    2,
                    3,
                    4,
                    5
                };

                // Act
                var shuffledItems = items.Shuffle();

                // Assert
                shuffledItems.ShouldNotBeSameAs(items);
            }

            [Test]
            public void ShouldContainSameItemsAfterShuffle()
            {
                // Arrange
                var items = new List<int>
                {
                    1,
                    2,
                    3,
                    4,
                    5
                };

                // Act
                var shuffledItems = items.Shuffle();

                // Assert
                shuffledItems.ShouldBeEquivalentTo(items);
            }
        }

        public class WhenUpdatingACollectionItem
        {
            [Test]
            public void ShouldThrowArgumentNullExceptionIfNullCollection()
            {
                // Arrange
                List<string> list = null;
                string item = "Hello";

                // Act & Assert
                Assert.Throws<ArgumentNullException>(() => list.Update(item, (s, i) => s == i));
            }

            [Test]
            public void ShouldThrowArgumentNullExceptionIfNullItem()
            {
                // Arrange
                var list = new List<string> {"Hello"};
                string item = null;

                // Act & Assert
                Assert.Throws<ArgumentNullException>(() => list.Update(item, (s, i) => s == i));
            }

            [Test]
            public void ShouldReturnTrueIfItemUpdated()
            {
                // Arrange
                TestObject objectToAdd = TestObjectFaker.Create().Generate();
                TestObject objectToUpdateWith = TestObjectFaker.Create().Generate();

                var list = new List<TestObject> {objectToAdd};

                // Act
                bool updated = list.Update(objectToUpdateWith, (s, i) => s.Name == objectToAdd.Name);

                // Assert
                updated.ShouldBeTrue();
            }

            [Test]
            public void ShouldReturnFalseIfItemToUpdateDoesNotExist()
            {
                // Arrange
                TestObject objectToAdd = TestObjectFaker.Create().Generate();
                TestObject objectToUpdateWith = TestObjectFaker.Create().Generate();

                var list = new List<TestObject> {objectToAdd};

                // Act
                bool updated = list.Update(objectToUpdateWith, (s, i) => s.Name == objectToUpdateWith.Name);

                // Assert
                updated.ShouldBeFalse();
            }
        }

        public class WhenUpdatingCollectionEqualToAnother
        {
            [Test]
            public void ShouldThrowArgumentNullExceptionIfNullCollection()
            {
                // Arrange
                List<string> list = null;

                // Act & Assert
                Assert.Throws<ArgumentNullException>(() => list.MakeEqualTo(null));
            }

            [Test]
            public void ShouldThrowArgumentNullExceptionIfNullSource()
            {
                // Arrange
                var list = new List<string> {"Hello"};

                // Act & Assert
                Assert.Throws<ArgumentNullException>(() => list.MakeEqualTo(null));
            }

            [Test]
            public void ShouldUpdateCollectionToBeEqualOther()
            {
                // Arrange
                var list = new List<string> {"Hello"};
                var update = new List<string> {"New", "List"};

                // Act
                list.MakeEqualTo(update);

                // Assert
                list.ShouldBeEquivalentTo(update);
            }
        }

        public class WhenAddingRangeOfItems
        {
            [Test]
            public void ShouldAddRangeOfItems()
            {
                // Arrange
                List<TestObject> objectsToAdd = TestObjectFaker.Create().Generate(10);

                var collection = new ObservableCollection<TestObject>();

                // Act
                collection.AddRange(objectsToAdd);

                // Assert
                foreach (TestObject item in objectsToAdd)
                {
                    collection.ShouldContain(item);
                }
            }
        }

        public class WhenRemovingRangeOfItems
        {
            [Test]
            public void ShouldRemoveRangeOfItems()
            {
                // Arrange
                List<TestObject> items = TestObjectFaker.Create().Generate(10);
                var itemsToRemove = items.Take(5).ToList();

                var collection = new ObservableCollection<TestObject>(items);

                // Act
                collection.RemoveRange(itemsToRemove);

                // Assert
                foreach (TestObject item in itemsToRemove)
                {
                    collection.ShouldNotContain(item);
                }
            }
        }

        public class WhenDeterminingIfCollectionsAreEquivalent
        {
            [TestCaseSource(nameof(ValidCases))]
            public void ShouldReturnTrueForValidCases(Collection<int> expected, Collection<int> actual)
            {
                CollectionExtensions.AreEquivalent(expected, actual).ShouldBeTrue();
            }

            [TestCaseSource(nameof(InvalidCases))]
            public void ShouldReturnFalseForInvalidCases(Collection<int> expected, Collection<int> actual)
            {
                CollectionExtensions.AreEquivalent(expected, actual).ShouldBeFalse();
            }

            private static object[] ValidCases =
            {
                new object[] {null, null},
                new object[] {new ObservableCollection<int> {1, 2, 3}, new ObservableCollection<int> {1, 2, 3}},
                new object[] {new ObservableCollection<int> {1, 2, 3}, new ObservableCollection<int> {3, 2, 1}},
            };

            private static object[] InvalidCases =
            {
                new object[] {null, new ObservableCollection<int>()},
                new object[] {new ObservableCollection<int>(), null},
                new object[] {new ObservableCollection<int> {1, 2, 3}, new ObservableCollection<int> {4, 5, 6}},
                new object[] {new ObservableCollection<int> {1, 2, 3}, new ObservableCollection<int> {1, 2, 3, 4}},
            };
        }

        public class WhenSortingObservableCollections
        {
            [Test]
            public void ShouldSortBySimpleType()
            {
                // Arrange
                var collection = new ObservableCollection<int> {3, 2, 1};

                // Act
                collection.Sort(x => x);

                // Assert
                collection.ShouldBe(new[] {1, 2, 3});
            }

            [Test]
            public void ShouldSortByComplexType()
            {
                // Arrange
                var collection = new ObservableCollection<ComplexObject>
                {
                    new() {Id = 0, Name = "James Croft"},
                    new() {Id = 1, Name = "Guy Wilmer"},
                    new() {Id = 2, Name = "Ben Hartley"},
                    new() {Id = 3, Name = "Adam Llewellyn"},
                };

                // Act
                collection.Sort(x => x.Name);

                // Assert
                collection.ShouldBe(new ComplexObject[]
                {
                    new() {Id = 3, Name = "Adam Llewellyn"}, new() {Id = 2, Name = "Ben Hartley"},
                    new() {Id = 1, Name = "Guy Wilmer"}, new() {Id = 0, Name = "James Croft"},
                });
            }

            private class ComplexObject : IEquatable<ComplexObject>
            {
                public int Id { get; set; }

                public string Name { get; set; }

                public bool Equals(ComplexObject other)
                {
                    if (ReferenceEquals(null, other))
                    {
                        return false;
                    }

                    if (ReferenceEquals(this, other))
                    {
                        return true;
                    }

                    return this.Id == other.Id && this.Name == other.Name;
                }

                public override bool Equals(object obj)
                {
                    if (ReferenceEquals(null, obj))
                    {
                        return false;
                    }

                    if (ReferenceEquals(this, obj))
                    {
                        return true;
                    }

                    return obj.GetType() == this.GetType() && this.Equals((ComplexObject)obj);
                }

                public override int GetHashCode()
                {
                    return HashCode.Combine(this.Id, this.Name);
                }
            }
        }
    }
}