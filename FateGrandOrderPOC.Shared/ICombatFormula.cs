using System.Collections.Generic;
using System.Threading.Tasks;

using FateGrandOrderPOC.Shared.Enums;
using FateGrandOrderPOC.Shared.Models;

namespace FateGrandOrderPOC.Shared
{
    public interface ICombatFormula
    {
        Task NoblePhantasmChainSimulator(List<PartyMember> party, List<EnemyMob> enemyMobs, WaveNumberEnum waveNumber);
        float SpecialAttackUp(PartyMember partyMember, EnemyMob enemy);
        void SetStatusEffects(PartyMember partyMember, ref float cardNpTypeUp, ref float attackUp, ref float powerModifier, ref float npGainUp);
        void SetStatusEffects(EnemyMob enemy, PartyMember partyMember, ref float defenseDownModifier, ref float cardDefenseDownModifier);
        float NpGainedFromEnemy(PartyMember partyMember, EnemyMob enemyMob, float npGainUp, float cardNpTypeUp, float npDamageForEnemyMob, List<float> npDistributionPercentages);
        float CalculatedNpPerHit(PartyMember partyMember, EnemyMob enemyMob, float cardNpTypeUp, float npGainUp);
        List<float> NpDistributionPercentages(PartyMember partyMember);
        float HealthRemaining(EnemyMob enemyMob, float npDamageForEnemyMob);
        Task<float> BaseNpDamage(PartyMember partyMember, EnemyMob enemy, int npChainPosition);
        int Overcharge(float npCharge, int npChainPosition);
        Task<float> AverageNpDamage(PartyMember partyMember, EnemyMob enemyMob, float modifiedNpDamage);
        Task<float> ChanceToKill(PartyMember partyMember, EnemyMob enemyMob, float modifiedNpDamage);
    }
}
