using System.Collections.Generic;
using System.Threading.Tasks;

using Autofac;

using FateGrandCalculator.AtlasAcademy.Json;
using FateGrandCalculator.Enums;
using FateGrandCalculator.Models;

using FateGrandCalculator.Test.Utility;
using FateGrandCalculator.Test.Utility.AutofacConfig;
using FateGrandCalculator.Test.Utility.Fixture;

using FluentAssertions;
using FluentAssertions.Execution;

using Xunit;
using Xunit.Abstractions;

namespace FateGrandCalculator.Test.Combat.GilFestNA2021
{
    public class GilFestFirstGardenNode : IClassFixture<WireMockFixture>
    {
        private readonly WireMockFixture _wireMockFixture;
        private readonly WireMockUtility _wireMockUtility;
        private readonly IContainer _container;
        private readonly ITestOutputHelper _output;

        public GilFestFirstGardenNode(WireMockFixture wireMockFixture, ITestOutputHelper output)
        {
            _wireMockFixture = wireMockFixture;
            _output = output;
            _wireMockUtility = new WireMockUtility("NA");
            _container = ContainerBuilderInit.Create("NA");
        }

        [Fact]
        public async Task LancelotLoopDoubleSkadi()
        {
            _wireMockFixture.CheckIfMockServerInUse();
            _wireMockUtility.AddStubs(_wireMockFixture);

            using (var scope = _container.BeginLifetimeScope())
            {
                ScopedClasses resolvedClasses = AutofacUtility.ResolveScope(scope);
                ConstantExportJson constantExportJson = await FrequentlyUsed.GetConstantExportJsonAsync(resolvedClasses.AtlasAcademyClient);
                List<PartyMember> party = new List<PartyMember>();

                EquipBasicJson equipBasicJson = constantExportJson.ListEquipBasicJson.Find(c => c.Id.ToString() == WireMockUtility.KSCOPE_CE);
                CraftEssence chaldeaSuperscope = FrequentlyUsed.GetCraftEssence(equipBasicJson, 100, true);

                MysticCode mysticCode = new MysticCode
                {
                    MysticCodeLevel = 10,
                    MysticCodeInfo = await resolvedClasses.AtlasAcademyClient.GetMysticCodeInfo(WireMockUtility.FRAGMENT_2004_ID)
                };

                #region Party Data
                // Lancelot (Berserker)
                ServantBasicJson basicJson = constantExportJson.ListServantBasicJson.Find(s => s.Id.ToString() == WireMockUtility.LANCELOT_BERSERKER);
                PartyMember partyLancelot = await FrequentlyUsed.PartyMemberAsync(basicJson, party, resolvedClasses, 5, false, chaldeaSuperscope);
                partyLancelot.Servant.SkillLevels = new int[] { 6, 8, 10 };
                party.Add(partyLancelot);

                // Skadi
                basicJson = constantExportJson.ListServantBasicJson.Find(s => s.Id.ToString() == WireMockUtility.SKADI_CASTER);
                PartyMember partySkadi = await FrequentlyUsed.PartyMemberAsync(basicJson, party, resolvedClasses, 2, false);
                party.Add(partySkadi);

                // Skadi Support
                PartyMember partySkadiSupport = await FrequentlyUsed.PartyMemberAsync(basicJson, party, resolvedClasses, 1, true);
                party.Add(partySkadiSupport);
                #endregion

                List<EnemyMob> enemyMobs = GetGardenMob();

                /* Simulate node combat */
                // Fight 1/3
                resolvedClasses.ServantSkillActivation.SkillActivation(mysticCode, 3, party, enemyMobs, 1); // Fragment of 2004's NP gain buff
                resolvedClasses.ServantSkillActivation.SkillActivation(partySkadi, 1, party, enemyMobs, 1); // Skadi quick up buff
                resolvedClasses.ServantSkillActivation.SkillActivation(partySkadiSupport, 1, party, enemyMobs, 2); // Skadi (support) quick up buff

                resolvedClasses.CombatFormula.AddPartyMemberToNpChain(party, partyLancelot).Should().BeTrue();
                resolvedClasses.CombatFormula.NoblePhantasmChainSimulator(party, enemyMobs, WaveNumberEnum.First, constantExportJson);

                _output.WriteLine($"{partyLancelot.Servant.ServantBasicInfo.Name} has {partyLancelot.NpCharge}% charge after the 1st fight");

                // Fight 2/3
                resolvedClasses.ServantSkillActivation.AdjustSkillCooldowns(party);
                resolvedClasses.ServantSkillActivation.SkillActivation(partySkadi, 3, party, enemyMobs, 1); // Skadi NP buff
                resolvedClasses.ServantSkillActivation.SkillActivation(partyLancelot, 3, party, enemyMobs, 1); // Zerkalot NP gain up

                resolvedClasses.CombatFormula.AddPartyMemberToNpChain(party, partyLancelot).Should().BeTrue();
                resolvedClasses.CombatFormula.NoblePhantasmChainSimulator(party, enemyMobs, WaveNumberEnum.Second, constantExportJson);

                _output.WriteLine($"{partyLancelot.Servant.ServantBasicInfo.Name} has {partyLancelot.NpCharge}% charge after the 2nd fight");

                // Fight 3/3
                resolvedClasses.ServantSkillActivation.AdjustSkillCooldowns(party);
                resolvedClasses.ServantSkillActivation.SkillActivation(mysticCode, 1, party, enemyMobs, 1); // Fragment of 2004's NP strength buff
                resolvedClasses.ServantSkillActivation.SkillActivation(partySkadi, 2, party, enemyMobs, 1); // Skadi enemy defense down
                resolvedClasses.ServantSkillActivation.SkillActivation(partySkadiSupport, 2, party, enemyMobs, 1); // Skadi (support) enemy defense down
                resolvedClasses.ServantSkillActivation.SkillActivation(partySkadiSupport, 3, party, enemyMobs, 1); // Skadi NP buff
                resolvedClasses.ServantSkillActivation.SkillActivation(partySkadiSupport, 2, party, enemyMobs, 1); // Skadi enemy defense down

                resolvedClasses.CombatFormula.AddPartyMemberToNpChain(party, partyLancelot).Should().BeTrue();
                resolvedClasses.CombatFormula.NoblePhantasmChainSimulator(party, enemyMobs, WaveNumberEnum.Third, constantExportJson);

                _output.WriteLine($"{partyLancelot.Servant.ServantBasicInfo.Name} has {partyLancelot.NpCharge}% charge after the 3rd fight");

                using (new AssertionScope())
                {
                    enemyMobs.Count.Should().Be(0);
                    partyLancelot.NpCharge.Should().Be(31);
                }
            }
        }

