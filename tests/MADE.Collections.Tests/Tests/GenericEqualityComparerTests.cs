namespace MADE.Collections.Tests.Tests
{
    using System.Diagnostics.CodeAnalysis;

    using MADE.Collections.Compare;
    using MADE.Collections.Tests.Fakes;

    using NUnit.Framework;

    using Shouldly;

    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class GenericEqualityComparerTests
    {
        public class WhenComparingObjects
        {
            [Test]
            public void ShouldReturnTrueIfObjectsSameReferenceAndComparingOnObject()
            {
                // Arrange
                string objectName = "Hello, World!";
                int objectCount = 10;

                var first = new TestObject { Name = objectName, Count = objectCount };
                TestObject second = first;

                var comparer = new GenericEqualityComparer<TestObject>(o => o);

                // Act
                bool areEqual = comparer.Equals(first, second);

                // Assert
                areEqual.ShouldBeTrue();
            }

            [Test]
            public void ShouldReturnTrueIfObjectsSimilarImplementIEquatableAndComparingOnObject()
            {
                // Arrange
                string objectName = "Hello, World!";
                int objectCount = 10;

                var first = new TestEqualityObject { Name = objectName, Count = objectCount };
                var second = new TestEqualityObject { Name = objectName, Count = objectCount };

                var comparer = new GenericEqualityComparer<TestEqualityObject>(o => o);

                // Act
                bool areEqual = comparer.Equals(first, second);

                // Assert
                areEqual.ShouldBeTrue();
            }

            [Test]
            public void ShouldReturnTrueIfObjectsSimilarAndComparingOnPropertyWithSameValue()
            {
                // Arrange
                string objectName = "Hello, World!";
                int objectCount = 10;

                var first = new TestObject { Name = objectName, Count = objectCount };
                var second = new TestObject { Name = objectName, Count = objectCount };

                var comparer = new GenericEqualityComparer<TestObject>(o => o.Name);

                // Act
                bool areEqual = comparer.Equals(first, second);

                // Assert
                areEqual.ShouldBeTrue();
            }

            [Test]
            public void ShouldReturnTrueIfObjectsDifferentAndComparingOnPropertyWithSameValue()
            {
                // Arrange
                int count = 10;
                var first = new TestObject { Name = "Hello, World", Count = count };
                var second = new TestObject { Name = "World, Hello", Count = count };

                var comparer = new GenericEqualityComparer<TestObject>(o => o.Count);

                // Act
                bool areEqual = comparer.Equals(first, second);

                // Assert
                areEqual.ShouldBeTrue();
            }

            [Test]
            public void ShouldReturnFaleIfObjectsDifferentAndComparingOnObject()
            {
                // Arrange
                var first = new TestObject { Name = "Hello, World", Count = 10 };
                var second = new TestObject { Name = "World, Hello", Count = 5 };

                var comparer = new GenericEqualityComparer<TestObject>(o => o);

                // Act
                bool areEqual = comparer.Equals(first, second);

                // Assert
                areEqual.ShouldBeFalse();
            }

            [Test]
            public void ShouldReturnFalseIfObjectsDifferentImplementIEquatableAndComparingOnObject()
            {
                // Arrange
                var first = new TestEqualityObject { Name = "Hello, World", Count = 10 };
                var second = new TestEqualityObject { Name = "World, Hello", Count = 5 };

                var comparer = new GenericEqualityComparer<TestEqualityObject>(o => o);

                // Act
                bool areEqual = comparer.Equals(first, second);

                // Assert
                areEqual.ShouldBeFalse();
            }

            [Test]
            public void ShouldReturnFalseIfObjectsDifferentAndComparingOnPropertyWithDifferentValue()
            {
                // Arrange
                var first = new TestObject { Name = "Hello, World", Count = 10 };
                var second = new TestObject { Name = "World, Hello", Count = 5 };

                var comparer = new GenericEqualityComparer<TestObject>(o => o.Name);

                // Act
                bool areEqual = comparer.Equals(first, second);

                // Assert
                areEqual.ShouldBeFalse();
            }
        }

        public class WhenRetrievingHashCode
        {
            [Test]
            public void ShouldReturnSameHashCodeWhenComparingOnObject()
            {
                // Arrange
                string objectName = "Hello, World!";
                int objectCount = 10;

                var obj = new TestObject { Name = objectName, Count = objectCount };

                var comparer = new GenericEqualityComparer<TestObject>(o => o);

                // Act
                int hashCode = comparer.GetHashCode(obj);

                // Assert
                hashCode.ShouldBe(obj.GetHashCode());
            }

            [Test]
            public void ShouldReturnSameHashCodeWhenComparingOnProperty()
            {
                // Arrange
                string objectName = "Hello, World!";
                int objectCount = 10;

                var obj = new TestObject { Name = objectName, Count = objectCount };

                var comparer = new GenericEqualityComparer<TestObject>(o => o.Name);

                // Act
                int hashCode = comparer.GetHashCode(obj);

                // Assert
                hashCode.ShouldBe(obj.Name.GetHashCode());
            }
        }
    }
}