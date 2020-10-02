using System.Collections.Generic;
using System.Threading.Tasks;

using FateGrandOrderPOC.Shared.AtlasAcademy.Json;

namespace FateGrandOrderPOC.Shared.AtlasAcademy
{
    public interface IAtlasAcademyClient
    {
        Task<ServantNiceJson> GetServantInfo(string servantId);

        Task<EquipNiceJson> GetCraftEssenceInfo(string ceId);

        Task<ClassAttackRateNiceJson> GetClassAttackRateInfo();

        Task<ConstantNiceJson> GetConstantGameInfo();
        
        Task<List<ServantBasicJson>> GetListBasicServantInfo();
    }
}
