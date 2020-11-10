using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

using Autofac;

using FateGrandOrderPOC.Shared;
using FateGrandOrderPOC.Shared.AtlasAcademy;
using FateGrandOrderPOC.Shared.AtlasAcademy.Json;
using FateGrandOrderPOC.Shared.Models;

using FluentAssertions;
using FluentAssertions.Execution;

using Newtonsoft.Json;

using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;

using Xunit;

namespace FateGrandOrderPOC.Test
{
    public class PartyCompositionTest : IClassFixture<WireMockFixture>
    {
        const string CONTENT_TYPE_HEADER = "Content-Type";
        const string CONTENT_TYPE_APPLICATION_JSON = "application/json";
        const string REGION = "NA";

        private readonly WireMockFixture _wiremockFixture;
        private readonly IContainer _container;

        public PartyCompositionTest(WireMockFixture wiremockFixture)
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
        public void ConfirmJsonDeserializationServantJP()
        {
            // Set "Copy to Output Directory" to "Copy if newer" for JSON files
            string fullPath = HelperMethods.JsonServantFilepath("JP", "Caster", "500300-TamamoNoMaeCasterEN.json");
            ServantNiceJson testServant = JsonConvert.DeserializeObject<ServantNiceJson>(File.ReadAllText(fullPath));

            testServant.Name.Should().Be("Tamamo-no-Mae");
        }

        [Fact]
        public void ConfirmJsonDeserializationServantNA()
        {
            // Set "Copy to Output Directory" to "Copy if newer" for JSON files
            string fullPath = HelperMethods.JsonServantFilepath("NA", "Caster", "500800-MerlinCaster.json");
            ServantNiceJson testServant = JsonConvert.DeserializeObject<ServantNiceJson>(File.ReadAllText(fullPath));

            testServant.Name.Should().Be("Merlin");
        }

        [Fact]
        public async Task GetPartyMemberInfo()
        {
            _wiremockFixture.CheckIfMockServerInUse();

            const string KSCOPE_CE = "9400340";
            const string DANTES_AVENGER = "1100200";

            List<PartyMember> party = new List<PartyMember>();

            // build mock servant response
            CreateNiceWireMockStub(REGION, "servant", DANTES_AVENGER, CreateServantMockResponse(REGION, "Avenger", "1100200-EdmondDantesAvenger.json"));

            // build mock craft essence response
            CreateNiceWireMockStub(REGION, "equip", KSCOPE_CE, CreateCraftEssenceMockResponse(REGION, "9400340-Kaleidoscope.json"));

            using (var scope = _container.BeginLifetimeScope())
            {
                AtlasAcademyClient aaClient = scope.Resolve<AtlasAcademyClient>();
                CombatFormula cfClient = scope.Resolve<CombatFormula>();

                CraftEssence chaldeaKscope = new CraftEssence
                {
                    CraftEssenceLevel = 100,
                    Mlb = true,
                    CraftEssenceInfo = await aaClient.GetCraftEssenceInfo(KSCOPE_CE)
                };

                Servant chaldeaServant = new Servant
                {
                    ServantLevel = 90,
                    NpLevel = 1,
                    FouHealth = 1000,
                    FouAttack = 1000,
                    SkillLevels = new int[3] { 10, 10, 10 },
                    IsSupportServant = false,
                    ServantInfo = await aaClient.GetServantInfo(DANTES_AVENGER)
                };

                PartyMember partyMember = cfClient.AddPartyMember(party, chaldeaServant, chaldeaKscope);
                cfClient.ApplyCraftEssenceEffects(partyMember);

                using (new AssertionScope())
                {
                    partyMember.EquippedCraftEssence.Mlb.Should().BeTrue();
                    partyMember.NpCharge.Should().Be(100.0f);
                }
            }
        }

        #region Private Methods
        private ServantNiceJson CreateServantMockResponse(string region, string className, string filename)
        {
            string fullFilepath = HelperMethods.JsonServantFilepath(region, className, filename);
            return JsonConvert.DeserializeObject<ServantNiceJson>(File.ReadAllText(fullFilepath));
        }

        private EquipNiceJson CreateCraftEssenceMockResponse(string region, string filename)
        {
            string fullFilepath = HelperMethods.JsonCraftEssenceFilepath(region, filename);
            return JsonConvert.DeserializeObject<EquipNiceJson>(File.ReadAllText(fullFilepath));
        }

        private void CreateNiceWireMockStub<T>(string region, string objectPath, string servantId, T mockResponse)
        {
            _wiremockFixture.MockServer
                .Given(Request.Create().WithPath($"/nice/{region}/{objectPath}/{servantId}").WithParam("lang", "en").UsingGet())
                .RespondWith(Response.Create().WithStatusCode(200).WithHeader(CONTENT_TYPE_HEADER, CONTENT_TYPE_APPLICATION_JSON).WithBodyAsJson(mockResponse));
        }
        #endregion
    }
}
