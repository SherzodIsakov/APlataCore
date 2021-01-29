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
    interface IMachineModelsService : IBaseService<MachineModels> { }
    public class MachineModelsService : ServiceBase, IMachineModelsService
    {
        private string BaseUri { get; set; }
        private TokensInfo TokensInfo { get; set; }
        private HttpClient Client;

        public MachineModelsService()
        {
            BaseUri = GetStoragePath();
            TokensInfo = GetTokenAsync("demo", "demo1").Result;

            Client = new HttpClient();
            Client.BaseAddress = new Uri(BaseUri);
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public MachineModelsService(string login, string password)
        {
            BaseUri = GetStoragePath();
            TokensInfo = GetTokenAsync(login, password).Result;

            Client = new HttpClient();
            Client.BaseAddress = new Uri(BaseUri);
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<MachineModels>> GetAllAsync()
        {
            HttpResponseMessage responseMessage = await Client.GetAsync("machinemodels?token=" + TokensInfo.Token);

            List<MachineModels> machinemodels = null;

            if (responseMessage.IsSuccessStatusCode)
            {
                var json = await responseMessage.Content.ReadAsStringAsync();
                machinemodels = JsonConvert.DeserializeObject<List<MachineModels>>(json);
            }

            return machinemodels;

        }

        #region MyRegion
        public async Task<MachineModels> GetAsync(int? id)
        {
            HttpResponseMessage responseMessage = await Client.GetAsync("machinemodels/" + id.ToString() + "?token=" + TokensInfo.Token);

            MachineModels machinemodels = null;

            if (responseMessage.IsSuccessStatusCode)
            {
                var json = await responseMessage.Content.ReadAsStringAsync();
                machinemodels = JsonConvert.DeserializeObject<MachineModels>(json);
            }

            return machinemodels;
        }
        public async Task<bool> CreateAsync(MachineModels machinemodels)
        {
            try
            {
                var json = JsonConvert.SerializeObject(machinemodels);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage postTask = await Client.PostAsync("machinemodels?token=" + TokensInfo.Token, data);

                return postTask.IsSuccessStatusCode;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public async Task<bool> EditAsync(int? Id, MachineModels machinemodels)
        {
            try
            {
                var json = JsonConvert.SerializeObject(machinemodels);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage putTask = await Client.PutAsync("machinemodels/" + Id.ToString() + "?token=" + TokensInfo.Token, data);

                //HttpResponseMessage putTask = await Client.PutAsJsonAsync<MachineModels>("api/machinemodels/id=" + machinemodels.Id.ToString(), machinemodels);

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
                HttpResponseMessage deleteTask = await Client.DeleteAsync("machinemodels/" + id.ToString() + "?token=" + TokensInfo.Token);

                return deleteTask.IsSuccessStatusCode;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public async Task<List<MachineModels>> SearchQueryAsync(string query)
        {
            HttpResponseMessage responseMessage = await Client.GetAsync("machinemodels?machine_name=" + query + "&token=" + TokensInfo.Token);

            List<MachineModels> machinemodels = null;

            if (responseMessage.IsSuccessStatusCode)
            {
                var json = await responseMessage.Content.ReadAsStringAsync();
                machinemodels = JsonConvert.DeserializeObject<List<MachineModels>>(json);
            }

            return machinemodels;
        }
        public async Task<List<MachineModels>> SearchLinqAsync(string query)
        {
            List<MachineModels> machinemodels = await GetAllAsync();

            return machinemodels
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