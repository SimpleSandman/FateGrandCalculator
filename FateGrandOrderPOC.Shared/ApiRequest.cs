using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

using Newtonsoft.Json;

using RestSharp;

namespace FateGrandOrderPOC.Shared
{
    public class ApiRequest
    {
        public static async Task<T> GetDesearlizeObjectAsync<T>(string basicUrl)
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

                    return JsonConvert.DeserializeObject<T>(response.Content);
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

        public static async Task<IRestResponse> GetResponseAsync(string basicUrl)
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
                    return await client.ExecuteAsync(request, cancellationToken.Token);
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
    }
}
