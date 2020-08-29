using System;

using FateGrandOrderPOC.Shared.AtlasAcademy.Json;

namespace FateGrandOrderPOC.Shared.AtlasAcademy.Calculations
{
    public class ConstantRate : IBaseRelation
    {
        private const float CLASS_ATTACK_RATE_DENOMINATOR = 1000.0f;

        public float GetAttackMultiplier(string constantName)
        {
            ConstantNiceJson constantGameInfo = AtlasAcademyRequest.GetConstantGameInfo();

            switch (constantName.ToUpper())
            {
                case "ATTACK_RATE":
                    return constantGameInfo.ATTACK_RATE / CLASS_ATTACK_RATE_DENOMINATOR;
                case "ENEMY_ATTACK_RATE_QUICK":
                    return constantGameInfo.ENEMY_ATTACK_RATE_QUICK / CLASS_ATTACK_RATE_DENOMINATOR;
                case "ENEMY_ATTACK_RATE_ARTS":
                    return constantGameInfo.ENEMY_ATTACK_RATE_ARTS / CLASS_ATTACK_RATE_DENOMINATOR;
                case "ENEMY_ATTACK_RATE_BUSTER":
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
