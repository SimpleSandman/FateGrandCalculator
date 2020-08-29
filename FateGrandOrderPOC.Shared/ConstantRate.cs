using System;

using FateGrandOrderPOC.Shared.AtlasAcademyJson;

namespace FateGrandOrderPOC.Shared
{
    public class ConstantRate : IBaseRelation
    {
        private const float CLASS_ATTACK_RATE_DENOMINATOR = 1000.0f;

        public float GetAttackMultiplier(string constantName)
        {
            ConstantNiceJson constantGameInfo = AtlasAcademyRequest.GetConstantGameInfo();

            switch (constantName.ToLower())
            {
                case "attackratequick":
                    return constantGameInfo.ENEMY_ATTACK_RATE_QUICK / CLASS_ATTACK_RATE_DENOMINATOR;
                case "attackratearts":
                    return constantGameInfo.ENEMY_ATTACK_RATE_ARTS / CLASS_ATTACK_RATE_DENOMINATOR;
                case "attackratebuster":
                    return constantGameInfo.ENEMY_ATTACK_RATE_BUSTER / CLASS_ATTACK_RATE_DENOMINATOR;
                default:
                    break;
            }

            return 0.0f;
        }

        public float GetAttackMultiplier(string attack, string defend)
        {
            throw new NotImplementedException();
        }
    }
}
