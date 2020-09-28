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
using System;
using System.Collections.Generic;

namespace FateGrandOrderPOC.Test
{

    public class JsonFilesystemHandler : IFileSystemHandler
    {
        public void CreateFolder(string path)
        {
            throw new NotImplementedException();
        }

        public void DeleteFile(string filename)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> EnumerateFiles(string path, bool includeSubdirectories)
        {
            throw new NotImplementedException();
        }

        public bool FileExists(string filename)
        {
            throw new NotImplementedException();
        }

        public bool FolderExists(string path)
        {
            throw new NotImplementedException();
        }

        public string GetMappingFolder()
        {
            throw new NotImplementedException();
        }

        public byte[] ReadFile(string filename)
        {
            throw new NotImplementedException();
        }

        public string ReadMappingFile(string path)
        {
            throw new NotImplementedException();
        }

        public byte[] ReadResponseBodyAsFile(string path)
        {
            throw new NotImplementedException();
        }

        public string ReadResponseBodyAsString(string path)
        {
            throw new NotImplementedException();
        }

        public void WriteFile(string filename, byte[] bytes)
        {
            throw new NotImplementedException();
        }

        public void WriteMappingFile(string path, string text)
        {
            throw new NotImplementedException();
        }
    }

    public class WiremockFixture : IDisposable
    {

        public WireMockServer MockServer { get; private set; }

        public WiremockFixture()
        {
            // bootstrap mockserver
            this.MockServer = WireMockServer.Start(new WireMockServerSettings

            {
                Urls = new[] { "http://localhost:9090/" },
                StartAdminInterface = true,
                Logger = new WireMockConsoleLogger(),
                AllowPartialMapping = true,
                FileSystemHandler = new JsonFilesystemHandler()
            });
        }

        public void Dispose()
        {
            MockServer.Stop();
        }

    }

    public class AtlasAcademyClientTest : IClassFixture<WiremockFixture>
    {

        const String CONTENT_TYPE_HEADER = "Content-Type";
        const String CONTENT_TYPE_APPLICATION_JSON = "application/json";

        private WiremockFixture WiremockFixture;

        public AtlasAcademyClientTest(WiremockFixture WiremockFixture)
        {
            this.WiremockFixture = WiremockFixture;
        }

        [Fact]
        public void TestGetServantInfo()
        {
            // build mock response
            ServantNiceJson mockResponse = new ServantNiceJson();
            mockResponse.Id = 1;
            mockResponse.AtkBase = 1000;

            WiremockFixture.MockServer.Given(Request.Create().WithPath("/nice/NA/servant/1").WithParam("lang", "en").UsingGet())
            .RespondWith(Response.Create().WithStatusCode(200).WithHeader(CONTENT_TYPE_HEADER, CONTENT_TYPE_APPLICATION_JSON).WithBodyAsJson(mockResponse));

            // test the REST client
            IAtlasAcademyClient client = new AtlasAcademyClient(() => "http://localhost:9090");
            ServantNiceJson response = client.GetServantInfo("1");

            response.AtkBase.Should().Be(1000);

        }

        [Fact]
        public void TestGetCraftEssenceInfo()
        {
            EquipNiceJson mockResponse = new EquipNiceJson();
            mockResponse.Id = 1;
            mockResponse.AtkBase = 600;

            WiremockFixture.MockServer.Given(Request.Create().WithPath("/nice/NA/equip/1").WithParam("lore", "true").WithParam("lang", "en").UsingGet())
            .RespondWith(Response.Create().WithStatusCode(200).WithHeader(CONTENT_TYPE_HEADER, CONTENT_TYPE_APPLICATION_JSON).WithBodyAsJson(mockResponse));

            IAtlasAcademyClient client = new AtlasAcademyClient(() => "http://localhost:9090");
            EquipNiceJson response = client.GetCraftEssenceInfo("1");

            response.Id.Should().Be(1);
            response.AtkBase.Should().Be(600);
        }

        [Fact]
        public void TestGetClassAttackRateInfo()
        {
            ClassAttackRateNiceJson mockResponse = new ClassAttackRateNiceJson();
            mockResponse.Lancer = 105;

            WiremockFixture.MockServer.Given(Request.Create().WithPath("/export/NA/NiceClassAttackRate.json").UsingGet())
            .RespondWith(Response.Create().WithStatusCode(200).WithHeader(CONTENT_TYPE_HEADER, CONTENT_TYPE_APPLICATION_JSON).WithBodyAsJson(mockResponse));

            IAtlasAcademyClient client = new AtlasAcademyClient(() => "http://localhost:9000");
            ClassAttackRateNiceJson response = client.GetClassAttackRateInfo();

            response.Lancer.Should().Be(105);
        }

        [Fact]
        public void TestGetConstantGameInfo()
        {
            ConstantNiceJson mockResponse = new ConstantNiceJson();
            mockResponse.USER_COST = 6;

            WiremockFixture.MockServer.Given(Request.Create().WithPath("/export/NA/NiceConstant.json").UsingGet())
            .RespondWith(Response.Create().WithStatusCode(200).WithHeader(CONTENT_TYPE_HEADER, CONTENT_TYPE_APPLICATION_JSON).WithBodyAsJson(mockResponse));

            IAtlasAcademyClient client = new AtlasAcademyClient(() => "http://localhost:9000");
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

            WiremockFixture.MockServer.Given(Request.Create().WithPath("/export/NA/basic_servant.json").UsingGet())
            .RespondWith(Response.Create().WithStatusCode(200).WithHeader(CONTENT_TYPE_HEADER, CONTENT_TYPE_APPLICATION_JSON).WithBodyAsJson(mockResponse));

            IAtlasAcademyClient client = new AtlasAcademyClient(() => "http://localhost:9000");
            List<ServantBasicJson> response = client.GetListBasicServantInfo();

            response.Should().Contain(json);
        }
    }
}
