using Microsoft.Extensions.Logging;
using ScriptureFeed.Web.Interfaces;
using ScriptureFeed.Web.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ScriptureFeed.Web.Services
{
    public class EmfApiService : IEmfApiService
    {
        private readonly ILogger<EmfApiService> Logger;
        private readonly EmfApiServiceSettings Settings;
        private readonly HttpClient HttpClient;

        public EmfApiService(ILogger<EmfApiService> logger, EmfApiServiceSettings settings)
        {
            Logger = logger;
            Settings = settings;
            HttpClient = new HttpClient();
            HttpClient.DefaultRequestHeaders.Add(Settings.ServiceKeyName, Settings.ServiceKeyValue);
            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<GetVerseResponse> GetBibleVerse(DateTime startDate, int pageSize)
        {
            HttpClient.BaseAddress = new Uri(Settings.BaseUrl);
            List<KeyValuePair<string, string>> paramsList = new()
            {
                new KeyValuePair<string, string>(Settings.SiteIdKey, Settings.SiteIdValue),
                new KeyValuePair<string, string>("startdate", startDate.ToString("yyyy-MM-dd")),
                new KeyValuePair<string, string>("pagesize", pageSize.ToString())
            };
            var paramsString = BuildQueryParameterString(paramsList);

            HttpResponseMessage response = await HttpClient.GetAsync($"{Settings.VerseByDateString}?{paramsString}").ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            GetVerseResponse verseResponse = JsonSerializer.Deserialize<GetVerseResponse>(responseBody);
            return verseResponse;
        }

        private static string BuildQueryParameterString(List<KeyValuePair<string, string>> paramsList)
        {
            StringBuilder paramString = new("");
            int listSize = paramsList.Count;
            if(listSize > 0)
            {
                for (int i = 0; i < paramsList.Count; i++)
                {
                    var param = paramsList[i];
                    if(i != 0)
                        paramString.Append("&");
                    paramString.Append($"{param.Key}={param.Value}");
                }
            }
            return paramString.ToString();
        }
    }
}
