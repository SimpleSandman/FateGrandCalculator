using System.Collections.Generic;
using System.Threading.Tasks;

using Autofac;

using FateGrandOrderPOC.Shared;
using FateGrandOrderPOC.Shared.AtlasAcademy;
using FateGrandOrderPOC.Shared.AtlasAcademy.Json;
using FateGrandOrderPOC.Shared.Enums;
using FateGrandOrderPOC.Shared.Models;
using FateGrandOrderPOC.Test.CoreModule;
using FateGrandOrderPOC.Test.Fixture;
using FateGrandOrderPOC.Test.HelperMethods;

using FluentAssertions;
using FluentAssertions.Execution;

using Xunit;

namespace FateGrandOrderPOC.Test
{
    public class SkillActivationTest : IClassFixture<WireMockFixture>
    {
        const string REGION = "NA";

        private readonly WireMockFixture _wiremockFixture;
        private readonly IContainer _container;

        public SkillActivationTest(WireMockFixture wiremockFixture)
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
        public async Task ActivatePartyMemberBuff()
        {
            _wiremockFixture.CheckIfMockServerInUse();

            const string SKADI_CASTER = "503900";
            const string DANTES_AVENGER = "1100200";

            // build mock servant responses
            ServantNiceJson mockSkadiResponse = LoadTestData.DeserializeServantJson(REGION, "Caster", "503900-ScathachSkadiCaster.json");
            LoadTestData.CreateNiceWireMockStub(_wiremockFixture, REGION, "servant", SKADI_CASTER, mockSkadiResponse);

            ServantNiceJson mockDantesResponse = LoadTestData.DeserializeServantJson(REGION, "Avenger", "1100200-EdmondDantesAvenger.json");
            LoadTestData.CreateNiceWireMockStub(_wiremockFixture, REGION, "servant", DANTES_AVENGER, mockDantesResponse);

            List<PartyMember> party = new List<PartyMember>();

            using (var scope = _container.BeginLifetimeScope())
            {
                AtlasAcademyClient aaClient = scope.Resolve<AtlasAcademyClient>();
                CombatFormula cfClient = scope.Resolve<CombatFormula>();
                ServantSkillActivation ssaClient = scope.Resolve<ServantSkillActivation>();

                /* Party data */
                #region Party Member
                Servant chaldeaAttackServant = new Servant
                {
                    ServantLevel = 90,
                    NpLevel = 1,
                    FouHealth = 1000,
                    FouAttack = 1000,
                    SkillLevels = new int[3] { 10, 10, 10 },
                    IsSupportServant = false,
                    ServantInfo = await aaClient.GetServantInfo(DANTES_AVENGER)
                };

                PartyMember partyMemberAttacker = cfClient.AddPartyMember(party, chaldeaAttackServant);
                cfClient.ApplyCraftEssenceEffects(partyMemberAttacker);

                party.Add(partyMemberAttacker);
                #endregion

                #region Party Member 2
                Servant chaldeaCaster = new Servant
                {
                    ServantLevel = 90,
                    NpLevel = 1,
                    FouHealth = 1000,
                    FouAttack = 1000,
                    SkillLevels = new int[3] { 10, 10, 10 },
                    IsSupportServant = false,
                    ServantInfo = await aaClient.GetServantInfo(SKADI_CASTER)
                };

                PartyMember partyMemberCaster = cfClient.AddPartyMember(party, chaldeaCaster);

                party.Add(partyMemberCaster);
                #endregion

                // Actual enemy stats from LB2's "Ablazed Mansion" or "Flaming Mansion" free quest
                List<EnemyMob> enemyMobs = new List<EnemyMob>
                {
                    new EnemyMob
                    {
                        Id = 0,
                        Name = "Walkure",
                        ClassName = ClassRelationEnum.Rider,
                        AttributeName = AttributeRelationEnum.Sky,
                        Gender = GenderRelationEnum.Female,
                        WaveNumber = WaveNumberEnum.First,
                        Health = 13933.0f,
                        IsSpecial = false,
                        Traits = new List<string>
                        {
                            "Divine", "Humanoid", "Female"
                        }
                    }
                };

                ssaClient.SkillActivation(partyMemberCaster, 1, party, 1, enemyMobs, 1); // Skadi quick up buff

                using (new AssertionScope())
                {
                    partyMemberAttacker.ActiveStatuses.Count.Should().BeGreaterThan(0);
                    partyMemberCaster.SkillCooldowns.Count.Should().BeGreaterThan(0);
                }
            }
        }
    }
}
