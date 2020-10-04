using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

using FateGrandOrderPOC.Shared.AtlasAcademy;
using FateGrandOrderPOC.Shared.AtlasAcademy.Json;

using FluentAssertions;

using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;

using Xunit;

namespace FateGrandOrderPOC.Test
{
    public class AtlasAcademyClientTest : IClassFixture<WireMockFixture>
    {
        const string CONTENT_TYPE_HEADER = "Content-Type";
        const string CONTENT_TYPE_APPLICATION_JSON = "application/json";
        const string REGION = "NA";

        private readonly WireMockFixture _wiremockFixture;
        private bool _isMockServerInUse;

        public AtlasAcademyClientTest(WireMockFixture wiremockFixture)
        {
            _wiremockFixture = wiremockFixture;
        }

        [Fact]
        public async Task TestGetServantInfo()
        {
            CheckIfMockServerInUse();

            // build mock response
            ServantNiceJson mockResponse = new ServantNiceJson
            {
                Id = 1,
                AtkBase = 1000
            };

            _wiremockFixture.MockServer.Given(Request.Create().WithPath($"/nice/{REGION}/servant/1").WithParam("lang", "en").UsingGet())
            .RespondWith(Response.Create().WithStatusCode(200).WithHeader(CONTENT_TYPE_HEADER, CONTENT_TYPE_APPLICATION_JSON).WithBodyAsJson(mockResponse));

            // test the REST client
            IAtlasAcademyClient client = new AtlasAcademyClient(() => WireMockFixture.SERVER_URL);
            ServantNiceJson response = await client.GetServantInfo("1");

            response.AtkBase.Should().Be(1000);
        }

        [Fact]
        public async Task TestGetCraftEssenceInfo()
        {
            CheckIfMockServerInUse();

            EquipNiceJson mockResponse = new EquipNiceJson
            {
                Id = 1,
                AtkBase = 600
            };

            _wiremockFixture.MockServer.Given(Request.Create().WithPath($"/nice/{REGION}/equip/1").WithParam("lore", "true").WithParam("lang", "en").UsingGet())
            .RespondWith(Response.Create().WithStatusCode(200).WithHeader(CONTENT_TYPE_HEADER, CONTENT_TYPE_APPLICATION_JSON).WithBodyAsJson(mockResponse));

            IAtlasAcademyClient client = new AtlasAcademyClient(() => WireMockFixture.SERVER_URL);
            EquipNiceJson response = await client.GetCraftEssenceInfo("1");

            response.Id.Should().Be(1);
            response.AtkBase.Should().Be(600);
        }

        [Fact]
        public async Task TestGetClassAttackRateInfo()
        {
            CheckIfMockServerInUse();

            ClassAttackRateNiceJson mockResponse = new ClassAttackRateNiceJson
            {
                Lancer = 105
            };

            _wiremockFixture.MockServer.Given(Request.Create().WithPath($"/export/{REGION}/NiceClassAttackRate.json").UsingGet())
            .RespondWith(Response.Create().WithStatusCode(200).WithHeader(CONTENT_TYPE_HEADER, CONTENT_TYPE_APPLICATION_JSON).WithBodyAsJson(mockResponse));

            IAtlasAcademyClient client = new AtlasAcademyClient(() => WireMockFixture.SERVER_URL);
            ClassAttackRateNiceJson response = await client.GetClassAttackRateInfo();

            response.Lancer.Should().Be(105);
        }

        [Fact]
        public async Task TestGetConstantGameInfo()
        {
            CheckIfMockServerInUse();

            ConstantNiceJson mockResponse = new ConstantNiceJson
            {
                USER_COST = 6
            };

            _wiremockFixture.MockServer.Given(Request.Create().WithPath($"/export/{REGION}/NiceConstant.json").UsingGet())
            .RespondWith(Response.Create().WithStatusCode(200).WithHeader(CONTENT_TYPE_HEADER, CONTENT_TYPE_APPLICATION_JSON).WithBodyAsJson(mockResponse));

            IAtlasAcademyClient client = new AtlasAcademyClient(() => WireMockFixture.SERVER_URL);
            ConstantNiceJson response = await client.GetConstantGameInfo();

            response.USER_COST.Should().Be(6);
        }

        [Fact]
        public async Task TestGetListBasicServantInfo()
        {
            CheckIfMockServerInUse();

            List<ServantBasicJson> mockResponse = new List<ServantBasicJson>();
            ServantBasicJson json = new ServantBasicJson
            {
                ClassName = "Lancer"
            };

            mockResponse.Add(json);

            _wiremockFixture.MockServer.Given(Request.Create().WithUrl($"{WireMockFixture.SERVER_URL}/export/{REGION}/basic_servant.json").UsingGet())
            .RespondWith(Response.Create().WithStatusCode(200).WithHeader(CONTENT_TYPE_HEADER, CONTENT_TYPE_APPLICATION_JSON).WithBodyAsJson(mockResponse));

            IAtlasAcademyClient client = new AtlasAcademyClient(() => WireMockFixture.SERVER_URL);
            List<ServantBasicJson> response = await client.GetListBasicServantInfo();

            response.Should().BeEquivalentTo(json);
        }

        #region Private Methods
        /// <summary>
        /// Check if the TCP port the mock server is using is free for the next test
        /// </summary>
        private void CheckIfMockServerInUse()
        {
            while (_isMockServerInUse)
            {
                IPGlobalProperties ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();

                _isMockServerInUse = ipGlobalProperties
                    .GetActiveTcpConnections()
                    .Any(p => p.LocalEndPoint.Port == _wiremockFixture.MockServer.Ports[0]);
            }

            _wiremockFixture.MockServer.Reset(); // clean up for the next test
        }
        #endregion
    }
}
