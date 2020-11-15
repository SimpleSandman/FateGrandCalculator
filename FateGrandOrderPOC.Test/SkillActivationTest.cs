using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Autofac;

using FateGrandOrderPOC.Shared.AtlasAcademy.Json;
using FateGrandOrderPOC.Shared.Enums;
using FateGrandOrderPOC.Shared.Models;
using FateGrandOrderPOC.Test.Fixture;
using FateGrandOrderPOC.Test.Utility;

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
            _container = ContainerBuilderInit.Create(REGION);
        }

        [Fact]
        public async Task ActivatePartyMemberSkills()
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
                ScopedClasses resolvedClasses = AutofacUtility.ResolveScope(scope);

                /* Party data */
                #region Party Member
                Servant chaldeaAttackServant = await FrequentlyUsed.ServantAsync(resolvedClasses.AtlasAcademyClient, DANTES_AVENGER, 1, false);

                PartyMember partyMemberAttacker = resolvedClasses.CombatFormula.AddPartyMember(party, chaldeaAttackServant);
                resolvedClasses.CombatFormula.ApplyCraftEssenceEffects(partyMemberAttacker);

                party.Add(partyMemberAttacker);
                #endregion

                #region Party Member 2
                Servant chaldeaCaster = await FrequentlyUsed.ServantAsync(resolvedClasses.AtlasAcademyClient, SKADI_CASTER, 1, false);

                PartyMember partyMemberCaster = resolvedClasses.CombatFormula.AddPartyMember(party, chaldeaCaster);

                party.Add(partyMemberCaster);
                #endregion

                #region Party Member Support
                Servant supportCaster = await FrequentlyUsed.ServantAsync(resolvedClasses.AtlasAcademyClient, SKADI_CASTER, 1, true);

                PartyMember partyMemberSupportCaster = resolvedClasses.CombatFormula.AddPartyMember(party, supportCaster);

                party.Add(partyMemberSupportCaster);
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

                resolvedClasses.ServantSkillActivation.SkillActivation(partyMemberCaster, 1, party, 1, enemyMobs, 1); // Skadi quick up buff
                resolvedClasses.ServantSkillActivation.SkillActivation(partyMemberSupportCaster, 1, party, 1, enemyMobs, 1); // Skadi (support) quick up buff
                resolvedClasses.ServantSkillActivation.SkillActivation(partyMemberAttacker, 2, party, 1, enemyMobs, 1); // Dante's 2nd skill
                resolvedClasses.ServantSkillActivation.AdjustSkillCooldowns(party); // before next turn
                resolvedClasses.ServantSkillActivation.SkillActivation(partyMemberCaster, 3, party, 1, enemyMobs, 1); // Skadi NP buff
                resolvedClasses.ServantSkillActivation.AdjustSkillCooldowns(party); // before next turn
                resolvedClasses.ServantSkillActivation.SkillActivation(partyMemberSupportCaster, 3, party, 1, enemyMobs, 1); // Skadi (support) NP buff
                resolvedClasses.ServantSkillActivation.SkillActivation(partyMemberSupportCaster, 2, party, 1, enemyMobs, 1); // Skadi (support) enemy defense down
                resolvedClasses.ServantSkillActivation.SkillActivation(partyMemberCaster, 2, party, 1, enemyMobs, 1); // Skadi enemy defense down
                resolvedClasses.ServantSkillActivation.SkillActivation(partyMemberAttacker, 1, party, 1, enemyMobs, 1); // Dante's 1st skill

                using (new AssertionScope())
                {
                    partyMemberAttacker.SkillCooldowns.Count.Should().Be(2); // 1st and 2nd skills used
                    partyMemberCaster.SkillCooldowns.Count.Should().Be(3); // all 3 skills used
                    partyMemberSupportCaster.SkillCooldowns.Count.Should().Be(3); // all 3 skills used
                    partyMemberAttacker.ActiveStatuses.Count.Should().Be(8); // Status Count = skadi 2 + skadi (support) 2
                                                                             // + dantes (1st skill) 3 + dantes (2nd skill) 1
                    enemyMobs
                        .FindAll(d => d.ActiveStatuses.Any(f => f.StatusEffect.FuncPopupText == "DEF Down"))
                        .Count.Should().Be(1); // find enemies with defense down
                }
            }
        }
    }
}
