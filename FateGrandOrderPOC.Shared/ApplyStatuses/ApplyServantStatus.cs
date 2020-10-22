using System;
using System.Collections.Generic;
using System.Linq;

using FateGrandOrderPOC.Shared.AtlasAcademy.Json;
using FateGrandOrderPOC.Shared.Models;

namespace FateGrandOrderPOC.Shared.ApplyStatuses
{
    public static class ApplyServantStatus
    {
        public static void ApplyFuncTargetType(string funcTargetType, int partyMemberPosition, PartyMember partyMemberActor, FunctionServant servantFunction,
            int currentSkillLevel, List<PartyMember> partyMemberTargets)
        {
            if (funcTargetType == "self")
            {
                ApplyStatus(partyMemberActor, servantFunction, currentSkillLevel, partyMemberActor);
            }
            else
            {
                switch (funcTargetType)
                {
                    case "ptAll":         // party
                    case "ptOther":       // party except self
                        ApplyStatus(partyMemberActor, servantFunction, currentSkillLevel, partyMemberTargets.Take(3).ToList());
                        break;
                    case "ptFull":        // party (including reserve)
                    case "ptOtherFull":   // party except self (including reserve)
                        ApplyStatus(partyMemberActor, servantFunction, currentSkillLevel, partyMemberTargets);
                        break;
                    case "ptOne":         // party member
                    case "ptselectSub":   // reserve party member
                        ApplyStatus(partyMemberActor, servantFunction, currentSkillLevel, partyMemberTargets[partyMemberPosition - 1]);
                        break;
                    default:
                        break;
                }
            }
        }

        public static void ApplyFuncTargetType(string funcTargetType, int partyMemberPosition, MysticCode mysticCode, FunctionServant mysticCodeFunction, 
            List<PartyMember> partyMemberTargets)
        {
            switch (funcTargetType)
            {
                case "ptAll":         // party
                case "ptOther":       // party except self
                    ApplyStatus(mysticCode, mysticCodeFunction, partyMemberTargets.Take(3).ToList());
                    break;
                case "ptFull":        // party (including reserve)
                case "ptOtherFull":   // party except self (including reserve)
                    ApplyStatus(mysticCode, mysticCodeFunction, partyMemberTargets);
                    break;
                case "ptOne":         // party member
                case "ptselectSub":   // reserve party member
                    ApplyStatus(mysticCode, mysticCodeFunction, partyMemberTargets[partyMemberPosition - 1]);
                    break;
                default:
                    break;
            }
        }

        #region Private Methods "Party Member"
        private static List<PartyMember> ApplyStatus(PartyMember partyMemberActor, FunctionServant servantFunction, int currentSkillLevel, List<PartyMember> partyMemberTargets)
        {
            foreach (PartyMember partyMemberTarget in partyMemberTargets)
            {
                ApplyPartyMemberStatus(partyMemberActor, servantFunction, currentSkillLevel, partyMemberTarget);
            }

            return partyMemberTargets;
        }

        private static PartyMember ApplyStatus(PartyMember partyMemberActor, FunctionServant servantFunction, int currentSkillLevel, PartyMember partyMemberTarget)
        {
            return ApplyPartyMemberStatus(partyMemberActor, servantFunction, currentSkillLevel, partyMemberTarget);
        }

        private static PartyMember ApplyPartyMemberStatus(PartyMember partyMemberActor, FunctionServant servantFunction, int currentSkillLevel, PartyMember partyMemberTarget)
        {
            string support = "";
            if (partyMemberActor.Servant.IsSupportServant)
            {
                support = "(Support) ";
            }

            switch (servantFunction.FuncType)
            {
                case "gainNp":
                    partyMemberTarget.NpCharge += servantFunction.Svals[currentSkillLevel - 1].Value / 100.0f;

                    PityNpGain(partyMemberTarget);

                    Console.WriteLine($"{partyMemberActor.Servant.ServantInfo.Name} {support}" + "has buffed " +
                        $"{partyMemberTarget.Servant.ServantInfo.Name}'s NP charge by " +
                        $"{servantFunction.Svals[currentSkillLevel - 1].Value / 100.0f}% and is now at {partyMemberTarget.NpCharge}%\n");
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

        #region Private Methods "Mystic Code"
        private static List<PartyMember> ApplyStatus(MysticCode mysticCode, FunctionServant mysticCodeFunction, List<PartyMember> partyMemberTargets)
        {
            foreach (PartyMember partyMemberTarget in partyMemberTargets)
            {
                ApplyPartyMemberStatus(mysticCode, mysticCodeFunction, partyMemberTarget);
            }

            return partyMemberTargets;
        }

        private static PartyMember ApplyStatus(MysticCode mysticCode, FunctionServant mysticCodeFunction, PartyMember partyMemberTarget)
        {
            return ApplyPartyMemberStatus(mysticCode, mysticCodeFunction, partyMemberTarget);
        }

        private static PartyMember ApplyPartyMemberStatus(MysticCode mysticCode, FunctionServant mysticCodeFunction, PartyMember partyMemberTarget)
        {
            switch (mysticCodeFunction.FuncType)
            {
                case "gainNp":
                    partyMemberTarget.NpCharge += mysticCodeFunction.Svals[mysticCode.MysticCodeLevel - 1].Value / 100.0f;

                    // Pity NP gain
                    if (partyMemberTarget.NpCharge == 99.0f)
                    {
                        partyMemberTarget.NpCharge++;
                    }

                    Console.WriteLine($"{mysticCode.MysticCodeInfo.Name} has buffed " +
                        $"{partyMemberTarget.Servant.ServantInfo.Name}'s NP charge by " +
                        $"{mysticCodeFunction.Svals[mysticCode.MysticCodeLevel - 1].Value / 100.0f}% and is now at {partyMemberTarget.NpCharge}%\n");
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

            return partyMemberTarget;
        }
        #endregion

        #region Private Shared Methods
        /// <summary>
        /// Adjust the NP gain to 100% if the charge reached 99%
        /// </summary>
        /// <param name="partyMemberTarget">Affected party member</param>
        private static void PityNpGain(PartyMember partyMemberTarget)
        {
            if (partyMemberTarget.NpCharge == 99.0f)
            {
                partyMemberTarget.NpCharge++;
            }
        }
        #endregion
    }
}
