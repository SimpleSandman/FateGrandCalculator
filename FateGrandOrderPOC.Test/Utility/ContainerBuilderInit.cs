using Autofac;

using FateGrandOrderPOC.Shared.AtlasAcademy;
using FateGrandOrderPOC.Test.CoreModule;
using FateGrandOrderPOC.Test.Fixture;

namespace FateGrandOrderPOC.Test.Utility
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
                AtlasAcademyClient = new NamedParameter("client", new AtlasAcademyClient(WireMockFixture.ServerUrl, region)),
                Region = new NamedParameter("region", region)
            });

            return builder.Build();
        }
    }
}
