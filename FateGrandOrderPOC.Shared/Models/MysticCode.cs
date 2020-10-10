using System.Collections.Generic;

using FateGrandOrderPOC.Shared.AtlasAcademy.Json;

namespace FateGrandOrderPOC.Shared.Models
{
    public class MysticCode
    {
        public int MysticCodeLevel { get; set; }
        public List<SkillCooldown> SkillCooldowns { get; set; }
        public MysticCodeNiceJson MysticCodeInfo { get; set; }
    }
}
