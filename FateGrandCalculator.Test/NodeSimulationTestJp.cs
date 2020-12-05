using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Autofac;

using FateGrandCalculator.Enums;
using FateGrandCalculator.Models;
using FateGrandCalculator.Test.Fixture;
using FateGrandCalculator.Test.Utility;

using FluentAssertions;

using Xunit;
using Xunit.Abstractions;

namespace FateGrandCalculator.Test
{
    public class NodeSimulationTestJp : IClassFixture<WireMockFixture>
    {
        private readonly WireMockFixture _wireMockFixture;
        private readonly WireMockUtility _wireMockUtility;
        private readonly IContainer _container;
        private readonly ITestOutputHelper _output;

        public NodeSimulationTestJp(WireMockFixture wireMockFixture, ITestOutputHelper output)
        {
            _wireMockFixture = wireMockFixture;
            _output = output;
            _wireMockUtility = new WireMockUtility("JP");
            _container = ContainerBuilderInit.Create("JP");
        }

        [Fact]
        public async Task ChristmasJP2018ArcherNode()
        {
            _wireMockFixture.CheckIfMockServerInUse();
            _wireMockUtility.AddStubs(_wireMockFixture);

            using (var scope = _container.BeginLifetimeScope())
            {
                ScopedClasses resolvedClasses = AutofacUtility.ResolveScope(scope);
                List<PartyMember> party = new List<PartyMember>();

                #region Lotto CEs
                CraftEssence chaldeaHolyMaidenTeachingMlb = new CraftEssence
                {
                    CraftEssenceLevel = 16,
                    Mlb = true,
                    CraftEssenceInfo = await resolvedClasses.AtlasAcademyClient.GetCraftEssenceInfo(WireMockUtility.HOLY_MAIDEN_TEACHING_CE)
                };

                CraftEssence chaldeaHolyMaidenTeaching = new CraftEssence
                {
                    CraftEssenceLevel = 1,
                    Mlb = false,
                    CraftEssenceInfo = await resolvedClasses.AtlasAcademyClient.GetCraftEssenceInfo(WireMockUtility.HOLY_MAIDEN_TEACHING_CE)
                };
                #endregion

                CraftEssence chaldeaSuperscope = new CraftEssence
                {
                    CraftEssenceLevel = 100,
                    Mlb = true,
                    CraftEssenceInfo = await resolvedClasses.AtlasAcademyClient.GetCraftEssenceInfo(WireMockUtility.KSCOPE_CE)
                };

                /* Party data */
                // Reference: https://www.youtube.com/watch?v=oBs_YT1ac-Y
                #region Parvati
                ChaldeaServant chaldeaParvati = await FrequentlyUsed.ServantAsync(resolvedClasses.AtlasAcademyClient, WireMockUtility.PARVATI_LANCER, 2, false);

                PartyMember partyParvati = resolvedClasses.CombatFormula.AddPartyMember(party, chaldeaParvati, chaldeaSuperscope);
                resolvedClasses.CombatFormula.ApplyCraftEssenceEffects(partyParvati);

                partyParvati.Servant.SkillLevels = new int[] { 10, 4, 4 }; // adjust to the reference video

                party.Add(partyParvati);
                #endregion

                #region Skadi
                ChaldeaServant chaldeaSkadi = await FrequentlyUsed.ServantAsync(resolvedClasses.AtlasAcademyClient, WireMockUtility.SKADI_CASTER, 1, false);

                PartyMember partySkadi = resolvedClasses.CombatFormula.AddPartyMember(party, chaldeaSkadi, chaldeaHolyMaidenTeachingMlb);
                resolvedClasses.CombatFormula.ApplyCraftEssenceEffects(partySkadi);

                partySkadi.Servant.SkillLevels = new int[] { 7, 7, 8 }; // adjust to the reference video

                party.Add(partySkadi);
                #endregion

                #region Skadi Support
                ChaldeaServant supportCaster = await FrequentlyUsed.ServantAsync(resolvedClasses.AtlasAcademyClient, WireMockUtility.SKADI_CASTER, 1, true);

                PartyMember partyMemberSupportCaster = resolvedClasses.CombatFormula.AddPartyMember(party, supportCaster, chaldeaHolyMaidenTeachingMlb);

                party.Add(partyMemberSupportCaster);
                #endregion

                MysticCode mysticCode = new MysticCode
                {
                    MysticCodeLevel = 7,
                    MysticCodeInfo = await resolvedClasses.AtlasAcademyClient.GetMysticCodeInfo(WireMockUtility.ARTIC_ID)
                };

                #region First Wave
                EnemyMob mafiaGhost1 = new EnemyMob
                {
                    Id = 0,
                    Name = "Mafia Ghost",
                    ClassName = ClassRelationEnum.Archer,
                    AttributeName = AttributeRelationEnum.Sky,
                    WaveNumber = WaveNumberEnum.First,
                    Health = 32149.0f,
                    Traits = new List<string>
                    {
                        "undead", "demonic"
                    }
                };

                EnemyMob mafiaGhost2 = new EnemyMob
                {
                    Id = 1,
                    Name = "Mafia Ghost",
                    ClassName = ClassRelationEnum.Archer,
                    AttributeName = AttributeRelationEnum.Sky,
                    WaveNumber = WaveNumberEnum.First,
                    Health = 32739.0f,
                    Traits = new List<string>
                    {
                        "undead", "demonic"
                    }
                };

                EnemyMob mafiaGhost3 = new EnemyMob
                {
                    Id = 2,
                    Name = "Mafia Ghost",
                    ClassName = ClassRelationEnum.Archer,
                    AttributeName = AttributeRelationEnum.Sky,
                    WaveNumber = WaveNumberEnum.First,
                    Health = 31560.0f,
                    Traits = new List<string>
                    {
                        "undead", "demonic"
                    }
                };
                #endregion

                #region Second Wave
                EnemyMob bossPet = new EnemyMob
                {
                    Id = 3,
                    Name = "Boss' Pet",
                    ClassName = ClassRelationEnum.Archer,
                    AttributeName = AttributeRelationEnum.Earth,
                    WaveNumber = WaveNumberEnum.Second,
                    Health = 104814.0f,
                    Traits = new List<string>
                    {
                        "beast"
                    }
                };

                EnemyMob mafiaGhost4 = new EnemyMob
                {
                    Id = 4,
                    Name = "Mafia Ghost",
                    ClassName = ClassRelationEnum.Archer,
                    AttributeName = AttributeRelationEnum.Sky,
                    WaveNumber = WaveNumberEnum.Second,
                    Health = 38586.0f,
                    Traits = new List<string>
                    {
                        "undead", "demonic"
                    }
                };

                EnemyMob mafiaGhost5 = new EnemyMob
                {
                    Id = 5,
                    Name = "Mafia Ghost",
                    ClassName = ClassRelationEnum.Archer,
                    AttributeName = AttributeRelationEnum.Sky,
                    WaveNumber = WaveNumberEnum.Second,
                    Health = 37927.0f,
                    Traits = new List<string>
                    {
                        "undead", "demonic"
                    }
                };
                #endregion

                #region Third Wave
                EnemyMob mafiaGhost6 = new EnemyMob
                {
                    Id = 6,
                    Name = "Mafia Ghost",
                    ClassName = ClassRelationEnum.Archer,
                    AttributeName = AttributeRelationEnum.Sky,
                    WaveNumber = WaveNumberEnum.Third,
                    Health = 45511.0f,
                    Traits = new List<string>
                    {
                        "undead", "demonic"
                    }
                };

                EnemyMob gamblingBookmaker = new EnemyMob
                {
                    Id = 7,
                    Name = "Gambling Bookmaker",
                    ClassName = ClassRelationEnum.Archer,
                    AttributeName = AttributeRelationEnum.Human,
                    WaveNumber = WaveNumberEnum.Third,
                    Health = 180792.0f,
                    Traits = new List<string>
                    {
                        "undead", "demonic"
                    }
                };

                EnemyMob mafiaGhost7 = new EnemyMob
                {
                    Id = 8,
                    Name = "Mafia Ghost",
                    ClassName = ClassRelationEnum.Archer,
                    AttributeName = AttributeRelationEnum.Sky,
                    WaveNumber = WaveNumberEnum.Third,
                    Health = 44771.0f,
                    Traits = new List<string>
                    {
                        "undead", "demonic"
                    }
                };
                #endregion

                List<EnemyMob> enemyMobs = new List<EnemyMob>
                {
                    mafiaGhost1, mafiaGhost2, mafiaGhost3,       // wave 1
                    bossPet, mafiaGhost4, mafiaGhost5,           // wave 2
                    mafiaGhost6, gamblingBookmaker, mafiaGhost7  // wave 3
                };

                /* Simulate node combat */
                // Fight 1/3
                resolvedClasses.ServantSkillActivation.SkillActivation(partySkadi, 1, party, 1, enemyMobs, 3); // Skadi quick up buff
                resolvedClasses.ServantSkillActivation.SkillActivation(partyMemberSupportCaster, 1, party, 1, enemyMobs, 3); // Skadi (support) quick up buff
                resolvedClasses.ServantSkillActivation.SkillActivation(partyParvati, 1, party, 1, enemyMobs, 3); // Parvati quick up buff

                resolvedClasses.CombatFormula.AddPartyMemberToNpChain(party, partyParvati);
                await resolvedClasses.CombatFormula.NoblePhantasmChainSimulator(party, enemyMobs, WaveNumberEnum.First, 3);

                _output.WriteLine($"{partyParvati.Servant.ServantInfo.Name} has {partyParvati.NpCharge}% charge after the 1st fight\n");

                // Fight 2/3
                resolvedClasses.ServantSkillActivation.AdjustSkillCooldowns(party);
                resolvedClasses.ServantSkillActivation.SkillActivation(partySkadi, 3, party, 1, enemyMobs, 3); // Skadi NP buff

                resolvedClasses.CombatFormula.AddPartyMemberToNpChain(party, partyParvati);
                await resolvedClasses.CombatFormula.NoblePhantasmChainSimulator(party, enemyMobs, WaveNumberEnum.Second, 3);

                _output.WriteLine($"{partyParvati.Servant.ServantInfo.Name} has {partyParvati.NpCharge}% charge after the 2nd fight");

                List<EnemyMob> waveSurvivors = enemyMobs.FindAll(w => w.WaveNumber == WaveNumberEnum.Second);
                foreach (EnemyMob enemy in waveSurvivors)
                {
                    _output.WriteLine($"{enemy.Name} has {enemy.Health} HP left");
                }

                waveSurvivors.Count.Should().Be(1);
                waveSurvivors.Any(h => h.Health < 30000.0f).Should().BeTrue();

                // To stay in line with the reference video
                enemyMobs.RemoveAll(w => w.WaveNumber == WaveNumberEnum.Second);
                _output.WriteLine($"Assume carding kills last monster...\n");

                // Fight 3/3
                resolvedClasses.ServantSkillActivation.AdjustSkillCooldowns(party);
                resolvedClasses.ServantSkillActivation.SkillActivation(partyParvati, 2, party, 1, enemyMobs, 3); // Parvati ATK, DEF, Star Drop Rate, & Crit Rate Up
                resolvedClasses.ServantSkillActivation.SkillActivation(partyMemberSupportCaster, 3, party, 1, enemyMobs, 3); // Skadi (support) NP buff
                resolvedClasses.ServantSkillActivation.SkillActivation(mysticCode, 2, party, 1, enemyMobs, 3); // Artic ATK & NP STR up
                resolvedClasses.ServantSkillActivation.SkillActivation(partySkadi, 2, party, 1, enemyMobs, 1); // Skadi enemy defense down
                resolvedClasses.ServantSkillActivation.SkillActivation(partyMemberSupportCaster, 2, party, 1, enemyMobs, 1); // Skadi (support) enemy defense down

                resolvedClasses.CombatFormula.AddPartyMemberToNpChain(party, partyParvati);
                await resolvedClasses.CombatFormula.NoblePhantasmChainSimulator(party, enemyMobs, WaveNumberEnum.Third, 3);

                waveSurvivors = enemyMobs.FindAll(w => w.WaveNumber == WaveNumberEnum.Third);
                foreach (EnemyMob enemy in waveSurvivors)
                {
                    _output.WriteLine($"{enemy.Name} has {enemy.Health} HP left");
                }

                waveSurvivors.Count.Should().Be(1);
                waveSurvivors.Any(h => h.Health < 21000.0f).Should().BeTrue();
            }
        }
    }
}
