using System;
using Xunit;
using WireMock.Server;
using WireMock.Settings;
using WireMock.Logging;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using FateGrandOrderPOC.Shared.AtlasAcademy;
using FateGrandOrderPOC.Shared.AtlasAcademy.Json;
using FluentAssertions;

namespace FateGrandOrderPOC.Test
{

    public class WiremockFixture : IDisposable
    {

        private WireMockServer mockServer { get; private set; }

        public WiremockFixture()
        {
            // bootstrap mockserver
            this.mockServer = WireMockServer.Start(new WireMockServerSettings
            {
                Urls = new[] { "http://localhost:9090/" },
                StartAdminInterface = true,
                Logger = new WireMockConsoleLogger(),
                AllowPartialMapping = true
            });
        }

        public void Dispose()
        {
            mockServer.Stop();
        }

    }

    public class AtlasAcademyClientTest : IClassFixture<WiremockFixture>
    {

        private WiremockFixture _wiremockFixture;

        public AtlasAcademyClientTest(WiremockFixture wiremockFixture)
        {
            this._wiremockFixture = wiremockFixture;
        }

        [Fact]
        public void TestGetServantInfo()
        {
            // build mock response
            ServantNiceJson mockResponse = new ServantNiceJson();
            mockResponse.Id = 1;
            mockResponse.AtkBase = 1000;

            _wiremockFixture.MockServer().Given(Request.Create().WithPath("/nice/NA/servant/1").WithParam("lang", "en").UsingGet())
            .RespondWith(Response.Create().WithStatusCode(200).WithHeader("Content-Type", "application/json").WithBodyAsJson(mockResponse));

            // test the REST client
            IAtlasAcademyClient client = new AtlasAcademyClient(() => "http://localhost:9090");
            ServantNiceJson response = client.GetServantInfo("1");

            response.AtkBase.Should().Be(1000);

        }

        [Fact]
        public void TestGetEquip()
        {

        }

        [Fact]
        public void TestGetClassAttackRateInfo()
        {

        }

        [Fact]
        public void TestGetConstantGameInfo()
        {

        }

        [Fact]
        public void TestGetListBasicServantInfo()
        {

        }
    }
}
