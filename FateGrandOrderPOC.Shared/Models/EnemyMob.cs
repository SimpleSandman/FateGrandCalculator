using System.Collections.Generic;

using FateGrandOrderPOC.Shared.Enums;

namespace FateGrandOrderPOC.Shared.Models
{
    public class EnemyMob
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Health { get; set; }
        /// <summary>
        /// If undead (1.2x additional class multipler)
        /// </summary>
        public bool IsSpecial { get; set; }
        public List<string> Traits { get; set; }
        public List<ActiveStatus> ActiveStatuses { get; set; } = new List<ActiveStatus>();
        public GenderRelationEnum Gender { get; set; }
        public AttributeRelationEnum AttributeName { get; set; }
        public ClassRelationEnum ClassName { get; set; }
        /// <summary>
        /// The wave number an enemy is assigned to
        /// </summary>
        public WaveNumberEnum WaveNumber { get; set; }
        /// <summary>
        /// The placement where the enemy spawns on the field (0 = left, 1 = middle, 2 = right)
        /// </summary>
        public WavePlacementEnum WavePlacement { get; set; }
    }
}
