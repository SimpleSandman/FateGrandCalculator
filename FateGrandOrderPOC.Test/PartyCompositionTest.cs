using System.Collections.Generic;
using System.Threading.Tasks;

using Autofac;

using FateGrandOrderPOC.Shared.AtlasAcademy;
using FateGrandOrderPOC.Shared.AtlasAcademy.Json;
using FateGrandOrderPOC.Shared.Models;
using FateGrandOrderPOC.Test.Fixture;
using FateGrandOrderPOC.Test.Utility;

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
            _container = ContainerBuilderInit.Create(REGION);
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
                ScopedClasses resolvedClasses = AutofacUtility.ResolveScope(scope);

                CraftEssence chaldeaKscope = new CraftEssence
                {
                    CraftEssenceLevel = 100,
                    Mlb = true,
                    CraftEssenceInfo = await resolvedClasses.AtlasAcademyClient.GetCraftEssenceInfo(KSCOPE_CE)
                };

                Servant chaldeaServant = await FrequentlyUsed.ServantAsync(resolvedClasses.AtlasAcademyClient, DANTES_AVENGER, 1, false);

                PartyMember partyMember = resolvedClasses.CombatFormula.AddPartyMember(party, chaldeaServant, chaldeaKscope);
                resolvedClasses.CombatFormula.ApplyCraftEssenceEffects(partyMember);

                using (new AssertionScope())
                {
                    partyMember.EquippedCraftEssence.Mlb.Should().BeTrue();
                    partyMember.NpCharge.Should().Be(100.0f);
                }
            }
        }

        [Fact]
        public async Task SetMysticCode()
        {
            _wiremockFixture.CheckIfMockServerInUse();

            const string ARTIC_ID = "110";

            // build mock mystic code response
            MysticCodeNiceJson mockMysticCodeResponse = LoadTestData.DeserializeMysticCodeJson(REGION, "110-Artic.json");
            LoadTestData.CreateNiceWireMockStub(_wiremockFixture, REGION, "MC", ARTIC_ID, mockMysticCodeResponse);

            using (var scope = _container.BeginLifetimeScope())
            {
                AtlasAcademyClient aaClient = scope.Resolve<AtlasAcademyClient>();

                MysticCode mysticCode = new MysticCode
                {
                    MysticCodeLevel = 4,
                    MysticCodeInfo = await aaClient.GetMysticCodeInfo(ARTIC_ID)
                };

                mysticCode.MysticCodeInfo.Id.Should().Be(110);
            }
        }
    }
}
