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
    }
}
