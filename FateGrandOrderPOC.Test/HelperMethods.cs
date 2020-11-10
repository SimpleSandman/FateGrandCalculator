using System;
using System.IO;

namespace FateGrandOrderPOC.Test
{
    public class HelperMethods
    {
        /// <summary>
        /// Get the full file path of the servant data. Set "Copy to Output Directory" property to "Copy if newer" for JSON files
        /// </summary>
        /// <param name="region">NA or JP</param>
        /// <param name="className">AlterEgo, Archer, Assassin, Avenger, Berserker, Caster, Foreigner, Lancer, MoonCancer, Rider, Ruler, Saber, or Shielder</param>
        /// <param name="servantFilename">The name of the servant's JSON file</param>
        /// <returns></returns>
        public static string JsonServantFilepath(string region, string className, string servantFilename)
        {
            return Path.Combine(Environment.CurrentDirectory, "Json", region, "ServantData", className, servantFilename);
        }

        /// <summary>
        /// Get the full file path of the craft essence data. Set "Copy to Output Directory" property to "Copy if newer" for JSON files
        /// </summary>
        /// <param name="region">NA or JP</param>
        /// <param name="craftEssenceFilename">The name of the craft essence's JSON file</param>
        /// <returns></returns>
        public static string JsonCraftEssenceFilepath(string region, string craftEssenceFilename)
        {
            return Path.Combine(Environment.CurrentDirectory, "Json", region, "CraftEssenceData", craftEssenceFilename);
        }
    }
}
