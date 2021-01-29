using APlataCore.BusinessLogic.Infrastructure;
using APlataCore.Domain.Infrastructure;
using APlataCore.Domain.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace APlataCore.BusinessLogic.Services.WebApiService
{
    interface IOwnersService : IBaseService<Owners> { }
    public class OwnersService : ServiceBase, IOwnersService
    {
        private string BaseUri { get; set; }
        private TokensInfo TokensInfo { get; set; }
        private HttpClient Client;

        public OwnersService()
        {
            BaseUri = GetStoragePath();
            TokensInfo = GetTokenAsync("demo", "demo1").Result;

            Client = new HttpClient();
            Client.BaseAddress = new Uri(BaseUri);
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public OwnersService(string login, string password)
        {
            BaseUri = GetStoragePath();
            TokensInfo = GetTokenAsync(login, password).Result;

            Client = new HttpClient();
            Client.BaseAddress = new Uri(BaseUri);
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<Owners>> GetAllAsync()
        {
            HttpResponseMessage responseMessage = await Client.GetAsync("owners?token=" + TokensInfo.Token);

            List<Owners> owners = null;

            if (responseMessage.IsSuccessStatusCode)
            {
                var json = await responseMessage.Content.ReadAsStringAsync();
                owners = JsonConvert.DeserializeObject<List<Owners>>(json);
            }

            return owners;
        }

        #region OwnersServices
        public async Task<Owners> GetAsync(int? id)
        {
            HttpResponseMessage responseMessage = await Client.GetAsync("owners/" + id.ToString() + "?token=" + TokensInfo.Token);

            Owners owners = null;

            if (responseMessage.IsSuccessStatusCode)
            {
                var json = await responseMessage.Content.ReadAsStringAsync();
                owners = JsonConvert.DeserializeObject<Owners>(json);
            }

            return owners;
        }
        public async Task<bool> CreateAsync(Owners owners)
        {
            try
            {
                var json = JsonConvert.SerializeObject(owners);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage postTask = await Client.PostAsync("owners?token=" + TokensInfo.Token, data);

                return postTask.IsSuccessStatusCode;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public async Task<bool> EditAsync(int? Id, Owners owners)
        {
            try
            {
                var json = JsonConvert.SerializeObject(owners);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage putTask = await Client.PutAsync("owners/" + Id.ToString() + "?token=" + TokensInfo.Token, data);

                //HttpResponseMessage putTask = await Client.PutAsJsonAsync<Owners>("api/owners/id=" + owners.Id.ToString(), owners);

                return putTask.IsSuccessStatusCode;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public async Task<bool> DeleteAsync(int? id)
        {
            try
            {
                HttpResponseMessage deleteTask = await Client.DeleteAsync("owners/" + id.ToString() + "?token=" + TokensInfo.Token);

                return deleteTask.IsSuccessStatusCode;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public async Task<List<Owners>> SearchQueryAsync(string query)
        {
            HttpResponseMessage responseMessage = await Client.GetAsync("owners?machine_name=" + query + "&token=" + TokensInfo.Token);

            List<Owners> owners = null;

            if (responseMessage.IsSuccessStatusCode)
            {
                var json = await responseMessage.Content.ReadAsStringAsync();
                owners = JsonConvert.DeserializeObject<List<Owners>>(json);
            }

            return owners;
        }
        public async Task<List<Owners>> SearchLinqAsync(string query)
        {
            List<Owners> owners = await GetAllAsync();

            return owners
                .Where(x =>
                x.Name.ToLower().Contains(query.ToLower()) ||
                x.Phone.ToString().ToLower().Contains(query.ToLower())
                )
                .ToList();
        }       
        #endregion
    }
}