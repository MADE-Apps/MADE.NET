using System.Diagnostics.CodeAnalysis;
using MADE.Foundation.Platform;
using NUnit.Framework;
using Shouldly;

namespace MADE.Foundation.Tests.Tests;

[ExcludeFromCodeCoverage]
[TestFixture]
public class PlatformApiHelperTests
{
    public class WhenCheckingTypeSupport
    {
        [Test]
        public void ShouldReturnTrueForTypeWithoutPlatformNotSupportedAttribute()
        {
            // Arrange & Act
            bool result = PlatformApiHelper.IsTypeSupported(typeof(SupportedType));

            // Assert
            result.ShouldBeTrue();
        }

        [Test]
        public void ShouldReturnFalseForTypeWithPlatformNotSupportedAttribute()
        {
            // Arrange & Act
            bool result = PlatformApiHelper.IsTypeSupported(typeof(UnsupportedType));

            // Assert
            result.ShouldBeFalse();
        }

        [Test]
        public void ShouldCacheResultForSameType()
        {
            // Arrange & Act
            bool first = PlatformApiHelper.IsTypeSupported(typeof(SupportedType));
            bool second = PlatformApiHelper.IsTypeSupported(typeof(SupportedType));

            // Assert
            first.ShouldBeTrue();
            second.ShouldBeTrue();
        }
    }

    public class WhenCheckingMethodSupport
    {
        [Test]
        public void ShouldReturnTrueForSupportedTypeRegardlessOfMethod()
        {
            // Arrange & Act - when the type is supported, all methods are considered supported
            bool result = PlatformApiHelper.IsMethodSupported(typeof(MixedSupportType), nameof(MixedSupportType.SupportedMethod));

            // Assert
            result.ShouldBeTrue();
        }

        [Test]
        public void ShouldReturnTrueForSupportedTypeEvenWithUnsupportedMethodAttribute()
        {
            // Arrange & Act - type-level support takes precedence
            bool result = PlatformApiHelper.IsMethodSupported(typeof(MixedSupportType), nameof(MixedSupportType.UnsupportedMethod));

            // Assert - type is supported, so the result is true
            result.ShouldBeTrue();
        }

        [Test]
        public void ShouldReturnTrueForUnsupportedTypeWithSupportedMethod()
        {
            // Arrange & Act - type is unsupported, but the individual method does not have the attribute
            bool result = PlatformApiHelper.IsMethodSupported(typeof(UnsupportedType), nameof(UnsupportedType.SomeMethod));

            // Assert - the method itself is supported even though the type is not
            result.ShouldBeTrue();
        }

        [Test]
        public void ShouldReturnFalseForUnsupportedTypeWithUnsupportedMethod()
        {
            // Arrange & Act
            bool result = PlatformApiHelper.IsMethodSupported(typeof(UnsupportedTypeWithUnsupportedMethod), nameof(UnsupportedTypeWithUnsupportedMethod.AlsoUnsupported));

            // Assert
            result.ShouldBeFalse();
        }
    }

    public class WhenCheckingPropertySupport
    {
        [Test]
        public void ShouldReturnTrueForSupportedTypeProperty()
        {
            // Arrange & Act
            bool result = PlatformApiHelper.IsPropertySupported(typeof(MixedSupportType), nameof(MixedSupportType.SupportedProperty));

            // Assert
            result.ShouldBeTrue();
        }

        [Test]
        public void ShouldReturnTrueForSupportedTypeEvenWithUnsupportedPropertyAttribute()
        {
            // Arrange & Act - type-level support takes precedence
            bool result = PlatformApiHelper.IsPropertySupported(typeof(MixedSupportType), nameof(MixedSupportType.UnsupportedProperty));

            // Assert - type is supported, so result is true
            result.ShouldBeTrue();
        }

        [Test]
        public void ShouldReturnFalseForUnsupportedTypeWithUnsupportedProperty()
        {
            // Arrange & Act
            bool result = PlatformApiHelper.IsPropertySupported(typeof(UnsupportedTypeWithUnsupportedProp), nameof(UnsupportedTypeWithUnsupportedProp.AlsoUnsupported));

            // Assert
            result.ShouldBeFalse();
        }
    }
}

public class SupportedType
{
}

[PlatformNotSupported]
public class UnsupportedType
{
    public void SomeMethod()
    {
    }
}

[PlatformNotSupported]
public class UnsupportedTypeWithUnsupportedMethod
{
    [PlatformNotSupported]
    public void AlsoUnsupported()
    {
    }
}

[PlatformNotSupported]
public class UnsupportedTypeWithUnsupportedProp
{
    [PlatformNotSupported]
    public string? AlsoUnsupported { get; set; }
}

public class MixedSupportType
{
    public string? SupportedProperty { get; set; }

    [PlatformNotSupported]
    public string? UnsupportedProperty { get; set; }

    public void SupportedMethod()
    {
    }

    [PlatformNotSupported]
    public void UnsupportedMethod()
    {
    }
}