        #region Private Method
        private List<EnemyMob> GetGardenMob()
        {
            #region First Wave
            EnemyMob dweller1 = new EnemyMob
            {
                Id = 0,
                Name = "Fairy Tale Dweller",
                ClassName = ClassRelationEnum.Caster,
                AttributeName = AttributeRelationEnum.Human,
                WaveNumber = WaveNumberEnum.First,
                Health = 18801.0f,
                Traits = new List<string>
                {
                    "genderMale", "humanoid"
                }
            };

            EnemyMob dweller2 = new EnemyMob
            {
                Id = 1,
                Name = "Fairy Tale Dweller",
                ClassName = ClassRelationEnum.Caster,
                AttributeName = AttributeRelationEnum.Human,
                WaveNumber = WaveNumberEnum.First,
                Health = 14601.0f,
                Traits = new List<string>
                {
                    "genderMale", "humanoid"
                }
            };

            EnemyMob dweller3 = new EnemyMob
            {
                Id = 2,
                Name = "Fairy Tale Dweller",
                ClassName = ClassRelationEnum.Caster,
                AttributeName = AttributeRelationEnum.Human,
                WaveNumber = WaveNumberEnum.First,
                Health = 18801.0f,
                Traits = new List<string>
                {
                    "genderMale", "humanoid"
                }
            };
            #endregion

            #region Second Wave
            EnemyMob dweller4 = new EnemyMob
            {
                Id = 3,
                Name = "Fairy Tale Dweller",
                ClassName = ClassRelationEnum.Caster,
                AttributeName = AttributeRelationEnum.Human,
                WaveNumber = WaveNumberEnum.Second,
                Health = 31555.0f,
                Traits = new List<string>
                {
                    "genderMale", "humanoid"
                }
            };

            EnemyMob demon = new EnemyMob
            {
                Id = 4,
                Name = "Fairy Tale Demon",
                ClassName = ClassRelationEnum.Caster,
                AttributeName = AttributeRelationEnum.Sky,
                WaveNumber = WaveNumberEnum.Second,
                Health = 55704.0f,
                Traits = new List<string>
                {
                    "daemon", "demonic"
                }
            };

            EnemyMob dweller5 = new EnemyMob
            {
                Id = 5,
                Name = "Fairy Tale Dweller",
                ClassName = ClassRelationEnum.Caster,
                AttributeName = AttributeRelationEnum.Human,
                WaveNumber = WaveNumberEnum.Second,
                Health = 32450.0f,
                Traits = new List<string>
                {
                    "genderMale", "humanoid"
                }
            };
            #endregion

            #region Third Wave
            EnemyMob nightView = new EnemyMob
            {
                Id = 6,
                Name = "Wow, A Street with a Wonderful Night View...",
                ClassName = ClassRelationEnum.Caster,
                AttributeName = AttributeRelationEnum.Human,
                WaveNumber = WaveNumberEnum.Third,
                Health = 88406.0f,
                Traits = new List<string>
                {
                    "genderFemale", "humanoid", "basedOnServant", "weakToEnumaElish"
                }
            };

            EnemyMob crudeNightscape = new EnemyMob
            {
                Id = 7,
                Name = "What a Crude Nightscape!",
                ClassName = ClassRelationEnum.Caster,
                AttributeName = AttributeRelationEnum.Human,
                WaveNumber = WaveNumberEnum.Third,
                Health = 67872.0f,
                Traits = new List<string>
                {
                    "genderMale", "childServant", "humanoid", "basedOnServant", "weakToEnumaElish"
                }
            };

            EnemyMob glitteringTown = new EnemyMob
            {
                Id = 8,
                Name = "What a Glittering Town!",
                ClassName = ClassRelationEnum.Caster,
                AttributeName = AttributeRelationEnum.Human,
                WaveNumber = WaveNumberEnum.Third,
                Health = 130702.0f,
                Traits = new List<string>
                {
                    "genderFemale", "humanoid", "basedOnServant", "weakToEnumaElish"
                }
            };
            #endregion

            return new List<EnemyMob>
            {
                dweller1, dweller2, dweller3,                // wave 1
                dweller4, demon, dweller5,                   // wave 2
                nightView, crudeNightscape, glitteringTown   // wave 3
            };
        }
        #endregion
    }
}
