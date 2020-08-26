using System;

using FateGrandOrderPOC.Shared.AtlasAcademyJson;

namespace FateGrandOrderPOC.Shared
{
    public class ClassAttackRate : BaseRelation
    {
        public override float GetAttackMultiplier(string className)
        {
            ClassAttackRateNiceJson classAttackRate = AtlasAcademyRequest.GetClassAttackRateInfo();

            switch (className.ToLower())
            {
                case "saber":
                    return classAttackRate.Saber;
                case "archer":
                    return classAttackRate.Archer;
                case "lancer":
                    return classAttackRate.Lancer;
            }

            return 0.0f;
        }

        public override float GetAttackMultiplier(string attack, string defend)
        {
            throw new NotImplementedException();
        }
    }
}
