using System;

namespace FateGrandCalculator.Extensions
{
    public static class FloatExtensions
    {
        // Method reference: https://stackoverflow.com/a/3875619/2113548
        // Float.MIN_NORMAL reference: https://docs.oracle.com/javase/7/docs/api/constant-values.html#java.lang.Float.MIN_NORMAL
        /// <summary>
        /// Given a margin of error (epsilon), check if two floats are nearly equal
        /// </summary>
        /// <param name="a">The first compared value</param>
        /// <param name="b">The second compared value</param>
        /// <param name="marginOfError">Also known as epsilon</param>
        /// <returns>True if the two floats are equal within the margin of error; otherwise, false</returns>
        public static bool NearlyEqual(this float a, float b, float marginOfError = 0.00001f)
        {
            const float minNormalFloat = (1 << 23) * float.Epsilon; // 1.17549435E-38f
            float absA = Math.Abs(a);
            float absB = Math.Abs(b);
            float diff = Math.Abs(a - b);
            float sumAbsolutes = absA + absB;

            if (a.Equals(b))
            {
                return true; // shortcut, handles infinities and NaN
            }
            else if (a == 0.0f || b == 0.0f || sumAbsolutes < minNormalFloat)
            {
                // a or b is zero or both are extremely close to it
                // relative error is less meaningful here
                return diff < (marginOfError * minNormalFloat);
            }
            else if (float.IsInfinity(sumAbsolutes))
            {
                if (float.IsFinite(diff))
                {
                    // since anything finite divided by +/- infinity always equals 0
                    // we will simply check if 0 is less than the margin of error
                    return 0.0f < marginOfError;
                }
                else
                {
                    // since infinity divided by infinity returns undefined (NaN or "not a number"),
                    // it will always be false because NaN cannot be compared to anything
                    // other than another NaN
                    return false;
                }
            }
            else
            {
                return diff / sumAbsolutes < marginOfError; // use relative error
            }
        }
    }
}
