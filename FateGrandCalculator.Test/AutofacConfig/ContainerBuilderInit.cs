using Autofac;

using FateGrandCalculator.Test.Fixture;

namespace FateGrandCalculator.Test.AutofacConfig
{
    public static class ContainerBuilderInit
    {
        /// <summary>
        /// Create the container Autofac needs for Dependency Injection
        /// </summary>
        /// <param name="region">NA or JP</param>
        /// <returns></returns>
        public static IContainer Create(string region)
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterModule(new AutofacModule
            {
                BaseApiUrl = new NamedParameter("baseApiUrl", WireMockFixture.ServerUrl),
                Region = new NamedParameter("region", region)
            });

            return builder.Build();
        }
    }
}
