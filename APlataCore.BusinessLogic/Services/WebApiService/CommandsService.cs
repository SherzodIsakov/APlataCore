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
    interface ICommandsService : IBaseService<Commands> { }
    public class CommandsService : ServiceBase, ICommandsService
    {
        private string BaseUri { get; set; }
        private TokensInfo TokensInfo { get; set; }
        private HttpClient Client;

        public CommandsService()
        {
            BaseUri = GetStoragePath();
            TokensInfo = GetTokenAsync("demo", "demo1").Result;

            Client = new HttpClient();
            Client.BaseAddress = new Uri(BaseUri);
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public CommandsService(string login, string password)
        {
            BaseUri = GetStoragePath();
            TokensInfo = GetTokenAsync(login, password).Result;

            Client = new HttpClient();
            Client.BaseAddress = new Uri(BaseUri);
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<Commands>> GetAllAsync()
        {
            HttpResponseMessage responseMessage = await Client.GetAsync("commands/types?token=" + TokensInfo.Token);

            List<Commands> commands = null;

            if (responseMessage.IsSuccessStatusCode)
            {
                var json = await responseMessage.Content.ReadAsStringAsync();
                commands = JsonConvert.DeserializeObject<List<Commands>>(json);
            }

            return commands;
        }

        #region MyRegion
        public async Task<Commands> GetAsync(int? id)
        {
            HttpResponseMessage responseMessage = await Client.GetAsync("commands/" + id.ToString() + "?token=" + TokensInfo.Token);

            Commands commands = null;

            if (responseMessage.IsSuccessStatusCode)
            {
                var json = await responseMessage.Content.ReadAsStringAsync();
                commands = JsonConvert.DeserializeObject<Commands>(json);
            }

            return commands;
        }
        public async Task<bool> CreateAsync(Commands commands)
        {
            try
            {
                var json = JsonConvert.SerializeObject(commands);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage postTask = await Client.PostAsync("commands?token=" + TokensInfo.Token, data);

                return postTask.IsSuccessStatusCode;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public async Task<bool> EditAsync(int? Id, Commands commands)
        {
            try
            {
                var json = JsonConvert.SerializeObject(commands);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage putTask = await Client.PutAsync("commands/" + Id.ToString() + "?token=" + TokensInfo.Token, data);

                //HttpResponseMessage putTask = await Client.PutAsJsonAsync<Commands>("api/commands/id=" + commands.Id.ToString(), commands);

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
                HttpResponseMessage deleteTask = await Client.DeleteAsync("commands/" + id.ToString() + "?token=" + TokensInfo.Token);

                return deleteTask.IsSuccessStatusCode;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public async Task<List<Commands>> SearchQueryAsync(string query)
        {
            HttpResponseMessage responseMessage = await Client.GetAsync("commands?machine_name=" + query + "&token=" + TokensInfo.Token);

            List<Commands> commands = null;

            if (responseMessage.IsSuccessStatusCode)
            {
                var json = await responseMessage.Content.ReadAsStringAsync();
                commands = JsonConvert.DeserializeObject<List<Commands>>(json);
            }

            return commands;
        }
        public async Task<List<Commands>> SearchLinqAsync(string query)
        {
            List<Commands> commands = await GetAllAsync();

            return commands
                .Where(x =>
                x.StrParameter1.ToLower().Contains(query.ToLower()) ||
                x.StrParameter2.ToLower().Contains(query.ToLower()) ||
                x.StateName.ToString().ToLower().Contains(query.ToLower())
                )
                .ToList();
        }

        #endregion
       
    }
}