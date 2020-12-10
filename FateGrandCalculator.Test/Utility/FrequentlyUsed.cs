using System.Collections.Generic;
using System.Threading.Tasks;

using FateGrandCalculator.AtlasAcademy;
using FateGrandCalculator.AtlasAcademy.Json;
using FateGrandCalculator.Models;

namespace FateGrandCalculator.Test.Utility
{
    public static class FrequentlyUsed
    {
        /// <summary>
        /// Get pre-defined servant info alongside NP level and if this is a support servant
        /// </summary>
        /// <param name="atlasAcademyClient">Client needed to pull servant info</param>
        /// <param name="servantId">The actual ID of a servant, not the collection ID</param>
        /// <param name="npLevel">The Noble Phantasm level of a servant</param>
        /// <param name="isSupportServant">Declare if this is a support (friend) servant</param>
        /// <param name="servantLevel">Set default max level for the servant, unless specified</param>
        /// <returns>Servant that has Fou HP & ATK 1000, skill levels are 10/10/10, and their max level before grails by default</returns>
        public static async Task<ChaldeaServant> ChaldeaServantAsync(AtlasAcademyClient atlasAcademyClient, string servantId, int npLevel, bool isSupportServant, int servantLevel = 0)
        {
            ServantNiceJson json = await atlasAcademyClient.GetServantInfo(servantId);
            servantLevel = servantLevel == 0 ? json.LvMax : servantLevel;

            return new ChaldeaServant
            {
                ServantLevel = servantLevel,
                NpLevel = npLevel,
                FouHealth = 1000,
                FouAttack = 1000,
                SkillLevels = new int[] { 10, 10, 10 },
                IsSupportServant = isSupportServant,
                ServantInfo = json
            };
        }

        public static async Task<PartyMember> PartyMemberAsync(string servantId, List<PartyMember> party, ScopedClasses resolvedClasses,
            int npLevel = 1, bool isSupport = false, CraftEssence craftEssence = null)
        {
            ChaldeaServant chaldeaServant = await ChaldeaServantAsync(resolvedClasses.AtlasAcademyClient, servantId, npLevel, isSupport);
            PartyMember partyMember = resolvedClasses.CombatFormula.AddPartyMember(party, chaldeaServant, craftEssence);

            if (craftEssence != null)
            {
                resolvedClasses.CombatFormula.ApplyCraftEssenceEffects(partyMember);
            }

            return partyMember;
        }

        public static async Task<CraftEssence> CraftEssenceAsync(ScopedClasses resolvedClasses, string ceId, int level = 1, bool isMlb = false)
        {
            return new CraftEssence
            {
                CraftEssenceLevel = level,
                Mlb = isMlb,
                CraftEssenceInfo = await resolvedClasses.AtlasAcademyClient.GetCraftEssenceInfo(ceId)
            };
        }
    }
}
