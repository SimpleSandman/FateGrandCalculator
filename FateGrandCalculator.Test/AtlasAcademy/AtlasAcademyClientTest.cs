using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Autofac;

using FateGrandCalculator.AtlasAcademy.Json;

using FateGrandCalculator.Test.Utility;
using FateGrandCalculator.Test.Utility.AutofacConfig;
using FateGrandCalculator.Test.Utility.Fixture;

using FluentAssertions;

using Newtonsoft.Json.Linq;

using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;

using Xunit;

namespace FateGrandCalculator.Test.AtlasAcademy
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
            _container = ContainerBuilderInit.Create(REGION);
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
                ScopedClasses resolvedClasses = AutofacUtility.ResolveScope(scope);
                ServantNiceJson response = await resolvedClasses.AtlasAcademyClient.GetServantInfo("1");

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
                ScopedClasses resolvedClasses = AutofacUtility.ResolveScope(scope);
                EquipNiceJson response = await resolvedClasses.AtlasAcademyClient.GetCraftEssenceInfo("1");

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
                ScopedClasses resolvedClasses = AutofacUtility.ResolveScope(scope);
                ClassAttackRateNiceJson response = await resolvedClasses.AtlasAcademyClient.GetClassAttackRateInfo();

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
                ScopedClasses resolvedClasses = AutofacUtility.ResolveScope(scope);
                ConstantNiceJson response = await resolvedClasses.AtlasAcademyClient.GetConstantGameInfo();

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
                ScopedClasses resolvedClasses = AutofacUtility.ResolveScope(scope);
                List<ServantBasicJson> response = await resolvedClasses.AtlasAcademyClient.GetListBasicServantInfo();

                response.First().Should().BeEquivalentTo(json);
            }
        }

        [Fact]
        public async Task GetListBasicEquipInfo()
        {
            _wiremockFixture.CheckIfMockServerInUse();

            List<EquipBasicJson> mockResponse = new List<EquipBasicJson>();
            EquipBasicJson json = new EquipBasicJson
            {
                Id = 0
            };

            mockResponse.Add(json);

            _wiremockFixture.MockServer
                .Given(Request.Create().WithPath($"/export/{REGION}/basic_equip.json").UsingGet())
                .RespondWith(Response.Create().WithStatusCode(200).WithHeader(CONTENT_TYPE_HEADER, CONTENT_TYPE_APPLICATION_JSON).WithBodyAsJson(mockResponse));

            using (var scope = _container.BeginLifetimeScope())
            {
                ScopedClasses resolvedClasses = AutofacUtility.ResolveScope(scope);
                List<EquipBasicJson> response = await resolvedClasses.AtlasAcademyClient.GetListBasicEquipInfo();

                response.First().Should().BeEquivalentTo(json);
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
                ScopedClasses resolvedClasses = AutofacUtility.ResolveScope(scope);
                MysticCodeNiceJson response = await resolvedClasses.AtlasAcademyClient.GetMysticCodeInfo("1");

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
                ScopedClasses resolvedClasses = AutofacUtility.ResolveScope(scope);
                JObject response = await resolvedClasses.AtlasAcademyClient.GetTraitEnumInfo();

                string traitName = response.Property("1")?.Value.ToString() ?? "";
                traitName.Should().Be("genderMale");
            }
        }

        [Fact]
        public async Task GetAttributeRelationInfo()
        {
            _wiremockFixture.CheckIfMockServerInUse();

            AttributeRelationNiceJson mockResponse = new AttributeRelationNiceJson
            {
                Human = new Human
                {
                    HumanMultiplier = 1000
                }
            };

            _wiremockFixture.MockServer
                .Given(Request.Create().WithPath($"/export/{REGION}/NiceAttributeRelation.json").UsingGet())
                .RespondWith(Response.Create().WithStatusCode(200).WithHeader(CONTENT_TYPE_HEADER, CONTENT_TYPE_APPLICATION_JSON).WithBodyAsJson(mockResponse));

            using (var scope = _container.BeginLifetimeScope())
            {
                ScopedClasses resolvedClasses = AutofacUtility.ResolveScope(scope);
                AttributeRelationNiceJson response = await resolvedClasses.AtlasAcademyClient.GetAttributeRelationInfo();

                response.Human.HumanMultiplier.Should().Be(1000);
            }
        }

        [Fact]
        public async Task GetClassRelationInfo()
        {
            _wiremockFixture.CheckIfMockServerInUse();

            ClassRelationNiceJson mockResponse = new ClassRelationNiceJson
            {
                Saber = new Saber
                {
                    SaberMultiplier = 1000
                }
            };

            _wiremockFixture.MockServer
                .Given(Request.Create().WithPath($"/export/{REGION}/NiceClassRelation.json").UsingGet())
                .RespondWith(Response.Create().WithStatusCode(200).WithHeader(CONTENT_TYPE_HEADER, CONTENT_TYPE_APPLICATION_JSON).WithBodyAsJson(mockResponse));

            using (var scope = _container.BeginLifetimeScope())
            {
                ScopedClasses resolvedClasses = AutofacUtility.ResolveScope(scope);
                ClassRelationNiceJson response = await resolvedClasses.AtlasAcademyClient.GetClassRelationInfo();

                response.Saber.SaberMultiplier.Should().Be(1000);
            }
        }

        [Fact]
        public async Task GetEnemyCollectionServantInfo()
        {
            _wiremockFixture.CheckIfMockServerInUse();

            // build mock response
            ServantNiceJson mockResponse = new ServantNiceJson
            {
                Id = 1,
                AtkBase = 1000
            };

            LoadTestData.CreateNiceWireMockStub(_wiremockFixture, REGION, "svt", "1", mockResponse);

            using (var scope = _container.BeginLifetimeScope())
            {
                ScopedClasses resolvedClasses = AutofacUtility.ResolveScope(scope);
                ServantNiceJson response = await resolvedClasses.AtlasAcademyClient.GetEnemyCollectionServantInfo("1");

                response.AtkBase.Should().Be(1000);
            }
        }
    }
}
