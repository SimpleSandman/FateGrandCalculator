using System;
using System.Collections.Generic;

using FateGrandOrderPOC.Shared.AtlasAcademy.Json;

namespace FateGrandOrderPOC.Shared.AtlasAcademy
{
    public class AtlasAcademyClient : IAtlasAcademyClient
    {

        private Func<string> baseUrl;

        public AtlasAcademyClient(Func<string> config)
        {
            this.baseUrl = config;
        }

        public ServantNiceJson GetServantInfo(string servantId)
        {
            return ApiRequest.GetDeserializeObjectAsync<ServantNiceJson>(baseUrl() + "/nice/NA/servant/" + servantId + "?lang=en").Result;
        }

        public EquipNiceJson GetCraftEssenceInfo(string ceId)
        {
            return ApiRequest.GetDeserializeObjectAsync<EquipNiceJson>(baseUrl() + "/nice/NA/equip/" + ceId + "?lore=true&lang=en").Result;
        }

        public ClassAttackRateNiceJson GetClassAttackRateInfo()
        {
            return ApiRequest.GetDeserializeObjectAsync<ClassAttackRateNiceJson>(baseUrl() + "/export/NA/NiceClassAttackRate.json").Result;
        }

        public ConstantNiceJson GetConstantGameInfo()
        {
            return ApiRequest.GetDeserializeObjectAsync<ConstantNiceJson>(baseUrl() + "/export/NA/NiceConstant.json").Result;
        }

        public List<ServantBasicJson> GetListBasicServantInfo()
        {
            return ApiRequest.GetDeserializeObjectAsync<List<ServantBasicJson>>(baseUrl() + "/export/NA/basic_servant.json").Result;
        }
    }
}
