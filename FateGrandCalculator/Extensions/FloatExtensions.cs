using System;

namespace FateGrandCalculator.Extensions
{
    public static class FloatExtensions
    {
        // Method reference: https://stackoverflow.com/a/3875619/2113548
        // MinNormal reference: https://docs.oracle.com/javase/7/docs/api/constant-values.html#java.lang.Float.MIN_NORMAL
        public static bool NearlyEqual(this float a, float b, float marginOfError = 0.00001f)
        {
            const float minNormalFloat = (1 << 23) * float.Epsilon; // 1.17549435E-38f
            float absA = Math.Abs(a);
            float absB = Math.Abs(b);
            float diff = Math.Abs(a - b);

            if (a.Equals(b))
            {
                return true; // shortcut, handles infinities
            }
            else if (a == 0.0f || b == 0.0f || absA + absB < minNormalFloat)
            {
                // a or b is zero or both are extremely close to it
                // relative error is less meaningful here
                return diff < (marginOfError * minNormalFloat);
            }
            else if (float.IsInfinity(absA + absB) && float.IsFinite(diff))
            {
                // since anything finite divided by +/- infinity equals 0
                // we will simply check if 0 is less than the margin of error
                return 0.0f < marginOfError;
            }
            else if (float.IsInfinity(absA + absB) && float.IsInfinity(diff))
            {
                // since infinity divided by infinity returns undefined (NaN or "not a number")
                // it will always be false
                return false;
            }
            else
            {
                return diff / (absA + absB) < marginOfError; // use relative error
            }
        }
    }
}
