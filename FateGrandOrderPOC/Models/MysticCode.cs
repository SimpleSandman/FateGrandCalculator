using System.Collections.Generic;

using FateGrandOrderPOC.AtlasAcademy.Json;

namespace FateGrandOrderPOC.Models
{
    public class MysticCode
    {
        public int MysticCodeLevel { get; set; }
        public List<SkillCooldown> SkillCooldowns { get; set; } = new List<SkillCooldown>();
        public MysticCodeNiceJson MysticCodeInfo { get; set; }
    }
}
