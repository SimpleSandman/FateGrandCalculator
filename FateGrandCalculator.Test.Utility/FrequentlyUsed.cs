﻿using System.Collections.Generic;
using System.Threading.Tasks;

using FateGrandCalculator.AtlasAcademy;
using FateGrandCalculator.AtlasAcademy.Calculations;
using FateGrandCalculator.AtlasAcademy.Json;
using FateGrandCalculator.Models;

using FateGrandCalculator.Test.Utility.AutofacConfig;

using Xunit.Abstractions;

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
        /// <param name="additionalDamageBonus">Damage bonus added manually for a party member. Example would be servant damage bonus for a specific event</param>
        /// <returns></returns>
        public static async Task<PartyMember> PartyMemberAsync(ServantBasicJson basicJson, List<PartyMember> party, ScopedClasses resolvedClasses,
            int npLevel = 1, bool isSupport = false, CraftEssence craftEssence = null, float additionalDamageBonus = 0.0f)
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

            return await resolvedClasses.CombatFormula.AddPartyMember(party, chaldeaServant, craftEssence, additionalDamageBonus);
        }

        /// <summary>
        /// Get the craft essence info
        /// </summary>
        /// <param name="craftEssenceJson">Basic information for a craft essence</param>
        /// <param name="level">The level of the craft essence</param>
        /// <param name="isMlb">Max limit broken craft essence</param>
        /// <returns></returns>
        public static CraftEssence GetCraftEssence(EquipBasicJson craftEssenceJson, int level = 1, bool isMlb = false)
        {
            return new CraftEssence
            {
                CraftEssenceLevel = level,
                Mlb = isMlb,
                CraftEssenceInfo = craftEssenceJson
            };
        }

        public static void ShowSurvivingEnemyHealth(List<EnemyMob> waveSurvivors, ITestOutputHelper output)
        {
            foreach (EnemyMob enemy in waveSurvivors)
            {
                output.WriteLine($"{enemy.Name} has {enemy.Health} HP left");
            }
        }

        public static async Task<ConstantExportJson> GetConstantExportJsonAsync(AtlasAcademyClient atlasAcademyClient)
        {
            return new ConstantExportJson
            {
                AttributeRelation = new AttributeRelation(await atlasAcademyClient.GetAttributeRelationInfo()),
                ClassAttackRate = new ClassAttackRate(await atlasAcademyClient.GetClassAttackRateInfo()),
                ClassRelation = new ClassRelation(await atlasAcademyClient.GetClassRelationInfo()),
                ConstantRate = new ConstantRate(await atlasAcademyClient.GetConstantGameInfo()),
                GrailCostNiceJson = await atlasAcademyClient.GetGrailCostInfo(),
                ListEquipBasicJson = await atlasAcademyClient.GetListBasicEquipInfo(),
                ListServantBasicJson = await atlasAcademyClient.GetListBasicServantInfo(),
                TraitEnumInfo = await atlasAcademyClient.GetTraitEnumInfo(),
            };
        }
    }
}
