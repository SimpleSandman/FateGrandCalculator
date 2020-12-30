using System.Collections.Generic;

namespace FateGrandCalculator.Models
{
    public class MasterChaldeaInfo
    {
        public string Region { get; set; }
        public int AccountLevel { get; set; }
        public List<ChaldeaServant> ChaldeaServants { get; set; }
        public List<CraftEssence> CraftEssences { get; set; }
        public List<MysticCode> MysticCodes { get; set; }
    }
}
