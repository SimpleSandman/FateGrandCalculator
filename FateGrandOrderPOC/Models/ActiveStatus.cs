using FateGrandOrderPOC.AtlasAcademy.Json;

namespace FateGrandOrderPOC.Models
{
    public class ActiveStatus
    {
        public Function StatusEffect { get; set; }
        public int AppliedSkillLevel { get; set; }
        public int ActiveTurnCount { get; set; } = -1;
    }
}
