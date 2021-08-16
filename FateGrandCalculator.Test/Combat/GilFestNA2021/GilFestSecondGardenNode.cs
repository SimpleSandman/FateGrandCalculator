using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Autofac;

using FateGrandCalculator.AtlasAcademy.Json;
using FateGrandCalculator.Enums;
using FateGrandCalculator.Extensions;
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
    public class GilFestSecondGardenNode : IClassFixture<WireMockFixture>
    {
        private readonly WireMockFixture _wireMockFixture;
        private readonly WireMockUtility _wireMockUtility;
        private readonly IContainer _container;
        private readonly ITestOutputHelper _output;

        public GilFestSecondGardenNode(WireMockFixture wireMockFixture, ITestOutputHelper output)
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
                resolvedClasses.ServantSkillActivation.SkillActivation(partySkadiSupport, 1, party, enemyMobs, 1); // Skadi (support) quick up buff

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

                partyLancelot.NpCharge.Should().Be(46);

                // NOTE: To avoid bottom line 46%, card against mecha and NP lancelot to get consistent NP
                partyLancelot.NpCharge = 49.0f;

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
                    partyLancelot.NpCharge.Should().Be(36);
                }
            }
        }

        [Fact]
        public async Task AtalanteNp5LoopDoubleSkadiWaver()
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
                    MysticCodeInfo = await resolvedClasses.AtlasAcademyClient.GetMysticCodeInfo(WireMockUtility.PLUGSUIT_ID)
                };

                #region Party Data
                // Atalante (Archer)
                ServantBasicJson basicJson = constantExportJson.ListServantBasicJson.Find(s => s.Id.ToString() == WireMockUtility.ATALANTE_ARCHER);
                PartyMember partyAtalante = await FrequentlyUsed.PartyMemberAsync(basicJson, party, resolvedClasses, 5, false, chaldeaSuperscope);
                partyAtalante.Servant.SkillLevels = new int[] { 8, 8, 6 };
                party.Add(partyAtalante);

                // Skadi
                basicJson = constantExportJson.ListServantBasicJson.Find(s => s.Id.ToString() == WireMockUtility.SKADI_CASTER);
                PartyMember partySkadi = await FrequentlyUsed.PartyMemberAsync(basicJson, party, resolvedClasses);
                party.Add(partySkadi);

                // Skadi Support
                PartyMember partySkadiSupport = await FrequentlyUsed.PartyMemberAsync(basicJson, party, resolvedClasses, 1, true);
                party.Add(partySkadiSupport);

                // Waver
                basicJson = constantExportJson.ListServantBasicJson.Find(s => s.Id.ToString() == WireMockUtility.WAVER_CASTER);
                PartyMember partyWaver = await FrequentlyUsed.PartyMemberAsync(basicJson, party, resolvedClasses);
                party.Add(partyWaver);
                #endregion

                List<EnemyMob> enemyMobs = GetGardenMob();

                /* Simulate node combat */
                // Fight 1/3
                resolvedClasses.ServantSkillActivation.SkillActivation(partyAtalante, 3, party, enemyMobs, 1); // Atalante 3T NP gain up
                resolvedClasses.ServantSkillActivation.SkillActivation(partySkadi, 1, party, enemyMobs, 1); // Skadi quick up buff
                resolvedClasses.ServantSkillActivation.SkillActivation(partySkadiSupport, 1, party, enemyMobs, 1); // Skadi (support) quick up buff

                resolvedClasses.CombatFormula.AddPartyMemberToNpChain(party, partyAtalante).Should().BeTrue();
                resolvedClasses.CombatFormula.NoblePhantasmChainSimulator(party, enemyMobs, WaveNumberEnum.First, constantExportJson);

                _output.WriteLine($"{partyAtalante.Servant.ServantBasicInfo.Name} has {partyAtalante.NpCharge}% charge after the 1st fight");

                // Fight 2/3
                resolvedClasses.ServantSkillActivation.AdjustSkillCooldowns(party);
                resolvedClasses.ServantSkillActivation.SkillActivation(partySkadi, 3, party, enemyMobs, 1); // Skadi NP buff
                resolvedClasses.ServantSkillActivation.SkillActivation(partyAtalante, 1, party, enemyMobs, 1); // Atalante party quick up

                resolvedClasses.CombatFormula.AddPartyMemberToNpChain(party, partyAtalante).Should().BeTrue();
                resolvedClasses.CombatFormula.NoblePhantasmChainSimulator(party, enemyMobs, WaveNumberEnum.Second, constantExportJson);

                _output.WriteLine($"{partyAtalante.Servant.ServantBasicInfo.Name} has {partyAtalante.NpCharge}% charge after the 2nd fight");

                // Fight 3/3
                resolvedClasses.ServantSkillActivation.AdjustSkillCooldowns(party);
                resolvedClasses.ServantSkillActivation.SkillActivation(partySkadi, 2, party, enemyMobs, 1); // Skadi enemy defense down
                resolvedClasses.ServantSkillActivation.SkillActivation(partySkadiSupport, 2, party, enemyMobs, 1); // Skadi (support) enemy defense down
                resolvedClasses.ServantSkillActivation.SkillActivation(partySkadiSupport, 3, party, enemyMobs, 1); // Skadi (support) NP buff

                resolvedClasses.ServantSkillActivation.SkillActivation(mysticCode, 3, party, enemyMobs, 3, 3, 4); // Swap Skadi (support) with Waver

                resolvedClasses.ServantSkillActivation.SkillActivation(mysticCode, 1, party, enemyMobs, 1); // Plugsuit attack up
                resolvedClasses.ServantSkillActivation.SkillActivation(partyWaver, 3, party, enemyMobs, 1); // Waver attack up to party with 10% charge

                resolvedClasses.CombatFormula.AddPartyMemberToNpChain(party, partyAtalante).Should().BeTrue();
                resolvedClasses.CombatFormula.NoblePhantasmChainSimulator(party, enemyMobs, WaveNumberEnum.Third, constantExportJson);

                _output.WriteLine($"{partyAtalante.Servant.ServantBasicInfo.Name} has {partyAtalante.NpCharge}% charge after the 3rd fight");

                using (new AssertionScope())
                {
                    enemyMobs.Count.Should().Be(0);
                    partyAtalante.NpCharge.Should().Be(46);
                }
            }
        }

        [Fact]
        public async Task FinalWaveAtalanteNp1()
        {
            _wireMockFixture.CheckIfMockServerInUse();
            _wireMockUtility.AddStubs(_wireMockFixture);

            using (var scope = _container.BeginLifetimeScope())
            {
                ScopedClasses resolvedClasses = AutofacUtility.ResolveScope(scope);
                ConstantExportJson constantExportJson = await FrequentlyUsed.GetConstantExportJsonAsync(resolvedClasses.AtlasAcademyClient);
                List<PartyMember> party = new List<PartyMember>();

                EquipBasicJson equipBasicJson = constantExportJson.ListEquipBasicJson.Find(c => c.Id.ToString() == WireMockUtility.HOLY_NIGHT_SUPPER_CE);
                CraftEssence chaldeaHolyNightSupper = FrequentlyUsed.GetCraftEssence(equipBasicJson, 100, true);

                MysticCode mysticCode = new MysticCode
                {
                    MysticCodeLevel = 10,
                    MysticCodeInfo = await resolvedClasses.AtlasAcademyClient.GetMysticCodeInfo(WireMockUtility.PLUGSUIT_ID)
                };

                #region Party Data
                // Atalante (Archer)
                ServantBasicJson basicJson = constantExportJson.ListServantBasicJson.Find(s => s.Id.ToString() == WireMockUtility.ATALANTE_ARCHER);
                PartyMember partyAtalante = await FrequentlyUsed.PartyMemberAsync(basicJson, party, resolvedClasses, 1, false, chaldeaHolyNightSupper);
                partyAtalante.Servant.SkillLevels = new int[] { 10, 8, 10 };
                party.Add(partyAtalante);
                
                // Skadi
                basicJson = constantExportJson.ListServantBasicJson.Find(s => s.Id.ToString() == WireMockUtility.SKADI_CASTER);
                PartyMember partySkadi = await FrequentlyUsed.PartyMemberAsync(basicJson, party, resolvedClasses);
                party.Add(partySkadi);

                // Skadi Support
                PartyMember partySkadiSupport = await FrequentlyUsed.PartyMemberAsync(basicJson, party, resolvedClasses, 1, true);
                party.Add(partySkadiSupport);

                // Waver
                basicJson = constantExportJson.ListServantBasicJson.Find(s => s.Id.ToString() == WireMockUtility.WAVER_CASTER);
                PartyMember partyWaver = await FrequentlyUsed.PartyMemberAsync(basicJson, party, resolvedClasses);
                party.Add(partyWaver);
                #endregion

                List<EnemyMob> enemyMobs = GetGardenMob();

                /* Simulate final phase */
                enemyMobs.RemoveAll(e => e.WaveNumber != WaveNumberEnum.Third);

                // Fight 3/3
                resolvedClasses.ServantSkillActivation.AdjustSkillCooldowns(party);
                resolvedClasses.ServantSkillActivation.SkillActivation(partyAtalante, 1, party, enemyMobs, 1); // Atalante party quick up
                resolvedClasses.ServantSkillActivation.SkillActivation(partySkadi, 1, party, enemyMobs, 1); // Skadi quick up buff
                resolvedClasses.ServantSkillActivation.SkillActivation(partySkadi, 2, party, enemyMobs, 1); // Skadi enemy defense down
                resolvedClasses.ServantSkillActivation.SkillActivation(partySkadi, 3, party, enemyMobs, 1); // Skadi NP charge
                resolvedClasses.ServantSkillActivation.SkillActivation(partySkadiSupport, 1, party, enemyMobs, 1); // Skadi (support) quick up buff
                resolvedClasses.ServantSkillActivation.SkillActivation(partySkadiSupport, 2, party, enemyMobs, 1); // Skadi (support) enemy defense down
                resolvedClasses.ServantSkillActivation.SkillActivation(partySkadiSupport, 3, party, enemyMobs, 1); // Skadi (support) NP charge

                resolvedClasses.ServantSkillActivation.SkillActivation(mysticCode, 3, party, enemyMobs, 3, 3, 4); // Swap Skadi (support) with Waver
                resolvedClasses.ServantSkillActivation.SkillActivation(mysticCode, 1, party, enemyMobs, 1); // Plugsuit attack up

                resolvedClasses.ServantSkillActivation.SkillActivation(partyWaver, 1, party, enemyMobs, 1); // Waver crit up to Atalante with 30% charge
                resolvedClasses.ServantSkillActivation.SkillActivation(partyWaver, 2, party, enemyMobs, 1); // Waver defense up to party with 10% charge
                resolvedClasses.ServantSkillActivation.SkillActivation(partyWaver, 3, party, enemyMobs, 1); // Waver attack up to party with 10% charge

                resolvedClasses.CombatFormula.AddPartyMemberToNpChain(party, partyAtalante).Should().BeTrue();
                resolvedClasses.CombatFormula.NoblePhantasmChainSimulator(party, enemyMobs, WaveNumberEnum.Third, constantExportJson);

                _output.WriteLine($"{partyAtalante.Servant.ServantBasicInfo.Name} has {partyAtalante.NpCharge}% charge after the 3rd fight");

                FrequentlyUsed.ShowSurvivingEnemyHealth(enemyMobs, _output);

                using (new AssertionScope())
                {
                    enemyMobs.Count.Should().Be(0);
                }
            }
        }

        [Fact]
        public async Task AtalanteNp1LoopDoubleSkadiArash()
        {
            _wireMockFixture.CheckIfMockServerInUse();
            _wireMockUtility.AddStubs(_wireMockFixture);

            using (var scope = _container.BeginLifetimeScope())
            {
                ScopedClasses resolvedClasses = AutofacUtility.ResolveScope(scope);
                ConstantExportJson constantExportJson = await FrequentlyUsed.GetConstantExportJsonAsync(resolvedClasses.AtlasAcademyClient);
                List<PartyMember> party = new List<PartyMember>();

                EquipBasicJson equipBasicJson = constantExportJson.ListEquipBasicJson.Find(c => c.Id.ToString() == WireMockUtility.HOLY_NIGHT_SUPPER_CE);
                CraftEssence chaldeaHolyNightSupper = FrequentlyUsed.GetCraftEssence(equipBasicJson, 100, true);

                equipBasicJson = constantExportJson.ListEquipBasicJson.Find(c => c.Id.ToString() == WireMockUtility.KSCOPE_CE);
                CraftEssence chaldeaKscope = FrequentlyUsed.GetCraftEssence(equipBasicJson, 20);

                MysticCode mysticCode = new MysticCode
                {
                    MysticCodeLevel = 10,
                    MysticCodeInfo = await resolvedClasses.AtlasAcademyClient.GetMysticCodeInfo(WireMockUtility.PLUGSUIT_ID)
                };

                #region Party Data
                // Atalante (Archer)
                ServantBasicJson basicJson = constantExportJson.ListServantBasicJson.Find(s => s.Id.ToString() == WireMockUtility.ATALANTE_ARCHER);
                PartyMember partyAtalante = await FrequentlyUsed.PartyMemberAsync(basicJson, party, resolvedClasses, 1, false, chaldeaHolyNightSupper);
                partyAtalante.Servant.SkillLevels = new int[] { 10, 4, 10 };
                party.Add(partyAtalante);

                // Skadi
                basicJson = constantExportJson.ListServantBasicJson.Find(s => s.Id.ToString() == WireMockUtility.SKADI_CASTER);
                PartyMember partySkadi = await FrequentlyUsed.PartyMemberAsync(basicJson, party, resolvedClasses);
                party.Add(partySkadi);

                // Skadi Support
                PartyMember partySkadiSupport = await FrequentlyUsed.PartyMemberAsync(basicJson, party, resolvedClasses, 1, true);
                party.Add(partySkadiSupport);

                // Arash
                basicJson = constantExportJson.ListServantBasicJson.Find(s => s.Id.ToString() == WireMockUtility.ARASH_ARCHER);
                PartyMember partyArash = await FrequentlyUsed.PartyMemberAsync(basicJson, party, resolvedClasses, 5, false, chaldeaKscope);
                party.Add(partyArash);
                #endregion

                List<EnemyMob> enemyMobs = GetGardenMob();

                /* Simulate node combat */
                // Fight 1/3
                resolvedClasses.ServantSkillActivation.SkillActivation(partyAtalante, 3, party, enemyMobs, 1); // Atalante 3T NP gain up
                resolvedClasses.ServantSkillActivation.SkillActivation(partySkadi, 1, party, enemyMobs, 1); // Skadi quick up buff
                resolvedClasses.ServantSkillActivation.SkillActivation(partySkadiSupport, 1, party, enemyMobs, 1); // Skadi (support) quick up buff
                resolvedClasses.ServantSkillActivation.SkillActivation(partySkadiSupport, 3, party, enemyMobs, 1); // Skadi (support) NP charge

                resolvedClasses.CombatFormula.AddPartyMemberToNpChain(party, partyAtalante).Should().BeTrue();
                resolvedClasses.CombatFormula.NoblePhantasmChainSimulator(party, enemyMobs, WaveNumberEnum.First, constantExportJson);

                _output.WriteLine($"{partyAtalante.Servant.ServantBasicInfo.Name} has {partyAtalante.NpCharge}% charge after the 1st fight");

                // Fight 2/3
                resolvedClasses.ServantSkillActivation.AdjustSkillCooldowns(party);
                resolvedClasses.ServantSkillActivation.SkillActivation(mysticCode, 3, party, enemyMobs, 3, 3, 4); // Swap Skadi (support) with Arash

                resolvedClasses.ServantSkillActivation.SkillActivation(partyArash, 3, party, enemyMobs);

                resolvedClasses.CombatFormula.AddPartyMemberToNpChain(party, partyArash).Should().BeTrue();
                resolvedClasses.CombatFormula.NoblePhantasmChainSimulator(party, enemyMobs, WaveNumberEnum.Second, constantExportJson);

                List<EnemyMob> waveSurvivors = enemyMobs.FindAll(e => e.WaveNumber == WaveNumberEnum.Second);
                FrequentlyUsed.ShowSurvivingEnemyHealth(waveSurvivors, _output);

                waveSurvivors.Count(h => h.Health.NearlyEqual(11842.398f)).Should().Be(1);
                enemyMobs.RemoveAll(e => e.WaveNumber == WaveNumberEnum.Second);

                _output.WriteLine($"{partyAtalante.Servant.ServantBasicInfo.Name} has {partyAtalante.NpCharge}% charge after the 2nd fight");

                // Fight 3/3
                resolvedClasses.ServantSkillActivation.AdjustSkillCooldowns(party);
                resolvedClasses.ServantSkillActivation.SkillActivation(partyAtalante, 1, party, enemyMobs, 1); // Atalante party quick up
                resolvedClasses.ServantSkillActivation.SkillActivation(partySkadi, 3, party, enemyMobs, 1); // Skadi NP charge
                resolvedClasses.ServantSkillActivation.SkillActivation(partySkadi, 2, party, enemyMobs, 1); // Skadi enemy defense down
                resolvedClasses.ServantSkillActivation.SkillActivation(partySkadiSupport, 2, party, enemyMobs, 1); // Skadi (support) enemy defense down

                resolvedClasses.ServantSkillActivation.SkillActivation(mysticCode, 1, party, enemyMobs, 1); // Plugsuit attack up

                resolvedClasses.CombatFormula.AddPartyMemberToNpChain(party, partyAtalante).Should().BeTrue();
                resolvedClasses.CombatFormula.NoblePhantasmChainSimulator(party, enemyMobs, WaveNumberEnum.Third, constantExportJson);

                _output.WriteLine($"{partyAtalante.Servant.ServantBasicInfo.Name} has {partyAtalante.NpCharge}% charge after the 3rd fight");

                FrequentlyUsed.ShowSurvivingEnemyHealth(enemyMobs, _output);

                enemyMobs.Count(h => h.Health.NearlyEqual(5291.2344f)).Should().Be(1);
            }
        }

        #region Private Method
        private List<EnemyMob> GetGardenMob()
        {
            #region First Wave
            EnemyMob driver1 = new EnemyMob
            {
                Id = 0,
                Name = "Limousine Driver",
                ClassName = ClassRelationEnum.Saber,
                AttributeName = AttributeRelationEnum.Sky,
                WaveNumber = WaveNumberEnum.First,
                Health = 26427.0f,
                Traits = new List<string>()
            };

            EnemyMob host1 = new EnemyMob
            {
                Id = 1,
                Name = "Limousine Host",
                ClassName = ClassRelationEnum.Saber,
                AttributeName = AttributeRelationEnum.Sky,
                WaveNumber = WaveNumberEnum.First,
                Health = 17066.0f,
                Traits = new List<string>()
            };

            EnemyMob host2 = new EnemyMob
            {
                Id = 2,
                Name = "Limousine Host",
                ClassName = ClassRelationEnum.Saber,
                AttributeName = AttributeRelationEnum.Sky,
                WaveNumber = WaveNumberEnum.First,
                Health = 17066.0f,
                Traits = new List<string>()
            };
            #endregion

            #region Second Wave
            EnemyMob violence = new EnemyMob
            {
                Id = 3,
                Name = "Violence Limousine",
                ClassName = ClassRelationEnum.Saber,
                AttributeName = AttributeRelationEnum.Earth,
                WaveNumber = WaveNumberEnum.Second,
                Health = 65660.0f,
                Traits = new List<string>
                {
                    "wildbeast", "demonic", "mechanical", "superGiant"
                }
            };

            EnemyMob driver2 = new EnemyMob
            {
                Id = 4,
                Name = "Limousine Driver",
                ClassName = ClassRelationEnum.Saber,
                AttributeName = AttributeRelationEnum.Sky,
                WaveNumber = WaveNumberEnum.Second,
                Health = 30869.0f,
                Traits = new List<string>()
            };
            #endregion

            #region Third Wave
            EnemyMob india = new EnemyMob
            {
                Id = 5,
                Name = "India's Holy Maiden",
                ClassName = ClassRelationEnum.Saber,
                AttributeName = AttributeRelationEnum.Human,
                WaveNumber = WaveNumberEnum.Third,
                Health = 88469.0f,
                Traits = new List<string>
                {
                    "genderFemale", "humanoid", "basedOnServant", "weakToEnumaElish", "divine", "saberface", "riding"
                }
            };

            EnemyMob france = new EnemyMob
            {
                Id = 6,
                Name = "France's Holy Maiden",
                ClassName = ClassRelationEnum.Ruler,
                AttributeName = AttributeRelationEnum.Star,
                WaveNumber = WaveNumberEnum.Third,
                Health = 45279.0f,
                Traits = new List<string>
                {
                    "genderMale", "humanoid", "basedOnServant", "weakToEnumaElish", "saberface"
                }
            };

            EnemyMob bananaPrince = new EnemyMob
            {
                Id = 7,
                Name = "Banana Prince",
                ClassName = ClassRelationEnum.Saber,
                AttributeName = AttributeRelationEnum.Sky,
                WaveNumber = WaveNumberEnum.Third,
                Health = 131923.0f,
                Traits = new List<string>
                {
                    "genderFemale", "humanoid", "basedOnServant", "weakToEnumaElish", "divine", "riding", "brynhildsBeloved"
                }
            };
            #endregion

            return new List<EnemyMob>
            {
                driver1, host1, host2,       // wave 1
                violence, driver2,           // wave 2
                india, france, bananaPrince  // wave 3
            };
        }
        #endregion
    }
}
