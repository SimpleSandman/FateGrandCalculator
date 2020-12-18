using System;

using FateGrandCalculator.AtlasAcademy.Interfaces;
using FateGrandCalculator.AtlasAcademy.Json;

namespace FateGrandCalculator.AtlasAcademy.Calculations
{
    public class ConstantRate : IBaseRelation
    {
        private const float CLASS_ATTACK_RATE_DENOMINATOR = 1000.0f;
        private readonly ConstantNiceJson _constantNiceJson;

        public ConstantRate(ConstantNiceJson constantGameInfo) 
        {
            _constantNiceJson = constantGameInfo;
        }

        public float GetAttackMultiplier(string constantName)
        {
            switch (constantName.ToUpper())
            {
                case "ATTACK_RATE":
                    return _constantNiceJson.ATTACK_RATE / CLASS_ATTACK_RATE_DENOMINATOR;
                case "ENEMY_ATTACK_RATE_QUICK":
                    return _constantNiceJson.ENEMY_ATTACK_RATE_QUICK / CLASS_ATTACK_RATE_DENOMINATOR;
                case "ENEMY_ATTACK_RATE_ARTS":
                    return _constantNiceJson.ENEMY_ATTACK_RATE_ARTS / CLASS_ATTACK_RATE_DENOMINATOR;
                case "ENEMY_ATTACK_RATE_BUSTER":
                    return _constantNiceJson.ENEMY_ATTACK_RATE_BUSTER / CLASS_ATTACK_RATE_DENOMINATOR;
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
