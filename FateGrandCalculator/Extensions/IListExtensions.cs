using System.Collections.Generic;

namespace FateGrandCalculator.Extensions
{
    public static class IListExtensions
    {
        public static void Swap<T>(this IList<T> list, int indexA, int indexB)
        {
            T tmp = list[indexA];
            list[indexA] = list[indexB];
            list[indexB] = tmp;
        }

        public static void Swap<T>(this IList<T> list, T objectA, T objectB)
        {
            int indexA = list.IndexOf(objectA);
            int indexB = list.IndexOf(objectB);

            T tmp = list[indexA];
            list[indexA] = list[indexB];
            list[indexB] = tmp;
        }
    }
}
