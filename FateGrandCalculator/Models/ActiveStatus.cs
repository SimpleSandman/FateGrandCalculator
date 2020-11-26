using FateGrandCalculator.AtlasAcademy.Json;

namespace FateGrandCalculator.Models
{
    public class ActiveStatus
    {
        public Function StatusEffect { get; set; }
        public int AppliedSkillLevel { get; set; }
        public int ActiveTurnCount { get; set; } = -1;
    }
}
