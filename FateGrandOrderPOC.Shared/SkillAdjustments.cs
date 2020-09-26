using System;
using System.Collections.Generic;
using System.Linq;

using FateGrandOrderPOC.Shared.AtlasAcademy.Json;
using FateGrandOrderPOC.Shared.Models;

namespace FateGrandOrderPOC.Shared
{
    public class SkillAdjustments
    {
        /// <summary>
        /// Reduce cooldown counters for all front-line party members at the end of the turn
        /// </summary>
        public List<PartyMember> AdjustSkillCooldowns(List<PartyMember> party)
        {
            foreach (PartyMember partyMember in party.Take(3))
            {
                foreach (SkillCooldown skillCooldown in partyMember.SkillCooldowns)
                {
                    if (skillCooldown.Cooldown == 1)
                    {
                        partyMember.SkillCooldowns.Remove(skillCooldown);
                    }
                    else
                    {
                        skillCooldown.Cooldown--;
                    }
                }

                foreach (ActiveStatus activeStatus in partyMember.ActiveStatuses)
                {
                    if (activeStatus.ActiveTurnCount == 1)
                    {
                        partyMember.ActiveStatuses.Remove(activeStatus);
                    }
                    else
                    {
                        activeStatus.ActiveTurnCount--;
                    }
                }
            }

            return party;
        }

        /// <summary>
        /// Buff a party member with the desired skill based on the actor's list of available skills
        /// </summary>
        /// <param name="partyMemberActor">The acting party member that is giving the buff</param>
        /// <param name="actorSkillNumber">Skill position number (left = 1, middle = 2, right = 3)</param>
        /// <param name="party">The targeted party members that are receiving the buff</param>
        /// <param name="partyMemberPosition">The position the selected party member is currently sitting (1-6)</param>
        public void BuffSystem(PartyMember partyMemberActor, int actorSkillNumber, List<PartyMember> party, int partyMemberPosition)
        {
            if (partyMemberActor.SkillCooldowns.Exists(s => s.SkillId == actorSkillNumber))
            {
                Console.WriteLine($"WARNING: Cannot buff using {partyMemberActor.Servant.ServantInfo.Name}'s #{actorSkillNumber} skill because of cooldown!");
                return; // don't buff again
            }

            // Get highest priority skill
            SkillServant skill = partyMemberActor
                .Servant
                .ServantInfo
                .Skills
                .FindAll(s => s.Num == actorSkillNumber)
                .Aggregate((agg, next) =>
                    next.Priority >= agg.Priority ? next : agg);

            List<FunctionServant> servantFunctions 
                = (from f in skill.Functions
                    where (f.FuncTargetType == "self"
                        || f.FuncTargetType == "ptOne"         // party member
                        || f.FuncTargetType == "ptAll"         // party
                        || f.FuncTargetType == "ptFull"        // party (including reserve)
                        || f.FuncTargetType == "ptOther"       // party except self
                        || f.FuncTargetType == "ptOtherFull"   // party except self (including reserve)
                        || f.FuncTargetType == "ptselectSub")  // reserve party member
                        && f.FuncTargetTeam != "enemy"
                    select f).ToList();

            if (servantFunctions == null || servantFunctions.Count == 0)
            {
                Console.WriteLine($"ERROR: Cannot find the specified servant function for {partyMemberActor.Servant.ServantInfo.Name}'s #{actorSkillNumber} skill");
                return; // didn't find any buffs that apply to other members
            }

            int currentSkillLevel = partyMemberActor.Servant.SkillLevels[actorSkillNumber - 1];

            foreach (FunctionServant servantFunction in servantFunctions)
            {
                ApplyPartyFuncTargetType(servantFunction.FuncTargetType, partyMemberPosition, partyMemberActor, servantFunction, currentSkillLevel, party);
            }

            partyMemberActor.SkillCooldowns.Add(new SkillCooldown
            {
                SkillId = actorSkillNumber,
                Cooldown = skill.Cooldown[currentSkillLevel - 1]
            });
        }

        #region Private Methods
        private void ApplyPartyFuncTargetType(string funcTargetType, int partyMemberPosition, PartyMember partyMemberActor, FunctionServant servantFunction,
            int currentSkillLevel, List<PartyMember> partyMemberTargets)
        {
            if (funcTargetType == "self")
            {
                ApplyBuff(partyMemberActor, servantFunction, currentSkillLevel, partyMemberActor);
            }
            else
            {
                switch (funcTargetType)
                {
                    case "ptAll":         // party
                    case "ptOther":       // party except self
                        ApplyBuff(partyMemberActor, servantFunction, currentSkillLevel, partyMemberTargets.Take(3).ToList());
                        break;
                    case "ptFull":        // party (including reserve)
                    case "ptOtherFull":   // party except self (including reserve)
                        ApplyBuff(partyMemberActor, servantFunction, currentSkillLevel, partyMemberTargets);
                        break;
                    case "ptOne":         // party member
                    case "ptselectSub":   // reserve party member
                        ApplyBuff(partyMemberActor, servantFunction, currentSkillLevel, partyMemberTargets[partyMemberPosition - 1]);
                        break;
                    default:
                        break;
                }
            }
        }

        private List<PartyMember> ApplyBuff(PartyMember partyMemberActor, FunctionServant servantFunction, int currentSkillLevel, List<PartyMember> partyMemberTargets)
        {
            foreach (PartyMember partyMemberTarget in partyMemberTargets)
            {
                ApplyPartyMemberBuff(partyMemberActor, servantFunction, currentSkillLevel, partyMemberTarget);
            }

            return partyMemberTargets;
        }

        private PartyMember ApplyBuff(PartyMember partyMemberActor, FunctionServant servantFunction, int currentSkillLevel, PartyMember partyMemberTarget)
        {
            return ApplyPartyMemberBuff(partyMemberActor, servantFunction, currentSkillLevel, partyMemberTarget);
        }

        private PartyMember ApplyPartyMemberBuff(PartyMember partyMemberActor, FunctionServant servantFunction, int currentSkillLevel, PartyMember partyMemberTarget)
        {
            string support = "";
            if (partyMemberActor.Servant.IsSupportServant)
            {
                support = "(Support) ";
            }

            // TODO: Add more buffs here
            switch (servantFunction.FuncType)
            {
                case "gainNp":
                    partyMemberTarget.NpCharge += servantFunction.Svals[currentSkillLevel - 1].Value / 100.0f;

                    // Pity NP gain
                    if (partyMemberTarget.NpCharge == 99.0f)
                    {
                        partyMemberTarget.NpCharge++;
                    }

                    Console.WriteLine($"{partyMemberActor.Servant.ServantInfo.Name} {support}" + "has buffed " +
                        $"{partyMemberTarget.Servant.ServantInfo.Name}'s NP charge by " +
                        $"{servantFunction.Svals[currentSkillLevel - 1].Value / 100.0f}% and is now at {partyMemberTarget.NpCharge}%");
                    break;
                default:
                    partyMemberTarget.ActiveStatuses.Add(new ActiveStatus 
                    { 
                        StatusEffect = servantFunction,
                        AppliedSkillLevel = currentSkillLevel,
                        ActiveTurnCount = servantFunction.Svals[currentSkillLevel - 1].Turn
                    });
                    break;
            }

            return partyMemberTarget;
        }
        #endregion
    }
}
