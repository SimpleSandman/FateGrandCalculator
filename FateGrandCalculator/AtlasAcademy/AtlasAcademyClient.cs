using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

using FateGrandCalculator.AtlasAcademy.Interfaces;
using FateGrandCalculator.AtlasAcademy.Json;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using RestSharp;

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
            return await GetDeserializeObjectAsync<ServantNiceJson>($"{_baseApiUrl}/nice/{_region}/servant/{servantId}?lore=true&lang=en");
        }

        public async Task<EquipNiceJson> GetCraftEssenceInfo(string ceId)
        {
            return await GetDeserializeObjectAsync<EquipNiceJson>($"{_baseApiUrl}/nice/{_region}/equip/{ceId}?lore=true&lang=en");
        }

        public async Task<ClassAttackRateNiceJson> GetClassAttackRateInfo()
        {
            return await GetDeserializeObjectAsync<ClassAttackRateNiceJson>($"{_baseApiUrl}/export/{_region}/NiceClassAttackRate.json");
        }

        public async Task<ConstantNiceJson> GetConstantGameInfo()
        {
            return await GetDeserializeObjectAsync<ConstantNiceJson>($"{_baseApiUrl}/export/{_region}/NiceConstant.json");
        }

        public async Task<List<ServantBasicJson>> GetListBasicServantInfo()
        {
            return await GetDeserializeObjectAsync<List<ServantBasicJson>>($"{_baseApiUrl}/export/{_region}/basic_servant.json");
        }

        public async Task<MysticCodeNiceJson> GetMysticCodeInfo(string mcId)
        {
            return await GetDeserializeObjectAsync<MysticCodeNiceJson>($"{_baseApiUrl}/nice/{_region}/MC/{mcId}");
        }

        public async Task<JObject> GetTraitEnumInfo()
        {
            return await GetDeserializeObjectAsync<JObject>($"{_baseApiUrl}/export/JP/nice_trait.json"); // same for both NA and JP
        }

        public async Task<AttributeRelationNiceJson> GetAttributeRelationInfo()
        {
            return await GetDeserializeObjectAsync<AttributeRelationNiceJson>($"{_baseApiUrl}/export/{_region}/NiceAttributeRelation.json");
        }

        public async Task<ClassRelationNiceJson> GetClassRelationInfo()
        {
            return await GetDeserializeObjectAsync<ClassRelationNiceJson>($"{_baseApiUrl}/export/{_region}/NiceClassRelation.json");
        }

        public async Task<ServantNiceJson> GetEnemyCollectionServantInfo(string servantId)
        {
            return await GetDeserializeObjectAsync<ServantNiceJson>($"{_baseApiUrl}/nice/{_region}/svt/{servantId}");
        }

        #region Private Method
        private async Task<T> GetDeserializeObjectAsync<T>(string basicUrl)
        {
            try
            {
                RestClient client = new RestClient(basicUrl);
                RestRequest request = new RestRequest(Method.GET);
                request.AddHeader("Cache-Control", "no-cache");
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Accept", "application/json");

                var cancellationToken = new CancellationTokenSource();

                try
                {
                    IRestResponse<T> response = await client.ExecuteAsync<T>(request, cancellationToken.Token);
                    if (response.IsSuccessful)
                    {
                        return JsonConvert.DeserializeObject<T>(response.Content);
                    }
                    else
                    {
                        string message = $"The client threw an exception with the status code \"{response.StatusCode}\"";

                        if (!string.IsNullOrEmpty(response.Content))
                        {
                            message += $". The contents of the response is as follows, \"{response.Content}\"";
                        }

                        if (response.ErrorException != null)
                        {
                            throw new ApplicationException(message, response.ErrorException);
                        }

                        throw new ApplicationException(message);
                    }
                }
                catch (WebException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex.Message);
            }

            return default;
        }
        #endregion
    }
}
