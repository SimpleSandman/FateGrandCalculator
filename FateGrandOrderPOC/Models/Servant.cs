using FateGrandOrderPOC.AtlasAcademy.Json;

namespace FateGrandOrderPOC.Models
{
    public class Servant
    {
        public int ServantLevel { get; set; }
        /// <summary>
        /// List of the three skill levels ordered from left to right { 10, 10, 10 }
        /// </summary>
        public int[] SkillLevels { get; set; }
        public int NpLevel { get; set; }
        public int FouAttack { get; set; }
        public int FouHealth { get; set; }
        public bool IsSupportServant { get; set; }
        public ServantNiceJson ServantInfo { get; set; }
    }
}
