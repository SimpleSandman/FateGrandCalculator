using FateGrandOrderPOC.Shared.AtlasAcademyJson;

namespace FateGrandOrderPOC.Shared.Models
{
    public class ChaldeaServant
    {
        public int Level { get; set; }
        public int SkillLevel1 { get; set; }
        public int SkillLevel2 { get; set; }
        public int SkillLevel3 { get; set; }
        public int Np { get; set; }
        public int FouAttack { get; set; }
        public int FouHealth { get; set; }
        public ServantNiceJson ServantInfo { get; set; }
    }
}
