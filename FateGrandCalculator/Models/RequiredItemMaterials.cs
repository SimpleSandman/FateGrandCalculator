using System.Collections.Generic;

using FateGrandCalculator.AtlasAcademy.Json;

namespace FateGrandCalculator.Models
{
    public class RequiredItemMaterials
    {
        public int Qp { get; set; }
        public int FourStarEmber { get; set; }
        public int FourStarEmberClassBonus { get; set; }
        public int FiveStarEmber { get; set; }
        public int FiveStarEmberClassBonus { get; set; }
        public List<ItemParent> Items { get; set; } = new List<ItemParent>();
    }
}
