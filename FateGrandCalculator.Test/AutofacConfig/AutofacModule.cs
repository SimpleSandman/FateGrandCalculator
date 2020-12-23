using Autofac;

using FateGrandCalculator.AtlasAcademy;
using FateGrandCalculator.Core.Combat;

namespace FateGrandCalculator.Test.AutofacConfig
{
    public class AutofacModule : Module
    {
        public Autofac.Core.Parameter BaseApiUrl { get; set; }
        public Autofac.Core.Parameter Region { get; set; }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AtlasAcademyClient>()
                .WithParameter(BaseApiUrl)
                .WithParameter(Region);

            builder.RegisterType<CombatFormula>();
            builder.RegisterType<ServantSkillActivation>();
        }
    }
}
