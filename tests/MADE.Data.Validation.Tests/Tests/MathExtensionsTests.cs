namespace MADE.Data.Validation.Tests.Tests
{
    using System.Diagnostics.CodeAnalysis;
    using Extensions;
    using NUnit.Framework;
    using Shouldly;

    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class MathExtensionsTests
    {
        public class WhenCheckingIfValueIsZero
        {
            [Test]
            public void ShouldReturnTrueIfDoubleZero()
            {
                // Act
                bool isZero = 0d.IsZero();

                // Assert
                isZero.ShouldBeTrue();
            }

            [TestCase(0.05d)]
            [TestCase(-0.05d)]
            public void ShouldReturnFalseIfDoubleNotZero(double value)
            {
                // Act
                bool isZero = value.IsZero();

                // Assert
                isZero.ShouldBeFalse();
            }

            [Test]
            public void ShouldReturnTrueIfFloatZero()
            {
                // Act
                bool isZero = 0f.IsZero();

                // Assert
                isZero.ShouldBeTrue();
            }

            [TestCase(0.05f)]
            [TestCase(-0.05f)]
            public void ShouldReturnFalseIfFloatNotZero(float value)
            {
                // Act
                bool isZero = value.IsZero();

                // Assert
                isZero.ShouldBeFalse();
            }
        }

        public class WhenCheckingIfValueIsCloseToAnother
        {
            [TestCase(1, 1)]
            public void ShouldReturnTrueIfIntClose(int value, int compare)
            {
                // Act
                bool isCloseTo = value.IsCloseTo(compare);

                // Assert
                isCloseTo.ShouldBeTrue();
            }

            [TestCase(1, 2)]
            public void ShouldReturnFalseIfIntNotClose(int value, int compare)
            {
                // Act
                bool isCloseTo = value.IsCloseTo(compare);

                // Assert
                isCloseTo.ShouldBeFalse();
            }

            [TestCase(0.005d, 0.005d)]
            [TestCase(0.00000005d, 0.00000005d)]
            public void ShouldReturnTrueIfDoubleClose(double value, double compare)
            {
                // Act
                bool isCloseTo = value.IsCloseTo(compare);

                // Assert
                isCloseTo.ShouldBeTrue();
            }

            [TestCase(1.005d, 0.005d)]
            [TestCase(1.00000005d, 0.00000005d)]
            public void ShouldReturnFalseIfDoubleNotClose(double value, double compare)
            {
                // Act
                bool isCloseTo = value.IsCloseTo(compare);

                // Assert
                isCloseTo.ShouldBeFalse();
            }

            [TestCase(0.005d, 0.005d)]
            [TestCase(0.00000005d, 0.00000005d)]
            public void ShouldReturnTrueIfNullableDoubleClose(double? value, double? compare)
            {
                // Act
                bool isCloseTo = value.IsCloseTo(compare);

                // Assert
                isCloseTo.ShouldBeTrue();
            }

            [TestCase(null, 0.005d)]
            [TestCase(1.005d, null)]
            [TestCase(1.005d, 0.005d)]
            [TestCase(1.00000005d, 0.00000005d)]
            public void ShouldReturnFalseIfNullableDoubleNotClose(double? value, double? compare)
            {
                // Act
                bool isCloseTo = value.IsCloseTo(compare);

                // Assert
                isCloseTo.ShouldBeFalse();
            }

            [TestCase(0.005f, 0.005f)]
            [TestCase(0.00000005f, 0.00000005f)]
            public void ShouldReturnTrueIfFloatClose(float value, float compare)
            {
                // Act
                bool isCloseTo = value.IsCloseTo(compare);

                // Assert
                isCloseTo.ShouldBeTrue();
            }

            [TestCase(1.005f, 0.005f)]
            [TestCase(1.00000005f, 0.00000005f)]
            public void ShouldReturnFalseIfFloatNotClose(float value, float compare)
            {
                // Act
                bool isCloseTo = value.IsCloseTo(compare);

                // Assert
                isCloseTo.ShouldBeFalse();
            }

            [TestCase(0.005f, 0.005f)]
            [TestCase(0.00000005f, 0.00000005f)]
            public void ShouldReturnTrueIfNullableFloatClose(float? value, float? compare)
            {
                // Act
                bool isCloseTo = value.IsCloseTo(compare);

                // Assert
                isCloseTo.ShouldBeTrue();
            }

            [TestCase(null, 0.005f)]
            [TestCase(1.005f, null)]
            [TestCase(1.005f, 0.005f)]
            [TestCase(1.00000005f, 0.00000005f)]
            public void ShouldReturnFalseIfNullableFloatNotClose(float? value, float? compare)
            {
                // Act
                bool isCloseTo = value.IsCloseTo(compare);

                // Assert
                isCloseTo.ShouldBeFalse();
            }
        }

        public class WhenCheckingIfValueIsGreaterThan
        {
            [TestCase(0.05d, 0.000005d)]
            [TestCase(1d, 0.99999999d)]
            public void ShouldReturnTrueIfGreaterThan(double value, double compare)
            {
                // Act
                bool isGreaterThan = value.IsGreaterThan(compare);

                // Assert
                isGreaterThan.ShouldBeTrue();
            }

            [TestCase(0.000005d, 0.05d)]
            [TestCase(0.99999999d, 1d)]
            public void ShouldReturnFalseIfNotGreaterThan(double value, double compare)
            {
                // Act
                bool isGreaterThan = value.IsGreaterThan(compare);

                // Assert
                isGreaterThan.ShouldBeFalse();
            }
        }

        public class WhenCheckingIfValueIsLessThan
        {
            [TestCase(0.000005d, 0.05d)]
            [TestCase(0.99999999d, 1d)]
            public void ShouldReturnTrueIfLessThan(double value, double compare)
            {
                // Act
                bool isGreaterThan = value.IsLessThan(compare);

                // Assert
                isGreaterThan.ShouldBeTrue();
            }

            [TestCase(0.05d, 0.000005d)]
            [TestCase(1d, 0.99999999d)]
            public void ShouldReturnFalseIfNotLessThan(double value, double compare)
            {
                // Act
                bool isGreaterThan = value.IsLessThan(compare);

                // Assert
                isGreaterThan.ShouldBeFalse();
            }
        }
    }
}