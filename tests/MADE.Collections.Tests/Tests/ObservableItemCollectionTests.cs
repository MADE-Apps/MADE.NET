namespace MADE.Collections.Tests.Tests
{
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Threading.Tasks;

    using MADE.Collections.ObjectModel;
    using MADE.Collections.Tests.Fakes;
    using MADE.Testing;

    using NUnit.Framework;

    using Shouldly;

    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class ObservableItemCollectionTests
    {
        public class WhenInitializing
        {
            [Test]
            public void ShouldBeEmptyIfDefaultConstructor()
            {
                // Act
                var collection = new ObservableItemCollection<TestObservableObject>();

                // Assert
                collection.Count.ShouldBe(0);
            }

            [Test]
            public void ShouldContainItemsIfInitializedAsEnumerable()
            {
                // Arrange
                IEnumerable<TestObservableObject> objects = TestObservableObjectFaker.Create().Generate(10);

                // Act
                var collection = new ObservableItemCollection<TestObservableObject>(objects);

                // Assert
                collection.Count.ShouldBe(objects.Count());
                collection.ShouldBeEquivalentTo(objects);
            }

            [Test]
            public void ShouldContainItemsIfInitializedAsList()
            {
                // Arrange
                List<TestObservableObject> objects = TestObservableObjectFaker.Create().Generate(10);

                // Act
                var collection = new ObservableItemCollection<TestObservableObject>(objects);

                // Assert
                collection.Count.ShouldBe(objects.Count());
                collection.ShouldBeEquivalentTo(objects);
            }
        }

        public class WhenAddingItems
        {
            [Test]
            public void ShouldAddRangeOfItems()
            {
                // Arrange
                List<TestObservableObject> objectsToAdd = TestObservableObjectFaker.Create().Generate(10);

                var collection = new ObservableItemCollection<TestObservableObject>();

                // Act
                collection.AddRange(objectsToAdd);

                // Assert
                foreach (TestObservableObject item in objectsToAdd)
                {
                    collection.ShouldContain(item);
                }
            }

            [Test]
            public void ShouldAddSingleItem()
            {
                // Arrange
                TestObservableObject objectToAdd = TestObservableObjectFaker.Create().Generate();

                // Act
                var collection = new ObservableItemCollection<TestObservableObject> { objectToAdd };

                // Assert
                collection.ShouldContain(objectToAdd);
            }

            [Test]
            public async Task ShouldRaiseCollectionChangedEventForAdd()
            {
                // Arrange
                TestObservableObject objectToAdd = TestObservableObjectFaker.Create().Generate();

                var collection = new ObservableItemCollection<TestObservableObject>();

                var tcs = new TaskCompletionSource<NotifyCollectionChangedEventArgs>();

                collection.CollectionChanged += (sender, args) =>
                {
                    tcs.SetResult(args);
                };

                // Act
                collection.Add(objectToAdd);

                // Assert
                NotifyCollectionChangedEventArgs collectionChanged = await tcs.Task;

                collectionChanged.ShouldNotBeNull();
                collectionChanged.NewItems.Contains(objectToAdd).ShouldBeTrue();
            }

            [Test]
            public async Task ShouldRaiseCollectionChangedEventForAddRange()
            {
                // Arrange
                List<TestObservableObject> objectsToAdd = TestObservableObjectFaker.Create().Generate(10);

                var collection = new ObservableItemCollection<TestObservableObject>();

                var tcs = new TaskCompletionSource<NotifyCollectionChangedEventArgs>();

                collection.CollectionChanged += (sender, args) =>
                {
                    tcs.SetResult(args);
                };

                // Act
                collection.AddRange(objectsToAdd);

                // Assert
                NotifyCollectionChangedEventArgs collectionChanged = await tcs.Task;
                collectionChanged.ShouldNotBeNull();
                collectionChanged.NewItems.Count.ShouldBe(objectsToAdd.Count);
                foreach (object item in collectionChanged.NewItems)
                {
                    objectsToAdd.ShouldContain(item);
                }
            }
        }

        public class WhenRemovingItems
        {
            [Test]
            public void ShouldRemoveRangeOfItems()
            {
                // Arrange
                List<TestObservableObject> items = TestObservableObjectFaker.Create().Generate(10);
                var itemsToRemove = items.Take(5).ToList();

                var collection = new ObservableItemCollection<TestObservableObject>(items);

                // Act
                collection.RemoveRange(itemsToRemove);

                // Assert
                foreach (TestObservableObject item in itemsToRemove)
                {
                    collection.ShouldNotContain(item);
                }
            }

            [Test]
            public void ShouldRemoveSingleItem()
            {
                // Arrange
                TestObservableObject objectToRemove = TestObservableObjectFaker.Create().Generate();
                var collection = new ObservableItemCollection<TestObservableObject> { objectToRemove };

                // Act
                collection.Remove(objectToRemove);

                // Assert
                collection.ShouldNotContain(objectToRemove);
            }

            [Test]
            public async Task ShouldRaiseCollectionChangedEventForRemove()
            {
                // Arrange
                TestObservableObject objectToRemove = TestObservableObjectFaker.Create().Generate();
                var collection = new ObservableItemCollection<TestObservableObject> { objectToRemove };

                var tcs = new TaskCompletionSource<NotifyCollectionChangedEventArgs>();

                collection.CollectionChanged += (sender, args) =>
                {
                    tcs.SetResult(args);
                };

                // Act
                collection.Remove(objectToRemove);

                // Assert
                NotifyCollectionChangedEventArgs collectionChanged = await tcs.Task;

                collectionChanged.ShouldNotBeNull();
                collectionChanged.OldItems.Contains(objectToRemove).ShouldBeTrue();
            }

            [Test]
            public async Task ShouldRaiseCollectionChangedEventForRemoveRange()
            {
                // Arrange
                List<TestObservableObject> objectsToRemove = TestObservableObjectFaker.Create().Generate(10);

                var collection = new ObservableItemCollection<TestObservableObject>();

                var tcs = new TaskCompletionSource<NotifyCollectionChangedEventArgs>();

                collection.CollectionChanged += (sender, args) =>
                {
                    tcs.SetResult(args);
                };

                // Act
                collection.RemoveRange(objectsToRemove);

                // Assert
                NotifyCollectionChangedEventArgs collectionChanged = await tcs.Task;
                collectionChanged.ShouldNotBeNull();
                collectionChanged.OldItems.Count.ShouldBe(objectsToRemove.Count);
                foreach (object item in collectionChanged.OldItems)
                {
                    objectsToRemove.ShouldContain(item);
                }
            }
        }

        public class WhenItemPropertyChanges
        {
            [Test]
            public async Task ShouldRaisePropertyChangedEvent()
            {
                // Arrange
                TestObservableObject obj = TestObservableObjectFaker.Create().Generate();

                var collection = new ObservableItemCollection<TestObservableObject> { obj };

                var tcs = new TaskCompletionSource<ObservableItemCollectionPropertyChangedEventArgs>();

                collection.ItemPropertyChanged += (sender, args) =>
                {
                    tcs.SetResult(args);
                };

                // Act
                obj.Name = "Hello, World!";

                // Assert
                ObservableItemCollectionPropertyChangedEventArgs result = await tcs.Task;

                result.ShouldNotBeNull();
                result.Sender.ShouldBe(obj);
                result.EventArgs.ShouldNotBeNull();
                result.EventArgs.PropertyName.ShouldBe(nameof(TestObservableObject.Name));
            }
        }
    }
}