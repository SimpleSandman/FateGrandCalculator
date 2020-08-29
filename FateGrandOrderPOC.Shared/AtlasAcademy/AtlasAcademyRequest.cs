using System.Collections.Generic;

using FateGrandOrderPOC.Shared.AtlasAcademy.Json;

namespace FateGrandOrderPOC.Shared.AtlasAcademy
{
    public static class AtlasAcademyRequest
    {
        public static ServantNiceJson GetServantInfo(string servantId)
        {
            return ApiRequest.GetDesearlizeObjectAsync<ServantNiceJson>("https://api.atlasacademy.io/nice/NA/servant/" + servantId + "?lang=en").Result;
        }

        public static EquipNiceJson GetCraftEssenceInfo(string ceId)
        {
            return ApiRequest.GetDesearlizeObjectAsync<EquipNiceJson>("https://api.atlasacademy.io/nice/NA/equip/" + ceId + "?lore=true&lang=en").Result;
        }

        public static ClassAttackRateNiceJson GetClassAttackRateInfo()
        {
            return ApiRequest.GetDesearlizeObjectAsync<ClassAttackRateNiceJson>("https://api.atlasacademy.io/export/NA/NiceClassAttackRate.json").Result;
        }

        public static ConstantNiceJson GetConstantGameInfo()
        {
            return ApiRequest.GetDesearlizeObjectAsync<ConstantNiceJson>("https://api.atlasacademy.io/export/NA/NiceConstant.json").Result;
        }

        public static List<ServantBasicJson> GetListBasicServantInfo()
        {
            return ApiRequest.GetDesearlizeObjectAsync<List<ServantBasicJson>>("https://api.atlasacademy.io/export/NA/basic_servant.json").Result;
        }
    }
}
