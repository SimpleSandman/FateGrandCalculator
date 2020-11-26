using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using FateGrandCalculator.AtlasAcademy.Json;
using FateGrandCalculator.Enums;
using FateGrandCalculator.Models;

namespace FateGrandCalculator
{
    public interface ICombatFormula
    {
        Task NoblePhantasmChainSimulator(List<PartyMember> party, List<EnemyMob> enemyMobs, WaveNumberEnum waveNumber, int enemyPosition);
        float SpecialAttackUp(PartyMember partyMember, EnemyMob enemy);
        Tuple<float, float, float, float> SetStatusEffects(PartyMember partyMember, float cardNpTypeUp, float attackUp, float powerModifier, float npGainUp);
        Tuple<float, float> SetStatusEffects(EnemyMob enemy, PartyMember partyMember, float defenseDownModifier, float cardDefenseDownModifier);
        float NpGainedFromEnemy(PartyMember partyMember, EnemyMob enemyMob, float npGainUp, float cardNpTypeUp, float npDamageForEnemyMob, List<float> npDistributionPercentages);
        float CalculatedNpPerHit(PartyMember partyMember, EnemyMob enemyMob, float cardNpTypeUp, float npGainUp);
        List<float> NpDistributionPercentages(PartyMember partyMember);
        float AttemptToKillEnemy(EnemyMob enemyMob, float npDamageForEnemyMob);
        Task<float> BaseNpDamage(PartyMember partyMember, EnemyMob enemy, Sval svalNp);
        int Overcharge(float npCharge, int npChainPosition);
        Task<float> AverageNpDamage(PartyMember partyMember, EnemyMob enemyMob, float modifiedNpDamage);
        Task<float> ChancesToKill(PartyMember partyMember, EnemyMob enemyMob, float modifiedNpDamage);
        void AddPartyMemberToNpChain(List<PartyMember> party, PartyMember partyMember);
        PartyMember AddPartyMember(List<PartyMember> party, Servant chaldeaServant, CraftEssence chaldeaCraftEssence = null);
        void ApplyCraftEssenceEffects(PartyMember partyMember);
    }
}
