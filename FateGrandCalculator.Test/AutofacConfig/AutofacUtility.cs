﻿using Autofac;

using FateGrandCalculator.AtlasAcademy;
using FateGrandCalculator.Core.Combat;

namespace FateGrandCalculator.Test.AutofacConfig
{
    public static class AutofacUtility
    {
        public static ScopedClasses ResolveScope(ILifetimeScope scope)
        {
            return new ScopedClasses
            {
                AtlasAcademyClient = scope.Resolve<AtlasAcademyClient>(),
                CombatFormula = scope.Resolve<CombatFormula>(),
                ServantSkillActivation = scope.Resolve<ServantSkillActivation>()
            };
        }
    }
}
