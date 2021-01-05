using System.Collections.Generic;

using FateGrandCalculator.Models;

namespace FateGrandCalculator.Extensions
{
    public static class ListChaldeaServantExtensions
    {
        public static void Update(this List<ChaldeaServant> chaldeaServants, ChaldeaServant chaldeaServant)
        {
            int index = chaldeaServants.FindIndex(c => c.ServantBasicInfo.Id == chaldeaServant.ServantBasicInfo.Id);

            if (index == -1)
            {
                chaldeaServants.Add(chaldeaServant);
            }
            else
            {
                chaldeaServants.RemoveAt(index);
                chaldeaServants.Insert(index, chaldeaServant);
            }
        }
    }
}
