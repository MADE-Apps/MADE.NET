namespace MADE.Collections.Tests.Tests
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using MADE.Collections.Generic;
    using NUnit.Framework;
    using Shouldly;

    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class LimitedListTests
    {
        public class WhenConstructingLimitedLists
        {
            [Test]
            public void ShouldSetLimit()
            {
                // Arrange
                const int Limit = 10;

                // Act
                var limitedList = new LimitedList<int>(Limit);

                // Assert
                limitedList.Limit.ShouldBe(Limit);
            }
        }

        public class WhenAddingItems
        {
            [Test]
            public void ShouldThrowInvalidOperationExceptionIfExceedsLimit()
            {
                // Arrange
                var limitedList = new LimitedList<int>(1) { 1 };

                // Act & Assert
                Should.Throw<InvalidOperationException>(() => limitedList.Add(2));
            }
        }

        public class WhenInsertingItems
        {
            [Test]
            public void ShouldThrowInvalidOperationExceptionIfExceedsLimit()
            {
                // Arrange
                var limitedList = new LimitedList<int>(1) { 1 };

                // Act & Assert
                Should.Throw<InvalidOperationException>(() => limitedList.Insert(0, 2));
            }
        }
    }
}