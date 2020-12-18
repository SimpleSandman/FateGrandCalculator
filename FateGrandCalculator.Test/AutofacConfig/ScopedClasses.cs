using FateGrandCalculator.AtlasAcademy;
using FateGrandCalculator.Core;

namespace FateGrandCalculator.Test.AutofacConfig
{
    public sealed class ScopedClasses
    {
        public AtlasAcademyClient AtlasAcademyClient { get; set; }
        public CombatFormula CombatFormula { get; set; }
        public ServantSkillActivation ServantSkillActivation { get; set; }
    }
}
