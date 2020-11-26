using System.Collections.Generic;
using System.Threading.Tasks;

using FateGrandCalculator.AtlasAcademy.Json;

using Newtonsoft.Json.Linq;

namespace FateGrandCalculator.AtlasAcademy
{
    public class AtlasAcademyClient : IAtlasAcademyClient
    {
        private readonly string _baseApiUrl;
        private readonly string _region;

        public AtlasAcademyClient(string baseApiUrl, string region)
        {
            _baseApiUrl = baseApiUrl;
            _region = region;
        }

        public async Task<ServantNiceJson> GetServantInfo(string servantId)
        {
            return await ApiRequest.GetDeserializeObjectAsync<ServantNiceJson>($"{_baseApiUrl}/nice/{_region}/servant/{servantId}?lore=true&lang=en");
        }

        public async Task<EquipNiceJson> GetCraftEssenceInfo(string ceId)
        {
            return await ApiRequest.GetDeserializeObjectAsync<EquipNiceJson>($"{_baseApiUrl}/nice/{_region}/equip/{ceId}?lore=true&lang=en");
        }

        public async Task<ClassAttackRateNiceJson> GetClassAttackRateInfo()
        {
            return await ApiRequest.GetDeserializeObjectAsync<ClassAttackRateNiceJson>($"{_baseApiUrl}/export/{_region}/NiceClassAttackRate.json");
        }

        public async Task<ConstantNiceJson> GetConstantGameInfo()
        {
            return await ApiRequest.GetDeserializeObjectAsync<ConstantNiceJson>($"{_baseApiUrl}/export/{_region}/NiceConstant.json");
        }

        public async Task<List<ServantBasicJson>> GetListBasicServantInfo()
        {
            return await ApiRequest.GetDeserializeObjectAsync<List<ServantBasicJson>>($"{_baseApiUrl}/export/{_region}/basic_servant.json");
        }

        public async Task<MysticCodeNiceJson> GetMysticCodeInfo(string mcId)
        {
            return await ApiRequest.GetDeserializeObjectAsync<MysticCodeNiceJson>($"{_baseApiUrl}/nice/{_region}/MC/{mcId}");
        }

        public async Task<JObject> GetTraitEnumInfo()
        {
            return await ApiRequest.GetDeserializeObjectAsync<JObject>($"{_baseApiUrl}/export/JP/nice_trait.json"); // same for both NA and JP
        }
    }
}
