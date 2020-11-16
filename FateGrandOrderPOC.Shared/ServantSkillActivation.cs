using System;
using System.Collections.Generic;
using System.Linq;

using FateGrandOrderPOC.Shared.ApplyStatuses;
using FateGrandOrderPOC.Shared.AtlasAcademy.Json;
using FateGrandOrderPOC.Shared.Models;

namespace FateGrandOrderPOC.Shared
{
    public class ServantSkillActivation : IServantSkillActivation
    {
        /// <summary>
        /// Buff a party member with the desired skill based on the servant's list of available skills
        /// </summary>
        /// <param name="partyMemberActor">The acting party member that is giving the status effect</param>
        /// <param name="actorSkillPositionNumber">Skill position number (left = 1, middle = 2, right = 3)</param>
        /// <param name="party">The targeted party members that are receiving the status effect</param>
        /// <param name="partyMemberPosition">The position the selected party member is currently sitting (1-6)</param>
        /// <param name="enemies">The targeted enemies that are receiving the status effect</param>
        /// <param name="enemyPosition">The position the selected enemy is currently sitting (1-3)</param>
        public void SkillActivation(PartyMember partyMemberActor, int actorSkillPositionNumber, List<PartyMember> party, int partyMemberPosition, 
            List<EnemyMob> enemies, int enemyPosition)
        {
            if (partyMemberActor.SkillCooldowns.Exists(s => s.SkillId == actorSkillPositionNumber))
            {
#if DEBUG
                Console.WriteLine($"WARNING: Cannot buff using {partyMemberActor.Servant.ServantInfo.Name}'s #{actorSkillPositionNumber} skill because of cooldown!");
#endif
                return; // don't activate again
            }

            // Get highest priority skill
            Skill skill = partyMemberActor
                .Servant
                .ServantInfo
                .Skills
                .FindAll(s => s.Num == actorSkillPositionNumber)
                .Aggregate((agg, next) =>
                    next.Priority >= agg.Priority ? next : agg);

            int currentSkillLevel = partyMemberActor.Servant.SkillLevels[actorSkillPositionNumber - 1];

            // Target party members for buffs/debuffs
            List<Function> partyBuffServantFunctions = GetPartyBuffServantFunctions(skill);
            if (partyBuffServantFunctions?.Count > 0)
            {
                foreach (Function servantFunction in partyBuffServantFunctions)
                {
                    ApplyServantStatus.ApplyFuncTargetType(partyMemberPosition, partyMemberActor, servantFunction, currentSkillLevel, party);
                }
            }

            // Target enemies for buffs/debuffs
            List<Function> enemyServantFunctions = GetEnemyServantFunctions(skill);
            if (enemyServantFunctions?.Count > 0)
            {
                foreach (Function servantFunction in enemyServantFunctions)
                {
                    ApplyEnemyStatus.ApplyFuncTargetType(enemyPosition, servantFunction, currentSkillLevel, enemies);
                }
            }

            if (partyBuffServantFunctions?.Count > 0 || enemyServantFunctions?.Count > 0)
            {
                partyMemberActor.SkillCooldowns.Add(new SkillCooldown
                {
                    SkillId = actorSkillPositionNumber,
                    Cooldown = skill.Cooldown[currentSkillLevel - 1]
                });
            }
        }

