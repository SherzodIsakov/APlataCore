//using APlataCore.BusinessLogic.Infrastructure;
//using APlataCore.Domain.Infrastructure;
//using APlataCore.Domain.Models;
//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Net.Http;
//using System.Net.Http.Headers;
//using System.Text;
//using System.Threading.Tasks;

//namespace APlataCore.BusinessLogic.Services.WebApiService
//{
//    interface IMachinesService : IBaseService<Machines> { }
//    public class MachinesService : ServiceBase, IMachinesService
//    {
//        private string BaseUri { get; set; }
//        private TokensInfo TokensInfo { get; set; }
//        private HttpClient Client;

//        public MachinesService()
//        {
//            BaseUri = GetStoragePath();
//            TokensInfo = GetTokenAsync("demo", "demo1").Result;

//            Client = new HttpClient();
//            Client.BaseAddress = new Uri(BaseUri);
//            Client.DefaultRequestHeaders.Accept.Clear();
//            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
//        }
//        public MachinesService(string login, string password)
//        {
//            BaseUri = GetStoragePath();
//            TokensInfo = GetTokenAsync(login, password).Result;

//            Client = new HttpClient();
//            Client.BaseAddress = new Uri(BaseUri);
//            Client.DefaultRequestHeaders.Accept.Clear();
//            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
//        }

//        public async Task<List<Machines>> GetAllAsync()
//        {
//            HttpResponseMessage responseMessage = await Client.GetAsync("machines?token=" + TokensInfo.Token);

//            List<Machines> machines = null;

//            if (responseMessage.IsSuccessStatusCode)
//            {
//                var json = await responseMessage.Content.ReadAsStringAsync();
//                machines = JsonConvert.DeserializeObject<List<Machines>>(json);
//            }

//            return machines;

//        }
//        public async Task<Machines> GetAsync(int? id)
//        {
//            HttpResponseMessage responseMessage = await Client.GetAsync("machines/" + id.ToString() + "?token=" + TokensInfo.Token);

//            Machines machines = null;

//            if (responseMessage.IsSuccessStatusCode)
//            {
//                var json = await responseMessage.Content.ReadAsStringAsync();
//                machines = JsonConvert.DeserializeObject<Machines>(json);
//            }

//            return machines;
//        }

//        public async Task<bool> CreateAsync(Machines machines)
//        {
//            try
//            {
//                var json = JsonConvert.SerializeObject(machines);
//                var data = new StringContent(json, Encoding.UTF8, "application/json");
//                HttpResponseMessage postTask = await Client.PostAsync("machines?token=" + TokensInfo.Token, data);

//                return postTask.IsSuccessStatusCode;
//            }
//            catch (Exception)
//            {

//                return false;
//            }
//        }
//        public async Task<bool> EditAsync(int? Id, Machines machines)
//        {
//            try
//            {
//                var json = JsonConvert.SerializeObject(machines);
//                var data = new StringContent(json, Encoding.UTF8, "application/json");
//                HttpResponseMessage putTask = await Client.PutAsync("machines/" + Id.ToString() + "?token=" + TokensInfo.Token, data);

//                //HttpResponseMessage putTask = await Client.PutAsJsonAsync<Machines>("api/machines/id=" + machines.Id.ToString(), machines);

//                return putTask.IsSuccessStatusCode;
//            }
//            catch (Exception)
//            {

//                return false;
//            }
//        }
//        public async Task<bool> DeleteAsync(int? id)
//        {
//            try
//            {
//                HttpResponseMessage deleteTask = await Client.DeleteAsync("machines/" + id.ToString() + "?token=" + TokensInfo.Token);

//                return deleteTask.IsSuccessStatusCode;
//            }
//            catch (Exception)
//            {

//                return false;
//            }
//        }

//        public async Task<List<Machines>> SearchQueryAsync(string query)
//        {
//            HttpResponseMessage responseMessage = await Client.GetAsync("machines?machine_name=" + query + "&token=" + TokensInfo.Token);

//            List<Machines> machines = null;

//            if (responseMessage.IsSuccessStatusCode)
//            {
//                var json = await responseMessage.Content.ReadAsStringAsync();
//                machines = JsonConvert.DeserializeObject<List<Machines>>(json);
//            }

//            return machines;
//        }
//        public async Task<List<Machines>> SearchLinqAsync(string query)
//        {
//            List<Machines> machines = await GetAllAsync();

//            return machines
//                .Where(x =>
//                x.Name.ToLower().Contains(query.ToLower()) ||
//                x.MachineModel.ToLower().Contains(query.ToLower()) ||
//                x.Address.ToLower().Contains(query.ToLower()) ||
//                x.ModelID.ToString().ToLower().Contains(query.ToLower()) ||
//                x.OwnerID.ToString().ToLower().Contains(query.ToLower())
//                )
//                .ToList();
//        }
//    }
//}