using System;
using System.IO;

using FateGrandCalculator.AtlasAcademy.Json;
using FateGrandCalculator.Test.Fixture;

using Newtonsoft.Json;

using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;

namespace FateGrandCalculator.Test.Utility
{
    public static class LoadTestData
    {
        /// <summary>
        /// Convert the servant info from its JSON file to ServantNiceJson
        /// </summary>
        /// <param name="region">NA or JP</param>
        /// <param name="className">AlterEgo, Archer, Assassin, Avenger, Berserker, Caster, Foreigner, Lancer, MoonCancer, Rider, Ruler, Saber, or Shielder</param>
        /// <param name="filename">The name of the servant's JSON file</param>
        /// <returns></returns>
        public static ServantNiceJson DeserializeServantJson(string region, string className, string filename)
        {
            string fullFilepath = JsonServantFilepath(region, className, filename);
            return JsonConvert.DeserializeObject<ServantNiceJson>(File.ReadAllText(fullFilepath));
        }

        /// <summary>
        /// Convert the craft essence info from its JSON file to EquipNiceJson
        /// </summary>
        /// <param name="region">NA or JP</param>
        /// <param name="filename">The name of the craft essence's JSON file</param>
        /// <returns></returns>
        public static EquipNiceJson DeserializeCraftEssenceJson(string region, string filename)
        {
            string fullFilepath = JsonCraftEssenceFilepath(region, filename);
            return JsonConvert.DeserializeObject<EquipNiceJson>(File.ReadAllText(fullFilepath));
        }

        /// <summary>
        /// Convert the mystic code info from its JSON file to MysticCodeNiceJson
        /// </summary>
        /// <param name="region">NA or JP</param>
        /// <param name="filename">The name of the mystic code's JSON file</param>
        /// <returns></returns>
        public static MysticCodeNiceJson DeserializeMysticCodeJson(string region, string filename)
        {
            string fullFilepath = JsonMysticCodeFilepath(region, filename);
            return JsonConvert.DeserializeObject<MysticCodeNiceJson>(File.ReadAllText(fullFilepath));
        }

        /// <summary>
        /// Convert the export info from its JSON file to its object equivalent
        /// </summary>
        /// <param name="region">NA or JP</param>
        /// <param name="filename">The name of the export's JSON file</param>
        /// <returns></returns>
        public static T DeserializeExportJson<T>(string region, string filename)
        {
            string fullFilepath = JsonExportFilepath(region, filename);
            return JsonConvert.DeserializeObject<T>(File.ReadAllText(fullFilepath));
        }

        /// <summary>
        /// Creates a WireMock stub to represent a mock request and response
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="wiremockFixture">Mock server</param>
        /// <param name="region">NA or JP</param>
        /// <param name="objectPath">Path for NICE json data like "servant" or "equip" or "MC"</param>
        /// <param name="id">The actual ID, not their collection ID</param>
        /// <param name="mockResponse">The expected JSON response</param>
        public static void CreateNiceWireMockStub<T>(WireMockFixture wiremockFixture, string region, string objectPath, string id, T mockResponse)
        {
            wiremockFixture.MockServer
                .Given(Request.Create().WithPath($"/nice/{region}/{objectPath}/{id}").WithParam("lang", "en").UsingGet())
                .RespondWith(Response.Create().WithStatusCode(200).WithHeader("Content-Type", "application/json").WithBodyAsJson(mockResponse));
        }

        /// <summary>
        /// Creates a WireMock stub to represent a mock request and response
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="wiremockFixture">Mock server</param>
        /// <param name="region">NA or JP</param>
        /// <param name="exportFilename">The export's file name</param>
        /// <param name="mockResponse">The expected JSON response</param>
        public static void CreateExportWireMockStub<T>(WireMockFixture wiremockFixture, string region, string exportFilename, T mockResponse)
        {
            wiremockFixture.MockServer
                .Given(Request.Create().WithPath($"/export/{region}/{exportFilename}").UsingGet())
                .RespondWith(Response.Create().WithStatusCode(200).WithHeader("Content-Type", "application/json").WithBodyAsJson(mockResponse));
        }

        #region Private Methods
        /// <summary>
        /// Get the full file path of the servant data. Set "Copy to Output Directory" property to "Copy if newer" for JSON files
        /// </summary>
        /// <param name="region">NA or JP</param>
        /// <param name="className">AlterEgo, Archer, Assassin, Avenger, Berserker, Caster, Foreigner, Lancer, MoonCancer, Rider, Ruler, Saber, or Shielder</param>
        /// <param name="servantFilename">The name of the servant's JSON file</param>
        /// <returns>The filepath of the servant test data</returns>
        private static string JsonServantFilepath(string region, string className, string servantFilename)
        {
            return Path.Combine(Environment.CurrentDirectory, "Json", region, "ServantData", className, servantFilename);
        }

        /// <summary>
        /// Get the full file path of the craft essence data. Set "Copy to Output Directory" property to "Copy if newer" for JSON files
        /// </summary>
        /// <param name="region">NA or JP</param>
        /// <param name="craftEssenceFilename">The name of the craft essence's JSON file</param>
        /// <returns>The filepath of the craft essence test data</returns>
        private static string JsonCraftEssenceFilepath(string region, string craftEssenceFilename)
        {
            return Path.Combine(Environment.CurrentDirectory, "Json", region, "CraftEssenceData", craftEssenceFilename);
        }

        /// <summary>
        /// Get the full file path of the mystic code data. Set "Copy to Output Directory" property to "Copy if newer" for JSON files
        /// </summary>
        /// <param name="region">NA or JP</param>
        /// <param name="mysticCodeFilename">The name of the mystic code's JSON file</param>
        /// <returns>The filepath of the mystic code test data</returns>
        private static string JsonMysticCodeFilepath(string region, string mysticCodeFilename)
        {
            return Path.Combine(Environment.CurrentDirectory, "Json", region, "MysticCodeData", mysticCodeFilename);
        }

        /// <summary>
        /// Get the full file path of the mystic code data. Set "Copy to Output Directory" property to "Copy if newer" for JSON files
        /// </summary>
        /// <param name="region">NA or JP</param>
        /// <param name="exportFilename">The name of the export's JSON file</param>
        /// <returns>The filepath of the export test data</returns>
        private static string JsonExportFilepath(string region, string exportFilename)
        {
            return Path.Combine(Environment.CurrentDirectory, "Json", region, "Export", exportFilename);
        }
        #endregion
    }
}
