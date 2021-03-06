﻿using Autofac;

using FateGrandCalculator.AtlasAcademy;
using FateGrandCalculator.Core.Combat;
using FateGrandCalculator.Core.Management;

namespace FateGrandCalculator.Test.Utility.AutofacConfig
{
    public class AutofacModule : Module
    {
        public Autofac.Core.Parameter AtlasAcademyClient { get; set; }
        public Autofac.Core.Parameter BaseApiUrl { get; set; }
        public Autofac.Core.Parameter ChaldeaFileLocation { get; set; }
        public Autofac.Core.Parameter Region { get; set; }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AtlasAcademyClient>()
                .WithParameter(BaseApiUrl)
                .WithParameter(Region);

            builder.RegisterType<ChaldeaIO>()
                .WithParameter(ChaldeaFileLocation);

            builder.RegisterType<CombatFormula>()
                .WithParameter(AtlasAcademyClient);

            builder.RegisterType<MaterialCalculation>();
            builder.RegisterType<ServantSkillActivation>();
        }
    }
}
