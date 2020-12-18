using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Autofac;

using FateGrandCalculator.Enums;
using FateGrandCalculator.Extensions;
using FateGrandCalculator.Models;
using FateGrandCalculator.Test.AutofacConfig;
using FateGrandCalculator.Test.Fixture;
using FateGrandCalculator.Test.Utility;

using FluentAssertions;
using FluentAssertions.Execution;

using Xunit;
using Xunit.Abstractions;

namespace FateGrandCalculator.Test
{
    public class QuetzmasJp : IClassFixture<WireMockFixture>
    {
        private readonly WireMockFixture _wireMockFixture;
        private readonly WireMockUtility _wireMockUtility;
        private readonly IContainer _container;
        private readonly ITestOutputHelper _output;

        public QuetzmasJp(WireMockFixture wireMockFixture, ITestOutputHelper output)
        {
            _wireMockFixture = wireMockFixture;
            _output = output;
            _wireMockUtility = new WireMockUtility("JP");
            _container = ContainerBuilderInit.Create("JP");
        }

        [Fact]
        public async Task ArcherNodeParvatiDoubleSkadiByReference()
        {
            _wireMockFixture.CheckIfMockServerInUse();
            _wireMockUtility.AddStubs(_wireMockFixture);

            using (var scope = _container.BeginLifetimeScope())
            {
                ScopedClasses resolvedClasses = AutofacUtility.ResolveScope(scope);
                List<PartyMember> party = new List<PartyMember>();
                CraftEssence chaldeaSuperscope = await FrequentlyUsed.CraftEssenceAsync(resolvedClasses, WireMockUtility.KSCOPE_CE, 100, true);

                /* Party data */
                // Reference: https://www.youtube.com/watch?v=oBs_YT1ac-Y
                #region Parvati
                PartyMember partyParvati = await FrequentlyUsed.PartyMemberAsync(WireMockUtility.PARVATI_LANCER, party, resolvedClasses, 2, false, chaldeaSuperscope);
                partyParvati.Servant.SkillLevels = new int[] { 10, 4, 4 }; // adjust to the reference video

                party.Add(partyParvati);
                #endregion

                #region Skadi
                PartyMember partySkadi = await FrequentlyUsed.PartyMemberAsync(WireMockUtility.SKADI_CASTER, party, resolvedClasses);
                partySkadi.Servant.SkillLevels = new int[] { 7, 7, 8 }; // adjust to the reference video

                party.Add(partySkadi);
                #endregion

                #region Skadi Support
                PartyMember partySupportSkadi = await FrequentlyUsed.PartyMemberAsync(WireMockUtility.SKADI_CASTER, party, resolvedClasses, 1, true);

                party.Add(partySupportSkadi);
                #endregion

                MysticCode mysticCode = new MysticCode
                {
                    MysticCodeLevel = 7,
                    MysticCodeInfo = await resolvedClasses.AtlasAcademyClient.GetMysticCodeInfo(WireMockUtility.ARTIC_ID)
                };

                List<EnemyMob> enemyMobs = GetEnemyChristmas2018();

                /* Simulate node combat */
                // Fight 1/3
                resolvedClasses.ServantSkillActivation.SkillActivation(partySkadi, 1, party, enemyMobs, 1); // Skadi quick up buff
                resolvedClasses.ServantSkillActivation.SkillActivation(partySupportSkadi, 1, party, enemyMobs, 1); // Skadi (support) quick up buff
                resolvedClasses.ServantSkillActivation.SkillActivation(partyParvati, 1, party, enemyMobs, 1); // Parvati quick up buff

                resolvedClasses.CombatFormula.AddPartyMemberToNpChain(party, partyParvati).Should().BeTrue();
                resolvedClasses.CombatFormula.NoblePhantasmChainSimulator(party, enemyMobs, WaveNumberEnum.First);

                _output.WriteLine($"{partyParvati.Servant.ServantInfo.Name} has {partyParvati.NpCharge}% charge after the 1st fight\n");

                // Fight 2/3
                resolvedClasses.ServantSkillActivation.AdjustSkillCooldowns(party);
                resolvedClasses.ServantSkillActivation.SkillActivation(partySkadi, 3, party, enemyMobs, 1); // Skadi NP buff

                resolvedClasses.CombatFormula.AddPartyMemberToNpChain(party, partyParvati).Should().BeTrue();
                resolvedClasses.CombatFormula.NoblePhantasmChainSimulator(party, enemyMobs, WaveNumberEnum.Second);

                _output.WriteLine($"{partyParvati.Servant.ServantInfo.Name} has {partyParvati.NpCharge}% charge after the 2nd fight");

                List<EnemyMob> waveSurvivors = enemyMobs.FindAll(w => w.WaveNumber == WaveNumberEnum.Second);
                ShowSurvivingEnemyHealth(waveSurvivors);

                waveSurvivors.Count.Should().Be(1);
                waveSurvivors.Any(h => h.Health < 30000.0f).Should().BeTrue();

                // To stay in line with the reference video
                enemyMobs.RemoveAll(w => w.WaveNumber == WaveNumberEnum.Second);
                _output.WriteLine($"Assume carding kills last monster...\n");

                // Fight 3/3
                resolvedClasses.ServantSkillActivation.AdjustSkillCooldowns(party);
                resolvedClasses.ServantSkillActivation.SkillActivation(partyParvati, 2, party, enemyMobs, 1); // Parvati ATK, DEF, Star Drop Rate, & Crit Rate Up
                resolvedClasses.ServantSkillActivation.SkillActivation(partySupportSkadi, 3, party, enemyMobs, 1); // Skadi (support) NP buff
                resolvedClasses.ServantSkillActivation.SkillActivation(mysticCode, 2, party, enemyMobs, 1); // Artic ATK & NP STR up
                resolvedClasses.ServantSkillActivation.SkillActivation(partySkadi, 2, party, enemyMobs, 1); // Skadi enemy defense down
                resolvedClasses.ServantSkillActivation.SkillActivation(partySupportSkadi, 2, party, enemyMobs, 1); // Skadi (support) enemy defense down

                resolvedClasses.CombatFormula.AddPartyMemberToNpChain(party, partyParvati).Should().BeTrue();
                resolvedClasses.CombatFormula.NoblePhantasmChainSimulator(party, enemyMobs, WaveNumberEnum.Third);

                waveSurvivors = enemyMobs.FindAll(w => w.WaveNumber == WaveNumberEnum.Third);
                ShowSurvivingEnemyHealth(waveSurvivors);

                using (new AssertionScope())
                {
                    waveSurvivors.Count.Should().Be(1);
                    waveSurvivors.Any(h => h.Health < 21000.0f).Should().BeTrue();
                }
            }
        }

