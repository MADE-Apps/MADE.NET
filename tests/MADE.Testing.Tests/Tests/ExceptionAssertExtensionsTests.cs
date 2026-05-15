using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using Shouldly;

namespace MADE.Testing.Tests.Tests;

[ExcludeFromCodeCoverage]
[TestFixture]
public class ExceptionAssertExtensionsTests
{
    public class WhenAssertingShouldThrow
    {
        [Test]
        public void ShouldPassWhenExpectedExceptionIsThrown()
        {
            Action action = () => throw new InvalidOperationException("test");
            var ex = action.ShouldThrow<InvalidOperationException>();
            ex.Message.ShouldBe("test");
        }

        [Test]
        public void ShouldFailWhenNoExceptionIsThrown()
        {
            Action action = () => { };
            Should.Throw<AssertFailedException>(() => action.ShouldThrow<InvalidOperationException>());
        }

        [Test]
        public void ShouldFailWhenDifferentExceptionIsThrown()
        {
            Action action = () => throw new ArgumentException("wrong");
            Should.Throw<AssertFailedException>(() => action.ShouldThrow<InvalidOperationException>());
        }
    }

    public class WhenAssertingShouldThrowAsync
    {
        [Test]
        public async Task ShouldPassWhenExpectedExceptionIsThrown()
        {
            Func<Task> action = () => throw new InvalidOperationException("async test");
            var ex = await action.ShouldThrowAsync<InvalidOperationException>();
            ex.Message.ShouldBe("async test");
        }
    }

    public class WhenAssertingShouldNotThrow
    {
        [Test]
        public void ShouldPassWhenNoExceptionIsThrown()
        {
            Action action = () => { };
            Should.NotThrow(() => action.ShouldNotThrow());
        }

        [Test]
        public void ShouldFailWhenExceptionIsThrown()
        {
            Action action = () => throw new InvalidOperationException();
            Should.Throw<AssertFailedException>(() => action.ShouldNotThrow());
        }
    }

    public class WhenAssertingShouldNotThrowAsync
    {
        [Test]
        public async Task ShouldPassWhenNoExceptionIsThrown()
        {
            Func<Task> action = () => Task.CompletedTask;
            await Should.NotThrowAsync(() => action.ShouldNotThrowAsync());
        }

        [Test]
        public async Task ShouldFailWhenExceptionIsThrown()
        {
            Func<Task> action = () => throw new InvalidOperationException();
            await Should.ThrowAsync<AssertFailedException>(() => action.ShouldNotThrowAsync());
        }
    }
}
