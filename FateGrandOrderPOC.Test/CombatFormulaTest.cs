using System.Collections.Generic;
using System.Threading.Tasks;

using FateGrandOrderPOC.Shared.AtlasAcademy;
using FateGrandOrderPOC.Shared.AtlasAcademy.Json;

using FluentAssertions;

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
        public async Task ReplaceThisWithRealTesting()
        {
            _wiremockFixture.CheckIfMockServerInUse();

            // do some testing here
        }
    }
}
