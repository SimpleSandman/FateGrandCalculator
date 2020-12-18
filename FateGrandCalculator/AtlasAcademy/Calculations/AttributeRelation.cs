using System;

using FateGrandCalculator.AtlasAcademy.Interfaces;
using FateGrandCalculator.AtlasAcademy.Json;
using FateGrandCalculator.Enums;

namespace FateGrandCalculator.AtlasAcademy.Calculations
{
    public class AttributeRelation : IBaseRelation
    {
        private const float ATTRIBUTE_DENOMINATOR = 1000.0f;
        private readonly AttributeRelationNiceJson _attributeRelations;

        public AttributeRelation(AttributeRelationNiceJson attributeRelations) 
        {
            _attributeRelations = attributeRelations;
        }

        public float GetAttackMultiplier(string attack)
        {
            throw new NotImplementedException();
        }

        public float GetAttackMultiplier(string atkAttribute, string defAttribute)
        {
            // Reference: https://fategrandorder.fandom.com/wiki/Attributes
            float[,] damageMultiplier =
            {
                { _attributeRelations.Human.HumanMultiplier / ATTRIBUTE_DENOMINATOR, _attributeRelations.Human.SkyMultiplier / ATTRIBUTE_DENOMINATOR, _attributeRelations.Human.EarthMultiplier / ATTRIBUTE_DENOMINATOR, _attributeRelations.Human.StarMultiplier / ATTRIBUTE_DENOMINATOR, _attributeRelations.Human.BeastMultiplier / ATTRIBUTE_DENOMINATOR },
                { _attributeRelations.Sky.HumanMultiplier / ATTRIBUTE_DENOMINATOR, _attributeRelations.Sky.SkyMultiplier / ATTRIBUTE_DENOMINATOR, _attributeRelations.Sky.EarthMultiplier / ATTRIBUTE_DENOMINATOR, _attributeRelations.Sky.StarMultiplier / ATTRIBUTE_DENOMINATOR, _attributeRelations.Sky.BeastMultiplier / ATTRIBUTE_DENOMINATOR },
                { _attributeRelations.Earth.HumanMultiplier / ATTRIBUTE_DENOMINATOR, _attributeRelations.Earth.SkyMultiplier / ATTRIBUTE_DENOMINATOR, _attributeRelations.Earth.EarthMultiplier / ATTRIBUTE_DENOMINATOR, _attributeRelations.Earth.StarMultiplier / ATTRIBUTE_DENOMINATOR, _attributeRelations.Earth.BeastMultiplier / ATTRIBUTE_DENOMINATOR },
                { _attributeRelations.Star.HumanMultiplier / ATTRIBUTE_DENOMINATOR, _attributeRelations.Star.SkyMultiplier / ATTRIBUTE_DENOMINATOR, _attributeRelations.Star.EarthMultiplier / ATTRIBUTE_DENOMINATOR, _attributeRelations.Star.StarMultiplier / ATTRIBUTE_DENOMINATOR, _attributeRelations.Star.BeastMultiplier / ATTRIBUTE_DENOMINATOR },
                { _attributeRelations.Beast.HumanMultiplier / ATTRIBUTE_DENOMINATOR, _attributeRelations.Beast.SkyMultiplier / ATTRIBUTE_DENOMINATOR, _attributeRelations.Beast.EarthMultiplier / ATTRIBUTE_DENOMINATOR, _attributeRelations.Beast.StarMultiplier / ATTRIBUTE_DENOMINATOR, _attributeRelations.Beast.BeastMultiplier / ATTRIBUTE_DENOMINATOR }
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
