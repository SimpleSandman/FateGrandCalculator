using System.Threading.Tasks;

using FateGrandOrderPOC.Shared.AtlasAcademy;
using FateGrandOrderPOC.Shared.AtlasAcademy.Json;
using FateGrandOrderPOC.Shared.Models;

namespace FateGrandOrderPOC.Test
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
        /// <returns>Servant that has Fou HP & ATK 1000, skill levels are 10/10/10, and their max level before grails</returns>
        public static async Task<Servant> ServantAsync(AtlasAcademyClient atlasAcademyClient, string servantId, int npLevel, bool isSupportServant)
        {
            ServantNiceJson json = await atlasAcademyClient.GetServantInfo(servantId);

            if (json == null)
            {
                return new Servant
                {
                    ServantLevel = 1,
                    NpLevel = npLevel,
                    FouHealth = 0,
                    FouAttack = 0,
                    SkillLevels = new int[] { 1, 1, 1 },
                    IsSupportServant = isSupportServant
                };
            }

            return new Servant
            {
                ServantLevel = json.LvMax,
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
