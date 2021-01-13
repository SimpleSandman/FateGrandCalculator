using Autofac;

using FateGrandCalculator.AtlasAcademy;
using FateGrandCalculator.Core.Combat;
using FateGrandCalculator.Core.Management;

namespace FateGrandCalculator.Test.Utility.AutofacConfig
{
    public static class AutofacUtility
    {
        public static ScopedClasses ResolveScope(ILifetimeScope scope)
        {
            return new ScopedClasses
            {
                AtlasAcademyClient = scope.Resolve<AtlasAcademyClient>(),
                ChaldeaIO = scope.Resolve<ChaldeaIO>(),
                CombatFormula = scope.Resolve<CombatFormula>(),
                MaterialCalculation = scope.Resolve<MaterialCalculation>(),
                ServantSkillActivation = scope.Resolve<ServantSkillActivation>()
            };
        }
    }
}
