namespace MADE.Data.Validation.Tests.Tests
{
    using System.Diagnostics.CodeAnalysis;
    using Extensions;

    using MADE.Data.Validation.Exceptions;

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

        public class WhenCheckingIfValueIsInRange
        {
            [TestCase(0, 0, 2)]
            [TestCase(1, 0, 2)]
            [TestCase(2, 0, 2)]
            public void ShouldReturnTrueIfIntInRange(int value, int lower, int upper)
            {
                // Act
                bool isInRange = value.IsInRange(lower, upper);

                // Assert
                isInRange.ShouldBeTrue();
            }

            [TestCase(-1, 0, 1)]
            [TestCase(2, 0, 1)]
            public void ShouldReturnFalseIfIntNotInRange(int value, int lower, int upper)
            {
                // Act
                bool isInRange = value.IsInRange(lower, upper);

                // Assert
                isInRange.ShouldBeFalse();
            }

            [Test]
            public void ShouldThrowInvalidRangeExceptionIfIntRangeInvalid()
            {
                Assert.Throws<InvalidRangeException>(
                    () =>
                    {
                        bool isInRange = 0.IsInRange(3, 1);
                    });
            }

            [TestCase(0, 0, 1)]
            [TestCase(1, 0, 1)]
            [TestCase(0.0000001f, 0, 1)]
            [TestCase(0.9999999f, 0, 1)]
            public void ShouldReturnTrueIfFloatInRange(float value, float lower, float upper)
            {
                // Act
                bool isInRange = value.IsInRange(lower, upper);

                // Assert
                isInRange.ShouldBeTrue();
            }

            [TestCase(-0.0000001f, 0, 1)]
            [TestCase(1.0000001f, 0, 1)]
            public void ShouldReturnFalseIfFloatNotInRange(float value, float lower, float upper)
            {
                // Act
                bool isInRange = value.IsInRange(lower, upper);

                // Assert
                isInRange.ShouldBeFalse();
            }

            [Test]
            public void ShouldThrowInvalidRangeExceptionIfFloatRangeInvalid()
            {
                Assert.Throws<InvalidRangeException>(
                    () =>
                    {
                        bool isInRange = 0f.IsInRange(3, 1);
                    });
            }

            [TestCase(0, 0, 1)]
            [TestCase(1, 0, 1)]
            [TestCase(0.0000001d, 0, 1)]
            [TestCase(0.9999999d, 0, 1)]
            public void ShouldReturnTrueIfDoubleInRange(double value, double lower, double upper)
            {
                // Act
                bool isInRange = value.IsInRange(lower, upper);

                // Assert
                isInRange.ShouldBeTrue();
            }

            [TestCase(-0.0000001d, 0, 1)]
            [TestCase(1.0000001d, 0, 1)]
            public void ShouldReturnFalseIfDoubleNotInRange(double value, double lower, double upper)
            {
                // Act
                bool isInRange = value.IsInRange(lower, upper);

                // Assert
                isInRange.ShouldBeFalse();
            }

            [Test]
            public void ShouldThrowInvalidRangeExceptionIfDoubleRangeInvalid()
            {
                Assert.Throws<InvalidRangeException>(
                    () =>
                    {
                        bool isInRange = 0d.IsInRange(3, 1);
                    });
            }
        }
    }
}