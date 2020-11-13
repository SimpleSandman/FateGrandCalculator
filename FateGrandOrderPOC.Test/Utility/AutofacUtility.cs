using Autofac;

using FateGrandOrderPOC.Shared;
using FateGrandOrderPOC.Shared.AtlasAcademy;

namespace FateGrandOrderPOC.Test.Utility
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

    public sealed class ScopedClasses
    {
        public AtlasAcademyClient AtlasAcademyClient { get; set; }
        public CombatFormula CombatFormula { get; set; }
        public ServantSkillActivation ServantSkillActivation { get; set; }
    }
}
