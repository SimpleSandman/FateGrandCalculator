using FateGrandCalculator.Extensions;

using FluentAssertions;
using FluentAssertions.Execution;

using Xunit;

namespace FateGrandCalculator.Test
{
    public class FloatNearlyEqualTest
    {
        // Reference: https://floating-point-gui.de/errors/NearlyEqualsTest.java
        public FloatNearlyEqualTest() { }

        /** Regular large numbers - generally not problematic */
        [Fact]
        public void Big()
        {
            using (new AssertionScope())
            {
                FloatExtensions.NearlyEqual(1000000f, 1000001f).Should().BeTrue();
                FloatExtensions.NearlyEqual(1000001f, 1000000f).Should().BeTrue();
                FloatExtensions.NearlyEqual(10000f, 10001f).Should().BeFalse();
                FloatExtensions.NearlyEqual(10001f, 10000f).Should().BeFalse();
            }
        }

        /** Negative large numbers */
        [Fact]
        public void BigNeg()
        {
            using (new AssertionScope())
            {
                FloatExtensions.NearlyEqual(-1000000f, -1000001f).Should().BeTrue();
                FloatExtensions.NearlyEqual(-1000001f, -1000000f).Should().BeTrue();
                FloatExtensions.NearlyEqual(-10000f, -10001f).Should().BeFalse();
                FloatExtensions.NearlyEqual(-10001f, -10000f).Should().BeFalse();
            }
            
        }

        /** Numbers around 1 */
        [Fact]
        public void Mid()
        {
            using (new AssertionScope())
            {
                FloatExtensions.NearlyEqual(1.0000001f, 1.0000002f).Should().BeTrue();
                FloatExtensions.NearlyEqual(1.0000002f, 1.0000001f).Should().BeTrue();
                FloatExtensions.NearlyEqual(1.0002f, 1.0001f).Should().BeFalse();
                FloatExtensions.NearlyEqual(1.0001f, 1.0002f).Should().BeFalse();
            }
            
        }

        /** Numbers around -1 */
        [Fact]
        public void MidNeg()
        {
            using (new AssertionScope())
            {
                FloatExtensions.NearlyEqual(-1.000001f, -1.000002f).Should().BeTrue();
                FloatExtensions.NearlyEqual(-1.000002f, -1.000001f).Should().BeTrue();
                FloatExtensions.NearlyEqual(-1.0001f, -1.0002f).Should().BeFalse();
                FloatExtensions.NearlyEqual(-1.0002f, -1.0001f).Should().BeFalse();
            }
            
        }

        /** Numbers between 1 and 0 */
        [Fact]
        public void Small()
        {
            using (new AssertionScope())
            {
                FloatExtensions.NearlyEqual(0.000000001000001f, 0.000000001000002f).Should().BeTrue();
                FloatExtensions.NearlyEqual(0.000000001000002f, 0.000000001000001f).Should().BeTrue();
                FloatExtensions.NearlyEqual(0.000000000001002f, 0.000000000001001f).Should().BeFalse();
                FloatExtensions.NearlyEqual(0.000000000001001f, 0.000000000001002f).Should().BeFalse();
            }
        }

        /** Numbers between -1 and 0 */
        [Fact]
        public void SmallNeg()
        {
            using (new AssertionScope())
            {
                FloatExtensions.NearlyEqual(-0.000000001000001f, -0.000000001000002f).Should().BeTrue();
                FloatExtensions.NearlyEqual(-0.000000001000002f, -0.000000001000001f).Should().BeTrue();
                FloatExtensions.NearlyEqual(-0.000000000001002f, -0.000000000001001f).Should().BeFalse();
                FloatExtensions.NearlyEqual(-0.000000000001001f, -0.000000000001002f).Should().BeFalse();
            }
        }

        /** Small differences away from zero */
        [Fact]
        public void SmallDiffs()
        {
            using (new AssertionScope())
            {
                FloatExtensions.NearlyEqual(0.3f, 0.30000003f).Should().BeTrue();
                FloatExtensions.NearlyEqual(-0.3f, -0.30000003f).Should().BeTrue();
            }
        }

        /** Comparisons involving zero */
        [Fact]
        public void Zero()
        {
            using (new AssertionScope())
            {
                FloatExtensions.NearlyEqual(0.0f, 0.0f).Should().BeTrue();
                FloatExtensions.NearlyEqual(0.0f, -0.0f).Should().BeTrue();
                FloatExtensions.NearlyEqual(-0.0f, -0.0f).Should().BeTrue();
                FloatExtensions.NearlyEqual(0.00000001f, 0.0f).Should().BeFalse();
                FloatExtensions.NearlyEqual(0.0f, 0.00000001f).Should().BeFalse();
                FloatExtensions.NearlyEqual(-0.00000001f, 0.0f).Should().BeFalse();
                FloatExtensions.NearlyEqual(0.0f, -0.00000001f).Should().BeFalse();

                FloatExtensions.NearlyEqual(0.0f, 1e-40f, 0.01f).Should().BeTrue();
                FloatExtensions.NearlyEqual(1e-40f, 0.0f, 0.01f).Should().BeTrue();
                FloatExtensions.NearlyEqual(1e-40f, 0.0f, 0.000001f).Should().BeFalse();
                FloatExtensions.NearlyEqual(0.0f, 1e-40f, 0.000001f).Should().BeFalse();

                FloatExtensions.NearlyEqual(0.0f, -1e-40f, 0.1f).Should().BeTrue();
                FloatExtensions.NearlyEqual(-1e-40f, 0.0f, 0.1f).Should().BeTrue();
                FloatExtensions.NearlyEqual(-1e-40f, 0.0f, 0.00000001f).Should().BeFalse();
                FloatExtensions.NearlyEqual(0.0f, -1e-40f, 0.00000001f).Should().BeFalse();
            }
            
        }

