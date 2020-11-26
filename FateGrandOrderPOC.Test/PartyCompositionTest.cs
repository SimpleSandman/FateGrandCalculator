using System.Collections.Generic;
using System.Threading.Tasks;

using Autofac;

using FateGrandOrderPOC.AtlasAcademy;
using FateGrandOrderPOC.AtlasAcademy.Json;
using FateGrandOrderPOC.Models;
using FateGrandOrderPOC.Test.Fixture;
using FateGrandOrderPOC.Test.Utility;

using FluentAssertions;
using FluentAssertions.Execution;

using Xunit;

namespace FateGrandOrderPOC.Test
{
    public class PartyCompositionTest : IClassFixture<WireMockFixture>
    {
        private readonly WireMockFixture _wiremockFixture;
        private readonly IContainer _container;

        public PartyCompositionTest(WireMockFixture wiremockFixture)
        {
            _wiremockFixture = wiremockFixture;
            _container = ContainerBuilderInit.Create(WireMockUtility.REGION);
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
            WireMockUtility.AddStubs(_wiremockFixture);

            List<PartyMember> party = new List<PartyMember>();

            using (var scope = _container.BeginLifetimeScope())
            {
                ScopedClasses resolvedClasses = AutofacUtility.ResolveScope(scope);

                CraftEssence chaldeaKscope = new CraftEssence
                {
                    CraftEssenceLevel = 100,
                    Mlb = true,
                    CraftEssenceInfo = await resolvedClasses.AtlasAcademyClient.GetCraftEssenceInfo(WireMockUtility.KSCOPE_CE)
                };

                Servant chaldeaServant = await FrequentlyUsed.ServantAsync(resolvedClasses.AtlasAcademyClient, WireMockUtility.DANTES_AVENGER, 1, false);

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
            WireMockUtility.AddStubs(_wiremockFixture);

            using (var scope = _container.BeginLifetimeScope())
            {
                AtlasAcademyClient aaClient = scope.Resolve<AtlasAcademyClient>();

                MysticCode mysticCode = new MysticCode
                {
                    MysticCodeLevel = 4,
                    MysticCodeInfo = await aaClient.GetMysticCodeInfo(WireMockUtility.ARTIC_ID)
                };

                mysticCode.MysticCodeInfo.Id.Should().Be(110);
            }
        }
    }
}
