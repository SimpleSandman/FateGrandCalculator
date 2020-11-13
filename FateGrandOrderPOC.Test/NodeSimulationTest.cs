using System.Collections.Generic;
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
using Xunit.Abstractions;

namespace FateGrandOrderPOC.Test
{
    public class NodeSimulationTest : IClassFixture<WireMockFixture>
    {
        const string REGION = "NA";

        private readonly WireMockFixture _wiremockFixture;
        private readonly IContainer _container;
        private readonly ITestOutputHelper _output;

        public NodeSimulationTest(WireMockFixture wiremockFixture, ITestOutputHelper output)
        {
            _wiremockFixture = wiremockFixture;
            _output = output;
            _container = ContainerBuilderInit.Create(REGION);
        }

        [Fact]
        public async Task FlamingMansionLB2Dantes()
        {
            _wiremockFixture.CheckIfMockServerInUse();

            const string KSCOPE_CE = "9400340";
            const string DANTES_AVENGER = "1100200";
            const string SKADI_CASTER = "503900";
            const string ARTIC_ID = "110";

            #region Mock Responses
            // build mock servant responses
            ServantNiceJson mockSkadiResponse = LoadTestData.DeserializeServantJson(REGION, "Caster", "503900-ScathachSkadiCaster.json");
            LoadTestData.CreateNiceWireMockStub(_wiremockFixture, REGION, "servant", SKADI_CASTER, mockSkadiResponse);

            ServantNiceJson mockDantesResponse = LoadTestData.DeserializeServantJson(REGION, "Avenger", "1100200-EdmondDantesAvenger.json");
            LoadTestData.CreateNiceWireMockStub(_wiremockFixture, REGION, "servant", DANTES_AVENGER, mockDantesResponse);

            // build mock craft essence response
            EquipNiceJson mockCraftEssenceResponse = LoadTestData.DeserializeCraftEssenceJson(REGION, "9400340-Kaleidoscope.json");
            LoadTestData.CreateNiceWireMockStub(_wiremockFixture, REGION, "equip", KSCOPE_CE, mockCraftEssenceResponse);

            // build mock mystic code response
            MysticCodeNiceJson mockMysticCodeResponse = LoadTestData.DeserializeMysticCodeJson(REGION, "110-Artic.json");
            LoadTestData.CreateNiceWireMockStub(_wiremockFixture, REGION, "MC", ARTIC_ID, mockMysticCodeResponse);

            AddExportStubs();
            #endregion

            using (var scope = _container.BeginLifetimeScope())
            {
                ScopedClasses resolvedClasses = AutofacUtility.ResolveScope(scope);
                List<PartyMember> party = new List<PartyMember>();

                /* Party data */
                #region Party Member
                CraftEssence chaldeaKscope = new CraftEssence
                {
                    CraftEssenceLevel = 100,
                    Mlb = true,
                    CraftEssenceInfo = await resolvedClasses.AtlasAcademyClient.GetCraftEssenceInfo(KSCOPE_CE)
                };

                Servant chaldeaAttackServant = new Servant
                {
                    ServantLevel = 90,
                    NpLevel = 1,
                    FouHealth = 1000,
                    FouAttack = 1000,
                    SkillLevels = new int[] { 10, 10, 10 },
                    IsSupportServant = false,
                    ServantInfo = await resolvedClasses.AtlasAcademyClient.GetServantInfo(DANTES_AVENGER)
                };

                PartyMember partyMemberAttacker = resolvedClasses.CombatFormula.AddPartyMember(party, chaldeaAttackServant, chaldeaKscope);
                resolvedClasses.CombatFormula.ApplyCraftEssenceEffects(partyMemberAttacker);

                party.Add(partyMemberAttacker);
                #endregion

                #region Party Member 2
                Servant chaldeaCaster = new Servant
                {
                    ServantLevel = 90,
                    NpLevel = 1,
                    FouHealth = 1000,
                    FouAttack = 1000,
                    SkillLevels = new int[] { 10, 10, 10 },
                    IsSupportServant = false,
                    ServantInfo = await resolvedClasses.AtlasAcademyClient.GetServantInfo(SKADI_CASTER)
                };

                PartyMember partyMemberCaster = resolvedClasses.CombatFormula.AddPartyMember(party, chaldeaCaster);

                party.Add(partyMemberCaster);
                #endregion

                #region Party Member Support
                Servant supportCaster = new Servant
                {
                    ServantLevel = 90,
                    NpLevel = 1,
                    FouHealth = 1000,
                    FouAttack = 1000,
                    SkillLevels = new int[] { 10, 10, 10 },
                    IsSupportServant = true,
                    ServantInfo = await resolvedClasses.AtlasAcademyClient.GetServantInfo(SKADI_CASTER)
                };

                PartyMember partyMemberSupportCaster = resolvedClasses.CombatFormula.AddPartyMember(party, supportCaster);

                party.Add(partyMemberSupportCaster);
                #endregion

                MysticCode mysticCode = new MysticCode
                {
                    MysticCodeLevel = 4,
                    MysticCodeInfo = await resolvedClasses.AtlasAcademyClient.GetMysticCodeInfo(ARTIC_ID)
                };

                /* Enemy node data */
                #region First Wave
                EnemyMob walkure1 = new EnemyMob
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
                };

                EnemyMob walkure2 = new EnemyMob
                {
                    Id = 1,
                    Name = "Walkure",
                    ClassName = ClassRelationEnum.Rider,
                    AttributeName = AttributeRelationEnum.Sky,
                    Gender = GenderRelationEnum.Female,
                    WaveNumber = WaveNumberEnum.First,
                    Health = 14786.0f,
                    IsSpecial = false,
                    Traits = new List<string>
                {
                    "Divine", "Humanoid", "Female"
                }
                };

                EnemyMob muspell1 = new EnemyMob
                {
                    Id = 2,
                    Name = "Muspell",
                    ClassName = ClassRelationEnum.Berserker,
                    AttributeName = AttributeRelationEnum.Earth,
                    Gender = GenderRelationEnum.Male,
                    WaveNumber = WaveNumberEnum.First,
                    Health = 23456.0f,
                    IsSpecial = false,
                    Traits = new List<string>
                {
                    "Giant", "Humanoid", "Male", "Super Large"
                }
                };
                #endregion

                #region Second Wave
                EnemyMob muspell2 = new EnemyMob
                {
                    Id = 3,
                    Name = "Muspell",
                    ClassName = ClassRelationEnum.Berserker,
                    AttributeName = AttributeRelationEnum.Earth,
                    Gender = GenderRelationEnum.Male,
                    WaveNumber = WaveNumberEnum.Second,
                    Health = 25554.0f,
                    IsSpecial = false,
                    Traits = new List<string>
                {
                    "Giant", "Humanoid", "Male", "Super Large"
                }
                };

                EnemyMob walkure3 = new EnemyMob
                {
                    Id = 4,
                    Name = "Walkure",
                    ClassName = ClassRelationEnum.Rider,
                    AttributeName = AttributeRelationEnum.Sky,
                    Gender = GenderRelationEnum.Female,
                    WaveNumber = WaveNumberEnum.Second,
                    Health = 19047.0f,
                    IsSpecial = false,
                    Traits = new List<string>
                {
                    "Divine", "Humanoid", "Female"
                }
                };

                EnemyMob muspell3 = new EnemyMob
                {
                    Id = 5,
                    Name = "Muspell",
                    ClassName = ClassRelationEnum.Berserker,
                    AttributeName = AttributeRelationEnum.Earth,
                    Gender = GenderRelationEnum.Male,
                    WaveNumber = WaveNumberEnum.Second,
                    Health = 26204.0f,
                    IsSpecial = false,
                    Traits = new List<string>
                {
                    "Giant", "Humanoid", "Male", "Super Large"
                }
                };
                #endregion

                #region Third Wave
                EnemyMob walkure4 = new EnemyMob
                {
                    Id = 6,
                    Name = "Walkure",
                    ClassName = ClassRelationEnum.Rider,
                    AttributeName = AttributeRelationEnum.Sky,
                    Gender = GenderRelationEnum.Female,
                    WaveNumber = WaveNumberEnum.Third,
                    Health = 42926.0f,
                    IsSpecial = false,
                    Traits = new List<string>
                {
                    "Divine", "Humanoid", "Female"
                }
                };

                EnemyMob walkure5 = new EnemyMob
                {
                    Id = 7,
                    Name = "Walkure",
                    ClassName = ClassRelationEnum.Rider,
                    AttributeName = AttributeRelationEnum.Sky,
                    Gender = GenderRelationEnum.Female,
                    WaveNumber = WaveNumberEnum.Third,
                    Health = 180753.0f,
                    IsSpecial = false,
                    Traits = new List<string>
                {
                    "Divine", "Humanoid", "Female"
                }
                };

                EnemyMob muspell4 = new EnemyMob
                {
                    Id = 8,
                    Name = "Muspell",
                    ClassName = ClassRelationEnum.Berserker,
                    AttributeName = AttributeRelationEnum.Earth,
                    Gender = GenderRelationEnum.Male,
                    WaveNumber = WaveNumberEnum.Third,
                    Health = 61289.0f,
                    IsSpecial = false,
                    Traits = new List<string>
                {
                    "Giant", "Humanoid", "Male", "Super Large"
                }
                };
                #endregion

                List<EnemyMob> enemyMobs = new List<EnemyMob>
                {
                    walkure1, walkure2, muspell1, // 1st wave
                    muspell2, walkure3, muspell3, // 2nd wave
                    walkure4, walkure5, muspell4  // 3rd wave
                };

                /* Simulate node combat */
                // Fight 1/3
                resolvedClasses.ServantSkillActivation.SkillActivation(partyMemberCaster, 1, party, 1, enemyMobs, 1); // Skadi quick up buff
                resolvedClasses.ServantSkillActivation.SkillActivation(partyMemberSupportCaster, 1, party, 1, enemyMobs, 1); // Skadi (support) quick up buff
                resolvedClasses.ServantSkillActivation.SkillActivation(partyMemberAttacker, 2, party, 1, enemyMobs, 1); // Dante's 2nd skill

                resolvedClasses.CombatFormula.NpChargeCheck(party, partyMemberAttacker);
                await resolvedClasses.CombatFormula.NoblePhantasmChainSimulator(party, enemyMobs, WaveNumberEnum.First);

                // Fight 2/3
                resolvedClasses.ServantSkillActivation.AdjustSkillCooldowns(party);
                resolvedClasses.ServantSkillActivation.SkillActivation(partyMemberCaster, 3, party, 1, enemyMobs, 1); // Skadi NP buff

                resolvedClasses.CombatFormula.NpChargeCheck(party, partyMemberAttacker);
                await resolvedClasses.CombatFormula.NoblePhantasmChainSimulator(party, enemyMobs, WaveNumberEnum.Second);

                // Fight 3/3
                resolvedClasses.ServantSkillActivation.AdjustSkillCooldowns(party);
                resolvedClasses.ServantSkillActivation.SkillActivation(partyMemberSupportCaster, 3, party, 1, enemyMobs, 1); // Skadi (support) NP buff
                resolvedClasses.ServantSkillActivation.SkillActivation(partyMemberSupportCaster, 2, party, 1, enemyMobs, 1); // Skadi (support) enemy defense down
                resolvedClasses.ServantSkillActivation.SkillActivation(partyMemberCaster, 2, party, 1, enemyMobs, 1); // Skadi enemy defense down
                resolvedClasses.ServantSkillActivation.SkillActivation(partyMemberAttacker, 1, party, 1, enemyMobs, 1); // Dante's 1st skill
                resolvedClasses.ServantSkillActivation.SkillActivation(mysticCode, 2, party, 1, enemyMobs, 1); // Artic mystic code ATK and NP damage up

                resolvedClasses.CombatFormula.NpChargeCheck(party, partyMemberAttacker);
                await resolvedClasses.CombatFormula.NoblePhantasmChainSimulator(party, enemyMobs, WaveNumberEnum.Third);

                foreach (EnemyMob enemyMob in enemyMobs.FindAll(e => e.Health > 0.0f))
                {
                    _output.WriteLine($"{enemyMob.Name} has {enemyMob.Health} HP left");
                }

                _output.WriteLine($"{partyMemberAttacker.Servant.ServantInfo.Name} has {partyMemberAttacker.NpCharge}% charge after the fight");

                using (new AssertionScope())
                {
                    enemyMobs.Count.Should().Be(1);
                    enemyMobs.Find(e => e.Health > 0.0f).Health.Should().Be(47025.5f);
                    partyMemberAttacker.NpCharge.Should().Be(52);
                }
            }
        }

