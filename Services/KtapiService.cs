using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Client2.Models;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace Client2.Utils
{

    public static class KtapiService
    {

        public async static Task<JArray> GetPackagesFromKTAPI(string key, string secret, string endpoint)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("Authorization", $"Keypair key=\"{key}\" secret=\"{secret}\"");
            var streamTask = client.GetStringAsync($"{endpoint}/v1.0/packages");
            var packagesList = JArray.Parse(await streamTask);
            return packagesList;
        }

        public async static Task<JObject> GetPackageDetailsFromKTAPI(string packageId, string key, string secret, string endpoint)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("Authorization", $"Keypair key=\"{key}\" secret=\"{secret}\"");
            var streamTask = client.GetStringAsync($"{endpoint}/v1.0/packages/{packageId}");
            var package = JObject.Parse(await streamTask);
            return package;
        }

        public async static Task<JObject> GetPackagePricesFromKTAPI(string packageId, object data, string key, string secret, string endpoint)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("Authorization", $"Keypair key=\"{key}\" secret=\"{secret}\"");
            var response = await client.PostAsync(
                    $"{endpoint}/v1.0/packages/{packageId}/prices",
                    new StringContent(data.ToString()));
            string content = await response.Content.ReadAsStringAsync();
            var prices = JObject.Parse(content);
            return prices;
        }

        public async static Task<JObject> GetPackagePricesWithAlternativesFromKTAPI(string packageId, object data, string key, string secret, string endpoint)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("Authorization", $"Keypair key=\"{key}\" secret=\"{secret}\"");
            var response = await client.PostAsync(
                    $"{endpoint}/v2.0/packages/{packageId}/prices",
                    new StringContent(data.ToString()));
            string content = await response.Content.ReadAsStringAsync();
            var prices = JObject.Parse(content);
            return prices;
        }


    }

}
