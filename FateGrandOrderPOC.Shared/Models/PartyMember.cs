using System.Collections.Generic;

using FateGrandOrderPOC.Shared.AtlasAcademy.Json;
using FateGrandOrderPOC.Shared.Enums;

namespace FateGrandOrderPOC.Shared.Models
{
    public class PartyMember
    {
        public int Id { get; set; }
        public Servant Servant { get; set; }
        public CraftEssence EquippedCraftEssence { get; set; }
        public int TotalAttack { get; set; }
        public int TotalHealth { get; set; }
        /// <summary>
        /// Current noble phantasm bar's charge with a range from 0% to 500%. Needs a minimum of 100% in order to execute the NP
        /// </summary>
        public float NpCharge { get; set; }
        public NpChainOrderEnum NpChainOrder { get; set; } = NpChainOrderEnum.None;
        public NoblePhantasm NoblePhantasm { get; set; }
        public List<ActiveStatus> ActiveStatuses { get; set; } = new List<ActiveStatus>();
        public List<SkillCooldown> SkillCooldowns { get; set; } = new List<SkillCooldown>();
    }
}
