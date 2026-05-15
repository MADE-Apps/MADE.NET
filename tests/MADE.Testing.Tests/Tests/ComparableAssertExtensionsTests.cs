using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using Shouldly;

namespace MADE.Testing.Tests.Tests;

[ExcludeFromCodeCoverage]
[TestFixture]
public class ComparableAssertExtensionsTests
{
    public class WhenAssertingShouldBeGreaterThan
    {
        [Test]
        public void ShouldPassWhenGreater()
        {
            Should.NotThrow(() => 10.ShouldBeGreaterThan(5));
        }

        [Test]
        public void ShouldFailWhenEqual()
        {
            Should.Throw<AssertFailedException>(() => 5.ShouldBeGreaterThan(5));
        }

        [Test]
        public void ShouldFailWhenLess()
        {
            Should.Throw<AssertFailedException>(() => 3.ShouldBeGreaterThan(5));
        }
    }

    public class WhenAssertingShouldBeLessThan
    {
        [Test]
        public void ShouldPassWhenLess()
        {
            Should.NotThrow(() => 3.ShouldBeLessThan(5));
        }

        [Test]
        public void ShouldFailWhenEqual()
        {
            Should.Throw<AssertFailedException>(() => 5.ShouldBeLessThan(5));
        }

        [Test]
        public void ShouldFailWhenGreater()
        {
            Should.Throw<AssertFailedException>(() => 10.ShouldBeLessThan(5));
        }
    }

    public class WhenAssertingShouldBeGreaterThanOrEqualTo
    {
        [Test]
        public void ShouldPassWhenGreater()
        {
            Should.NotThrow(() => 10.ShouldBeGreaterThanOrEqualTo(5));
        }

        [Test]
        public void ShouldPassWhenEqual()
        {
            Should.NotThrow(() => 5.ShouldBeGreaterThanOrEqualTo(5));
        }

        [Test]
        public void ShouldFailWhenLess()
        {
            Should.Throw<AssertFailedException>(() => 3.ShouldBeGreaterThanOrEqualTo(5));
        }
    }

    public class WhenAssertingShouldBeLessThanOrEqualTo
    {
        [Test]
        public void ShouldPassWhenLess()
        {
            Should.NotThrow(() => 3.ShouldBeLessThanOrEqualTo(5));
        }

        [Test]
        public void ShouldPassWhenEqual()
        {
            Should.NotThrow(() => 5.ShouldBeLessThanOrEqualTo(5));
        }

        [Test]
        public void ShouldFailWhenGreater()
        {
            Should.Throw<AssertFailedException>(() => 10.ShouldBeLessThanOrEqualTo(5));
        }
    }
}
