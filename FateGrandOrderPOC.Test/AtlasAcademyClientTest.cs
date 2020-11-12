using System.Collections.Generic;
using System.Threading.Tasks;

using Autofac;

using FateGrandOrderPOC.Shared.AtlasAcademy;
using FateGrandOrderPOC.Shared.AtlasAcademy.Json;
using FateGrandOrderPOC.Test.CoreModule;
using FateGrandOrderPOC.Test.Fixture;
using FateGrandOrderPOC.Test.Utility;

using FluentAssertions;

using Newtonsoft.Json.Linq;

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
        private readonly IContainer _container;

        public AtlasAcademyClientTest(WireMockFixture wiremockFixture)
        {
            _wiremockFixture = wiremockFixture;

            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterModule(new AutofacModule 
            { 
                BaseApiUrl = new NamedParameter("baseApiUrl", WireMockFixture.SERVER_URL),
                AtlasAcademyClient = new NamedParameter("client", new AtlasAcademyClient(WireMockFixture.SERVER_URL, REGION)),
                Region = new NamedParameter("region", REGION)
            });

            _container = builder.Build();
        }

        [Fact]
        public async Task GetServantInfo()
        {
            _wiremockFixture.CheckIfMockServerInUse();

            // build mock response
            ServantNiceJson mockResponse = new ServantNiceJson
            {
                Id = 1,
                AtkBase = 1000
            };

            LoadTestData.CreateNiceWireMockStub(_wiremockFixture, REGION, "servant", "1", mockResponse);

            using (var scope = _container.BeginLifetimeScope())
            {
                AtlasAcademyClient client = scope.Resolve<AtlasAcademyClient>();
                ServantNiceJson response = await client.GetServantInfo("1");

                response.AtkBase.Should().Be(1000);
            }
        }

        [Fact]
        public async Task GetCraftEssenceInfo()
        {
            _wiremockFixture.CheckIfMockServerInUse();

            EquipNiceJson mockResponse = new EquipNiceJson
            {
                Id = 1,
                AtkBase = 600
            };

            LoadTestData.CreateNiceWireMockStub(_wiremockFixture, REGION, "equip", "1", mockResponse);

            using (var scope = _container.BeginLifetimeScope())
            {
                AtlasAcademyClient client = scope.Resolve<AtlasAcademyClient>();
                EquipNiceJson response = await client.GetCraftEssenceInfo("1");

                response.Id.Should().Be(1);
                response.AtkBase.Should().Be(600);
            }
        }

        [Fact]
        public async Task GetClassAttackRateInfo()
        {
            _wiremockFixture.CheckIfMockServerInUse();

            ClassAttackRateNiceJson mockResponse = new ClassAttackRateNiceJson
            {
                Lancer = 105
            };

            _wiremockFixture.MockServer
                .Given(Request.Create().WithPath($"/export/{REGION}/NiceClassAttackRate.json").UsingGet())
                .RespondWith(Response.Create().WithStatusCode(200).WithHeader(CONTENT_TYPE_HEADER, CONTENT_TYPE_APPLICATION_JSON).WithBodyAsJson(mockResponse));

            using (var scope = _container.BeginLifetimeScope())
            {
                AtlasAcademyClient client = scope.Resolve<AtlasAcademyClient>();
                ClassAttackRateNiceJson response = await client.GetClassAttackRateInfo();

                response.Lancer.Should().Be(105);
            }
        }

        [Fact]
        public async Task GetConstantGameInfo()
        {
            _wiremockFixture.CheckIfMockServerInUse();

            ConstantNiceJson mockResponse = new ConstantNiceJson
            {
                USER_COST = 6
            };

            _wiremockFixture.MockServer
                .Given(Request.Create().WithPath($"/export/{REGION}/NiceConstant.json").UsingGet())
                .RespondWith(Response.Create().WithStatusCode(200).WithHeader(CONTENT_TYPE_HEADER, CONTENT_TYPE_APPLICATION_JSON).WithBodyAsJson(mockResponse));

            using (var scope = _container.BeginLifetimeScope())
            {
                AtlasAcademyClient client = scope.Resolve<AtlasAcademyClient>();
                ConstantNiceJson response = await client.GetConstantGameInfo();

                response.USER_COST.Should().Be(6);
            }
        }

        [Fact]
        public async Task GetListBasicServantInfo()
        {
            _wiremockFixture.CheckIfMockServerInUse();

            List<ServantBasicJson> mockResponse = new List<ServantBasicJson>();
            ServantBasicJson json = new ServantBasicJson
            {
                ClassName = "Lancer"
            };

            mockResponse.Add(json);

            _wiremockFixture.MockServer
                .Given(Request.Create().WithPath($"/export/{REGION}/basic_servant.json").UsingGet())
                .RespondWith(Response.Create().WithStatusCode(200).WithHeader(CONTENT_TYPE_HEADER, CONTENT_TYPE_APPLICATION_JSON).WithBodyAsJson(mockResponse));

            using (var scope = _container.BeginLifetimeScope())
            {
                AtlasAcademyClient client = scope.Resolve<AtlasAcademyClient>();
                List<ServantBasicJson> response = await client.GetListBasicServantInfo();

                response.Should().BeEquivalentTo(json);
            }
        }

        [Fact]
        public async Task GetMysticCodeInfo()
        {
            _wiremockFixture.CheckIfMockServerInUse();

            MysticCodeNiceJson mockResponse = new MysticCodeNiceJson
            {
                Id = 1
            };

            LoadTestData.CreateNiceWireMockStub(_wiremockFixture, REGION, "MC", "1", mockResponse);

            using (var scope = _container.BeginLifetimeScope())
            {
                AtlasAcademyClient client = scope.Resolve<AtlasAcademyClient>();
                MysticCodeNiceJson response = await client.GetMysticCodeInfo("1");

                response.Id.Should().Be(1);
            }
        }

        [Fact]
        public async Task GetTraitEnumInfo()
        {
            _wiremockFixture.CheckIfMockServerInUse();

            JObject mockResponse = new JObject
            {
                { "1", JToken.Parse(@"'genderMale'") }
            };

            // same for both NA and JP
            _wiremockFixture.MockServer
                .Given(Request.Create().WithPath($"/export/JP/nice_trait.json").UsingGet())
                .RespondWith(Response.Create().WithStatusCode(200).WithHeader(CONTENT_TYPE_HEADER, CONTENT_TYPE_APPLICATION_JSON).WithBodyAsJson(mockResponse));

            using (var scope = _container.BeginLifetimeScope())
            {
                AtlasAcademyClient client = scope.Resolve<AtlasAcademyClient>();
                JObject response = await client.GetTraitEnumInfo();

                string traitName = response.Property("1")?.Value.ToString() ?? "";
                traitName.Should().Be("genderMale");
            }
        }
    }
}
