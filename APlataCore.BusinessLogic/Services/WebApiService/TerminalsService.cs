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
    interface ITerminalsService : IBaseService<Terminals> { }
    public class TerminalsService : ServiceBase, ITerminalsService
    {
        private string BaseUri { get; set; }
        private TokensInfo TokensInfo { get; set; }
        private HttpClient Client;

        public TerminalsService()
        {
            BaseUri = GetStoragePath();
            TokensInfo = GetTokenAsync("demo", "demo1").Result;

            Client = new HttpClient();
            Client.BaseAddress = new Uri(BaseUri);
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public TerminalsService(string login, string password)
        {
            BaseUri = GetStoragePath();
            TokensInfo = GetTokenAsync(login, password).Result;

            Client = new HttpClient();
            Client.BaseAddress = new Uri(BaseUri);
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<Terminals>> GetAllAsync()
        {
            HttpResponseMessage responseMessage = await Client.GetAsync("terminals?token=" + TokensInfo.Token);

            List<Terminals> terminals = null;

            if (responseMessage.IsSuccessStatusCode)
            {
                var json = await responseMessage.Content.ReadAsStringAsync();
                terminals = JsonConvert.DeserializeObject<List<Terminals>>(json);
            }

            return terminals;

        }
        public async Task<Terminals> GetAsync(int? id)
        {
            HttpResponseMessage responseMessage = await Client.GetAsync("terminals/" + id.ToString() + "?token=" + TokensInfo.Token);

            Terminals terminals = null;

            if (responseMessage.IsSuccessStatusCode)
            {
                var json = await responseMessage.Content.ReadAsStringAsync();
                terminals = JsonConvert.DeserializeObject<Terminals>(json);
            }

            return terminals;
        }
        public async Task<bool> CreateAsync(Terminals terminals)
        {
            try
            {
                var json = JsonConvert.SerializeObject(terminals);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage postTask = await Client.PostAsync("terminals?token=" + TokensInfo.Token, data);

                return postTask.IsSuccessStatusCode;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public async Task<bool> EditAsync(int? Id, Terminals terminals)
        {
            try
            {
                var json = JsonConvert.SerializeObject(terminals);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage putTask = await Client.PutAsync("terminals/" + Id.ToString() + "?token=" + TokensInfo.Token, data);

                //HttpResponseMessage putTask = await Client.PutAsJsonAsync<Terminals>("api/terminals/id=" + terminals.Id.ToString(), terminals);

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
                HttpResponseMessage deleteTask = await Client.DeleteAsync("terminals/" + id.ToString() + "?token=" + TokensInfo.Token);

                return deleteTask.IsSuccessStatusCode;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public async Task<List<Terminals>> SearchQueryAsync(string query)
        {
            HttpResponseMessage responseMessage = await Client.GetAsync("terminals?machine_name=" + query + "&token=" + TokensInfo.Token);

            List<Terminals> terminals = null;

            if (responseMessage.IsSuccessStatusCode)
            {
                var json = await responseMessage.Content.ReadAsStringAsync();
                terminals = JsonConvert.DeserializeObject<List<Terminals>>(json);
            }

            return terminals;
        }
        public async Task<List<Terminals>> SearchLinqAsync(string query)
        {
            List<Terminals> terminals = await GetAllAsync();

            return terminals
                .Where(x =>
                x.MerchantID.ToLower().Contains(query.ToLower()) ||
                x.MachineName.ToLower().Contains(query.ToLower()) ||
                x.GsmOperator.ToLower().Contains(query.ToLower()) ||
                x.TerminalAddress.ToString().ToLower().Contains(query.ToLower()) ||
                x.TerminalPlace.ToString().ToLower().Contains(query.ToLower())
                )
                .ToList();
        }


        #region TerminalsServices
        public async Task<List<int>> GetAllTerminalsFreeAsync()
        {
            HttpResponseMessage responseMessage = await Client.GetAsync("terminals/free?token=" + TokensInfo.Token);

            List<int> terminalsFree = null;

            if (responseMessage.IsSuccessStatusCode)
            {
                var json = await responseMessage.Content.ReadAsStringAsync();
                terminalsFree = JsonConvert.DeserializeObject<List<int>>(json);
            }

            return terminalsFree;
        }
        public async Task<Terminals> GetTerminalInfoAsync(int? id)
        {
            HttpResponseMessage responseMessage = await Client.GetAsync("info?id=" + id.ToString() + "?token=" + TokensInfo.Token);

            Terminals terminals = null;

            if (responseMessage.IsSuccessStatusCode)
            {
                var json = await responseMessage.Content.ReadAsStringAsync();
                terminals = JsonConvert.DeserializeObject<Terminals>(json);
            }

            return terminals;
        }       
        public async Task<List<Commands>> GetTerminalCommandsAsync(int? id)
        {
            HttpResponseMessage responseMessage = await Client.GetAsync("terminals/" + id.ToString() + "/commands" + "?token=" + TokensInfo.Token);

            List<Commands> commands = null;

            if (responseMessage.IsSuccessStatusCode)
            {
                var json = await responseMessage.Content.ReadAsStringAsync();
                commands = JsonConvert.DeserializeObject<List<Commands>>(json);
            }

            return commands != null ? commands : new List<Commands>();
        }
        #endregion
    }
}