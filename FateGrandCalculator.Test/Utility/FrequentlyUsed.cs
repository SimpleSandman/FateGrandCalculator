using System.Collections.Generic;
using System.Threading.Tasks;

using FateGrandCalculator.AtlasAcademy;
using FateGrandCalculator.AtlasAcademy.Calculations;
using FateGrandCalculator.AtlasAcademy.Json;
using FateGrandCalculator.Models;
using FateGrandCalculator.Test.AutofacConfig;

namespace FateGrandCalculator.Test.Utility
{
    public static class FrequentlyUsed
    {
        /// <summary>
        /// Set up party member based on pre-defined servant and craft essence info
        /// </summary>
        /// <param name="basicJson">Basic information for a servant</param>
        /// <param name="party">List of party members</param>
        /// <param name="resolvedClasses">Dependently injected classes that are resolved using Autofac</param>
        /// <param name="npLevel">The Noble Phantasm level of a servant</param>
        /// <param name="isSupport">Declare if this is a support (friend) servant</param>
        /// <param name="craftEssence">The craft being used with the servant</param>
        /// <returns></returns>
        public static async Task<PartyMember> PartyMemberAsync(ServantBasicJson basicJson, List<PartyMember> party, ScopedClasses resolvedClasses,
            int npLevel = 1, bool isSupport = false, CraftEssence craftEssence = null)
        {
            ServantNiceJson json = await resolvedClasses.AtlasAcademyClient.GetServantInfo(basicJson.Id.ToString());

            ChaldeaServant chaldeaServant = new ChaldeaServant
            {
                ServantLevel = json.LvMax,
                NpLevel = npLevel,
                FouHealth = 1000,
                FouAttack = 1000,
                SkillLevels = new int[] { 10, 10, 10 },
                IsSupportServant = isSupport,
                ServantBasicInfo = basicJson
            };

            return resolvedClasses.CombatFormula.AddPartyMember(party, chaldeaServant, craftEssence, json);
        }

        /// <summary>
        /// Get the craft essence info
        /// </summary>
        /// <param name="resolvedClasses">Dependently injected classes that are resolved using Autofac</param>
        /// <param name="ceId">The actual ID of a craft essence, not the collection ID</param>
        /// <param name="level">The level of the craft essence</param>
        /// <param name="isMlb">Max limit broken craft essence</param>
        /// <returns></returns>
        public static async Task<CraftEssence> CraftEssenceAsync(ScopedClasses resolvedClasses, string ceId, int level = 1, bool isMlb = false)
        {
            return new CraftEssence
            {
                CraftEssenceLevel = level,
                Mlb = isMlb,
                CraftEssenceInfo = await resolvedClasses.AtlasAcademyClient.GetCraftEssenceInfo(ceId)
            };
        }

        public static async Task<ConstantExportJson> GetConstantExportJsonAsync(AtlasAcademyClient atlasAcademyClient)
        {
            return new ConstantExportJson
            {
                AttributeRelation = new AttributeRelation(await atlasAcademyClient.GetAttributeRelationInfo()),
                ClassAttackRate = new ClassAttackRate(await atlasAcademyClient.GetClassAttackRateInfo()),
                ClassRelation = new ClassRelation(await atlasAcademyClient.GetClassRelationInfo()),
                ConstantRate = new ConstantRate(await atlasAcademyClient.GetConstantGameInfo()),
                ListBasicServantJson = await atlasAcademyClient.GetListBasicServantInfo(),
                TraitEnumInfo = await atlasAcademyClient.GetTraitEnumInfo()
            };
        }
    }
}
