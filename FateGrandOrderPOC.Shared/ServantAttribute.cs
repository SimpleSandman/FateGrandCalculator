using System;

using FateGrandOrderPOC.Shared.AtlasAcademyJson;
using FateGrandOrderPOC.Shared.Extensions;

namespace FateGrandOrderPOC.Shared
{
    public enum ServantAttributeEnum
    {
        Human = 0,
        Sky = 1,
        Earth = 2,
        Star = 3,
        Beast = 4
    }

    public static class ServantAttribute
    {
        public static decimal GetAttackMultiplier(string atkAttribute, string defAttribute)
        {
            AttributeRelationNiceJson attributeRelations = ApiRequest.GetDesearlizeObjectAsync<AttributeRelationNiceJson>("https://api.atlasacademy.io/export/NA/NiceAttributeRelation.json").Result;

            // Reference: https://fategrandorder.fandom.com/wiki/Attributes
            decimal[,] damageMultiplier =
            {
                { attributeRelations.Human.HumanMultiplier/1000m, attributeRelations.Human.SkyMultiplier/1000m, attributeRelations.Human.EarthMultiplier/1000m, attributeRelations.Human.StarMultiplier/1000m, attributeRelations.Human.BeastMultiplier/1000m },
                { attributeRelations.Sky.HumanMultiplier/1000m, attributeRelations.Sky.SkyMultiplier/1000m, attributeRelations.Sky.EarthMultiplier/1000m, attributeRelations.Sky.StarMultiplier/1000m, attributeRelations.Sky.BeastMultiplier/1000m },
                { attributeRelations.Earth.HumanMultiplier/1000m, attributeRelations.Earth.SkyMultiplier/1000m, attributeRelations.Earth.EarthMultiplier/1000m, attributeRelations.Earth.StarMultiplier/1000m, attributeRelations.Earth.BeastMultiplier/1000m },
                { attributeRelations.Star.HumanMultiplier/1000m, attributeRelations.Star.SkyMultiplier/1000m, attributeRelations.Star.EarthMultiplier/1000m, attributeRelations.Star.StarMultiplier/1000m, attributeRelations.Star.BeastMultiplier/1000m },
                { attributeRelations.Beast.HumanMultiplier/1000m, attributeRelations.Beast.SkyMultiplier/1000m, attributeRelations.Beast.EarthMultiplier/1000m, attributeRelations.Beast.StarMultiplier/1000m, attributeRelations.Beast.BeastMultiplier/1000m }
            };

            bool validAtkAttribute = Enum.TryParse(atkAttribute.ToUpperFirstChar(), out ServantAttributeEnum atkServantAttribute);
            bool validDefAttribute = Enum.TryParse(defAttribute.ToUpperFirstChar(), out ServantAttributeEnum defServantAttribute);

            if (!validAtkAttribute || !validDefAttribute)
            {
                return 0.0m; // invalid attribute found
            }

            return damageMultiplier[(int)atkServantAttribute, (int)defServantAttribute];
        }
    }
}
