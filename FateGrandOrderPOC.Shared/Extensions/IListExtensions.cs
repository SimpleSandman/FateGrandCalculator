using System.Collections.Generic;

namespace FateGrandOrderPOC.Shared.Extensions
{
    public static class IListExtensions
    {
        public static void Swap<T>(this IList<T> list, int a, int b)
        {
            T tmp = list[a];
            list[a] = list[b];
            list[b] = tmp;
        }

        public static void Swap<T>(this IList<T> list, T a, T b)
        {
            int indexA = list.IndexOf(a);
            int indexB = list.IndexOf(b);

            T tmp = list[indexA];
            list[indexA] = list[indexB];
            list[indexB] = tmp;
        }
    }
}
