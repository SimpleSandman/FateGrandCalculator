using System;
using System.Threading.Tasks;

using FateGrandOrderPOC.AtlasAcademy.Json;
using FateGrandOrderPOC.Enums;

namespace FateGrandOrderPOC.AtlasAcademy.Calculations
{
    public class AttributeRelation : IBaseRelation
    {
        private const float ATTRIBUTE_DENOMINATOR = 1000.0f;

        public async Task<float> GetAttackMultiplier(string attack)
        {
            return await Task.FromException<float>(new NotImplementedException()).ConfigureAwait(false);
        }

        public async Task<float> GetAttackMultiplier(string atkAttribute, string defAttribute)
        {
            // TODO: Put this in AtlasAcademyClient.cs
            AttributeRelationNiceJson attributeRelations = await ApiRequest.GetDeserializeObjectAsync<AttributeRelationNiceJson>("https://api.atlasacademy.io/export/NA/NiceAttributeRelation.json");

             // Reference: https://fategrandorder.fandom.com/wiki/Attributes
            float[,] damageMultiplier =
            {
                { attributeRelations.Human.HumanMultiplier / ATTRIBUTE_DENOMINATOR, attributeRelations.Human.SkyMultiplier / ATTRIBUTE_DENOMINATOR, attributeRelations.Human.EarthMultiplier / ATTRIBUTE_DENOMINATOR, attributeRelations.Human.StarMultiplier / ATTRIBUTE_DENOMINATOR, attributeRelations.Human.BeastMultiplier / ATTRIBUTE_DENOMINATOR },
                { attributeRelations.Sky.HumanMultiplier / ATTRIBUTE_DENOMINATOR, attributeRelations.Sky.SkyMultiplier / ATTRIBUTE_DENOMINATOR, attributeRelations.Sky.EarthMultiplier / ATTRIBUTE_DENOMINATOR, attributeRelations.Sky.StarMultiplier / ATTRIBUTE_DENOMINATOR, attributeRelations.Sky.BeastMultiplier / ATTRIBUTE_DENOMINATOR },
                { attributeRelations.Earth.HumanMultiplier / ATTRIBUTE_DENOMINATOR, attributeRelations.Earth.SkyMultiplier / ATTRIBUTE_DENOMINATOR, attributeRelations.Earth.EarthMultiplier / ATTRIBUTE_DENOMINATOR, attributeRelations.Earth.StarMultiplier / ATTRIBUTE_DENOMINATOR, attributeRelations.Earth.BeastMultiplier / ATTRIBUTE_DENOMINATOR },
                { attributeRelations.Star.HumanMultiplier / ATTRIBUTE_DENOMINATOR, attributeRelations.Star.SkyMultiplier / ATTRIBUTE_DENOMINATOR, attributeRelations.Star.EarthMultiplier / ATTRIBUTE_DENOMINATOR, attributeRelations.Star.StarMultiplier / ATTRIBUTE_DENOMINATOR, attributeRelations.Star.BeastMultiplier / ATTRIBUTE_DENOMINATOR },
                { attributeRelations.Beast.HumanMultiplier / ATTRIBUTE_DENOMINATOR, attributeRelations.Beast.SkyMultiplier / ATTRIBUTE_DENOMINATOR, attributeRelations.Beast.EarthMultiplier / ATTRIBUTE_DENOMINATOR, attributeRelations.Beast.StarMultiplier / ATTRIBUTE_DENOMINATOR, attributeRelations.Beast.BeastMultiplier / ATTRIBUTE_DENOMINATOR }
            };

            bool validAtkAttribute = Enum.TryParse(atkAttribute, true, out AttributeRelationEnum atkServantAttribute);
            bool validDefAttribute = Enum.TryParse(defAttribute, true, out AttributeRelationEnum defServantAttribute);

            if (!validAtkAttribute || !validDefAttribute)
            {
                return 0.0f;  // invalid attribute found
            }

            return damageMultiplier[(int)atkServantAttribute, (int)defServantAttribute];
        }
    }
}
