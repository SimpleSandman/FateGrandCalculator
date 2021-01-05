using System.Collections.Generic;
using System.Threading.Tasks;

using Autofac;

using FateGrandCalculator.AtlasAcademy;
using FateGrandCalculator.AtlasAcademy.Json;
using FateGrandCalculator.Models;

using FateGrandCalculator.Test.Utility;
using FateGrandCalculator.Test.Utility.AutofacConfig;
using FateGrandCalculator.Test.Utility.Fixture;

using FluentAssertions;
using FluentAssertions.Execution;

using Xunit;

namespace FateGrandCalculator.Test.Combat
{
    public class PartyCompositionTest : IClassFixture<WireMockFixture>
    {
        private readonly WireMockFixture _wireMockFixture;
        private readonly WireMockUtility _wireMockUtility;
        private readonly IContainer _container;

        public PartyCompositionTest(WireMockFixture wireMockFixture)
        {
            _wireMockFixture = wireMockFixture;
            _wireMockUtility = new WireMockUtility("NA");
            _container = ContainerBuilderInit.Create("NA");
        }

        [Fact]
        public void ConfirmJsonDeserializationServantJP()
        {
            // Set "Copy to Output Directory" to "Copy if newer" for JSON files
            ServantNiceJson testServant = LoadTestData.DeserializeServantJson("JP", "Caster", "500300-TamamoNoMaeEN.json");

            testServant.Name.Should().Be("Tamamo-no-Mae");
        }

        [Fact]
        public void ConfirmJsonDeserializationServantNA()
        {
            // Set "Copy to Output Directory" to "Copy if newer" for JSON files
            ServantNiceJson testServant = LoadTestData.DeserializeServantJson("NA", "Caster", "500800-Merlin.json");

            testServant.Name.Should().Be("Merlin");
        }

        [Fact]
        public async Task CreatePartyMemberWithCraftEssence()
        {
            _wireMockFixture.CheckIfMockServerInUse();
            _wireMockUtility.AddStubs(_wireMockFixture);

            List<PartyMember> party = new List<PartyMember>();

            using (var scope = _container.BeginLifetimeScope())
            {
                ScopedClasses resolvedClasses = AutofacUtility.ResolveScope(scope);

                List<EquipBasicJson> equipBasicJsonList = await resolvedClasses.AtlasAcademyClient.GetListBasicEquipInfo();
                EquipBasicJson equipBasicJson = equipBasicJsonList.Find(c => c.Id.ToString() == WireMockUtility.KSCOPE_CE);
                CraftEssence chaldeaSuperscope = FrequentlyUsed.GetCraftEssence(equipBasicJson, 100, true);

                List<ServantBasicJson> basicJsonList = await resolvedClasses.AtlasAcademyClient.GetListBasicServantInfo();
                ServantBasicJson servantBasicJson = basicJsonList.Find(s => s.Id.ToString() == WireMockUtility.DANTES_AVENGER);
                PartyMember partyMember = await FrequentlyUsed.PartyMemberAsync(servantBasicJson, party, resolvedClasses, 1, false, chaldeaSuperscope);

                using (new AssertionScope())
                {
                    partyMember.Servant.ServantBasicInfo.Id.ToString().Should().Be(WireMockUtility.DANTES_AVENGER);
                    partyMember.EquippedCraftEssence.Mlb.Should().BeTrue();
                    partyMember.NpCharge.Should().Be(100.0f);
                }
            }
        }

        [Fact]
        public async Task SetMysticCode()
        {
            _wireMockFixture.CheckIfMockServerInUse();
            _wireMockUtility.AddStubs(_wireMockFixture);

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