        /**
        * Comparisons involving extreme values (overflow potential)
        */
        [Fact]
        public void ExtremeMax()
        {
            using (new AssertionScope())
            {
                FloatExtensions.NearlyEqual(float.MaxValue, float.MaxValue).Should().BeTrue();
                FloatExtensions.NearlyEqual(float.MaxValue, -float.MaxValue).Should().BeFalse();
                FloatExtensions.NearlyEqual(-float.MaxValue, float.MaxValue).Should().BeFalse();
                FloatExtensions.NearlyEqual(float.MaxValue, float.MaxValue / 2).Should().BeTrue();
                FloatExtensions.NearlyEqual(float.MaxValue, -float.MaxValue / 2).Should().BeFalse();
                FloatExtensions.NearlyEqual(-float.MaxValue, float.MaxValue / 2).Should().BeFalse();
            }
        }

        /**
         * Comparisons involving infinities
         */
        [Fact]
        public void Infinities()
        {
            using (new AssertionScope())
            {
                FloatExtensions.NearlyEqual(float.PositiveInfinity, float.PositiveInfinity).Should().BeTrue();
                FloatExtensions.NearlyEqual(float.NegativeInfinity, float.NegativeInfinity).Should().BeTrue();
                FloatExtensions.NearlyEqual(float.NegativeInfinity, float.PositiveInfinity).Should().BeFalse();
                FloatExtensions.NearlyEqual(float.PositiveInfinity, float.MaxValue).Should().BeFalse();
                FloatExtensions.NearlyEqual(float.NegativeInfinity, -float.MaxValue).Should().BeFalse();
            }
        }

        /**
         * Comparisons involving NaN values
         */
        [Fact]
        public void NaN()
        {
            using (new AssertionScope())
            {
                FloatExtensions.NearlyEqual(float.NaN, float.NaN).Should().BeTrue();
                FloatExtensions.NearlyEqual(float.NaN, 0.0f).Should().BeFalse();
                FloatExtensions.NearlyEqual(-0.0f, float.NaN).Should().BeFalse();
                FloatExtensions.NearlyEqual(float.NaN, -0.0f).Should().BeFalse();
                FloatExtensions.NearlyEqual(0.0f, float.NaN).Should().BeFalse();
                FloatExtensions.NearlyEqual(float.NaN, float.PositiveInfinity).Should().BeFalse();
                FloatExtensions.NearlyEqual(float.PositiveInfinity, float.NaN).Should().BeFalse();
                FloatExtensions.NearlyEqual(float.NaN, float.NegativeInfinity).Should().BeFalse();
                FloatExtensions.NearlyEqual(float.NegativeInfinity, float.NaN).Should().BeFalse();
                FloatExtensions.NearlyEqual(float.NaN, float.MaxValue).Should().BeFalse();
                FloatExtensions.NearlyEqual(float.MaxValue, float.NaN).Should().BeFalse();
                FloatExtensions.NearlyEqual(float.NaN, -float.MaxValue).Should().BeFalse();
                FloatExtensions.NearlyEqual(-float.MaxValue, float.NaN).Should().BeFalse();
                FloatExtensions.NearlyEqual(float.NaN, float.MinValue).Should().BeFalse();
                FloatExtensions.NearlyEqual(float.MinValue, float.NaN).Should().BeFalse();
                FloatExtensions.NearlyEqual(float.NaN, -float.MinValue).Should().BeFalse();
                FloatExtensions.NearlyEqual(-float.MinValue, float.NaN).Should().BeFalse();
            }
        }

        /** Comparisons of numbers on opposite sides of 0 */
        [Fact]
        public void Opposite()
        {
            using (new AssertionScope())
            {
                FloatExtensions.NearlyEqual(1.000000001f, -1.0f).Should().BeFalse();
                FloatExtensions.NearlyEqual(-1.0f, 1.000000001f).Should().BeFalse();
                FloatExtensions.NearlyEqual(-1.000000001f, 1.0f).Should().BeFalse();
                FloatExtensions.NearlyEqual(1.0f, -1.000000001f).Should().BeFalse();
                FloatExtensions.NearlyEqual(10 * float.MinValue, 10 * -float.MinValue).Should().BeFalse();
                FloatExtensions.NearlyEqual(10000 * float.MinValue, 10000 * -float.MinValue).Should().BeFalse();
            }
        }

        /**
         * The really tricky part - comparisons of numbers very close to zero.
         */
        [Fact]
        public void Ulp()
        {
            using (new AssertionScope())
            {
                FloatExtensions.NearlyEqual(float.MinValue, float.MinValue).Should().BeTrue();

                FloatExtensions.NearlyEqual(-float.MinValue, float.MinValue).Should().BeFalse();
                FloatExtensions.NearlyEqual(float.MinValue, -float.MinValue).Should().BeFalse();
                FloatExtensions.NearlyEqual(float.MinValue, 0.0f).Should().BeFalse();
                FloatExtensions.NearlyEqual(0.0f, float.MinValue).Should().BeFalse();
                FloatExtensions.NearlyEqual(-float.MinValue, 0.0f).Should().BeFalse();
                FloatExtensions.NearlyEqual(0.0f, -float.MinValue).Should().BeFalse();
                FloatExtensions.NearlyEqual(0.000000001f, -float.MinValue).Should().BeFalse();
                FloatExtensions.NearlyEqual(0.000000001f, float.MinValue).Should().BeFalse();
                FloatExtensions.NearlyEqual(float.MinValue, 0.000000001f).Should().BeFalse();
                FloatExtensions.NearlyEqual(-float.MinValue, 0.000000001f).Should().BeFalse();
            }
        }
    }
}
