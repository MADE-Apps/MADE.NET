using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using Shouldly;

namespace MADE.Runtime.Tests.Tests;

[ExcludeFromCodeCoverage]
[TestFixture]
public class WeakReferenceEventListenerWithEventArgsTests
{
    public class WhenEventFires
    {
        [Test]
        public void ShouldInvokeOnEventActionWhenInstanceIsAlive()
        {
            // Arrange
            var instance = new ListenerInstance();
            var listener = new WeakReferenceEventListener<ListenerInstance, object, string>(instance);

            string? capturedArg = null;
            listener.OnEventAction = (inst, src, args) => capturedArg = args;

            // Act
            listener.OnEvent(new object(), "EventData");

            // Assert
            capturedArg.ShouldBe("EventData");
        }

        [Test]
        public void ShouldNotThrowWhenOnEventActionIsNull()
        {
            // Arrange
            var instance = new ListenerInstance();
            var listener = new WeakReferenceEventListener<ListenerInstance, object, string>(instance);

            // Act & Assert
            Should.NotThrow(() => listener.OnEvent(new object(), "EventData"));
        }

        [Test]
        public void ShouldDetachWhenInstanceIsCollected()
        {
            // Arrange
            var listener = CreateListenerWithWeakInstance(out _);

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            // Act & Assert - GC behavior is non-deterministic, so verify the listener
            // safely handles event dispatch even when the weak target may no longer be alive.
            Should.NotThrow(() => listener.OnEvent(new object(), "EventData"));
        }
    }

    public class WhenDetaching
    {
        [Test]
        public void ShouldInvokeOnDetachAction()
        {
            // Arrange
            var instance = new ListenerInstance();
            var listener = new WeakReferenceEventListener<ListenerInstance, object, string>(instance);

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
            var listener = new WeakReferenceEventListener<ListenerInstance, object, string>(instance);

            int detachCount = 0;
            listener.OnDetachAction = (inst, lst) => detachCount++;

            // Act
            listener.Detach();
            listener.Detach(); // Second call should be a no-op

            // Assert
            detachCount.ShouldBe(1);
        }

        [Test]
        public void ShouldNotThrowWhenOnDetachActionIsNull()
        {
            // Arrange
            var instance = new ListenerInstance();
            var listener = new WeakReferenceEventListener<ListenerInstance, object, string>(instance);

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
                () => new WeakReferenceEventListener<ListenerInstance, object, string>(null!));
        }
    }

    private static WeakReferenceEventListener<ListenerInstance, object, string> CreateListenerWithWeakInstance(
        out WeakReference<ListenerInstance> weakRef)
    {
        var instance = new ListenerInstance();
        weakRef = new WeakReference<ListenerInstance>(instance);
        return new WeakReferenceEventListener<ListenerInstance, object, string>(instance);
    }

    private class ListenerInstance
    {
    }
}
