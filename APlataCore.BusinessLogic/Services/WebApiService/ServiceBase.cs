using APlataCore.Domain.Infrastructure;
using APlataCore.Domain.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace APlataCore.BusinessLogic.Services.WebApiService
{
    public abstract class ServiceBase
    {      
        protected string GetStoragePath()
        {
            return "http://178.57.218.210:98/";
        }
        public async Task<TokensInfo> GetTokenAsync(string login, string password)
        {
            string tokenUri = GetStoragePath() + "token?login=" + login + "&password=" + password;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(tokenUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(tokenUri);
                TokensInfo tokens = new TokensInfo();

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();

                    tokens = JsonConvert.DeserializeObject<TokensInfo>(json);
                    return tokens;
                }

                return tokens;
            }
        }
    }
}
