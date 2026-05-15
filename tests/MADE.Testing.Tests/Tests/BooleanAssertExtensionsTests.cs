using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using Shouldly;

namespace MADE.Testing.Tests.Tests;

[ExcludeFromCodeCoverage]
[TestFixture]
public class BooleanAssertExtensionsTests
{
    public class WhenAssertingShouldBeTrue
    {
        [Test]
        public void ShouldPassForTrue()
        {
            Should.NotThrow(() => true.ShouldBeTrue());
        }

        [Test]
        public void ShouldFailForFalse()
        {
            Should.Throw<AssertFailedException>(() => false.ShouldBeTrue());
        }
    }

    public class WhenAssertingShouldBeFalse
    {
        [Test]
        public void ShouldPassForFalse()
        {
            Should.NotThrow(() => false.ShouldBeFalse());
        }

        [Test]
        public void ShouldFailForTrue()
        {
            Should.Throw<AssertFailedException>(() => true.ShouldBeFalse());
        }
    }
}
