using FateGrandOrderPOC.Shared.AtlasAcademyJson;

namespace FateGrandOrderPOC.Shared.Models
{
    public sealed class EnemyServant : EnemyMob
    {
        public ServantNiceJson ServantInfo { get; private set; }
    }
}
