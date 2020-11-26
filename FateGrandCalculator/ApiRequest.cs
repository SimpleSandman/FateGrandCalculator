﻿using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

using Newtonsoft.Json;

using RestSharp;

namespace FateGrandCalculator
{
    public static class ApiRequest
    {
        public static async Task<T> GetDeserializeObjectAsync<T>(string basicUrl)
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
    }
}