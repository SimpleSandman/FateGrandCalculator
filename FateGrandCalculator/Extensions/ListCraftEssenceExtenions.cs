using System.Collections.Generic;

using FateGrandCalculator.Models;

namespace FateGrandCalculator.Extensions
{
    public static class ListCraftEssenceExtenions
    {
        public static void Update(this List<CraftEssence> craftEssences, CraftEssence craftEssence)
        {
            int index = craftEssences.FindIndex(c => c.CraftEssenceInfo.Id == craftEssence.CraftEssenceInfo.Id);

            if (index == -1)
            {
                craftEssences.Add(craftEssence);
            }
            else
            {
                craftEssences.RemoveAt(index);
                craftEssences.Insert(index, craftEssence);
            }
        }
    }
}
