using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Autofac;

using FateGrandCalculator.AtlasAcademy.Json;
using FateGrandCalculator.Models;

using FateGrandCalculator.Test.Utility;
using FateGrandCalculator.Test.Utility.AutofacConfig;
using FateGrandCalculator.Test.Utility.Fixture;

using FluentAssertions;
using FluentAssertions.Execution;

using Xunit;
using Xunit.Abstractions;

namespace FateGrandCalculator.Test.Management
{
    public class MaterialCalculationTest : IClassFixture<WireMockFixture>
    {
        private readonly WireMockFixture _wireMockFixture;
        private readonly WireMockUtility _wireMockUtility;
        private readonly IContainer _container;
        private readonly ITestOutputHelper _output;

        public MaterialCalculationTest(WireMockFixture wireMockFixture, ITestOutputHelper output)
        {
            _wireMockFixture = wireMockFixture;
            _output = output;
            _wireMockUtility = new WireMockUtility("NA");
            _container = ContainerBuilderInit.Create("NA");
        }

        [Fact]
        public async Task FreshWaverLored100()
        {
            _wireMockFixture.CheckIfMockServerInUse();
            _wireMockUtility.AddStubs(_wireMockFixture);

            using (var scope = _container.BeginLifetimeScope())
            {
                ScopedClasses resolvedClasses = AutofacUtility.ResolveScope(scope);
                ConstantExportJson constantExportJson = await FrequentlyUsed.GetConstantExportJsonAsync(resolvedClasses.AtlasAcademyClient);
                int servantId = 501900;
                ServantBasicJson servantBasicJson = constantExportJson.ListServantBasicJson.Find(s => s.Id == servantId);

                /* Load servants as if they're already in a save file */
                ChaldeaServant currentServant = new ChaldeaServant
                {
                    ServantBasicInfo = servantBasicJson,
                    FouAttack = 1000,
                    FouHealth = 1000,
                    IsSupportServant = false,
                    NpLevel = 1,
                    ServantLevel = 1,
                    SkillLevels = new int[] { 1, 1, 1 }
                };

                ChaldeaServant goalServant = new ChaldeaServant
                {
                    ServantBasicInfo = servantBasicJson,
                    FouAttack = 1000,
                    FouHealth = 1000,
                    IsSupportServant = false,
                    NpLevel = 1,
                    ServantLevel = 100,
                    SkillLevels = new int[] { 10, 10, 10 }
                };

                RequiredItemMaterials req = resolvedClasses.MaterialCalculation.HowMuchIsNeeded(
                    currentServant,
                    goalServant,
                    constantExportJson.GrailCostNiceJson,
                    await resolvedClasses.AtlasAcademyClient.GetServantInfo(servantId.ToString()));

                Dictionary<string, int> materials = resolvedClasses.MaterialCalculation.GroupItemParents(req.Items);
                MaterialOutput(req, materials);

                using (new AssertionScope())
                {
                    RequiredItemsShouldBe(req, 222600000, 5, 753, 627, 251, 209);
                    materials.Should().Contain("Caster Monument", 17);
                    materials.Should().Contain("Caster Piece", 17);
                    materials.Should().Contain("Crystallized Lore", 3);
                    materials.Should().Contain("Eternal Gear", 15);
                    materials.Should().Contain("Forbidden Page", 55);
                    materials.Should().Contain("Gem of Caster", 51);
                    materials.Should().Contain("Heart of the Foreign God", 33);
                    materials.Should().Contain("Magic Gem of Caster", 51);
                    materials.Should().Contain("Phoenix Feather", 70);
                    materials.Should().Contain("Secret Gem of Caster", 51);
                    materials.Should().Contain("Void's Dust", 105);
                }
            }
        }

        [Fact]
        public async Task FreshValkyrieSkilled100()
        {
            _wireMockFixture.CheckIfMockServerInUse();
            _wireMockUtility.AddStubs(_wireMockFixture);

            using (var scope = _container.BeginLifetimeScope())
            {
                ScopedClasses resolvedClasses = AutofacUtility.ResolveScope(scope);
                ConstantExportJson constantExportJson = await FrequentlyUsed.GetConstantExportJsonAsync(resolvedClasses.AtlasAcademyClient);
                int servantId = 303300;
                ServantBasicJson servantBasicJson = constantExportJson.ListServantBasicJson.Find(s => s.Id == servantId);

                /* Load servants as if they're already in a save file */
                ChaldeaServant currentServant = new ChaldeaServant
                {
                    ServantBasicInfo = servantBasicJson,
                    FouAttack = 1000,
                    FouHealth = 1000,
                    IsSupportServant = false,
                    NpLevel = 1,
                    ServantLevel = 1,
                    SkillLevels = new int[] { 1, 1, 1 }
                };

                ChaldeaServant goalServant = new ChaldeaServant
                {
                    ServantBasicInfo = servantBasicJson,
                    FouAttack = 1000,
                    FouHealth = 1000,
                    IsSupportServant = false,
                    NpLevel = 1,
                    ServantLevel = 100,
                    SkillLevels = new int[] { 9, 9, 9 }
                };

                RequiredItemMaterials req = resolvedClasses.MaterialCalculation.HowMuchIsNeeded(
                    currentServant,
                    goalServant,
                    constantExportJson.GrailCostNiceJson,
                    await resolvedClasses.AtlasAcademyClient.GetServantInfo(servantId.ToString()));

                Dictionary<string, int> materials = resolvedClasses.MaterialCalculation.GroupItemParents(req.Items);
                MaterialOutput(req, materials);

                using (new AssertionScope())
                {
                    RequiredItemsShouldBe(req, 102800000, 7, 753, 627, 251, 209);
                    materials.Should().Contain("Aurora Steel", 70);
                    materials.Should().Contain("Gem of Lancer", 42);
                    materials.Should().Contain("Ghost Lantern", 60);
                    materials.Should().Contain("Lancer Monument", 14);
                    materials.Should().Contain("Lancer Piece", 14);
                    materials.Should().Contain("Magic Gem of Lancer", 42);
                    materials.Should().Contain("Phoenix Feather", 11);
                    materials.Should().Contain("Proof of Hero", 132);
                    materials.Should().Contain("Secret Gem of Lancer", 42);
                    materials.Should().Contain("Seed of Yggdrasil", 53);
                }
            }
        }

