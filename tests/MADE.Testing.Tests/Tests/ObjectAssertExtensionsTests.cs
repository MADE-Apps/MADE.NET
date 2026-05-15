using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using Shouldly;

namespace MADE.Testing.Tests.Tests;

[ExcludeFromCodeCoverage]
[TestFixture]
public class ObjectAssertExtensionsTests
{
    public class WhenAssertingShouldBeNull
    {
        [Test]
        public void ShouldPassForNullValue()
        {
            object? value = null;
            Should.NotThrow(() => value.ShouldBeNull());
        }

        [Test]
        public void ShouldFailForNonNullValue()
        {
            object value = new();
            Should.Throw<AssertFailedException>(() => value.ShouldBeNull());
        }
    }

    public class WhenAssertingShouldNotBeNull
    {
        [Test]
        public void ShouldPassForNonNullValue()
        {
            object value = new();
            Should.NotThrow(() => value.ShouldNotBeNull());
        }

        [Test]
        public void ShouldFailForNullValue()
        {
            object? value = null;
            Should.Throw<AssertFailedException>(() => value.ShouldNotBeNull());
        }
    }
}
