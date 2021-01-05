using System.Threading.Tasks;

using FateGrandCalculator.AtlasAcademy.Json;

using Newtonsoft.Json.Linq;

namespace FateGrandCalculator.AtlasAcademy.Interfaces
{
    public interface IAtlasAcademyClient
    {
        Task<ServantNiceJson> GetServantInfo(string servantId);
        Task<EquipNiceJson> GetCraftEssenceInfo(string ceId);
        Task<ClassAttackRateNiceJson> GetClassAttackRateInfo();
        Task<ConstantNiceJson> GetConstantGameInfo();
        Task<EquipBasicJsonCollection> GetListBasicEquipInfo();
        Task<ServantBasicJsonCollection> GetListBasicServantInfo();
        Task<MysticCodeNiceJson> GetMysticCodeInfo(string mcId);
        Task<JObject> GetTraitEnumInfo();
        Task<AttributeRelationNiceJson> GetAttributeRelationInfo();
        Task<ClassRelationNiceJson> GetClassRelationInfo();
        Task<ServantNiceJson> GetEnemyCollectionServantInfo(string servantId);
    }
}
