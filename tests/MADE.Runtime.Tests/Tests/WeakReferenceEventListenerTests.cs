using System.Diagnostics.CodeAnalysis;
using MADE.Runtime;
using NUnit.Framework;
using Shouldly;

namespace MADE.Runtime.Tests.Tests;

[ExcludeFromCodeCoverage]
[TestFixture]
public class WeakReferenceEventListenerTests
{
    public class WhenEventFires
    {
        [Test]
        public void ShouldInvokeOnEventActionWhenInstanceIsAlive()
        {
            // Arrange
            var instance = new ListenerInstance();
            var listener = new WeakReferenceEventListener<ListenerInstance, string>(instance);

            string? capturedSource = null;
            listener.OnEventAction = (inst, src) => capturedSource = src;

            // Act
            listener.OnEvent("SourceData");

            // Assert
            capturedSource.ShouldBe("SourceData");
        }

        [Test]
        public void ShouldNotThrowWhenOnEventActionIsNull()
        {
            // Arrange
            var instance = new ListenerInstance();
            var listener = new WeakReferenceEventListener<ListenerInstance, string>(instance);

            // Act & Assert
            Should.NotThrow(() => listener.OnEvent("SourceData"));
        }
    }

    public class WhenDetaching
    {
        [Test]
        public void ShouldInvokeOnDetachAction()
        {
            // Arrange
            var instance = new ListenerInstance();
            var listener = new WeakReferenceEventListener<ListenerInstance, string>(instance);

            bool detachCalled = false;
            listener.OnDetachAction = (inst, lst) => detachCalled = true;

            // Act
            listener.Detach();

            // Assert
            detachCalled.ShouldBeTrue();
        }

        [Test]
        public void ShouldClearOnDetachActionAfterDetach()
        {
            // Arrange
            var instance = new ListenerInstance();
            var listener = new WeakReferenceEventListener<ListenerInstance, string>(instance);

            int detachCount = 0;
            listener.OnDetachAction = (inst, lst) => detachCount++;

            // Act
            listener.Detach();
            listener.Detach();

            // Assert
            detachCount.ShouldBe(1);
        }

        [Test]
        public void ShouldNotThrowWhenOnDetachActionIsNull()
        {
            // Arrange
            var instance = new ListenerInstance();
            var listener = new WeakReferenceEventListener<ListenerInstance, string>(instance);

            // Act & Assert
            Should.NotThrow(() => listener.Detach());
        }
    }

    public class WhenConstructing
    {
        [Test]
        public void ShouldThrowWhenInstanceIsNull()
        {
            // Act & Assert
            Should.Throw<ArgumentNullException>(
                () => new WeakReferenceEventListener<ListenerInstance, string>(null!));
        }
    }

    private class ListenerInstance
    {
    }
}
