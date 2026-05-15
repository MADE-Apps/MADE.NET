using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using Shouldly;

namespace MADE.Testing.Tests.Tests;

[ExcludeFromCodeCoverage]
[TestFixture]
public class StringAssertExtensionsTests
{
    public class WhenAssertingShouldContain
    {
        [Test]
        public void ShouldPassWhenContains()
        {
            Should.NotThrow(() => "Hello, World!".ShouldContain("World"));
        }

        [Test]
        public void ShouldFailWhenNotContains()
        {
            Should.Throw<AssertFailedException>(() => "Hello".ShouldContain("World"));
        }

        [Test]
        public void ShouldFailForNull()
        {
            string? value = null;
            Should.Throw<AssertFailedException>(() => value.ShouldContain("test"));
        }
    }

    public class WhenAssertingShouldNotContain
    {
        [Test]
        public void ShouldPassWhenNotContains()
        {
            Should.NotThrow(() => "Hello".ShouldNotContain("World"));
        }

        [Test]
        public void ShouldFailWhenContains()
        {
            Should.Throw<AssertFailedException>(() => "Hello, World!".ShouldNotContain("World"));
        }
    }

    public class WhenAssertingShouldStartWith
    {
        [Test]
        public void ShouldPassWhenStartsWith()
        {
            Should.NotThrow(() => "Hello, World!".ShouldStartWith("Hello"));
        }

        [Test]
        public void ShouldFailWhenDoesNotStartWith()
        {
            Should.Throw<AssertFailedException>(() => "Hello".ShouldStartWith("World"));
        }
    }

    public class WhenAssertingShouldEndWith
    {
        [Test]
        public void ShouldPassWhenEndsWith()
        {
            Should.NotThrow(() => "Hello, World!".ShouldEndWith("World!"));
        }

        [Test]
        public void ShouldFailWhenDoesNotEndWith()
        {
            Should.Throw<AssertFailedException>(() => "Hello".ShouldEndWith("World"));
        }
    }
}
