using System.Collections.Generic;
using System.Threading.Tasks;

using FateGrandCalculator.Enums;
using FateGrandCalculator.Models;

namespace FateGrandCalculator.Core.Interfaces
{
    public interface ICombatFormula
    {
        Task NoblePhantasmChainSimulator(List<PartyMember> party, List<EnemyMob> enemyMobs, WaveNumberEnum waveNumber, int enemyPosition);
        bool AddPartyMemberToNpChain(List<PartyMember> party, PartyMember partyMember);
        PartyMember AddPartyMember(List<PartyMember> party, ChaldeaServant chaldeaServant, CraftEssence chaldeaCraftEssence = null);
        void ApplyCraftEssenceEffects(PartyMember partyMember);
    }
}
