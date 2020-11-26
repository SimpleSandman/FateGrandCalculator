using System.Threading.Tasks;

namespace FateGrandOrderPOC.AtlasAcademy
{
    public interface IBaseRelation
    {
        Task<float> GetAttackMultiplier(string attack);
        Task<float> GetAttackMultiplier(string attack, string defend);
    }
}
