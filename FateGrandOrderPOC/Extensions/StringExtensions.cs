using System;
using System.Collections.Generic;
using System.Text;

namespace FateGrandOrderPOC.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Return the string with the first character in uppercase
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ToUpperFirstChar(this string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }

            char[] a = s.ToCharArray();
            a[0] = char.ToUpper(a[0]);
            return new string(a);
        }
    }
}
