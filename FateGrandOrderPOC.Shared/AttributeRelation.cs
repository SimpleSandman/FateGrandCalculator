using System;

using FateGrandOrderPOC.Shared.AtlasAcademyJson;
using FateGrandOrderPOC.Shared.Enums;

namespace FateGrandOrderPOC.Shared
{
    public class AttributeRelation : IBaseRelation
    {
        public float GetAttackMultiplier(string attack)
        {
            throw new NotImplementedException();
        }

        public float GetAttackMultiplier(string atkAttribute, string defAttribute)
        {
            AttributeRelationNiceJson attributeRelations = ApiRequest.GetDesearlizeObjectAsync<AttributeRelationNiceJson>("https://api.atlasacademy.io/export/NA/NiceAttributeRelation.json").Result;

            // Reference: https://fategrandorder.fandom.com/wiki/Attributes
            float[,] damageMultiplier =
            {
                { attributeRelations.Human.HumanMultiplier/1000f, attributeRelations.Human.SkyMultiplier/1000f, attributeRelations.Human.EarthMultiplier/1000f, attributeRelations.Human.StarMultiplier/1000f, attributeRelations.Human.BeastMultiplier/1000f },
                { attributeRelations.Sky.HumanMultiplier/1000f, attributeRelations.Sky.SkyMultiplier/1000f, attributeRelations.Sky.EarthMultiplier/1000f, attributeRelations.Sky.StarMultiplier/1000f, attributeRelations.Sky.BeastMultiplier/1000f },
                { attributeRelations.Earth.HumanMultiplier/1000f, attributeRelations.Earth.SkyMultiplier/1000f, attributeRelations.Earth.EarthMultiplier/1000f, attributeRelations.Earth.StarMultiplier/1000f, attributeRelations.Earth.BeastMultiplier/1000f },
                { attributeRelations.Star.HumanMultiplier/1000f, attributeRelations.Star.SkyMultiplier/1000f, attributeRelations.Star.EarthMultiplier/1000f, attributeRelations.Star.StarMultiplier/1000f, attributeRelations.Star.BeastMultiplier/1000f },
                { attributeRelations.Beast.HumanMultiplier/1000f, attributeRelations.Beast.SkyMultiplier/1000f, attributeRelations.Beast.EarthMultiplier/1000f, attributeRelations.Beast.StarMultiplier/1000f, attributeRelations.Beast.BeastMultiplier/1000f }
            };

            bool validAtkAttribute = Enum.TryParse(atkAttribute, true, out AttributeRelationEnum atkServantAttribute);
            bool validDefAttribute = Enum.TryParse(defAttribute, true, out AttributeRelationEnum defServantAttribute);

            if (!validAtkAttribute || !validDefAttribute)
            {
                return 0.0f; // invalid attribute found
            }

            return damageMultiplier[(int)atkServantAttribute, (int)defServantAttribute];
        }
    }
}
