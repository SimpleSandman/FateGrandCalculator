using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

using FateGrandOrderPOC.Shared.AtlasAcademy;
using FateGrandOrderPOC.Shared.AtlasAcademy.Json;

using FluentAssertions;

using Newtonsoft.Json;

using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;

using Xunit;


namespace FateGrandOrderPOC.Test
{
    public class CombatFormulaTest : IClassFixture<WireMockFixture>
    {
        private readonly WireMockFixture _wiremockFixture;

        public CombatFormulaTest(WireMockFixture wiremockFixture)
        {
            _wiremockFixture = wiremockFixture;
        }

        [Fact]
        public void ConfirmJsonDeserializationJP()
        {
            _wiremockFixture.CheckIfMockServerInUse();
            
            // Set "Copy to Output Directory" to "Copy if newer" for JSON files
            string fullPath = ServantFilePath("JP", "Caster", "500300-TamamoNoMaeCasterEN.json");
            ServantNiceJson testServant = JsonConvert.DeserializeObject<ServantNiceJson>(File.ReadAllText(fullPath));

            testServant.Name.Should().Be("Tamamo-no-Mae");
        }

        [Fact]
        public void ConfirmJsonDeserializationNA()
        {
            _wiremockFixture.CheckIfMockServerInUse();

            // Set "Copy to Output Directory" to "Copy if newer" for JSON files
            string fullPath = ServantFilePath("NA", "Caster", "500800-MerlinCaster.json");
            ServantNiceJson testServant = JsonConvert.DeserializeObject<ServantNiceJson>(File.ReadAllText(fullPath));

            testServant.Name.Should().Be("Merlin");
        }

        #region Private Methods
        /// <summary>
        /// Get the full file path for the servant data
        /// </summary>
        /// <param name="region">NA or JP</param>
        /// <param name="className">AlterEgo, Archer, Assassin, Avenger, Berserker, Caster, Foreigner, Lancer, MoonCancer, Rider, Ruler, Saber, or Shielder</param>
        /// <param name="servantFilename">The name of the servant's JSON file</param>
        /// <returns></returns>
        private string ServantFilePath(string region, string className, string servantFilename)
        {
            return Path.Combine(Environment.CurrentDirectory, "Json", region, "ServantData", className, servantFilename);
        }
        #endregion
    }
}
