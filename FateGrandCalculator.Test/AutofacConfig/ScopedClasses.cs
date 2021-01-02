using FateGrandCalculator.AtlasAcademy;
using FateGrandCalculator.Core.Combat;
using FateGrandCalculator.Core.Management;

namespace FateGrandCalculator.Test.AutofacConfig
{
    public sealed class ScopedClasses
    {
        public AtlasAcademyClient AtlasAcademyClient { get; set; }
        public ChaldeaIO ChaldeaIO { get; set; }
        public CombatFormula CombatFormula { get; set; }
        public ServantSkillActivation ServantSkillActivation { get; set; }
    }
}
