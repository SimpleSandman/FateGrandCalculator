using FateGrandOrderPOC.Shared.AtlasAcademy.Json;

namespace FateGrandOrderPOC.Shared.Models
{
    public class ActiveStatus
    {
        public FunctionServant StatusEffect { get; set; }
        public int AppliedSkillLevel { get; set; }
        public int ActiveTurnCount { get; set; } = -1;
    }
}
