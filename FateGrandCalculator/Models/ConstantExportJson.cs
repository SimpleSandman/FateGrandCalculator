using FateGrandCalculator.AtlasAcademy.Calculations;

using Newtonsoft.Json.Linq;

namespace FateGrandCalculator.Models
{
    public class ConstantExportJson
    {
        public AttributeRelation AttributeRelation { get; set; }
        public ClassRelation ClassRelation { get; set; }
        public ClassAttackRate ClassAttackRate { get; set; }
        public ConstantRate ConstantRate { get; set; }
        public JObject TraitEnumInfo { get; set; }
    }
}
