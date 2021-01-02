using Autofac;

using FateGrandCalculator.Models;
using FateGrandCalculator.Test.Attributes;
using FateGrandCalculator.Test.AutofacConfig;

using FluentAssertions;

using Xunit;

namespace FateGrandCalculator.Test
{
    [TestCaseOrderer("FateGrandCalculator.Test.Orderers.PriorityOrderer", "FateGrandCalculator.Test")]
    public class ChaldeaAccountDataTest
    {
        const string REGION = "NA";

        private readonly IContainer _container;

        public ChaldeaAccountDataTest()
        {
            _container = ContainerBuilderInit.Create(REGION);
        }

        [Fact, TestPriority(0)]
        public void SaveDataTest()
        {
            MasterChaldeaInfo masterChaldeaInfo = new MasterChaldeaInfo
            {
                Region = REGION,
                Username = "simple_sandman",
                AccountLevel = 150,
                FriendCode = "999,999,999"
            };

            using (var scope = _container.BeginLifetimeScope())
            {
                ScopedClasses resolvedClasses = AutofacUtility.ResolveScope(scope);
                resolvedClasses.ChaldeaIO.Save(masterChaldeaInfo);
            }
        }

        [Fact, TestPriority(1)]
        public void LoadDataTest()
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                ScopedClasses resolvedClasses = AutofacUtility.ResolveScope(scope);
                MasterChaldeaInfo masterChaldeaInfo = resolvedClasses.ChaldeaIO.Load();

                masterChaldeaInfo.Username.Should().Be("simple_sandman");
            }
        }
    }
}
