using System.Threading.Tasks;

using FateGrandOrderPOC.AtlasAcademy;
using FateGrandOrderPOC.AtlasAcademy.Json;
using FateGrandOrderPOC.Models;

namespace FateGrandOrderPOC.Test.Utility
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
        public static async Task<Servant> ServantAsync(AtlasAcademyClient atlasAcademyClient, string servantId, int npLevel, bool isSupportServant, int servantLevel = 0)
        {
            ServantNiceJson json = await atlasAcademyClient.GetServantInfo(servantId);
            servantLevel = servantLevel == 0 ? json.LvMax : servantLevel;

            return new Servant
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
    }
}
