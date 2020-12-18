using System;

using FateGrandCalculator.AtlasAcademy.Interfaces;
using FateGrandCalculator.AtlasAcademy.Json;

namespace FateGrandCalculator.AtlasAcademy.Calculations
{
    public class ClassAttackRate : IBaseRelation
    {
        private const float CLASS_ATTACK_RATE_DENOMINATOR = 1000.0f;
        private readonly ClassAttackRateNiceJson _classAttackRate;

        public ClassAttackRate(ClassAttackRateNiceJson classAttackRate) 
        {
            _classAttackRate = classAttackRate;
        }

        public float GetAttackMultiplier(string className)
        {
            switch (className.ToLower())
            {
                case "saber":
                    return _classAttackRate.Saber / CLASS_ATTACK_RATE_DENOMINATOR;
                case "archer":
                    return _classAttackRate.Archer / CLASS_ATTACK_RATE_DENOMINATOR;
                case "lancer":
                    return _classAttackRate.Lancer / CLASS_ATTACK_RATE_DENOMINATOR;
                case "rider":
                    return _classAttackRate.Rider / CLASS_ATTACK_RATE_DENOMINATOR;
                case "caster":
                    return _classAttackRate.Caster / CLASS_ATTACK_RATE_DENOMINATOR;
                case "assassin":
                    return _classAttackRate.Assassin / CLASS_ATTACK_RATE_DENOMINATOR;
                case "berserker":
                    return _classAttackRate.Berserker / CLASS_ATTACK_RATE_DENOMINATOR;
                case "shielder":
                    return _classAttackRate.Shielder / CLASS_ATTACK_RATE_DENOMINATOR;
                case "ruler":
                    return _classAttackRate.Ruler / CLASS_ATTACK_RATE_DENOMINATOR;
                case "alterEgo":
                    return _classAttackRate.AlterEgo / CLASS_ATTACK_RATE_DENOMINATOR;
                case "avenger":
                    return _classAttackRate.Avenger / CLASS_ATTACK_RATE_DENOMINATOR;
                case "demonGodPillar":
                    return _classAttackRate.DemonGodPillar / CLASS_ATTACK_RATE_DENOMINATOR;
                case "grandCaster":
                    return _classAttackRate.GrandCaster / CLASS_ATTACK_RATE_DENOMINATOR;
                case "beastII":
                    return _classAttackRate.BeastII / CLASS_ATTACK_RATE_DENOMINATOR;
                case "beastI":
                    return _classAttackRate.BeastI / CLASS_ATTACK_RATE_DENOMINATOR;
                case "moonCancer":
                    return _classAttackRate.Saber / CLASS_ATTACK_RATE_DENOMINATOR;
                case "beastIIIR":
                    return _classAttackRate.BeastIIIR / CLASS_ATTACK_RATE_DENOMINATOR;
                case "foreigner":
                    return _classAttackRate.Foreigner / CLASS_ATTACK_RATE_DENOMINATOR;
                case "beastIIIL":
                    return _classAttackRate.BeastIIIL / CLASS_ATTACK_RATE_DENOMINATOR;
                case "beastUnknown":
                    return _classAttackRate.BeastUnknown / CLASS_ATTACK_RATE_DENOMINATOR;
                case "unknown":
                    return _classAttackRate.Unknown / CLASS_ATTACK_RATE_DENOMINATOR;
                case "ALL":
                    return _classAttackRate.All / CLASS_ATTACK_RATE_DENOMINATOR;
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
