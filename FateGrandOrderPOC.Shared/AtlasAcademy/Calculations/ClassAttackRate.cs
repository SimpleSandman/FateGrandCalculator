using System;
using System.Threading.Tasks;

using FateGrandOrderPOC.Shared.AtlasAcademy.Json;

namespace FateGrandOrderPOC.Shared.AtlasAcademy.Calculations
{
    public class ClassAttackRate : IBaseRelation
    {
        private const float CLASS_ATTACK_RATE_DENOMINATOR = 1000.0f;

        private readonly IAtlasAcademyClient _aaClient;

        public ClassAttackRate(IAtlasAcademyClient client)
        {
            _aaClient = client;
        }           

        public async Task<float> GetAttackMultiplier(string className)
        {
            ClassAttackRateNiceJson classAttackRate = await _aaClient.GetClassAttackRateInfo();

            switch (className.ToLower())
            {
                case "saber":
                    return classAttackRate.Saber / CLASS_ATTACK_RATE_DENOMINATOR;
                case "archer":
                    return classAttackRate.Archer / CLASS_ATTACK_RATE_DENOMINATOR;
                case "lancer":
                    return classAttackRate.Lancer / CLASS_ATTACK_RATE_DENOMINATOR;
                case "rider":
                    return classAttackRate.Rider / CLASS_ATTACK_RATE_DENOMINATOR;
                case "caster":
                    return classAttackRate.Caster / CLASS_ATTACK_RATE_DENOMINATOR;
                case "assassin":
                    return classAttackRate.Assassin / CLASS_ATTACK_RATE_DENOMINATOR;
                case "berserker":
                    return classAttackRate.Berserker / CLASS_ATTACK_RATE_DENOMINATOR;
                case "shielder":
                    return classAttackRate.Shielder / CLASS_ATTACK_RATE_DENOMINATOR;
                case "ruler":
                    return classAttackRate.Ruler / CLASS_ATTACK_RATE_DENOMINATOR;
                case "alterEgo":
                    return classAttackRate.AlterEgo / CLASS_ATTACK_RATE_DENOMINATOR;
                case "avenger":
                    return classAttackRate.Avenger / CLASS_ATTACK_RATE_DENOMINATOR;
                case "demonGodPillar":
                    return classAttackRate.DemonGodPillar / CLASS_ATTACK_RATE_DENOMINATOR;
                case "grandCaster":
                    return classAttackRate.GrandCaster / CLASS_ATTACK_RATE_DENOMINATOR;
                case "beastII":
                    return classAttackRate.BeastII / CLASS_ATTACK_RATE_DENOMINATOR;
                case "beastI":
                    return classAttackRate.BeastI / CLASS_ATTACK_RATE_DENOMINATOR;
                case "moonCancer":
                    return classAttackRate.Saber / CLASS_ATTACK_RATE_DENOMINATOR;
                case "beastIIIR":
                    return classAttackRate.BeastIIIR / CLASS_ATTACK_RATE_DENOMINATOR;
                case "foreigner":
                    return classAttackRate.Foreigner / CLASS_ATTACK_RATE_DENOMINATOR;
                case "beastIIIL":
                    return classAttackRate.BeastIIIL / CLASS_ATTACK_RATE_DENOMINATOR;
                case "beastUnknown":
                    return classAttackRate.BeastUnknown / CLASS_ATTACK_RATE_DENOMINATOR;
                case "unknown":
                    return classAttackRate.Unknown / CLASS_ATTACK_RATE_DENOMINATOR;
                case "ALL":
                    return classAttackRate.All / CLASS_ATTACK_RATE_DENOMINATOR;
                default:
                    break;
            }

            return 0.0f;
        }

        public async Task<float> GetAttackMultiplier(string attack, string defend)
        {
            return await Task.FromException<float>(new NotImplementedException()).ConfigureAwait(false);
        }
    }
}
