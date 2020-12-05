using System.Collections.Generic;

using FateGrandCalculator.Enums;

namespace FateGrandCalculator.Models
{
    public class EnemyMob
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Health { get; set; }
        public List<string> Traits { get; set; }
        public List<ActiveStatus> ActiveStatuses { get; set; } = new List<ActiveStatus>();
        public AttributeRelationEnum AttributeName { get; set; }
        public ClassRelationEnum ClassName { get; set; }
        /// <summary>
        /// The wave number an enemy is assigned to
        /// </summary>
        public WaveNumberEnum WaveNumber { get; set; }
    }
}
