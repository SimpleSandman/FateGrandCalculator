using System.Collections.Generic;
using System.Threading.Tasks;

using FateGrandOrderPOC.Shared.AtlasAcademy.Json;

using Newtonsoft.Json.Linq;

namespace FateGrandOrderPOC.Shared.AtlasAcademy
{
    public class AtlasAcademyClient : IAtlasAcademyClient
    {
        private readonly string _baseApiUrl;

        public AtlasAcademyClient(string baseApiUrl)
        {
            _baseApiUrl = baseApiUrl;
        }

        public async Task<ServantNiceJson> GetServantInfo(string servantId)
        {
            return await ApiRequest.GetDeserializeObjectAsync<ServantNiceJson>(_baseApiUrl + "/nice/NA/servant/" + servantId + "?lang=en");
        }

        public async Task<EquipNiceJson> GetCraftEssenceInfo(string ceId)
        {
            return await ApiRequest.GetDeserializeObjectAsync<EquipNiceJson>(_baseApiUrl + "/nice/NA/equip/" + ceId + "?lore=true&lang=en");
        }

        public async Task<ClassAttackRateNiceJson> GetClassAttackRateInfo()
        {
            return await ApiRequest.GetDeserializeObjectAsync<ClassAttackRateNiceJson>(_baseApiUrl + "/export/NA/NiceClassAttackRate.json");
        }

        public async Task<ConstantNiceJson> GetConstantGameInfo()
        {
            return await ApiRequest.GetDeserializeObjectAsync<ConstantNiceJson>(_baseApiUrl + "/export/NA/NiceConstant.json");
        }

        public async Task<List<ServantBasicJson>> GetListBasicServantInfo()
        {
            return await ApiRequest.GetDeserializeObjectAsync<List<ServantBasicJson>>(_baseApiUrl + "/export/NA/basic_servant.json");
        }

        public async Task<MysticCodeNiceJson> GetMysticCodeInfo(string mcId)
        {
            return await ApiRequest.GetDeserializeObjectAsync<MysticCodeNiceJson>(_baseApiUrl + "/nice/NA/MC/" + mcId);
        }

        public async Task<JObject> GetTraitEnumInfo()
        {
            return await ApiRequest.GetDeserializeObjectAsync<JObject>(_baseApiUrl + "/export/JP/nice_trait.json"); // same for both NA and JP
        }
    }
}