        [Fact]
        public async Task ArcherNodeParvatiDoubleSkadiNoCard()
        {
            _wireMockFixture.CheckIfMockServerInUse();
            _wireMockUtility.AddStubs(_wireMockFixture);

            using (var scope = _container.BeginLifetimeScope())
            {
                ScopedClasses resolvedClasses = AutofacUtility.ResolveScope(scope);
                List<PartyMember> party = new List<PartyMember>();
                CraftEssence chaldeaSuperscope = await FrequentlyUsed.CraftEssenceAsync(resolvedClasses, WireMockUtility.KSCOPE_CE, 100, true);

                /* Party data */
                #region Parvati
                PartyMember partyParvati = await FrequentlyUsed.PartyMemberAsync(WireMockUtility.PARVATI_LANCER, party, resolvedClasses, 4, false, chaldeaSuperscope);
                partyParvati.Servant.SkillLevels = new int[] { 9, 8, 10 };

                party.Add(partyParvati);
                #endregion

                #region Skadi
                PartyMember partySkadi = await FrequentlyUsed.PartyMemberAsync(WireMockUtility.SKADI_CASTER, party, resolvedClasses);

                party.Add(partySkadi);
                #endregion

                #region Skadi Support
                PartyMember partySupportSkadi = await FrequentlyUsed.PartyMemberAsync(WireMockUtility.SKADI_CASTER, party, resolvedClasses, 1, true);

                party.Add(partySupportSkadi);
                #endregion

                MysticCode mysticCode = new MysticCode
                {
                    MysticCodeLevel = 10,
                    MysticCodeInfo = await resolvedClasses.AtlasAcademyClient.GetMysticCodeInfo(WireMockUtility.FRAGMENT_2004_ID)
                };

                List<EnemyMob> enemyMobs = GetEnemyChristmas2018();

                /* Simulate node combat */
                // Fight 1/3
                resolvedClasses.ServantSkillActivation.SkillActivation(partySkadi, 1, party, enemyMobs, 1); // Skadi quick up buff
                resolvedClasses.ServantSkillActivation.SkillActivation(partySupportSkadi, 1, party, enemyMobs, 1); // Skadi (support) quick up buff
                resolvedClasses.ServantSkillActivation.SkillActivation(partyParvati, 1, party, enemyMobs, 1); // Parvati quick up buff

                resolvedClasses.CombatFormula.AddPartyMemberToNpChain(party, partyParvati).Should().BeTrue();
                resolvedClasses.CombatFormula.NoblePhantasmChainSimulator(party, enemyMobs, WaveNumberEnum.First);

                _output.WriteLine($"{partyParvati.Servant.ServantInfo.Name} has {partyParvati.NpCharge}% charge after the 1st fight");

                // Fight 2/3
                resolvedClasses.ServantSkillActivation.AdjustSkillCooldowns(party);
                resolvedClasses.ServantSkillActivation.SkillActivation(partySkadi, 3, party, enemyMobs, 1); // Skadi NP buff
                resolvedClasses.ServantSkillActivation.SkillActivation(partyParvati, 2, party, enemyMobs, 1); // Parvati ATK, DEF, Star Drop Rate, & Crit Rate Up

                resolvedClasses.CombatFormula.AddPartyMemberToNpChain(party, partyParvati).Should().BeTrue();
                resolvedClasses.CombatFormula.NoblePhantasmChainSimulator(party, enemyMobs, WaveNumberEnum.Second);

                _output.WriteLine($"{partyParvati.Servant.ServantInfo.Name} has {partyParvati.NpCharge}% charge after the 2nd fight");

                // Fight 3/3
                resolvedClasses.ServantSkillActivation.AdjustSkillCooldowns(party);
                resolvedClasses.ServantSkillActivation.SkillActivation(partySupportSkadi, 3, party, enemyMobs, 1); // Skadi (support) NP buff
                resolvedClasses.ServantSkillActivation.SkillActivation(mysticCode, 1, party, enemyMobs, 1); // Fragment of 2004 NP STR up
                resolvedClasses.ServantSkillActivation.SkillActivation(partySkadi, 2, party, enemyMobs, 1); // Skadi enemy defense down
                resolvedClasses.ServantSkillActivation.SkillActivation(partySupportSkadi, 2, party, enemyMobs, 1); // Skadi (support) enemy defense down

                resolvedClasses.CombatFormula.AddPartyMemberToNpChain(party, partyParvati).Should().BeTrue();
                resolvedClasses.CombatFormula.NoblePhantasmChainSimulator(party, enemyMobs, WaveNumberEnum.Third);

                enemyMobs.Count.Should().Be(0);
            }
        }

