using System.Collections.Generic;
using System.Linq;

using FateGrandCalculator.AtlasAcademy.Json;
using FateGrandCalculator.Extensions;
using FateGrandCalculator.Models;

namespace FateGrandCalculator.ApplyStatuses
{
    public static class ApplyServantStatus
    {
        /// <summary>
        /// Apply "noble phantasm" buffs/debuffs
        /// </summary>
        /// <param name="partyMemberActor"></param>
        /// <param name="servantNpFunction"></param>
        /// <param name="partyMemberTargets"></param>
        public static void ApplyFuncTargetType(PartyMember partyMemberActor, Function servantNpFunction, List<PartyMember> partyMemberTargets)
        {
            if (servantNpFunction.FuncTargetType == "self")
            {
                ApplyStatus(servantNpFunction, partyMemberActor.Servant.NpLevel, partyMemberActor);
            }
            else
            {
                switch (servantNpFunction.FuncTargetType)
                {
                    case "ptAll":         // party
                    case "ptOther":       // party except self
                        ApplyStatus(servantNpFunction, partyMemberActor.Servant.NpLevel, partyMemberTargets.Take(3).ToList());
                        break;
                    case "ptFull":        // party (including reserve)
                    case "ptOtherFull":   // party except self (including reserve)
                        ApplyStatus(servantNpFunction, partyMemberActor.Servant.NpLevel, partyMemberTargets);
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Apply "servant party" buffs/debuffs
        /// </summary>
        /// <param name="partyMemberPosition"></param>
        /// <param name="partyMemberActor"></param>
        /// <param name="servantFunction"></param>
        /// <param name="currentSkillLevel"></param>
        /// <param name="partyMemberTargets"></param>
        public static void ApplyFuncTargetType(int partyMemberPosition, PartyMember partyMemberActor, Function servantFunction, int currentSkillLevel, 
            List<PartyMember> partyMemberTargets)
        {
            if (servantFunction.FuncTargetType == "self")
            {
                ApplyStatus(servantFunction, currentSkillLevel, partyMemberActor);
            }
            else
            {
                switch (servantFunction.FuncTargetType)
                {
                    case "ptAll":         // party
                    case "ptOther":       // party except self
                        ApplyStatus(servantFunction, currentSkillLevel, partyMemberTargets.Take(3).ToList());
                        break;
                    case "ptFull":        // party (including reserve)
                    case "ptOtherFull":   // party except self (including reserve)
                        ApplyStatus(servantFunction, currentSkillLevel, partyMemberTargets);
                        break;
                    case "ptOne":         // party member
                    case "ptselectSub":   // reserve party member
                        ApplyStatus(servantFunction, currentSkillLevel, partyMemberTargets[partyMemberPosition - 1]);
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Apply "mystic code" party buffs/debuffs
        /// </summary>
        /// <param name="partyMemberPosition"></param>
        /// <param name="mysticCode"></param>
        /// <param name="mysticCodeFunction"></param>
        /// <param name="partyMemberTargets"></param>
        /// <param name="reservePartyMemberIndex"></param>
        public static void ApplyFuncTargetType(int partyMemberPosition, MysticCode mysticCode, Function mysticCodeFunction, List<PartyMember> partyMemberTargets, 
            int reservePartyMemberIndex)
        {
            switch (mysticCodeFunction.FuncTargetType)
            {
                case "ptAll":           // party
                case "ptOther":         // party except self
                    ApplyStatus(mysticCode, mysticCodeFunction, partyMemberTargets.Take(3).ToList());
                    break;
                case "ptFull":          // party (including reserve)
                case "ptOtherFull":     // party except self (including reserve)
                    ApplyStatus(mysticCode, mysticCodeFunction, partyMemberTargets);
                    break;
                case "ptOne":           // party member
                case "ptselectSub":     // reserve party member
                    ApplyStatus(mysticCode, mysticCodeFunction, partyMemberTargets[partyMemberPosition - 1]);
                    break;
                case "ptselectOneSub":  // one reserve party member
                    SwapPartyMemberFunction(mysticCodeFunction, partyMemberTargets, partyMemberPosition - 1, reservePartyMemberIndex - 1);
                    break;
                default:
                    break;
            }
        }

        #region Private Methods "Party Member"
        private static void ApplyStatus(Function servantFunction, int level, List<PartyMember> partyMemberTargets)
        {
            foreach (PartyMember partyMemberTarget in partyMemberTargets)
            {
                ApplyPartyMemberStatus(servantFunction, level, partyMemberTarget);
            }
        }

        private static void ApplyStatus(Function servantFunction, int level, PartyMember partyMemberTarget)
        {
            ApplyPartyMemberStatus(servantFunction, level, partyMemberTarget);
        }

        private static void ApplyPartyMemberStatus(Function servantFunction, int level, PartyMember partyMemberTarget)
        {
            switch (servantFunction.FuncType)
            {
                case "gainNp":
                    partyMemberTarget.NpCharge += servantFunction.Svals[level - 1].Value / 100.0f;
                    PityNpGain(partyMemberTarget);
                    break;
                default:
                    partyMemberTarget.ActiveStatuses.Add(new ActiveStatus 
                    { 
                        StatusEffect = servantFunction,
                        AppliedSkillLevel = level,
                        ActiveTurnCount = servantFunction.Svals[level - 1].Turn
                    });
                    break;
            }
        }
        #endregion

        #region Private Methods "Mystic Code"
        private static void ApplyStatus(MysticCode mysticCode, Function mysticCodeFunction, List<PartyMember> partyMemberTargets)
        {
            foreach (PartyMember partyMemberTarget in partyMemberTargets)
            {
                ApplyPartyMemberStatus(mysticCode, mysticCodeFunction, partyMemberTarget);
            }
        }

        private static void ApplyStatus(MysticCode mysticCode, Function mysticCodeFunction, PartyMember partyMemberTarget)
        {
            ApplyPartyMemberStatus(mysticCode, mysticCodeFunction, partyMemberTarget);
        }

        private static void ApplyPartyMemberStatus(MysticCode mysticCode, Function mysticCodeFunction, PartyMember partyMemberTarget)
        {
            switch (mysticCodeFunction.FuncType)
            {
                case "gainNp":
                    partyMemberTarget.NpCharge += mysticCodeFunction.Svals[mysticCode.MysticCodeLevel - 1].Value / 100.0f;
                    PityNpGain(partyMemberTarget);
                    break;
                default:
                    partyMemberTarget.ActiveStatuses.Add(new ActiveStatus
                    {
                        StatusEffect = mysticCodeFunction,
                        AppliedSkillLevel = mysticCode.MysticCodeLevel,
                        ActiveTurnCount = mysticCodeFunction.Svals[mysticCode.MysticCodeLevel - 1].Turn
                    });
                    break;
            }
        }

        private static void SwapPartyMemberFunction(Function function, List<PartyMember> party, int activePartyMemberIndex, int reservePartyMemberIndex)
        {
            if (activePartyMemberIndex > -1
                && reservePartyMemberIndex > -1
                && function.FuncType == "replaceMember"
                && function.FuncTargetType == "ptselectOneSub"
                && function.FuncTargetTeam == "player")
            {
                party.Swap(activePartyMemberIndex, reservePartyMemberIndex);
            }
        }
        #endregion

        #region Private Shared Methods
        /// <summary>
        /// Adjust the NP gain to 100% if the charge reached 99% or cap the NP charge to 300%
        /// </summary>
        /// <param name="partyMemberTarget">Affected party member</param>
        private static void PityNpGain(PartyMember partyMemberTarget)
        {
            if (partyMemberTarget.NpCharge == 99.0f)
            {
                partyMemberTarget.NpCharge++; // pity NP gain
            }
            else if (partyMemberTarget.NpCharge > 300.0f)
            {
                partyMemberTarget.NpCharge = 300.0f; // set max charge
            }
        }
        #endregion
    }
}
