using System;

using System.Threading.Tasks;

using FateGrandCalculator.AtlasAcademy.Interfaces;
using FateGrandCalculator.AtlasAcademy.Json;

namespace FateGrandCalculator.AtlasAcademy.Calculations
{
    public class ConstantRate : IBaseRelation
    {
        private const float CLASS_ATTACK_RATE_DENOMINATOR = 1000.0f;
        private readonly IAtlasAcademyClient _aaClient;

        public ConstantRate(IAtlasAcademyClient client)
        {
            _aaClient = client;
        }

        public async Task<float> GetAttackMultiplier(string constantName)
        {
            ConstantNiceJson constantGameInfo = await _aaClient.GetConstantGameInfo();

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

        public async Task<float> GetAttackMultiplier(string attack, string defend)
        {
            return await Task.FromException<float>(new NotImplementedException()).ConfigureAwait(false);
        }
    }
}
