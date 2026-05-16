using System.Diagnostics.CodeAnalysis;
using MADE.Foundation.Platform;
using NUnit.Framework;
using Shouldly;

namespace MADE.Foundation.Tests.Tests;

[ExcludeFromCodeCoverage]
[TestFixture]
public class PlatformNotSupportedAttributeTests
{
    [Test]
    public void ShouldBeApplicableToAllTargets()
    {
        // Arrange
        var attribute = typeof(PlatformNotSupportedAttribute)
            .GetCustomAttributes(typeof(AttributeUsageAttribute), false)
            .Cast<AttributeUsageAttribute>()
            .Single();

        // Assert
        attribute.ValidOn.ShouldBe(AttributeTargets.All);
        attribute.Inherited.ShouldBeFalse();
    }

    [Test]
    public void ShouldBeConstructable()
    {
        // Arrange & Act
        var attribute = new PlatformNotSupportedAttribute();

        // Assert
        attribute.ShouldNotBeNull();
    }
}
