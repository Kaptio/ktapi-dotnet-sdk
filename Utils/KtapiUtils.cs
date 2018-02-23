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

    public static class KtapiUtils
    {
        public async static Task<JArray> GetPackagesFromKTAPI(string key, string secret)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("Authorization", $"Keypair key=\"{key}\" secret=\"{secret}\"");
            var streamTask = client.GetStringAsync("https://ktapi-staging.herokuapp.com/v1.0/packages");
            var packagesList = JArray.Parse(await streamTask);
            return packagesList;
        }

        public async static Task<JObject> GetPackageDetailsFromKTAPI(string packageId, string key, string secret)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("Authorization", $"Keypair key=\"{key}\" secret=\"{secret}\"");
            var streamTask = client.GetStringAsync($"https://ktapi-staging.herokuapp.com/v1.0/packages/{packageId}");
            var package = JObject.Parse(await streamTask);
            return package;
        }

    }

}
