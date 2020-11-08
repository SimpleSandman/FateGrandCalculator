using Autofac;

using FateGrandOrderPOC.Shared.AtlasAcademy;

namespace FateGrandOrderPOC.Test
{
    public class AutofacModule : Module
    {
        public Autofac.Core.Parameter BaseApiUrl { get; set; }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AtlasAcademyClient>().WithParameter(BaseApiUrl);
        }
    }
}
