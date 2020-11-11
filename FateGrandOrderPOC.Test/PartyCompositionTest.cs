using System.Collections.Generic;
using System.Threading.Tasks;

using Autofac;

using FateGrandOrderPOC.Shared;
using FateGrandOrderPOC.Shared.AtlasAcademy;
using FateGrandOrderPOC.Shared.AtlasAcademy.Json;
using FateGrandOrderPOC.Shared.Models;
using FateGrandOrderPOC.Test.CoreModule;
using FateGrandOrderPOC.Test.Fixture;
using FateGrandOrderPOC.Test.HelperMethods;

using FluentAssertions;
using FluentAssertions.Execution;

using Xunit;

namespace FateGrandOrderPOC.Test
{
    public class PartyCompositionTest : IClassFixture<WireMockFixture>
    {
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
            ServantNiceJson testServant = LoadTestData.DeserializeServantJson("JP", "Caster", "500300-TamamoNoMaeCasterEN.json");

            testServant.Name.Should().Be("Tamamo-no-Mae");
        }

        [Fact]
        public void ConfirmJsonDeserializationServantNA()
        {
            // Set "Copy to Output Directory" to "Copy if newer" for JSON files
            ServantNiceJson testServant = LoadTestData.DeserializeServantJson("NA", "Caster", "500800-MerlinCaster.json");

            testServant.Name.Should().Be("Merlin");
        }

        [Fact]
        public async Task CreatePartyMemberWithCraftEssence()
        {
            _wiremockFixture.CheckIfMockServerInUse();

            const string KSCOPE_CE = "9400340";
            const string DANTES_AVENGER = "1100200";

            List<PartyMember> party = new List<PartyMember>();

            // build mock servant response
            ServantNiceJson mockServantResponse = LoadTestData.DeserializeServantJson(REGION, "Avenger", "1100200-EdmondDantesAvenger.json");
            LoadTestData.CreateNiceWireMockStub(_wiremockFixture, REGION, "servant", DANTES_AVENGER, mockServantResponse);

            // build mock craft essence response
            EquipNiceJson mockCraftEssenceResponse = LoadTestData.DeserializeCraftEssenceJson(REGION, "9400340-Kaleidoscope.json");
            LoadTestData.CreateNiceWireMockStub(_wiremockFixture, REGION, "equip", KSCOPE_CE, mockCraftEssenceResponse);

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
    }
}
