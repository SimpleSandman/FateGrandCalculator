using System;
using System.Collections.Generic;

using FateGrandOrderPOC.Shared.AtlasAcademy;
using FateGrandOrderPOC.Shared.AtlasAcademy.Json;

using FluentAssertions;

using WireMock.Logging;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;
using WireMock.Settings;
using WireMock.Handlers;

using Xunit;

namespace FateGrandOrderPOC.Test
{
    public class WiremockFixture : IDisposable
    {
        public const string SERVER_URL = "http://localhost:9090/";

        public WireMockServer MockServer { get; private set; }

        public WiremockFixture()
        {
            // bootstrap mockserver
            MockServer = WireMockServer.Start(new WireMockServerSettings
            {
                Urls = new[] { SERVER_URL },
                StartAdminInterface = true,
                Logger = new WireMockConsoleLogger(),
                AllowPartialMapping = true
            });
        }

        public void Dispose()
        {
            MockServer.Stop();
        }
    }

    public class AtlasAcademyClientTest : IClassFixture<WiremockFixture>
    {
        const string CONTENT_TYPE_HEADER = "Content-Type";
        const string CONTENT_TYPE_APPLICATION_JSON = "application/json";
        const string REGION = "NA";

        private readonly WiremockFixture _wiremockFixture;

        public AtlasAcademyClientTest(WiremockFixture wiremockFixture)
        {
            _wiremockFixture = wiremockFixture;
        }

        [Fact]
        public void TestGetServantInfo()
        {
            // build mock response
            ServantNiceJson mockResponse = new ServantNiceJson();
            mockResponse.Id = 1;
            mockResponse.AtkBase = 1000;

            _wiremockFixture.MockServer.Given(Request.Create().WithPath($"/nice/{REGION}/servant/1").WithParam("lang", "en").UsingGet())
            .RespondWith(Response.Create().WithStatusCode(200).WithHeader(CONTENT_TYPE_HEADER, CONTENT_TYPE_APPLICATION_JSON).WithBodyAsJson(mockResponse));

            // test the REST client
            IAtlasAcademyClient client = new AtlasAcademyClient(() => WiremockFixture.SERVER_URL);
            ServantNiceJson response = client.GetServantInfo("1");

            response.AtkBase.Should().Be(1000);
        }

        [Fact]
        public void TestGetCraftEssenceInfo()
        {
            EquipNiceJson mockResponse = new EquipNiceJson();
            mockResponse.Id = 1;
            mockResponse.AtkBase = 600;

            _wiremockFixture.MockServer.Given(Request.Create().WithPath($"/nice/{REGION}/equip/1").WithParam("lore", "true").WithParam("lang", "en").UsingGet())
            .RespondWith(Response.Create().WithStatusCode(200).WithHeader(CONTENT_TYPE_HEADER, CONTENT_TYPE_APPLICATION_JSON).WithBodyAsJson(mockResponse));

            IAtlasAcademyClient client = new AtlasAcademyClient(() => WiremockFixture.SERVER_URL);
            EquipNiceJson response = client.GetCraftEssenceInfo("1");

            response.Id.Should().Be(1);
            response.AtkBase.Should().Be(600);
        }

        [Fact]
        public void TestGetClassAttackRateInfo()
        {
            ClassAttackRateNiceJson mockResponse = new ClassAttackRateNiceJson();
            mockResponse.Lancer = 105;

            _wiremockFixture.MockServer.Given(Request.Create().WithPath($"/export/{REGION}/NiceClassAttackRate.json").UsingGet())
            .RespondWith(Response.Create().WithStatusCode(200).WithHeader(CONTENT_TYPE_HEADER, CONTENT_TYPE_APPLICATION_JSON).WithBodyAsJson(mockResponse));

            IAtlasAcademyClient client = new AtlasAcademyClient(() => WiremockFixture.SERVER_URL);
            ClassAttackRateNiceJson response = client.GetClassAttackRateInfo();

            response.Lancer.Should().Be(105);
        }

        [Fact]
        public void TestGetConstantGameInfo()
        {
            ConstantNiceJson mockResponse = new ConstantNiceJson();
            mockResponse.USER_COST = 6;

            _wiremockFixture.MockServer.Given(Request.Create().WithPath($"/export/{REGION}/NiceConstant.json").UsingGet())
            .RespondWith(Response.Create().WithStatusCode(200).WithHeader(CONTENT_TYPE_HEADER, CONTENT_TYPE_APPLICATION_JSON).WithBodyAsJson(mockResponse));

            IAtlasAcademyClient client = new AtlasAcademyClient(() => WiremockFixture.SERVER_URL);
            ConstantNiceJson response = client.GetConstantGameInfo();

            response.USER_COST.Should().Be(6);
        }

        [Fact]
        public void TestGetListBasicServantInfo()
        {
            List<ServantBasicJson> mockResponse = new List<ServantBasicJson>();
            ServantBasicJson json = new ServantBasicJson();
            json.ClassName = "Lancer";
            mockResponse.Add(json);

            _wiremockFixture.MockServer.Given(Request.Create().WithUrl($"{WiremockFixture.SERVER_URL}/export/{REGION}/basic_servant.json").UsingGet())
            .RespondWith(Response.Create().WithStatusCode(200).WithHeader(CONTENT_TYPE_HEADER, CONTENT_TYPE_APPLICATION_JSON).WithBodyAsJson(mockResponse));

            IAtlasAcademyClient client = new AtlasAcademyClient(() => WiremockFixture.SERVER_URL);
            List<ServantBasicJson> response = client.GetListBasicServantInfo();

            response.Should().BeEquivalentTo(json);
        }
    }
}
