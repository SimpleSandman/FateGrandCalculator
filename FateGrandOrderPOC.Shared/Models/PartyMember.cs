using System.Collections.Generic;

using FateGrandOrderPOC.Shared.AtlasAcademyJson;

namespace FateGrandOrderPOC.Shared.Models
{
    public class PartyMember
    {
        public ChaldeaServant Servant { get; set; }
        public ChaldeaCraftEssence EquippedCraftEssence { get; set; }
        public int TotalAttack { get; set; }
        public int TotalHealth { get; set; }
        public int NpCharge { get; set; }
        public List<BuffServant> ActiveBuffs { get; set; }
        public List<BuffServant> ActiveDebuffs { get; set; }
    }
}
