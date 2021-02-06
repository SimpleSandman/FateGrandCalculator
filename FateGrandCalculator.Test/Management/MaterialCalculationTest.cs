using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            _wireMockUtility = new WireMockUtility("JP");
            _container = ContainerBuilderInit.Create("JP");
        }

        [Fact]
        public async Task FreshWaverMaxLevelNoGrail()
        {
            _wireMockFixture.CheckIfMockServerInUse();
            _wireMockUtility.AddStubs(_wireMockFixture);

            using (var scope = _container.BeginLifetimeScope())
            {
                ScopedClasses resolvedClasses = AutofacUtility.ResolveScope(scope);
                ConstantExportJson constantExportJson = await FrequentlyUsed.GetConstantExportJsonAsync(resolvedClasses.AtlasAcademyClient);
                int servantId = 104500;
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

                _output.WriteLine($"QP: {req.Qp}");
                _output.WriteLine($"Grail Count: {req.GrailCount}\n");
                _output.WriteLine($"4* Ember: {req.FourStarEmber}");
                _output.WriteLine($"4* Ember (Class Bonus): {req.FourStarEmberClassBonus}");
                _output.WriteLine($"5* Ember: {req.FiveStarEmber}");
                _output.WriteLine($"5* Ember (Class Bonus): {req.FiveStarEmberClassBonus}");

                var groupedIds = req.Items.GroupBy(i => i.ItemObject.Id);

                //foreach (ItemParent itemParent in req.Items)
                //{
                //    _output.WriteLine($"Amount: {itemParent.Amount}");
                //}
            }
        }
    }
}