        /// <summary>
        /// Buff a party member with the desired skill based on the mystic code's list of available skills
        /// </summary>
        /// <param name="mysticCode">The acting mystic code skill that is giving the buff</param>
        /// <param name="mysticCodeSkillPositionNumber">Skill position number (left = 1, middle = 2, right = 3)</param>
        /// <param name="party">The targeted party members that are receiving the buff</param>
        /// <param name="partyMemberPosition">The position the selected party member is currently sitting (1-6)</param>
        /// <param name="enemies">The targeted enemies that are receiving the status effect</param>
        /// <param name="enemyPosition">The position the selected enemy is currently sitting (1-3)</param>
        public void SkillActivation(MysticCode mysticCode, int mysticCodeSkillPositionNumber, List<PartyMember> party, int partyMemberPosition,
            List<EnemyMob> enemies, int enemyPosition)
        {
            if (mysticCode.SkillCooldowns.Exists(s => s.SkillId == mysticCodeSkillPositionNumber))
            {
#if DEBUG
                Console.WriteLine($"WARNING: Cannot buff using {mysticCode.MysticCodeInfo.Name}'s #{mysticCodeSkillPositionNumber} skill because of cooldown!");
#endif
                return; // don't activate again
            }

            Skill skill = mysticCode.MysticCodeInfo.Skills[mysticCodeSkillPositionNumber - 1];

            List<Function> mysticCodePartyBuffFunctions = GetPartyBuffServantFunctions(skill);
            if (mysticCodePartyBuffFunctions?.Count > 0)
            {
                foreach (Function mysticCodeFunction in mysticCodePartyBuffFunctions)
                {
                    ApplyServantStatus.ApplyFuncTargetType(partyMemberPosition, mysticCode, mysticCodeFunction, party);
                }
            }

            List<Function> mysticCodeEnemyServantFunctions = GetEnemyServantFunctions(skill);
            if (mysticCodeEnemyServantFunctions?.Count > 0)
            {
                foreach (Function mysticCodeFunction in mysticCodeEnemyServantFunctions)
                {
                    ApplyEnemyStatus.ApplyFuncTargetType(enemyPosition, mysticCode, mysticCodeFunction, enemies);
                }
            }

            if (mysticCodePartyBuffFunctions?.Count > 0 || mysticCodeEnemyServantFunctions?.Count > 0)
            {
                mysticCode.SkillCooldowns.Add(new SkillCooldown
                {
                    SkillId = mysticCodeSkillPositionNumber,
                    Cooldown = skill.Cooldown[mysticCode.MysticCodeLevel - 1]
                });
            }
        }

        /// <summary>
        /// Reduce cooldown counters for all front-line party members at the end of the turn
        /// </summary>
        /// <param name="party">The targeted party members that have active status effects</param>
        public void AdjustSkillCooldowns(List<PartyMember> party)
        {
            foreach (PartyMember partyMember in party.Take(3))
            {
                ReduceSkillCooldowns(partyMember.SkillCooldowns);

                foreach (ActiveStatus activeStatus in partyMember.ActiveStatuses)
                {
                    activeStatus.ActiveTurnCount--;
                }

                partyMember.ActiveStatuses.RemoveAll(a => a.ActiveTurnCount == 0);
            }
        }

        /// <summary>
        /// Reduce cooldown counters for all front-line party members at the end of the turn
        /// </summary>
        /// <param name="mysticCode">The acting mystic code skill that is giving the buff</param>
        public void AdjustSkillCooldowns(MysticCode mysticCode)
        {
            ReduceSkillCooldowns(mysticCode.SkillCooldowns);
        }

        #region Private Methods
        private void ReduceSkillCooldowns(List<SkillCooldown> skillCooldowns)
        {
            foreach (SkillCooldown skillCooldown in skillCooldowns)
            {
                if (skillCooldown.Cooldown == 1)
                {
                    skillCooldowns.Remove(skillCooldown);
                }
                else
                {
                    skillCooldown.Cooldown--;
                }
            }
        }

        private List<Function> GetPartyBuffServantFunctions(Skill skill)
        {
            return (from f in skill.Functions
                    where (f.FuncTargetType == "self"
                        || f.FuncTargetType == "ptOne"         // party member
                        || f.FuncTargetType == "ptAll"         // party
                        || f.FuncTargetType == "ptFull"        // party (including reserve)
                        || f.FuncTargetType == "ptOther"       // party except self
                        || f.FuncTargetType == "ptOtherFull"   // party except self (including reserve)
                        || f.FuncTargetType == "ptselectSub")  // reserve party member
                        && f.FuncTargetTeam != "enemy"
                    select f).ToList();
        }

        private List<Function> GetEnemyServantFunctions(Skill skill)
        {
            return (from f in skill.Functions
                    where (f.FuncTargetType == "enemy"                  // one enemy
                        || f.FuncTargetType == "enemyAll"               // enemies
                        || f.FuncTargetType == "enemyFull"              // enemies (including reserve)
                        || f.FuncTargetType == "enemyOther"             // other enemies besides target
                        || f.FuncTargetType == "enemyRandom"            // random enemy
                        || f.FuncTargetType == "enemyOtherFull"         // other enemies (including reserve)
                        || f.FuncTargetType == "enemyOneAnotherRandom") // other random enemy
                        && f.FuncTargetTeam != "player"
                    select f).ToList();
        }
        #endregion
    }
}
