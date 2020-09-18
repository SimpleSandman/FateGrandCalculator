using FateGrandOrderPOC.Shared.AtlasAcademy.Json;
using System;
using System.Collections.Generic;

namespace FateGrandOrderPOC.Shared.Models
{
    public class Servant
    {
        public int ServantLevel { get; set; }
        public int[] SkillLevels { get; set; }
        public int NpLevel { get; set; }
        public int FouAttack { get; set; }
        public int FouHealth { get; set; }
        public bool IsSupportServant { get; set; }
        public ServantNiceJson ServantInfo { get; set; }
    }
}
