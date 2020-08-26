using System.Collections.Generic;

using FateGrandOrderPOC.Shared.Enums;

namespace FateGrandOrderPOC.Shared.Models
{
    public class EnemyMob
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public bool IsSpecial { get; set; } // if undead (1.2x additional class multipler)
        public List<string> Traits { get; set; }
        public GenderRelationEnum Gender { get; set; }
        public AttributeRelationEnum AttributeName { get; set; }
        public ClassRelationEnum ClassName { get; set; }
    }
}
