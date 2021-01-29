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
    interface IItemsService : IBaseService<Items> { }
    public class ItemsService : ServiceBase, IItemsService
    {
        private string BaseUri { get; set; }
        private TokensInfo TokensInfo { get; set; }
        private HttpClient Client;

        public ItemsService()
        {
            BaseUri = GetStoragePath();
            TokensInfo = GetTokenAsync("demo", "demo1").Result;

            Client = new HttpClient();
            Client.BaseAddress = new Uri(BaseUri);
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public ItemsService(string login, string password)
        {
            BaseUri = GetStoragePath();
            TokensInfo = GetTokenAsync(login, password).Result;

            Client = new HttpClient();
            Client.BaseAddress = new Uri(BaseUri);
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<Items>> GetAllAsync()
        {
            HttpResponseMessage responseMessage = await Client.GetAsync("items?token=" + TokensInfo.Token);

            List<Items> items = null;

            if (responseMessage.IsSuccessStatusCode)
            {
                var json = await responseMessage.Content.ReadAsStringAsync();
                items = JsonConvert.DeserializeObject<List<Items>>(json);
            }

            return items;

        }
        public async Task<Items> GetAsync(int? id)
        {
            HttpResponseMessage responseMessage = await Client.GetAsync("items/" + id.ToString() + "?token=" + TokensInfo.Token);

            Items items = null;

            if (responseMessage.IsSuccessStatusCode)
            {
                var json = await responseMessage.Content.ReadAsStringAsync();
                items = JsonConvert.DeserializeObject<Items>(json);
            }

            return items;
        }

        #region MyRegion
        public async Task<bool> CreateAsync(Items items)
        {
            try
            {
                var json = JsonConvert.SerializeObject(items);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage postTask = await Client.PostAsync("items?token=" + TokensInfo.Token, data);

                return postTask.IsSuccessStatusCode;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public async Task<bool> EditAsync(int? Id, Items items)
        {
            try
            {
                var json = JsonConvert.SerializeObject(items);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage putTask = await Client.PutAsync("items/" + Id.ToString() + "?token=" + TokensInfo.Token, data);

                //HttpResponseMessage putTask = await Client.PutAsJsonAsync<Items>("api/items/id=" + items.Id.ToString(), items);

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
                HttpResponseMessage deleteTask = await Client.DeleteAsync("items/" + id.ToString() + "?token=" + TokensInfo.Token);

                return deleteTask.IsSuccessStatusCode;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public async Task<List<Items>> SearchQueryAsync(string query)
        {
            HttpResponseMessage responseMessage = await Client.GetAsync("items?machine_name=" + query + "&token=" + TokensInfo.Token);

            List<Items> items = null;

            if (responseMessage.IsSuccessStatusCode)
            {
                var json = await responseMessage.Content.ReadAsStringAsync();
                items = JsonConvert.DeserializeObject<List<Items>>(json);
            }

            return items;
        }
        public async Task<List<Items>> SearchLinqAsync(string query)
        {
            List<Items> items = await GetAllAsync();

            return items
                .Where(x =>
                //x.Name.ToLower().Contains(query.ToLower()) ||
                //x.MachineModel.ToLower().Contains(query.ToLower()) ||
                //x.Address.ToLower().Contains(query.ToLower()) ||
                //x.ModelID.ToString().ToLower().Contains(query.ToLower()) ||
                x.Name.ToString().ToLower().Contains(query.ToLower())
                )
                .ToList();
        }      
        #endregion
    }
}