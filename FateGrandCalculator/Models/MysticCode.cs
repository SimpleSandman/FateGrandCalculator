using System.Collections.Generic;

using FateGrandCalculator.AtlasAcademy.Json;

namespace FateGrandCalculator.Models
{
    public class MysticCode
    {
        public int MysticCodeLevel { get; set; }
        public List<SkillCooldown> SkillCooldowns { get; set; } = new List<SkillCooldown>();
        public MysticCodeNiceJson MysticCodeInfo { get; set; }
    }
}
