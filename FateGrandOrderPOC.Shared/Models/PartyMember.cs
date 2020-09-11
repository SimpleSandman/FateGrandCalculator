﻿using System.Collections.Generic;

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
        public float NpCharge { get; set; }
        public NpChainOrderEnum NpChainOrder { get; set; } = NpChainOrderEnum.None;
        public NoblePhantasm NoblePhantasm { get; set; }
        public float AttackUp { get; set; }
        /// <summary>
        /// How much NP we can generate in general
        /// </summary>
        public float NpGainUp { get; set; }
        /// <summary>
        /// How much card type effectiveness has been applied (based on NP card type)
        /// </summary>
        public float TypeUp { get; set; }
        /// <summary>
        /// Misc power modifiers added on
        /// </summary>
        public float PowerModifier { get; set; }
        public List<BuffServant> ActiveBuffs { get; set; }
        public List<BuffServant> ActiveDebuffs { get; set; }
    }
}