        [Fact]
        public async Task ArcherNodeParvatiDoubleSkadiWaver5CraftEssence()
        {
            _wireMockFixture.CheckIfMockServerInUse();
            _wireMockUtility.AddStubs(_wireMockFixture);

            using (var scope = _container.BeginLifetimeScope())
            {
                ScopedClasses resolvedClasses = AutofacUtility.ResolveScope(scope);
                List<PartyMember> party = new List<PartyMember>();
                CraftEssence chaldeaHolyNightSupper = await FrequentlyUsed.CraftEssenceAsync(resolvedClasses, WireMockUtility.HOLY_NIGHT_SUPPER_CE, 100, true);

                /* Party data */
                #region Parvati
                PartyMember partyParvati = await FrequentlyUsed.PartyMemberAsync(WireMockUtility.PARVATI_LANCER, party, resolvedClasses, 2, false, chaldeaHolyNightSupper);

                party.Add(partyParvati);
                #endregion

                #region Skadi
                PartyMember partySkadi = await FrequentlyUsed.PartyMemberAsync(WireMockUtility.SKADI_CASTER, party, resolvedClasses);

                party.Add(partySkadi);
                #endregion

                #region Skadi Support
                PartyMember partySupportSkadi = await FrequentlyUsed.PartyMemberAsync(WireMockUtility.SKADI_CASTER, party, resolvedClasses, 1, true);

                party.Add(partySupportSkadi);
                #endregion

                #region Waver
                PartyMember partyWaver = await FrequentlyUsed.PartyMemberAsync(WireMockUtility.WAVER_CASTER, party, resolvedClasses);

                party.Add(partyWaver);
                #endregion

                MysticCode mysticCode = new MysticCode
                {
                    MysticCodeLevel = 10,
                    MysticCodeInfo = await resolvedClasses.AtlasAcademyClient.GetMysticCodeInfo(WireMockUtility.PLUGSUIT_ID)
                };

                List<EnemyMob> enemyMobs = GetEnemyChristmas2018();

                /* Simulate node combat */
                // Fight 1/3
                resolvedClasses.ServantSkillActivation.SkillActivation(partySkadi, 1, party, enemyMobs, 1); // Skadi quick up buff
                resolvedClasses.ServantSkillActivation.SkillActivation(partySupportSkadi, 1, party, enemyMobs, 1); // Skadi (support) quick up buff
                resolvedClasses.ServantSkillActivation.SkillActivation(partySupportSkadi, 3, party, enemyMobs, 1); // Skadi (support) NP buff
                resolvedClasses.ServantSkillActivation.SkillActivation(partyParvati, 1, party, enemyMobs, 1); // Parvati quick up buff

                resolvedClasses.CombatFormula.AddPartyMemberToNpChain(party, partyParvati).Should().BeTrue();
                resolvedClasses.CombatFormula.NoblePhantasmChainSimulator(party, enemyMobs, WaveNumberEnum.First);

                _output.WriteLine($"{partyParvati.Servant.ServantInfo.Name} has {partyParvati.NpCharge}% charge after the 1st fight");

                // Fight 2/3
                resolvedClasses.ServantSkillActivation.AdjustSkillCooldowns(party);
                resolvedClasses.ServantSkillActivation.SkillActivation(partySupportSkadi, 2, party, enemyMobs, 1); // Skadi (support) enemy defense down
                resolvedClasses.ServantSkillActivation.SkillActivation(mysticCode, 3, party, enemyMobs, 3, 3, 4); // Plugsuit swap Skadi (support) for Waver
                resolvedClasses.ServantSkillActivation.SkillActivation(partyWaver, 1, party, enemyMobs, 1); // Waver crit damage on Parvati with 30% charge
                resolvedClasses.ServantSkillActivation.SkillActivation(partyWaver, 2, party, enemyMobs, 1); // Waver defense up to party with 10% charge
                resolvedClasses.ServantSkillActivation.SkillActivation(partyWaver, 3, party, enemyMobs, 1); // Waver attack up to party with 10% charge

                resolvedClasses.CombatFormula.AddPartyMemberToNpChain(party, partyParvati).Should().BeTrue();
                resolvedClasses.CombatFormula.NoblePhantasmChainSimulator(party, enemyMobs, WaveNumberEnum.Second);

                _output.WriteLine($"{partyParvati.Servant.ServantInfo.Name} has {partyParvati.NpCharge}% charge after the 2nd fight");

                List<EnemyMob> waveSurvivors = enemyMobs.FindAll(w => w.WaveNumber == WaveNumberEnum.Second);
                ShowSurvivingEnemyHealth(waveSurvivors);

                // Fight 3/3
                resolvedClasses.ServantSkillActivation.AdjustSkillCooldowns(party);
                resolvedClasses.ServantSkillActivation.AdjustSkillCooldowns(mysticCode);
                resolvedClasses.ServantSkillActivation.SkillActivation(partySkadi, 2, party, enemyMobs, 1); // Skadi enemy defense down
                resolvedClasses.ServantSkillActivation.SkillActivation(partySkadi, 3, party, enemyMobs, 1); // Skadi NP buff
                resolvedClasses.ServantSkillActivation.SkillActivation(partyParvati, 2, party, enemyMobs, 1); // Parvati ATK, DEF, Star Drop Rate, & Crit Rate Up
                resolvedClasses.ServantSkillActivation.SkillActivation(mysticCode, 1, party, enemyMobs, 1); // Plugsuit ATK up

                resolvedClasses.CombatFormula.AddPartyMemberToNpChain(party, partyParvati).Should().BeTrue();
                resolvedClasses.CombatFormula.NoblePhantasmChainSimulator(party, enemyMobs, WaveNumberEnum.Third);

                waveSurvivors = enemyMobs.FindAll(w => w.WaveNumber == WaveNumberEnum.Third);
                ShowSurvivingEnemyHealth(waveSurvivors);

                using (new AssertionScope())
                {
                    enemyMobs.Count.Should().Be(1);
                    waveSurvivors.Any(h => h.Health < 4000.0f).Should().BeTrue();
                }
            }
        }

