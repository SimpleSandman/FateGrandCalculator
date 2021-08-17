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
    public class GilFestThirdGardenNode : IClassFixture<WireMockFixture>
    {
        private readonly WireMockFixture _wireMockFixture;
        private readonly WireMockUtility _wireMockUtility;
        private readonly IContainer _container;
        private readonly ITestOutputHelper _output;

        public GilFestThirdGardenNode(WireMockFixture wireMockFixture, ITestOutputHelper output)
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
                    partyLancelot.NpCharge.Should().Be(35);
                }
            }
        }

        [Fact]
        public async Task AtalanteNp1LoopDoubleSkadiWaver()
        {
            _wireMockFixture.CheckIfMockServerInUse();
            _wireMockUtility.AddStubs(_wireMockFixture);

            using (var scope = _container.BeginLifetimeScope())
            {
                ScopedClasses resolvedClasses = AutofacUtility.ResolveScope(scope);
                ConstantExportJson constantExportJson = await FrequentlyUsed.GetConstantExportJsonAsync(resolvedClasses.AtlasAcademyClient);
                List<PartyMember> party = new List<PartyMember>();

                EquipBasicJson equipBasicJson = constantExportJson.ListEquipBasicJson.Find(c => c.Id.ToString() == WireMockUtility.KSCOPE_CE);
                CraftEssence chaldeaKscope = FrequentlyUsed.GetCraftEssence(equipBasicJson, 20);

                MysticCode mysticCode = new MysticCode
                {
                    MysticCodeLevel = 10,
                    MysticCodeInfo = await resolvedClasses.AtlasAcademyClient.GetMysticCodeInfo(WireMockUtility.PLUGSUIT_ID)
                };

                #region Party Data
                // Atalante (Archer)
                ServantBasicJson basicJson = constantExportJson.ListServantBasicJson.Find(s => s.Id.ToString() == WireMockUtility.ATALANTE_ARCHER);
                PartyMember partyAtalante = await FrequentlyUsed.PartyMemberAsync(basicJson, party, resolvedClasses, 5, false, chaldeaKscope);
                party.Add(partyAtalante);

                // Skadi
                basicJson = constantExportJson.ListServantBasicJson.Find(s => s.Id.ToString() == WireMockUtility.SKADI_CASTER);
                PartyMember partySkadi = await FrequentlyUsed.PartyMemberAsync(basicJson, party, resolvedClasses);
                party.Add(partySkadi);

                // Waver
                basicJson = constantExportJson.ListServantBasicJson.Find(s => s.Id.ToString() == WireMockUtility.WAVER_CASTER);
                PartyMember partyWaver = await FrequentlyUsed.PartyMemberAsync(basicJson, party, resolvedClasses);
                party.Add(partyWaver);

                // Skadi Support
                basicJson = constantExportJson.ListServantBasicJson.Find(s => s.Id.ToString() == WireMockUtility.SKADI_CASTER);
                PartyMember partySkadiSupport = await FrequentlyUsed.PartyMemberAsync(basicJson, party, resolvedClasses, 1, true);
                party.Add(partySkadiSupport);
                #endregion

                List<EnemyMob> enemyMobs = GetGardenMob();

                /* Simulate node combat */
                // Fight 1/3
                resolvedClasses.ServantSkillActivation.SkillActivation(partyAtalante, 3, party, enemyMobs, 1); // Atalante 3T NP gain up
                resolvedClasses.ServantSkillActivation.SkillActivation(partySkadi, 1, party, enemyMobs, 1); // Skadi quick up buff
                resolvedClasses.ServantSkillActivation.SkillActivation(partyWaver, 2, party, enemyMobs, 1); // Waver defense up to party with 10% charge
                resolvedClasses.ServantSkillActivation.SkillActivation(partyWaver, 3, party, enemyMobs, 1); // Waver attack up to party with 10% charge

                resolvedClasses.CombatFormula.AddPartyMemberToNpChain(party, partyAtalante).Should().BeTrue();
                resolvedClasses.CombatFormula.NoblePhantasmChainSimulator(party, enemyMobs, WaveNumberEnum.First, constantExportJson);

                _output.WriteLine($"{partyAtalante.Servant.ServantBasicInfo.Name} has {partyAtalante.NpCharge}% charge after the 1st fight");

                // Fight 2/3
                resolvedClasses.ServantSkillActivation.AdjustSkillCooldowns(party);
                resolvedClasses.ServantSkillActivation.SkillActivation(partySkadi, 3, party, enemyMobs, 1); // Skadi NP buff
                resolvedClasses.ServantSkillActivation.SkillActivation(partySkadi, 2, party, enemyMobs, 1); // Skadi enemy defense down

                resolvedClasses.ServantSkillActivation.SkillActivation(mysticCode, 3, party, enemyMobs, 2, 3, 4); // Swap Skadi with Skadi (support)

                resolvedClasses.ServantSkillActivation.SkillActivation(partySkadiSupport, 1, party, enemyMobs, 1); // Skadi (support) quick up buff

                resolvedClasses.CombatFormula.AddPartyMemberToNpChain(party, partyAtalante).Should().BeTrue();
                resolvedClasses.CombatFormula.NoblePhantasmChainSimulator(party, enemyMobs, WaveNumberEnum.Second, constantExportJson);

                _output.WriteLine($"{partyAtalante.Servant.ServantBasicInfo.Name} has {partyAtalante.NpCharge}% charge after the 2nd fight");

                // Fight 3/3
                resolvedClasses.ServantSkillActivation.AdjustSkillCooldowns(party);
                resolvedClasses.ServantSkillActivation.SkillActivation(partyWaver, 1, party, enemyMobs, 1); // Waver crit up to Atalante with 30% charge
                resolvedClasses.ServantSkillActivation.SkillActivation(mysticCode, 1, party, enemyMobs, 1); // Plugsuit attack up
                resolvedClasses.ServantSkillActivation.SkillActivation(partyAtalante, 1, party, enemyMobs, 1); // Atalante party quick up
                resolvedClasses.ServantSkillActivation.SkillActivation(partySkadiSupport, 2, party, enemyMobs, 1); // Skadi (support) enemy defense down
                resolvedClasses.ServantSkillActivation.SkillActivation(partySkadiSupport, 3, party, enemyMobs, 1); // Skadi (support) NP buff

                resolvedClasses.CombatFormula.AddPartyMemberToNpChain(party, partyAtalante).Should().BeTrue();
                resolvedClasses.CombatFormula.NoblePhantasmChainSimulator(party, enemyMobs, WaveNumberEnum.Third, constantExportJson);

                _output.WriteLine($"{partyAtalante.Servant.ServantBasicInfo.Name} has {partyAtalante.NpCharge}% charge after the 3rd fight");

                using (new AssertionScope())
                {
                    enemyMobs.Count.Should().Be(0);
                    partyAtalante.NpCharge.Should().Be(65);
                }
            }
        }

        [Fact]
        public async Task AtalanteNp1LoopDoubleSkadiReines()
        {
            _wireMockFixture.CheckIfMockServerInUse();
            _wireMockUtility.AddStubs(_wireMockFixture);

            using (var scope = _container.BeginLifetimeScope())
            {
                ScopedClasses resolvedClasses = AutofacUtility.ResolveScope(scope);
                ConstantExportJson constantExportJson = await FrequentlyUsed.GetConstantExportJsonAsync(resolvedClasses.AtlasAcademyClient);
                List<PartyMember> party = new List<PartyMember>();

                EquipBasicJson equipBasicJson = constantExportJson.ListEquipBasicJson.Find(c => c.Id.ToString() == WireMockUtility.KSCOPE_CE);
                CraftEssence chaldeaKscope = FrequentlyUsed.GetCraftEssence(equipBasicJson, 20);

                MysticCode mysticCode = new MysticCode
                {
                    MysticCodeLevel = 10,
                    MysticCodeInfo = await resolvedClasses.AtlasAcademyClient.GetMysticCodeInfo(WireMockUtility.PLUGSUIT_ID)
                };

                #region Party Data
                // Atalante (Archer)
                ServantBasicJson basicJson = constantExportJson.ListServantBasicJson.Find(s => s.Id.ToString() == WireMockUtility.ATALANTE_ARCHER);
                PartyMember partyAtalante = await FrequentlyUsed.PartyMemberAsync(basicJson, party, resolvedClasses, 5, false, chaldeaKscope);
                party.Add(partyAtalante);

                // Skadi
                basicJson = constantExportJson.ListServantBasicJson.Find(s => s.Id.ToString() == WireMockUtility.SKADI_CASTER);
                PartyMember partySkadi = await FrequentlyUsed.PartyMemberAsync(basicJson, party, resolvedClasses);
                party.Add(partySkadi);

                // Sima Yi Reines
                basicJson = constantExportJson.ListServantBasicJson.Find(s => s.Id.ToString() == WireMockUtility.SIMA_YI_RIDER);
                PartyMember partySimaYiReines = await FrequentlyUsed.PartyMemberAsync(basicJson, party, resolvedClasses);
                party.Add(partySimaYiReines);

                // Skadi Support
                basicJson = constantExportJson.ListServantBasicJson.Find(s => s.Id.ToString() == WireMockUtility.SKADI_CASTER);
                PartyMember partySkadiSupport = await FrequentlyUsed.PartyMemberAsync(basicJson, party, resolvedClasses, 1, true);
                party.Add(partySkadiSupport);
                #endregion

                List<EnemyMob> enemyMobs = GetGardenMob();

                /* Simulate node combat */
                // Fight 1/3
                resolvedClasses.ServantSkillActivation.SkillActivation(partyAtalante, 3, party, enemyMobs, 1); // Atalante 3T NP gain up
                resolvedClasses.ServantSkillActivation.SkillActivation(partySkadi, 1, party, enemyMobs, 1); // Skadi quick up buff
                resolvedClasses.ServantSkillActivation.SkillActivation(partySimaYiReines, 2, party, enemyMobs, 1); // Sima Yi 20% NP charge and 40% attack

                resolvedClasses.ServantSkillActivation.SkillActivation(mysticCode, 3, party, enemyMobs, 3, 3, 4); // Swap Sima Yi with Skadi (support)

                resolvedClasses.ServantSkillActivation.SkillActivation(partySkadiSupport, 1, party, enemyMobs, 1); // Skadi (support) quick up buff

                resolvedClasses.CombatFormula.AddPartyMemberToNpChain(party, partyAtalante).Should().BeTrue();
                resolvedClasses.CombatFormula.NoblePhantasmChainSimulator(party, enemyMobs, WaveNumberEnum.First, constantExportJson);

                _output.WriteLine($"{partyAtalante.Servant.ServantBasicInfo.Name} has {partyAtalante.NpCharge}% charge after the 1st fight");

                // Fight 2/3
                resolvedClasses.ServantSkillActivation.AdjustSkillCooldowns(party);
                resolvedClasses.ServantSkillActivation.SkillActivation(partySkadi, 3, party, enemyMobs, 1); // Skadi NP charge

                resolvedClasses.CombatFormula.AddPartyMemberToNpChain(party, partyAtalante).Should().BeTrue();
                resolvedClasses.CombatFormula.NoblePhantasmChainSimulator(party, enemyMobs, WaveNumberEnum.Second, constantExportJson);

                _output.WriteLine($"{partyAtalante.Servant.ServantBasicInfo.Name} has {partyAtalante.NpCharge}% charge after the 2nd fight");

                // Fight 3/3
                resolvedClasses.ServantSkillActivation.AdjustSkillCooldowns(party);
                resolvedClasses.ServantSkillActivation.SkillActivation(partySkadi, 2, party, enemyMobs, 1); // Skadi enemy defense down
                resolvedClasses.ServantSkillActivation.SkillActivation(partyAtalante, 1, party, enemyMobs, 1); // Atalante party quick up

                resolvedClasses.ServantSkillActivation.SkillActivation(partySkadiSupport, 2, party, enemyMobs, 1); // Skadi (support) enemy defense down
                resolvedClasses.ServantSkillActivation.SkillActivation(partySkadiSupport, 3, party, enemyMobs, 1); // Skadi (support) NP charge

                resolvedClasses.CombatFormula.AddPartyMemberToNpChain(party, partyAtalante).Should().BeTrue();
                resolvedClasses.CombatFormula.NoblePhantasmChainSimulator(party, enemyMobs, WaveNumberEnum.Third, constantExportJson);

                _output.WriteLine($"{partyAtalante.Servant.ServantBasicInfo.Name} has {partyAtalante.NpCharge}% charge after the 3rd fight");

                using (new AssertionScope())
                {
                    enemyMobs.Count.Should().Be(0);
                    partyAtalante.NpCharge.Should().Be(66);
                }
            }
        }

        [Fact]
        public async Task AtalanteNp1LoopDoubleSkadiHelena()
        {
            _wireMockFixture.CheckIfMockServerInUse();
            _wireMockUtility.AddStubs(_wireMockFixture);

            using (var scope = _container.BeginLifetimeScope())
            {
                ScopedClasses resolvedClasses = AutofacUtility.ResolveScope(scope);
                ConstantExportJson constantExportJson = await FrequentlyUsed.GetConstantExportJsonAsync(resolvedClasses.AtlasAcademyClient);
                List<PartyMember> party = new List<PartyMember>();

                EquipBasicJson equipBasicJson = constantExportJson.ListEquipBasicJson.Find(c => c.Id.ToString() == WireMockUtility.KSCOPE_CE);
                CraftEssence chaldeaKscope = FrequentlyUsed.GetCraftEssence(equipBasicJson, 20);

                MysticCode mysticCode = new MysticCode
                {
                    MysticCodeLevel = 9,
                    MysticCodeInfo = await resolvedClasses.AtlasAcademyClient.GetMysticCodeInfo(WireMockUtility.PLUGSUIT_ID)
                };

                #region Party Data
                // Atalante (Archer)
                ServantBasicJson basicJson = constantExportJson.ListServantBasicJson.Find(s => s.Id.ToString() == WireMockUtility.ATALANTE_ARCHER);
                PartyMember partyAtalante = await FrequentlyUsed.PartyMemberAsync(basicJson, party, resolvedClasses, 5, false, chaldeaKscope);
                partyAtalante.Servant.SkillLevels = new int[] { 10, 4, 10 };
                party.Add(partyAtalante);

                // Skadi
                basicJson = constantExportJson.ListServantBasicJson.Find(s => s.Id.ToString() == WireMockUtility.SKADI_CASTER);
                PartyMember partySkadi = await FrequentlyUsed.PartyMemberAsync(basicJson, party, resolvedClasses);
                party.Add(partySkadi);

                // Helena (Caster)
                basicJson = constantExportJson.ListServantBasicJson.Find(s => s.Id.ToString() == WireMockUtility.HELENA_CASTER);
                PartyMember partyHelena = await FrequentlyUsed.PartyMemberAsync(basicJson, party, resolvedClasses);
                partyHelena.Servant.SkillLevels = new int[] { 10, 4, 10 };
                party.Add(partyHelena);

                // Skadi Support
                basicJson = constantExportJson.ListServantBasicJson.Find(s => s.Id.ToString() == WireMockUtility.SKADI_CASTER);
                PartyMember partySkadiSupport = await FrequentlyUsed.PartyMemberAsync(basicJson, party, resolvedClasses, 1, true);
                party.Add(partySkadiSupport);
                #endregion

                List<EnemyMob> enemyMobs = GetGardenMob();

                /* Simulate node combat */
                // Fight 1/3
                resolvedClasses.ServantSkillActivation.SkillActivation(partyAtalante, 3, party, enemyMobs, 1); // Atalante 3T NP gain up
                resolvedClasses.ServantSkillActivation.SkillActivation(partySkadi, 1, party, enemyMobs, 1); // Skadi quick up buff
                resolvedClasses.ServantSkillActivation.SkillActivation(partyHelena, 1, party, enemyMobs, 1); // Helena party 20% NP charge
                resolvedClasses.ServantSkillActivation.SkillActivation(partyHelena, 3, party, enemyMobs, 1); // Helena 3T party QAB card type buff

                resolvedClasses.ServantSkillActivation.SkillActivation(mysticCode, 3, party, enemyMobs, 3, 3, 4); // Swap Helena with Skadi (support)

                resolvedClasses.ServantSkillActivation.SkillActivation(partySkadiSupport, 1, party, enemyMobs, 1); // Skadi (support) quick up buff

                resolvedClasses.CombatFormula.AddPartyMemberToNpChain(party, partyAtalante).Should().BeTrue();
                resolvedClasses.CombatFormula.NoblePhantasmChainSimulator(party, enemyMobs, WaveNumberEnum.First, constantExportJson);

                _output.WriteLine($"{partyAtalante.Servant.ServantBasicInfo.Name} has {partyAtalante.NpCharge}% charge after the 1st fight");

                // Fight 2/3
                resolvedClasses.ServantSkillActivation.AdjustSkillCooldowns(party);
                resolvedClasses.ServantSkillActivation.SkillActivation(partySkadi, 3, party, enemyMobs, 1); // Skadi NP charge

                resolvedClasses.CombatFormula.AddPartyMemberToNpChain(party, partyAtalante).Should().BeTrue();
                resolvedClasses.CombatFormula.NoblePhantasmChainSimulator(party, enemyMobs, WaveNumberEnum.Second, constantExportJson);

                _output.WriteLine($"{partyAtalante.Servant.ServantBasicInfo.Name} has {partyAtalante.NpCharge}% charge after the 2nd fight");

                // Fight 3/3
                resolvedClasses.ServantSkillActivation.AdjustSkillCooldowns(party);
                resolvedClasses.ServantSkillActivation.SkillActivation(partySkadi, 2, party, enemyMobs, 1); // Skadi enemy defense down
                resolvedClasses.ServantSkillActivation.SkillActivation(partyAtalante, 1, party, enemyMobs, 1); // Atalante party quick up
                resolvedClasses.ServantSkillActivation.SkillActivation(partySkadiSupport, 2, party, enemyMobs, 1); // Skadi (support) enemy defense down
                resolvedClasses.ServantSkillActivation.SkillActivation(partySkadiSupport, 3, party, enemyMobs, 1); // Skadi (support) NP charge

                resolvedClasses.CombatFormula.AddPartyMemberToNpChain(party, partyAtalante).Should().BeTrue();
                resolvedClasses.CombatFormula.NoblePhantasmChainSimulator(party, enemyMobs, WaveNumberEnum.Third, constantExportJson);

                _output.WriteLine($"{partyAtalante.Servant.ServantBasicInfo.Name} has {partyAtalante.NpCharge}% charge after the 3rd fight");

                using (new AssertionScope())
                {
                    enemyMobs.Count.Should().Be(0);
                    partyAtalante.NpCharge.Should().Be(69);
                }
            }
        }

        #region Private Method
        private List<EnemyMob> GetGardenMob()
        {
            #region First Wave
            EnemyMob ghost1 = new EnemyMob
            {
                Id = 0,
                Name = "Shaking Ghost",
                ClassName = ClassRelationEnum.Saber,
                AttributeName = AttributeRelationEnum.Sky,
                WaveNumber = WaveNumberEnum.First,
                Health = 20845.0f,
                Traits = new List<string> 
                {
                    "demonic", "undead", "genderUnknown"
                }
            };

            EnemyMob ghost2 = new EnemyMob
            {
                Id = 1,
                Name = "Absent-Minded Ghost",
                ClassName = ClassRelationEnum.Saber,
                AttributeName = AttributeRelationEnum.Sky,
                WaveNumber = WaveNumberEnum.First,
                Health = 11026.0f,
                Traits = new List<string>
                {
                    "demonic", "undead", "genderUnknown"
                }
            };

            EnemyMob ghost3 = new EnemyMob
            {
                Id = 2,
                Name = "Absent-Minded Ghost",
                ClassName = ClassRelationEnum.Saber,
                AttributeName = AttributeRelationEnum.Sky,
                WaveNumber = WaveNumberEnum.First,
                Health = 11026.0f,
                Traits = new List<string>
                {
                    "demonic", "undead", "genderUnknown"
                }
            };
            #endregion

            #region Second Wave
            EnemyMob ghost4 = new EnemyMob
            {
                Id = 3,
                Name = "Shaking Ghost",
                ClassName = ClassRelationEnum.Saber,
                AttributeName = AttributeRelationEnum.Sky,
                WaveNumber = WaveNumberEnum.Second,
                Health = 24788.0f,
                Traits = new List<string>
                {
                    "demonic", "undead", "genderUnknown"
                }
            };

            EnemyMob ghost5 = new EnemyMob
            {
                Id = 4,
                Name = "Shaking Ghost",
                ClassName = ClassRelationEnum.Saber,
                AttributeName = AttributeRelationEnum.Sky,
                WaveNumber = WaveNumberEnum.Second,
                Health = 24788.0f,
                Traits = new List<string>
                {
                    "demonic", "undead", "genderUnknown"
                }
            };

            EnemyMob glassesGod = new EnemyMob
            {
                Id = 5,
                Name = "Glasses God",
                ClassName = ClassRelationEnum.Saber,
                AttributeName = AttributeRelationEnum.Earth,
                WaveNumber = WaveNumberEnum.Second,
                Health = 65660.0f,
                Traits = new List<string>
                {
                    "demonic", "superGiant", "genderUnknown"
                }
            };
            #endregion

            #region Third Wave
            EnemyMob cardboardGlassesShine = new EnemyMob
            {
                Id = 6,
                Name = "Glasses Shine!",
                ClassName = ClassRelationEnum.Saber,
                AttributeName = AttributeRelationEnum.Earth,
                WaveNumber = WaveNumberEnum.Third,
                Health = 140976.0f,
                Traits = new List<string>
                {
                    "genderMale", "humanoid", "basedOnServant", "weakToEnumaElish", "riding", "king", "dragon", "brynhildsBeloved"
                }
            };

            EnemyMob kaibaGlassesShine = new EnemyMob
            {
                Id = 7,
                Name = "Glasses...Shine...!",
                ClassName = ClassRelationEnum.Saber,
                AttributeName = AttributeRelationEnum.Earth,
                WaveNumber = WaveNumberEnum.Third,
                Health = 81360.0f,
                Traits = new List<string>
                {
                    "genderMale", "humanoid", "basedOnServant", "weakToEnumaElish", "divine", "dragon", "riding", "brynhildsBeloved"
                }
            };

            EnemyMob hearingEyePopOut = new EnemyMob
            {
                Id = 8,
                Name = "I Heard That Having Great Insight...Can Cause Your Eye To Pop Out",
                ClassName = ClassRelationEnum.Saber,
                AttributeName = AttributeRelationEnum.Human,
                WaveNumber = WaveNumberEnum.Third,
                Health = 76290.0f,
                Traits = new List<string>
                {
                    "genderMale", "humanoid", "basedOnServant", "weakToEnumaElish", "riding", "brynhildsBeloved"
                }
            };
            #endregion

            return new List<EnemyMob>
            {
                ghost1, ghost2, ghost3,                                       // wave 1
                ghost4, ghost5, glassesGod,                                   // wave 2
                cardboardGlassesShine, kaibaGlassesShine, hearingEyePopOut    // wave 3
            };
        }
        #endregion
    }
}
