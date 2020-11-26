using Autofac;

using FateGrandCalculator;
using FateGrandCalculator.AtlasAcademy;

namespace FateGrandCalculator.Test.CoreModule
{
    public class AutofacModule : Module
    {
        public Autofac.Core.Parameter BaseApiUrl { get; set; }
        public Autofac.Core.Parameter AtlasAcademyClient { get; set; }
        public Autofac.Core.Parameter Region { get; set; }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AtlasAcademyClient>()
                .WithParameter(BaseApiUrl)
                .WithParameter(Region);

            builder.RegisterType<CombatFormula>()
                .WithParameter(AtlasAcademyClient);

            builder.RegisterType<ServantSkillActivation>();
        }
    }
}