        #region Private Methods
        /// <summary>
        /// Create constant game data endpoints as WireMock stubs
        /// </summary>
        private void AddExportStubs()
        {
            const string CONSTANT_RATE_JSON = "NiceConstant.json";
            const string CLASS_ATTACK_RATE_JSON = "NiceClassAttackRate.json";
            const string CLASS_RELATION_JSON = "NiceClassRelation.json";
            const string ATTRIBUTE_RELATION_JSON = "NiceAttributeRelation.json";

            // build necessary export mock responses
            ConstantNiceJson mockConstantRateResponse = LoadTestData.DeserializeExportJson<ConstantNiceJson>(REGION, CONSTANT_RATE_JSON);
            LoadTestData.CreateExportWireMockStub(_wiremockFixture, REGION, CONSTANT_RATE_JSON, mockConstantRateResponse);

            ClassAttackRateNiceJson mockClassAttackRateResponse = LoadTestData.DeserializeExportJson<ClassAttackRateNiceJson>(REGION, CLASS_ATTACK_RATE_JSON);
            LoadTestData.CreateExportWireMockStub(_wiremockFixture, REGION, CLASS_ATTACK_RATE_JSON, mockClassAttackRateResponse);

            ClassRelationNiceJson mockClassRelationResponse = LoadTestData.DeserializeExportJson<ClassRelationNiceJson>(REGION, CLASS_RELATION_JSON);
            LoadTestData.CreateExportWireMockStub(_wiremockFixture, REGION, CLASS_RELATION_JSON, mockClassRelationResponse);

            AttributeRelationNiceJson mockAttributeRelationResponse = LoadTestData.DeserializeExportJson<AttributeRelationNiceJson>(REGION, ATTRIBUTE_RELATION_JSON);
            LoadTestData.CreateExportWireMockStub(_wiremockFixture, REGION, ATTRIBUTE_RELATION_JSON, mockAttributeRelationResponse);
        }
        #endregion
    }
}
