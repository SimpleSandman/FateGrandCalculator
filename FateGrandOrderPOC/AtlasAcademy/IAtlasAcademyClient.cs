using System.Collections.Generic;
using System.Threading.Tasks;

using FateGrandOrderPOC.AtlasAcademy.Json;

using Newtonsoft.Json.Linq;

namespace FateGrandOrderPOC.AtlasAcademy
{
    public interface IAtlasAcademyClient
    {
        Task<ServantNiceJson> GetServantInfo(string servantId);
        Task<EquipNiceJson> GetCraftEssenceInfo(string ceId);
        Task<ClassAttackRateNiceJson> GetClassAttackRateInfo();
        Task<ConstantNiceJson> GetConstantGameInfo();        
        Task<List<ServantBasicJson>> GetListBasicServantInfo();
        Task<MysticCodeNiceJson> GetMysticCodeInfo(string mcId);
        Task<JObject> GetTraitEnumInfo();
    }
}
