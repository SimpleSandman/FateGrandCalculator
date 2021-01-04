using System.Collections.Generic;

using Autofac;

using FateGrandCalculator.Models;

using FateGrandCalculator.Test.Utility.Attributes;
using FateGrandCalculator.Test.Utility.AutofacConfig;

using FluentAssertions;
using FluentAssertions.Execution;

using Xunit;

namespace FateGrandCalculator.Test.Management
{
    [TestCaseOrderer("FateGrandCalculator.Test.Orderers.PriorityOrderer", "FateGrandCalculator.Test")]
    public class ChaldeaAccountDataTest
    {
        [Theory, TestPriority(0)]
        [InlineData("NA")]
        [InlineData("JP")]
        public void SaveDataTest(string region)
        {
            IContainer container = ContainerBuilderInit.Create(region);

            MasterChaldeaInfo masterChaldeaInfo = new MasterChaldeaInfo
            {
                Region = region,
                Username = "simple_sandman",
                AccountLevel = 150,
                FriendCode = "999,999,999",
                ChaldeaServants = new List<ChaldeaServant>(),
                CraftEssences = new List<CraftEssence>(),
                MysticCodes = new List<MysticCode>()
            };

            using (var scope = container.BeginLifetimeScope())
            {
                ScopedClasses resolvedClasses = AutofacUtility.ResolveScope(scope);
                resolvedClasses.ChaldeaIO.Save(masterChaldeaInfo);
            }
        }

        [Theory, TestPriority(1)]
        [InlineData("NA")]
        [InlineData("JP")]
        public void LoadDataTest(string region)
        {
            IContainer container = ContainerBuilderInit.Create(region);

            using (var scope = container.BeginLifetimeScope())
            {
                ScopedClasses resolvedClasses = AutofacUtility.ResolveScope(scope);
                MasterChaldeaInfo masterChaldeaInfo = resolvedClasses.ChaldeaIO.Load();

                using (new AssertionScope())
                {
                    masterChaldeaInfo.Region.Should().Be(region);
                    masterChaldeaInfo.Username.Should().Be("simple_sandman");
                    masterChaldeaInfo.AccountLevel.Should().Be(150);
                    masterChaldeaInfo.FriendCode.Should().Be("999,999,999");
                }
            }
        }
    }
}
