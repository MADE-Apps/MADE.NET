using System.Diagnostics.CodeAnalysis;
using MADE.Runtime;
using NUnit.Framework;
using Shouldly;

namespace MADE.Runtime.Tests.Tests;

[ExcludeFromCodeCoverage]
[TestFixture]
public class WeakReferenceCallbackTests
{
    public class WhenInvokingCallback
    {
        [Test]
        public void ShouldInvokeCallbackWithParameter()
        {
            // Arrange
            string? capturedValue = null;
            Action<string> action = s => capturedValue = s;
            var callback = new WeakReferenceCallback(action, typeof(string));

            // Act
            callback.Invoke("Hello");

            // Assert
            capturedValue.ShouldBe("Hello");
        }

        [Test]
        public void ShouldReportIsAliveWhenTargetExists()
        {
            // Arrange
            var target = new CallbackTarget();
            Action<string> action = target.Handle;
            var callback = new WeakReferenceCallback(action, typeof(string));

            // Act & Assert
            callback.IsAlive.ShouldBeTrue();
        }

        [Test]
        public void ShouldStoreExpectedType()
        {
            // Arrange
            Action<int> action = _ => { };
            var callback = new WeakReferenceCallback(action, typeof(int));

            // Act & Assert
            callback.Type.ShouldBe(typeof(int));
        }

        [Test]
        public void ShouldThrowWhenCallbackIsNoLongerAlive()
        {
            // Arrange
            var callback = CreateCallbackWithWeakTarget();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            // Act & Assert
            if (!callback.IsAlive)
            {
                Should.Throw<InvalidOperationException>(() => callback.Invoke("test"));
            }
        }
    }

    private static WeakReferenceCallback CreateCallbackWithWeakTarget()
    {
        var target = new CallbackTarget();
        Action<string> action = target.Handle;
        return new WeakReferenceCallback(action, typeof(string));
    }

    private class CallbackTarget
    {
        public string? LastValue { get; private set; }

        public void Handle(string value)
        {
            this.LastValue = value;
        }
    }
}
