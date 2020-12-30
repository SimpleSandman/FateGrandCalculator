using System.Collections.Generic;

using FateGrandCalculator.AtlasAcademy.Json;
using FateGrandCalculator.Enums;
using FateGrandCalculator.Models;

namespace FateGrandCalculator.Core.Combat.Interfaces
{
    public interface ICombatFormula
    {
        void NoblePhantasmChainSimulator(List<PartyMember> party, List<EnemyMob> enemyMobs, WaveNumberEnum waveNumber, ConstantExportJson exportJson, int enemyPosition = 3);
        bool AddPartyMemberToNpChain(List<PartyMember> party, PartyMember partyMember);
        PartyMember AddPartyMember(List<PartyMember> party, ChaldeaServant chaldeaServant, CraftEssence chaldeaCraftEssence, ServantNiceJson servantNiceJson);
    }
}