        [Fact]
        public async Task FreshUshiwakamaruRiderSkilled100()
        {
            _wireMockFixture.CheckIfMockServerInUse();
            _wireMockUtility.AddStubs(_wireMockFixture);

            using (var scope = _container.BeginLifetimeScope())
            {
                ScopedClasses resolvedClasses = AutofacUtility.ResolveScope(scope);
                ConstantExportJson constantExportJson = await FrequentlyUsed.GetConstantExportJsonAsync(resolvedClasses.AtlasAcademyClient);
                int servantId = 401400;
                ServantBasicJson servantBasicJson = constantExportJson.ListServantBasicJson.Find(s => s.Id == servantId);

                /* Load servants as if they're already in a save file */
                ChaldeaServant currentServant = new ChaldeaServant
                {
                    ServantBasicInfo = servantBasicJson,
                    FouAttack = 1000,
                    FouHealth = 1000,
                    IsSupportServant = false,
                    NpLevel = 1,
                    ServantLevel = 1,
                    SkillLevels = new int[] { 1, 1, 1 }
                };

                ChaldeaServant goalServant = new ChaldeaServant
                {
                    ServantBasicInfo = servantBasicJson,
                    FouAttack = 1000,
                    FouHealth = 1000,
                    IsSupportServant = false,
                    NpLevel = 1,
                    ServantLevel = 100,
                    SkillLevels = new int[] { 9, 9, 9 }
                };

                RequiredItemMaterials req = resolvedClasses.MaterialCalculation.HowMuchIsNeeded(
                    currentServant,
                    goalServant,
                    constantExportJson.GrailCostNiceJson,
                    await resolvedClasses.AtlasAcademyClient.GetServantInfo(servantId.ToString()));

                Dictionary<string, int> materials = resolvedClasses.MaterialCalculation.GroupItemParents(req.Items);
                MaterialOutput(req, materials);

                using (new AssertionScope())
                {
                    RequiredItemsShouldBe(req, 72130000, 9, 753, 627, 251, 209);
                    materials.Should().Contain("Eternal Gear", 48);
                    materials.Should().Contain("Gem of Rider", 36);
                    materials.Should().Contain("Ghost Lantern", 11);
                    materials.Should().Contain("Magic Gem of Rider", 36);
                    materials.Should().Contain("Meteor Horseshoe", 40);
                    materials.Should().Contain("Octuplet Crystals", 56);
                    materials.Should().Contain("Proof of Hero", 105);
                    materials.Should().Contain("Rider Monument", 12);
                    materials.Should().Contain("Rider Piece", 12);
                    materials.Should().Contain("Secret Gem of Rider", 36);
                }
            }
        }

        #region Private Methods
        private void RequiredItemsShouldBe(RequiredItemMaterials req, int qp, int grailCount, int fourStarEmber, 
            int fourStarEmberClassBonus, int fiveStarEmber, int fiveStarEmberClassBonus)
        {
            req.Qp.Should().Be(qp);
            req.GrailCount.Should().Be(grailCount);
            req.FourStarEmber.Should().Be(fourStarEmber);
            req.FourStarEmberClassBonus.Should().Be(fourStarEmberClassBonus);
            req.FiveStarEmber.Should().Be(fiveStarEmber);
            req.FiveStarEmberClassBonus.Should().Be(fiveStarEmberClassBonus);
        }

        private void MaterialOutput(RequiredItemMaterials req, Dictionary<string, int> materials)
        {
            _output.WriteLine($"QP: {req.Qp:n0}");
            _output.WriteLine($"Grail count: {req.GrailCount}\n");
            _output.WriteLine($"4* Ember: {req.FourStarEmber:n0}");
            _output.WriteLine($"4* Ember (Class Bonus): {req.FourStarEmberClassBonus:n0}");
            _output.WriteLine($"5* Ember: {req.FiveStarEmber:n0}");
            _output.WriteLine($"5* Ember (Class Bonus): {req.FiveStarEmberClassBonus:n0}\n");

            foreach (KeyValuePair<string, int> material in materials.OrderBy(m => m.Key))
            {
                _output.WriteLine($"Name: {material.Key}");
                _output.WriteLine($"Amount: {material.Value}\n");
            }
        }
        #endregion
    }
}
