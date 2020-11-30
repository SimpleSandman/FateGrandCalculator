using System.Collections.Generic;

using FateGrandCalculator.ApplyStatuses;
using FateGrandCalculator.AtlasAcademy.Json;
using FateGrandCalculator.Models;

namespace FateGrandCalculator.Core
{
    public static class NoblePhantasmSkillActivation
    {
        public static void SkillActivation(Function servantNpFunction, PartyMember partyMember, List<PartyMember> party, List<EnemyMob> enemies, int enemyPosition)
        {
            if (IsPartyFunction(servantNpFunction)) // Target party members for buffs/debuffs
            {
                ApplyServantStatus.ApplyFuncTargetType(partyMember, servantNpFunction, party);
            }
            else if (IsEnemyFunction(servantNpFunction)) // Target enemies for buffs/debuffs
            {
                ApplyEnemyStatus.ApplyFuncTargetType(enemyPosition, servantNpFunction, partyMember.Servant.NpLevel, enemies);
            }
        }

        private static bool IsPartyFunction(Function function)
        {
            if ((function.FuncTargetType == "self"
                || function.FuncTargetType == "ptOne"         // party member
                || function.FuncTargetType == "ptAll"         // party
                || function.FuncTargetType == "ptFull"        // party (including reserve)
                || function.FuncTargetType == "ptOther"       // party except self
                || function.FuncTargetType == "ptOtherFull"   // party except self (including reserve)
                || function.FuncTargetType == "ptselectSub")  // reserve party member
                && function.FuncTargetTeam != "enemy")
            {
                return true;
            }

            return false;
        }

        private static bool IsEnemyFunction(Function function)
        {
            if ((function.FuncTargetType == "enemy"                    // one enemy
                || function.FuncTargetType == "enemyAll"               // enemies
                || function.FuncTargetType == "enemyFull"              // enemies (including reserve)
                || function.FuncTargetType == "enemyOther"             // other enemies besides target
                || function.FuncTargetType == "enemyRandom"            // random enemy
                || function.FuncTargetType == "enemyOtherFull"         // other enemies (including reserve)
                || function.FuncTargetType == "enemyOneAnotherRandom") // other random enemy
                && function.FuncTargetTeam != "player")
            {
                return true;
            }

            return false;
        }
    }
}