        [Fact]
        public async Task ArcherNodeArashParvatiMerlinSkadi()
        {
            _wireMockFixture.CheckIfMockServerInUse();
            _wireMockUtility.AddStubs(_wireMockFixture);

            using (var scope = _container.BeginLifetimeScope())
            {
                ScopedClasses resolvedClasses = AutofacUtility.ResolveScope(scope);
                List<PartyMember> party = new List<PartyMember>();
                CraftEssence chaldeaHolyNightSupper = await FrequentlyUsed.CraftEssenceAsync(resolvedClasses, WireMockUtility.HOLY_NIGHT_SUPPER_CE, 88, true);
                CraftEssence chaldeaKscope = await FrequentlyUsed.CraftEssenceAsync(resolvedClasses, WireMockUtility.KSCOPE_CE, 20, false);

                /* Party data */
                #region Arash
                PartyMember partyArash = await FrequentlyUsed.PartyMemberAsync(WireMockUtility.ARASH_ARCHER, party, resolvedClasses, 5, false, chaldeaHolyNightSupper);
                partyArash.Servant.SkillLevels = new int[] { 6, 6, 10 };
                party.Add(partyArash);
                #endregion

                #region Parvati
                PartyMember partyParvati = await FrequentlyUsed.PartyMemberAsync(WireMockUtility.PARVATI_LANCER, party, resolvedClasses, 2, false, chaldeaKscope);
                party.Add(partyParvati);
                #endregion

                #region Merlin
                PartyMember partyMerlin = await FrequentlyUsed.PartyMemberAsync(WireMockUtility.MERLIN_CASTER, party, resolvedClasses);
                party.Add(partyMerlin);
                #endregion

                #region Skadi Support
                PartyMember partySkadiSupport = await FrequentlyUsed.PartyMemberAsync(WireMockUtility.SKADI_CASTER, party, resolvedClasses, 1, true);
                party.Add(partySkadiSupport);
                #endregion

                MysticCode mysticCode = new MysticCode
                {
                    MysticCodeLevel = 10,
                    MysticCodeInfo = await resolvedClasses.AtlasAcademyClient.GetMysticCodeInfo(WireMockUtility.FRAGMENT_2004_ID)
                };

                List<EnemyMob> enemyMobs = GetEnemyChristmas2018();

                /* Simulate node combat */
                // Fight 1/3
                resolvedClasses.ServantSkillActivation.SkillActivation(partyArash, 3, party, enemyMobs); // Arash NP charge
                resolvedClasses.ServantSkillActivation.SkillActivation(partyMerlin, 1, party, enemyMobs); // Merlin ATK UP and NP charge

                resolvedClasses.CombatFormula.AddPartyMemberToNpChain(party, partyArash).Should().BeTrue();
                resolvedClasses.CombatFormula.NoblePhantasmChainSimulator(party, enemyMobs, WaveNumberEnum.First);

                // Fight 2/3
                resolvedClasses.ServantSkillActivation.AdjustSkillCooldowns(party);
                resolvedClasses.ServantSkillActivation.SkillActivation(partySkadiSupport, 1, party, enemyMobs, 2); // Skadi (support) quick buff
                resolvedClasses.ServantSkillActivation.SkillActivation(partyParvati, 1, party, enemyMobs); // Parvati quick up buff
                resolvedClasses.ServantSkillActivation.SkillActivation(mysticCode, 3, party, enemyMobs, 2); // Fragment NP gain buff

                resolvedClasses.CombatFormula.AddPartyMemberToNpChain(party, partyParvati).Should().BeTrue();
                resolvedClasses.CombatFormula.NoblePhantasmChainSimulator(party, enemyMobs, WaveNumberEnum.Second);

                _output.WriteLine($"{partyParvati.Servant.ServantInfo.Name} has {partyParvati.NpCharge}% charge after the 2nd fight");

                List<EnemyMob> waveSurvivors = enemyMobs.FindAll(w => w.WaveNumber == WaveNumberEnum.Second);
                ShowSurvivingEnemyHealth(waveSurvivors);

                waveSurvivors.Count(h => h.Health.NearlyEqual(39286.863f)).Should().Be(1);

                // Fight 3/3
                resolvedClasses.ServantSkillActivation.AdjustSkillCooldowns(party);
                resolvedClasses.ServantSkillActivation.AdjustSkillCooldowns(mysticCode);
                resolvedClasses.ServantSkillActivation.SkillActivation(partyParvati, 2, party, enemyMobs); // Parvati ATK, DEF, Star Drop Rate, & Crit Rate Up
                resolvedClasses.ServantSkillActivation.SkillActivation(partyParvati, 3, party, enemyMobs, 2); // Parvati NP charge to self
                resolvedClasses.ServantSkillActivation.SkillActivation(partySkadiSupport, 2, party, enemyMobs); // Skadi enemy defense down
                resolvedClasses.ServantSkillActivation.SkillActivation(partySkadiSupport, 3, party, enemyMobs, 2); // Skadi NP buff
                resolvedClasses.ServantSkillActivation.SkillActivation(mysticCode, 1, party, enemyMobs, 2); // Fragment NP STR up

                resolvedClasses.CombatFormula.AddPartyMemberToNpChain(party, partyParvati).Should().BeTrue();
                resolvedClasses.CombatFormula.NoblePhantasmChainSimulator(party, enemyMobs, WaveNumberEnum.Third);

                waveSurvivors = enemyMobs.FindAll(w => w.WaveNumber == WaveNumberEnum.Third);
                ShowSurvivingEnemyHealth(waveSurvivors);

                using (new AssertionScope())
                {
                    enemyMobs.Count.Should().Be(2);
                    waveSurvivors.Count(h => h.Health.NearlyEqual(46759.25f)).Should().Be(1);
                }
            }
        }

        #region Private Methods
        private void ShowSurvivingEnemyHealth(List<EnemyMob> waveSurvivors)
        {
            foreach (EnemyMob enemy in waveSurvivors)
            {
                _output.WriteLine($"{enemy.Name} has {enemy.Health} HP left");
            }
        }

        private List<EnemyMob> GetEnemyChristmas2018()
        {
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

            return new List<EnemyMob>
            {
                mafiaGhost1, mafiaGhost2, mafiaGhost3,       // wave 1
                bossPet, mafiaGhost4, mafiaGhost5,           // wave 2
                mafiaGhost6, gamblingBookmaker, mafiaGhost7  // wave 3
            };
        }
        #endregion
    }
}
